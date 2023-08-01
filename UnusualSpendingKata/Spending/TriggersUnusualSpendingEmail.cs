namespace UnusualSpendingKata.Spending;

public class TriggersUnusualSpendingEmail
{
    // Caveat:
    // We don't control who invokes our TriggersUnusualSpendingEmail#trigger(userId) entry point, or when;
    // nor can we change its method signature, as it represents a public interface that something else
    // (maybe a job scheduler system) is depending on

    private readonly EmailWrapper emailService;
    private readonly UserPaymentsWrapper userPaymentService;

    public TriggersUnusualSpendingEmail(EmailWrapper emailService, UserPaymentsWrapper userPaymentService)
    {
        this.emailService = emailService;
        this.userPaymentService = userPaymentService;
    }

    public void Trigger(long userId)
    {
        var spendingDate = new SpendingDate();
        var lastMonth = userPaymentService.Fetch(userId, spendingDate.LastMonthYear, spendingDate.LastMonth);
       // var lastMonthSpend = lastMonth.Select(payment => payment.Price).Sum();
        var thisMonth = userPaymentService.Fetch(userId, spendingDate.ThisMonthYear, spendingDate.ThisMonth);
        //var thisMonthSpend = thisMonth.Select(payment => payment.Price).Sum();

    }
}