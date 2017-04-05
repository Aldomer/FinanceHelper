using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceHelper.Classes
{
    internal class BudgetData
    {
        internal Dictionary<FinanceCategory, decimal> CombinedCategoryTotalAmount = new Dictionary<FinanceCategory, decimal>();

        internal BudgetData()
        {
            foreach (FinanceCategory financeCategory in Enum.GetValues(typeof(FinanceCategory)))
            {
                CombinedCategoryTotalAmount.Add(financeCategory, 0.00m);
            }

            InitializeBudgetItemList();
        }

        internal void CombineCategoryTotalAmount(BankFileLoader bankFileLoader)
        {
            AddCategoryTotalAmount(bankFileLoader.ChaseFinanceData.CategoryTotalAmount);
            AddCategoryTotalAmount(bankFileLoader.CyprusFinanceData.CategoryTotalAmount);
            AddCategoryTotalAmount(bankFileLoader.CapitalFinanceData.CategoryTotalAmount);
        }

        private void AddCategoryTotalAmount(Dictionary<FinanceCategory, decimal> CategoryTotalAmount)
        {
            foreach(FinanceCategory financeCategory in CategoryTotalAmount.Keys)
            {
                CombinedCategoryTotalAmount[financeCategory] += CategoryTotalAmount[financeCategory];
            }
        }

        internal List<BudgetItem> BudgetItemList = new List<BudgetItem>();

        internal void InitializeBudgetItemList()
        {
            AddBudgetItem("Income");
            AddBudgetItem(FinanceCategory.IDS, 5979.34m);
            AddBudgetItem(FinanceCategory.ChildSupport, 198.00m);
            AddBudgetItem(FinanceCategory.BeachBody, 28.00m);
            AddBudgetItem(FinanceCategory.MiscellaneousIncome, 0.00m);
            AddBudgetItem("Total Income");
            AddBudgetItem("EmptyLine1");
            AddBudgetItem("Expenses");
            AddBudgetItem("Giving");
            AddBudgetItem(FinanceCategory.Tithing, 900.44m);
            AddBudgetItem(FinanceCategory.FastOfferings, 40.00m);
            AddBudgetItem("Savings");
            AddBudgetItem(FinanceCategory.EmergencyFund, 100.00m);
            AddBudgetItem("Housing");
            AddBudgetItem(FinanceCategory.Mortgage, 2087.73m);
            AddBudgetItem(FinanceCategory.Hoa, 0.00m);
            AddBudgetItem(FinanceCategory.Sewer, 25.00m);
            AddBudgetItem(FinanceCategory.NaturalGas, 60.00m);
            AddBudgetItem(FinanceCategory.Power, 140.00m);
            AddBudgetItem(FinanceCategory.Phone, 120.00m);
            AddBudgetItem(FinanceCategory.Television, 120.00m);
            AddBudgetItem(FinanceCategory.EmergencyFund, 39.38m);
            AddBudgetItem(FinanceCategory.BluffdaleCity, 48.70m);
            AddBudgetItem(FinanceCategory.Internet, 66.95m);
            AddBudgetItem("Transportation");
            AddBudgetItem(FinanceCategory.CarMaintenance, 200.00m);
            AddBudgetItem(FinanceCategory.CarReplacement, 0.00m);
            AddBudgetItem(FinanceCategory.CarPayment, 660.00m);
        }

        internal void AddBudgetItem(string header)
        {
            BudgetItemList.Add(new BudgetItem(header));
        }

        internal void AddBudgetItem(FinanceCategory financeCategory, decimal plannedAmount)
        {
            BudgetItemList.Add(new BudgetItem(financeCategory, plannedAmount, CombinedCategoryTotalAmount[financeCategory]));
        }

        internal Dictionary<string, decimal?> PlannedBudgetOld = new Dictionary<string, decimal?>
        {
            { "Food", null },
            { "Groceries", 200.0m },
            { "Restaurants", 0.00m },
            { "Lifestyle", null },
            { "Pet Care", 50.00m },
            { "Clothing", 0.00m },
            { "School Tuition", 110.00m },
            { "Sports & Entertainment", 0.00m },
            { "Miscellaneous Lifestyle", 0.00m },
            { "Let's Play Music", 60.00m },
            { "Netflix", 8.54m },
            { "Shakeology", 230.00m },
            { "Hulu", 11.99m },
            { "Megan Allowance", 20.00m },
            { "Nick Allowance", 20.00m },
            { "Eli Allowance", 20.00m },
            { "Penny Allowance", 20.00m },
            { "Amazon Prime", 0.00m },
            { "Insurance & Tax", null },
            { "Life Insurance", 80.48m },
            { "Auto Insurance", 86.10m },
            { "House Insurance", 0.00m },
            { "Debt", null },
            { "Amazon Credit Card", 200.00m },
            { "Amazon Credit Card Interest", 0.00m },
            { "Total Expenses", null },
            { "Left to Budget", null },
        };
    }

    internal class BudgetItem
    {
        internal FinanceCategory Category;
        internal decimal PlannedAmount;
        internal decimal SpentAmount;
        internal decimal RemainingAmount;

        internal string Header;

        internal BudgetItem(string header)
        {
            Header = header;
        }

        internal BudgetItem(FinanceCategory category, decimal plannedAmount, decimal spentAmount = 0, decimal remainingAmount = 0)
        {
            Category = category;
            PlannedAmount = plannedAmount;
            SpentAmount = spentAmount;
            RemainingAmount = remainingAmount;

            Header = String.Empty;
        }
    }
}
