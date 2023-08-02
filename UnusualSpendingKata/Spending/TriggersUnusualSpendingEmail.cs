using System.Collections;
using System.Text;

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
        var thisMonth = userPaymentService.Fetch(userId, spendingDate.ThisMonthYear, spendingDate.ThisMonth);
        if (!thisMonth.Any())
        {
            return;
        }
        var lastMonth = userPaymentService.Fetch(userId, spendingDate.LastMonthYear, spendingDate.LastMonth);

        var overSpends = CheckForOverSpendingByCategory(thisMonth, lastMonth);
        if (overSpends.Count == 0)
        {
            return;
        }

        var total = overSpends.Select(overSpend => overSpend.Amount).Sum();
        var subject = string.Format("Unusual spending of ${0} detected", total);
        var body = LayoutMessage(overSpends);
        
        emailService.Email((int)userId, subject, body);
    }

    // Subject should look like: "Unusual spending of $1076 detected!"
    // Hello card user!
    //
    // We have detected unusually high spending on your card in these categories:
    //
    // * You spent $148 on groceries
    // * You spent $928 on travel
    //
    // Love,
    //
    // The Credit Card Company
    
    private string LayoutMessage(List<OverSpend> overSpends)
    {
        var message = new StringBuilder();
        message.Append("Hello card user!\n");
        message.Append('\n');
        message.Append("We have detected unusually high spending on your card in these categories:\n");
        message.Append('\n');
        overSpends.ForEach(overSpend =>
        {
            message.Append(string.Format("* You spent ${0} on {1}\n", overSpend.Amount, overSpend.Category.ToString()));
        });
        message.Append('\n');
        message.Append("Love,");
        message.Append('\n');
        message.Append("The Credit Card Company");
        return message.ToString();
    }

    private List<OverSpend> CheckForOverSpendingByCategory(IEnumerable<Payment> thisMonth, IEnumerable<Payment> lastMonth)
    {
        var currentCategoryList = ExtractCategories(thisMonth);
        var previousCategoryList = ExtractCategories(lastMonth);

        var overSpends = new List<OverSpend>();
        foreach (var category in currentCategoryList.Keys)
        {
            if (previousCategoryList.ContainsKey(category))
            {
                var limit = Math.Round(previousCategoryList[category] * 1.5);
                if (currentCategoryList[category] >= limit)
                {
                    overSpends.Add(new OverSpend(category, currentCategoryList[category] - previousCategoryList[category]));
                }
            }
            else
            {
                overSpends.Add(new OverSpend(category, currentCategoryList[category]));
            }
        }

        return overSpends;
    }

    private Dictionary<Category, int> ExtractCategories(IEnumerable<Payment> payments)
    {
        var categories = new Dictionary<Category, int>();
        foreach (var payment in payments)
        {
            if (categories.ContainsKey(payment.Category))
            {
                categories[payment.Category] += payment.Price;
            }
            else
            {
                categories[payment.Category] = payment.Price;
            }
        }

        return categories;
    }

    internal class OverSpend
    {
        public Category Category { get; private set; }
        public int Amount { get; private set; }

        public OverSpend(Category category, int overSpend)
        {
            this.Category = category;
            this.Amount = overSpend;
        }
    }
}