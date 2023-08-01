namespace UnusualSpendingKata.Spending;

public abstract class UserPaymentsWrapper
{
    private readonly FetchesUserPaymentsByMonth? paymentService;
    
    public static UserPaymentsWrapper CreateForTesting(Dictionary<long, IEnumerable<DatedPayment>> paymentDatabase)
    {
        return new TestingPaymentsWrapper(paymentDatabase);
    }

    public static UserPaymentsWrapper Create()
    {
        return new ProductionPaymentsWrapper();
    }

    protected UserPaymentsWrapper() {}

    public abstract IEnumerable<Payment> Fetch(long userId, int year, int month);
}

internal class TestingPaymentsWrapper : UserPaymentsWrapper
{
    private readonly Dictionary<long, IEnumerable<DatedPayment>> paymentDatabase;
    
    public TestingPaymentsWrapper(Dictionary<long, IEnumerable<DatedPayment>> paymentDatabase) => this.paymentDatabase = paymentDatabase;

    public override IEnumerable<Payment> Fetch(long userId, int year, int month)
    {
        var response = new List<Payment>();
        if (paymentDatabase.ContainsKey(userId))
        {
            var matches = paymentDatabase[userId].Where(payment => payment.Month == month && payment.Year == year).ToList();
            response.AddRange(matches);
        }
        return response;
    }
}

internal class ProductionPaymentsWrapper : UserPaymentsWrapper
{
    private readonly FetchesUserPaymentsByMonth paymentService = FetchesUserPaymentsByMonth.GetInstance();
    
    public ProductionPaymentsWrapper() {}
    
    public override IEnumerable<Payment> Fetch(long userId, int year, int month)
    {
        return paymentService.Fetch(userId, year, month);
    }
}
