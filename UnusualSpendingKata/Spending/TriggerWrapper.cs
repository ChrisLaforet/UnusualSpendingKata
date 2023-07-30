namespace UnusualSpendingKata.Spending;

public class TriggerWrapper
{
    private readonly UserPaymentsWrapper paymentService;
    private TriggerWrapper(UserPaymentsWrapper paymentService) => this.paymentService = paymentService;
    
    public static string? TriggerForTesting(int userId)
    {
        var emailService = EmailWrapper.CreateForTesting();

        if (userId != 0)
        {
            var triggerService = new TriggersUnusualSpendingEmail(emailService);
            triggerService.Trigger(userId);
        }

        return emailService.LastBody;
    }
}