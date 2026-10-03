
ShoppingList list = null;

try
{
    
    list = new ShoppingList("aldos_items.txt");

    Console.WriteLine("Försöker ladda in filen");

    list.Load();

    Console.WriteLine("Det gick inte att ladda filen");
}
catch
{
    Console.WriteLine("Filen saknas");
}

while (true) 
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");
    
    // Added string input to allow the user to enter text.
    // Also, replace int.parse with int.TryParse to prevvent
    // program from craching when text is entered.
    string input = Console.ReadLine();
    int.TryParse(input, out int choice);

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        Console.Write("Pris: ");
        input = Console.ReadLine();  // Added input to allow the user to enter text or numbers

        if (int.TryParse(input, out int price)) 
        {
           list.Add(new Item(name, price));
           Console.WriteLine("Varan har lagts till");
        }
        else
        {
            Console.WriteLine("Fel! Ange priset i siffror. Varankommer inta att läggas till");
        }
            
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        int number = int.Parse(Console.ReadLine());
        list.RemoveAt(number);
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
}
