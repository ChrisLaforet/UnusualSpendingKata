using UnusualSpendingKata.Spending;

namespace UnusualSpendingKata;

// Instructions are here: https://kata-log.rocks/unusual-spending-kata
// Details are here: https://github.com/testdouble/contributing-tests/wiki/Unusual-Spending-Kata
// Java starter project here: https://github.com/testdouble/java-testing-example/tree/main/unusual-spending

public class UnitTests
{
    private TriggersUnusualSpendingEmail systemUnderTest = new TriggersUnusualSpendingEmail(EmailWrapper.CreateForTesting());

    private const int INACTIVE_USER_ID = 0;
    private const int ACTIVE_USER_ID = 1;
    
    [Fact]
    public void givenSystemUnderTest_whenTriggeredWithInactiveUserId_thenDoesNothing()
    {
        systemUnderTest.Trigger(INACTIVE_USER_ID);
        
    }
}