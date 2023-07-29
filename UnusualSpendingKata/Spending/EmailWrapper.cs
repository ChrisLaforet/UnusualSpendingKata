namespace UnusualSpendingKata.Spending;

public class EmailWrapper
{
    private bool isProduction = true;

    public static EmailWrapper CreateForTesting()
    {
        return new EmailWrapper(false);
    }

    public static EmailWrapper Create()
    {
        return new EmailWrapper(true);
    }

    private EmailWrapper(bool isProduction) => this.isProduction = isProduction;

    public void Email(int userId, string subject, string body)
    {
        if (isProduction)
        {
            EmailsUser.Email(userId, subject, body);
        }
    }
}