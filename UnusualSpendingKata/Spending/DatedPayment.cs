namespace UnusualSpendingKata.Spending;

public class DatedPayment : Payment
{
    public int Month { get; private set; }
    public int Year { get; private set; }
    
    public DatedPayment(int month, int year, int price, string description, Category category) : base(price, description, category)
    {
        this.Month = month;
        this.Year = year;
    }
}