namespace UnusualSpendingKata.Spending;

public class Payment
{
    public int Price { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }

    public Payment(int price, string description, Category category)
    {
        this.Price = price;
        this.Description = description;
        this.Category = category;
    }
}