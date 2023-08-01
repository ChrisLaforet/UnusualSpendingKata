namespace UnusualSpendingKata.Spending;

public class SpendingDate
{
    public int LastMonth { get; private set; }
    public int LastMonthYear { get; private set; }
    public int ThisMonth { get; private set; }
    public int ThisMonthYear { get; private set; }

    public SpendingDate()
    {
        var now = DateTime.Now;
        ThisMonthYear = now.Year;
        ThisMonth = now.Month;
        if (now.Month >= 1)
        {
            LastMonthYear = ThisMonthYear;
            LastMonth = ThisMonth - 1;
        }
        else
        {
            LastMonthYear = ThisMonthYear - 1;
            LastMonth = ThisMonthYear;
        }
    }
}