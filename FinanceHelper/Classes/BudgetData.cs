using System;
using System.Collections.Generic;
using System.Linq;

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

            SetTotalRowValues();
        }

        private void AddCategoryTotalAmount(Dictionary<FinanceCategory, decimal> CategoryTotalAmount)
        {
            foreach(FinanceCategory financeCategory in CategoryTotalAmount.Keys)
            {
                CombinedCategoryTotalAmount[financeCategory] += CategoryTotalAmount[financeCategory];

                List<BudgetItem> searchResults = BudgetItemList.Where(budgetItem => budgetItem.Category == financeCategory).ToList();

                if (searchResults.Count > 0)
                {
                    BudgetItem budgetItem = null;
                    if (financeCategory == FinanceCategory.NoMatch)
                    {
                        List<BudgetItem> removeHeaders = searchResults.Where(searchResult => searchResult.Header == String.Empty).ToList();

                        if (removeHeaders.Count > 0)
                            budgetItem = removeHeaders[0];
                    }
                    else
                        budgetItem = searchResults[0];

                    if (budgetItem != null)
                    {
                        budgetItem.SpentAmount += CategoryTotalAmount[financeCategory];
                    }
                }
            }
        }

        private void SetTotalRowValues()
        {

        }

        internal List<BudgetItem> BudgetItemList = new List<BudgetItem>();

        internal void InitializeBudgetItemList()
        {
            AddBudgetItem(BudgetGroup.Label, "Income");
            AddIncome();
            
            AddBudgetItem(BudgetGroup.Label, "Expenses");
            AddGiving();
            AddSavings();
            AddHousing();
            AddTransportation();
            AddGroceriesandEatingOut();
            AddLifestyle();
            AddStreaming();
            AddInsuranceAndTax();
            AddDebt();
            AddOtherExpenses();
            AddTotalExpense();
        }

        private void AddIncome()
        {
            BudgetGroup budgetGroup = BudgetGroup.TotalIncome;

            AddBudgetItem(FinanceCategory.IDS, budgetGroup, 6047.48m);
            AddBudgetItem(FinanceCategory.ChildSupport, budgetGroup, 198.00m);
            AddBudgetItem(BudgetGroup.Label, "Total Income");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddGiving()
        {
            BudgetGroup budgetGroup = BudgetGroup.Giving;

            AddBudgetItem(BudgetGroup.Label, "Giving");
            AddBudgetItem(FinanceCategory.Tithing, budgetGroup, 912.12m);
            AddBudgetItem(FinanceCategory.FastOfferings, budgetGroup, 40.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddSavings()
        {
            BudgetGroup budgetGroup = BudgetGroup.Savings;

            AddBudgetItem(BudgetGroup.Label, "Savings");
            AddBudgetItem(FinanceCategory.EmergencyFund, budgetGroup, 100.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddHousing()
        {
            BudgetGroup budgetGroup = BudgetGroup.Housing;

            AddBudgetItem(BudgetGroup.Label, "Housing");
            AddBudgetItem(FinanceCategory.Mortgage, budgetGroup, 2152.04m);
            AddBudgetItem(FinanceCategory.HomeImprovement, budgetGroup, 75.00m);
            AddBudgetItem(FinanceCategory.Hoa, budgetGroup, 10.00m);
            AddBudgetItem(FinanceCategory.Sewer, budgetGroup, 25.00m);
            AddBudgetItem(FinanceCategory.NaturalGas, budgetGroup, 60.00m);
            AddBudgetItem(FinanceCategory.Power, budgetGroup, 140.00m);
            AddBudgetItem(FinanceCategory.Phone, budgetGroup, 165.00m);
            AddBudgetItem(FinanceCategory.Television, budgetGroup, 88.31m);
            AddBudgetItem(FinanceCategory.City, budgetGroup, 50.00m);
            AddBudgetItem(FinanceCategory.Internet, budgetGroup, 58.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddTransportation()
        {
            BudgetGroup budgetGroup = BudgetGroup.Transportation;

            AddBudgetItem(BudgetGroup.Label, "Transportation");
            AddBudgetItem(FinanceCategory.Gas, budgetGroup, 180.00m);
            AddBudgetItem(FinanceCategory.CarMaintenance, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.CarPayment, budgetGroup, 660.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddGroceriesandEatingOut()
        {
            BudgetGroup budgetGroup = BudgetGroup.GroceriesAndEatingOut;

            AddBudgetItem(BudgetGroup.Label, "Groceries and Eating Out");
            AddBudgetItem(FinanceCategory.Groceries, budgetGroup, 370.00m);
            AddBudgetItem(FinanceCategory.EatingOut, budgetGroup, 100.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddLifestyle()
        {
            BudgetGroup budgetGroup = BudgetGroup.Lifestyle;

            AddBudgetItem(BudgetGroup.Label, "Lifestyle");
            AddBudgetItem(FinanceCategory.PetCare, budgetGroup, 25.00m);
            AddBudgetItem(FinanceCategory.Clothing, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.Entertainment, budgetGroup, 60.00m);
            AddBudgetItem(FinanceCategory.Sports, budgetGroup, 5.00m);
            AddBudgetItem(FinanceCategory.Pedicure, budgetGroup, 15.00m);
            AddBudgetItem(FinanceCategory.Music, budgetGroup, 60.00m);
            AddBudgetItem(FinanceCategory.Health, budgetGroup, 130.00m);
            AddBudgetItem(FinanceCategory.MeganAllowance, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.NickAllowance, budgetGroup, 30.00m);
            AddBudgetItem(FinanceCategory.EliSavings, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.PennySavings, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.PaigeSavings, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.Baby4Savings, budgetGroup, 20.00m);
            AddBudgetItem(FinanceCategory.Vacation, budgetGroup, 100.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddStreaming()
        {
            BudgetGroup budgetGroup = BudgetGroup.Streaming;

            AddBudgetItem(BudgetGroup.Label, "Streaming");
            AddBudgetItem(FinanceCategory.Netflix, budgetGroup, 10.67m);
            AddBudgetItem(FinanceCategory.Hulu, budgetGroup, 11.99m);
            AddBudgetItem(FinanceCategory.AmazonPrime, budgetGroup, 8.25m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddInsuranceAndTax()
        {
            BudgetGroup budgetGroup = BudgetGroup.InsuranceAndTax;

            AddBudgetItem(BudgetGroup.Label, "Insurance & Tax");
            AddBudgetItem(FinanceCategory.LifeInsurance, budgetGroup, 80.48m);
            AddBudgetItem(FinanceCategory.AutoInsurance, budgetGroup, 86.10m);
            AddBudgetItem(FinanceCategory.HouseInsurance, budgetGroup, 0.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddDebt()
        {
            BudgetGroup budgetGroup = BudgetGroup.Debt;

            AddBudgetItem(BudgetGroup.Label, "Debt");
            AddBudgetItem(FinanceCategory.CreditCard, budgetGroup, 100.00m);
            AddBudgetItem(FinanceCategory.CreditCardChildSupport, budgetGroup, 200.00m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddOtherExpenses()
        {
            BudgetGroup budgetGroup = BudgetGroup.OtherExpenses;

            AddBudgetItem(BudgetGroup.Label, "Other Expenses");
            AddBudgetItem(FinanceCategory.OtherExpenses, budgetGroup, 0m);
            AddBudgetItem(BudgetGroup.Label, "Total");
            AddBudgetItem(BudgetGroup.EmptyLine, "EmptyLine1");
        }

        private void AddTotalExpense()
        {
            AddBudgetItem(BudgetGroup.Label, "Total Expense");
        }

        internal void AddBudgetItem(BudgetGroup budgetGroup, string header)
        {
            BudgetItemList.Add(new BudgetItem(budgetGroup, header));
        }

        internal void AddBudgetItem(FinanceCategory financeCategory, BudgetGroup budgetGroup, decimal plannedAmount)
        {
            BudgetItemList.Add(new BudgetItem(financeCategory, budgetGroup, plannedAmount));
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

    internal enum BudgetGroup
    {
        Debt,
        EmptyLine,
        Giving,
        GroceriesAndEatingOut,
        Housing,
        InsuranceAndTax,
        Label,
        Lifestyle,
        OtherExpenses,
        Savings,
        Streaming,
        TotalExpenses,
        TotalIncome,
        Transportation,
    }

    internal class BudgetItem
    {
        internal FinanceCategory Category;
        internal BudgetGroup Group;
        internal decimal PlannedAmount;
        internal decimal SpentAmount;
        internal decimal RemainingAmount
        {
            get
            {
                return PlannedAmount - SpentAmount;
            }
        }

        internal string Header;

        internal BudgetItem(BudgetGroup group, string header)
        {
            Header = header;

            Group = group;
        }

        internal BudgetItem(FinanceCategory category, BudgetGroup group, decimal plannedAmount)
        {
            Category = category;
            PlannedAmount = plannedAmount;
            SpentAmount = 0;

            Group = group;

            Header = String.Empty;
        }
    }
}
