namespace UnusualSpendingKata.Spending;

public class TriggersUnusualSpendingEmail
{
    // Caveat:
    // We don't control who invokes our TriggersUnusualSpendingEmail#trigger(userId) entry point, or when;
    // nor can we change its method signature, as it represents a public interface that something else
    // (maybe a job scheduler system) is depending on

    private readonly EmailWrapper emailService;

    public TriggersUnusualSpendingEmail(EmailWrapper emailService) => this.emailService = emailService;

    public void Trigger(long userId)
    {
            
    }
}