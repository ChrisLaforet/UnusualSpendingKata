namespace UnusualSpendingKata.Spending;

public class UserPaymentsWrapper
{
    private FetchesUserPaymentsByMonth paymentService;
    
    public static UserPaymentsWrapper CreateForTesting(IEnumerable<Payment> payments)
    {
        // TODO: handle injection of payments
        return new UserPaymentsWrapper(false);
    }

    public static UserPaymentsWrapper Create()
    {
        return new UserPaymentsWrapper(true);
    }

    private UserPaymentsWrapper(bool isProduction)
    {
        if (isProduction)
        {
            paymentService = FetchesUserPaymentsByMonth.GetInstance();
        }
    }
    
    public IEnumerable<Payment> Fetch(int userId, int year, int month)
    {
        if (paymentService)
        {
            return paymentService.Fetch(userId, year, month);
        }
    }
}