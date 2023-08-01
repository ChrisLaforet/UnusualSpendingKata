using UnusualSpendingKata.Spending;
using System;

namespace UnusualSpendingKata;

// Instructions are here: https://kata-log.rocks/unusual-spending-kata
// Details are here: https://github.com/testdouble/contributing-tests/wiki/Unusual-Spending-Kata
// Java starter project here: https://github.com/testdouble/java-testing-example/tree/main/unusual-spending

public class UnitTests
{
    private const int INACTIVE_USER_ID = 0;
    private const int NON_SPENDING_USER_ID = 1;
    private const int EQUAL_SPENDING_USER_ID = 2;
    private const int NEW_SPENDING_USER_ID = 3;
    private const int NO_SPENDING_THIS_MONTH_USER_ID = 4;
    private const int SUSPICIOUS_SPENDING_USER_ID = 5;

    private readonly SpendingDate spendingDate = new SpendingDate();
    
    
    [Fact]
    public void GivenTrigger_WhenTriggeredWithInactiveUserId_ThenReturnsEmptyBody()
    { 
        var body = TriggerWrapper.TriggerForTesting(INACTIVE_USER_ID, PrepareDatabase());
        Assert.Null(body);
    }

    [Fact]
    public void GivenTrigger_WhenTriggeredWithUserWithoutSpendingThisMonth_ThenReturnsEmptyMessageBody()
    {
        var body = TriggerWrapper.TriggerForTesting(NON_SPENDING_USER_ID, PrepareDatabase());
        Assert.Null(body);
    }

    [Fact]
    public void GivenUserPaymentsWrapperWithUserThatHasNoPayments_WhenFetchingPayments_ThenReturnsEmptyList()
    {
        var service = UserPaymentsWrapper.CreateForTesting(PrepareDatabase());
        Assert.False(service.Fetch(NON_SPENDING_USER_ID, spendingDate.LastMonthYear, spendingDate.LastMonth).Any());
    }
    
    [Fact]
    public void GivenUserPaymentsWrapperWithUserThatHasPayments_WhenFetchingPayments_ThenReturnsList()
    {
        var service = UserPaymentsWrapper.CreateForTesting(PrepareDatabase());
        Assert.True(service.Fetch(EQUAL_SPENDING_USER_ID, spendingDate.LastMonthYear, spendingDate.LastMonth).Any());
    }
    
    [Fact]
    public void GivenTrigger_WhenTriggeredWithUserWithEqualSpendingThisMonth_ThenReturnsEmptyMessageBody()
    {
        var body = TriggerWrapper.TriggerForTesting(EQUAL_SPENDING_USER_ID, PrepareDatabase());
        Assert.Null(body);
    }

    [Fact]
    public void GivenTrigger_WhenTriggeredWithUserWithNewSpendingThisMonth_ThenReturnsMessageBody()
    {
        var body = TriggerWrapper.TriggerForTesting(NEW_SPENDING_USER_ID, PrepareDatabase());
        Assert.NotNull(body);
    }

    private Dictionary<long, IEnumerable<DatedPayment>> PrepareDatabase()
    {
        var paymentDatabase = new Dictionary<long, IEnumerable<DatedPayment>>();
        paymentDatabase[EQUAL_SPENDING_USER_ID] = PrepareEqualPaymentUserData();
        paymentDatabase[NEW_SPENDING_USER_ID] = PrepareNewPaymentUserData();

        return paymentDatabase;
    }

    private List<DatedPayment> PrepareEqualPaymentUserData()
    {
        var payments = new List<DatedPayment>();
        payments.Add(new DatedPayment(spendingDate.LastMonth, spendingDate.LastMonthYear, 50, "Dinner", Category.Restaurants));
        payments.Add(new DatedPayment(spendingDate.LastMonth, spendingDate.LastMonthYear, 25, "Lunch", Category.Restaurants));
        payments.Add(new DatedPayment(spendingDate.LastMonth, spendingDate.LastMonthYear, 65, "Fill'r Up", Category.Gas));

        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 68, "Fill'r Up", Category.Gas));
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 20, "Breakfast", Category.Restaurants));
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 30, "Lunch", Category.Restaurants));
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 18, "Breakfast", Category.Restaurants));
        return payments;
    }
    
    
    private List<DatedPayment> PrepareNewPaymentUserData()
    {
        var payments = new List<DatedPayment>();
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 68, "Fill'r Up", Category.Gas));
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 20, "Breakfast", Category.Restaurants));
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 30, "Lunch", Category.Restaurants));
        payments.Add(new DatedPayment(spendingDate.ThisMonth, spendingDate.ThisMonthYear, 18, "Breakfast", Category.Restaurants));
        return payments;
    }
}