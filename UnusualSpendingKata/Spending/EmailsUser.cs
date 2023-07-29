namespace UnusualSpendingKata.Spending;

public class EmailsUser
{
    // Caveat:
    // We don't control how e-mails are sent, all we know is that it's specified by the interface
    // spending.EmailsUser.email(userId, subject, body)
    //
    // EmailsUser.email is a static method, which is also painful to mock and we'll want to wrap it, too

    static public void Email(int userId, string subject, string body)
    {
        // some magic vendor-provided voodoo here
    }
}