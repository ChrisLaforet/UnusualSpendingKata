using UnusualSpendingKata.Spending;

namespace UnusualSpendingKata;

// Instructions are here: https://kata-log.rocks/unusual-spending-kata
// Details are here: https://github.com/testdouble/contributing-tests/wiki/Unusual-Spending-Kata
// Java starter project here: https://github.com/testdouble/java-testing-example/tree/main/unusual-spending

public class UnitTests
{
    private const int INACTIVE_USER_ID = 0;
    private const int NON_SPENDING_USER_ID = 1;
    private const int EQUAL_SPENDING_USER_ID = 2;
    private const int UNUSUAL_SPENDING_USER_ID = 3;

    private const int LAST_MONTH = 5;
    private const int THIS_MONTH = LAST_MONTH + 1;
    private const int YEAR = 2023;
    
    [Fact]
    public void givenSystemUnderTest_whenTriggeredWithInactiveUserId_thenReturnsEmptyBody()
    { 
        var body = TriggerWrapper.TriggerForTesting(INACTIVE_USER_ID);
        Assert.Null(body);
    }

    [Fact]
    public void givenSystemUnderTest_whenTriggeredWithUserWithoutSpendingThisMonth_thenReturnsEmptyMessageBody()
    {
        var body = TriggerWrapper.TriggerForTesting(NON_SPENDING_USER_ID);
        Assert.Null(body);
    }
    
    [Fact]
    public void givenSystemUnderTest_whenTriggeredWithUserWithEqualSpendingThisMonth_thenReturnsEmptyMessageBody()
    {
        var body = TriggerWrapper.TriggerForTesting(EQUAL_SPENDING_USER_ID);
        Assert.Null(body);
    }

    private Dictionary<int, IEnumerable<DatedPayment>> prepareDatabase()
    {
        var equalUserPayments = new List<Payment>();
        equalUserPayments.Add(new DatedPayment(LAST_MONTH, YEAR, 50, "Dinner", Category.Restaurants));
        equalUserPayments.Add(new DatedPayment(LAST_MONTH, YEAR, 25, "Lunch", Category.Restaurants));
        equalUserPayments.Add(new DatedPayment(LAST_MONTH, YEAR, 65, "Fill'r Up", Category.Gas));
        
        equalUserPayments.Add(new DatedPayment(THIS_MONTH, YEAR, 68, "Fill'r Up", Category.Gas));
        equalUserPayments.Add(new DatedPayment(LAST_MONTH, YEAR, 20, "Breakfast", Category.Restaurants));
        equalUserPayments.Add(new DatedPayment(LAST_MONTH, YEAR, 30, "Lunch", Category.Restaurants));
        equalUserPayments.Add(new DatedPayment(LAST_MONTH, YEAR, 18, "Breakfast", Category.Restaurants));

        var paymentDatabase = new Dictionary<int, IEnumerable<DatedPayment>>();
        paymentDatabase[EQUAL_SPENDING_USER_ID] = equalUserPayments;

        return paymentDatabase;
    }
}