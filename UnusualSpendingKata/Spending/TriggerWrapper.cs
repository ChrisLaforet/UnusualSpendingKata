namespace UnusualSpendingKata.Spending;

public class TriggerWrapper
{
    private TriggerWrapper() {}
    
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