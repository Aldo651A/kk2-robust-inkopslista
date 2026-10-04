ShoppingList list = null;
list = new ShoppingList("aldos_items.txt");
list.Load();



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
        // This condition ensures the program won't crash
        // if the user enters text instead of a number
        // Using an if-else statement makes them program safer
        if (!int.TryParse(input, out int price)) 
        {
           Console.WriteLine("Fel! Ange priset i siffror. Varan kommer inta att läggas till");
           continue;
        }
        
        if (!list.Add(new Item(name, price)))
        {
            Console.WriteLine("Fel! Varan är alldelse för dyrt. Du sprängde budgettaket! ");
            continue;
        }
            
        Console.WriteLine("Varan har lagts till");
   
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        input = Console.ReadLine();
        // This condition ensures the program won't crash
        // if the user enters text instead of a number
        // Using an if-else statement makes them program safer
        if (int.TryParse(input, out int number))
        {
            try // Here i am using a try-catch to catch the error
            {   // The error is: index is out of range of the array
                list.RemoveAt(number);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Fel! nummeret finns inte i listan");
            }
        }
        else
        {
            Console.WriteLine("Fel! Ange ett tal i siffror");
        }

    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();

        try // Here i am using a try-catch to catch
        {   // the error, because the code is trying to use 
            // a reference that is null.
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
        catch (NullReferenceException)
        {
            Console.WriteLine("Något gick fel i sökningen");
        }
    
    }
    else if (choice == 5)
    {
        break;
    }
}
