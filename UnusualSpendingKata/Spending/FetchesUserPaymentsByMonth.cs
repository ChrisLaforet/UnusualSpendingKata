namespace UnusualSpendingKata.Spending;

public class FetchesUserPaymentsByMonth
{
    // Caveat:
    // We don't control how payments are fetched, that's Somebody Else's Job™; all we have is an agreed-upon contract:
    // spending.FetchesUserPaymentsByMonth#fetch(userId, year, month)
    //
    // Instances of FetchesUserPaymentsByMonth are provided by a factory method, which would be painful to mock
    // and means we'll want to write a Wrapper Object for it
        
    private FetchesUserPaymentsByMonth() {}

    public static FetchesUserPaymentsByMonth GetInstance()
    {
        // some painful processing here that requires connectivity data
        return null;            // to prohibit us from using this in our unit tests
    }
    
    public IEnumerable<Payment> Fetch(int userId, int year, int month)
    {
        // some magic vendor-provided voodoo here - we have no control over it
        return new List<Payment>();
    }
}