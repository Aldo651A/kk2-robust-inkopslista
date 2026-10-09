// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        if (price < 0)
        {   // "This ensures the price is not negative: 
            // throw functions as an alert that rejects any product with a negative price."
            throw new ArgumentOutOfRangeException(nameof(price), "Fel! Priset kan inte vara negativt. ");
        }
        // Checks for an empty or whitespace-only line. 
        // If empty, it proceeds to an ArgumentException.
        if (string.IsNullOrWhiteSpace(name))
        {
            // If a method is invalid or an item is 
            // missing a name, it will not be added.
            throw new ArgumentException();
        }
    
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
