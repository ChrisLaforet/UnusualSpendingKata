namespace UnusualSpendingKata.Spending;

public abstract class UserPaymentsWrapper
{
    private readonly FetchesUserPaymentsByMonth? paymentService;
    
    public static UserPaymentsWrapper CreateForTesting(Dictionary<int, IEnumerable<Payment>> paymentDatabase)
    {
        return new TestingPaymentsWrapper(paymentDatabase);
    }

    public static UserPaymentsWrapper Create()
    {
        return new ProductionPaymentsWrapper();
    }

    protected UserPaymentsWrapper() {}

    public abstract IEnumerable<Payment> Fetch(int userId, int year, int month);
}

internal class TestingPaymentsWrapper : UserPaymentsWrapper
{
    private readonly Dictionary<int, IEnumerable<Payment>> paymentDatabase;
    
    public TestingPaymentsWrapper(Dictionary<int, IEnumerable<Payment>> paymentDatabase) => this.paymentDatabase = paymentDatabase;

    public override IEnumerable<Payment> Fetch(int userId, int year, int month)
    {
        return new List<Payment>();
    }
}

internal class ProductionPaymentsWrapper : UserPaymentsWrapper
{
    private readonly FetchesUserPaymentsByMonth paymentService = FetchesUserPaymentsByMonth.GetInstance();
    
    public ProductionPaymentsWrapper() {}
    
    public override IEnumerable<Payment> Fetch(int userId, int year, int month)
    {
        return paymentService.Fetch(userId, year, month);
    }
}
