// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
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
        // int to (int i = 0;)
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
        {
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
        }
        catch
        {
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
    public void Load()
    {
        try
        {
            
            string text = File.ReadAllText(path);
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                    
                string[] parts = line.Split(';');
                
                items.Add(new Item(parts[1], int.Parse(parts[0])));
 
            }
    
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("Inläsningen misslyckades - filen hittades inte");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Det blev ett ANNAT fel inuti Load:{ ex.Message} ");
        }

    }
}
