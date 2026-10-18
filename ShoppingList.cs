// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    // Defines the total budget limit for the shopping list.
    public int BudgetLimit { get; set; } = 300;

    public ShoppingList(string path) 
    {
        this.path = path;
    }
    // Converted this into a public bool to set a price limit 
    //  on the items. Now the method must return either true or false.
    public bool Add(Item item)
    {
        // Checks if within the budget limit. 
        // Returns false if exceeded, otherwise true.
        if (Total() + item.Price > BudgetLimit)
        {
        return false;
        }
        
        items.Add(item);
        
        return true;
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }
    
    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;
        // When the loop was set to (int i = 1;)the program skipped
        // the very first item in the list. We have to change loop  
        // int to (int i = 0;) Now we will get the right total sum
        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name.Contains(name))
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {   // When you wrote to the console, the product name 
            // was missing. Thats was because item's Name and Price
            // were missing. 
            Console.WriteLine($"{i + 1}.{items[i].Name} {items[i].Price}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\n", lines) + "\n");
            Console.WriteLine("Listan är sparad.");
        }
        // If we have a problem with eh hardware or the file is locked,
        // the program won't crash.
        catch (IOException)
        {
            Console.WriteLine("Tyvärr! Det gick inta att spara listan");
        }
    
    }

    // Reads the file back into the list.
    public void Load()
    {
        try // Since the program cannot find or access the items.txt file
        {   // it crashed immediately. I had to implement a try-catch block
            // to allow the program to start.
            string text = File.ReadAllText(path);
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {   // Here we detect if the line is empty and triggers continue
                // which means skip directly to the next line and ignore the rest 
                // of the code below, saving the program from crashing
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                    
                string[] parts = line.Split(';');
                // if the user makes a mistake in aldos_items.txt.
                // The program will start and load everything it can.
                if (!int.TryParse(parts[0], out int price))
                {
                    // If we encounter a damaged file, then continue will jump 
                    // to the next loop. This saves the program from crashing.
                    continue;
                }

                try
                {   // try and to catch negative prices and empty txt files
                    items.Add(new Item(parts[1], price));
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Hoppade över en vara i filen: Priset var negativ. ");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Hoppade äver en vara i filen: Namnet var tomt. ");
                }
            
            }
    
        }
        catch(FileNotFoundException) // The file couldn't be found.
        {
            Console.WriteLine("Inläsningen misslyckades - filen hittades inte");
        }
        // If the index is not in range or the file is damaged
        // it stops the program from crashing.
        catch(IndexOutOfRangeException) 
        {
            Console.WriteLine("Inläsning misslyckades - filen har felaktig format");
        }

    }
}
