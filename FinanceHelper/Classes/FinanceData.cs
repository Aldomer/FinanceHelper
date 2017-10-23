using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceHelper.Classes
{
    internal class FinanceData
    {
        internal decimal TotalAmount;

        internal Dictionary<FinanceCategory, decimal> CategoryTotalAmount = new Dictionary<FinanceCategory, decimal>();

        internal List<FinanceDataItem> FinanceDataList = new List<FinanceDataItem>();

        internal FinanceData()
        {
            foreach (FinanceCategory financeCategory in Enum.GetValues(typeof(FinanceCategory)))
            {
                CategoryTotalAmount.Add(financeCategory, 0);
            }
        }

        internal void AddFinanceDataItem(FinanceDataItem financeDataItem)
        {
            decimal positiveAmount = financeDataItem.Amount * -1;

            if (positiveAmount > 0)
            {
                TotalAmount += positiveAmount;

                CategoryTotalAmount[financeDataItem.Category] += positiveAmount;
            }

            FinanceDataList.Add(financeDataItem);
        }

        internal class FinanceDataItem
        {
            internal DateTime TransDate;
            internal DateTime PostDate;
            internal string Description;
            internal decimal Amount;
            internal FinanceType Type;
            internal FinanceCategory Category;

            Dictionary<string, FinanceCategory> DescriptionToCategory = new Dictionary<string, FinanceCategory>
            {
                { "ach check", FinanceCategory.Check },
                { "amazon", FinanceCategory.Entertainment },
                { "amzn", FinanceCategory.Entertainment },
                { "apollo burger", FinanceCategory.EatingOut },
                { "artic circle", FinanceCategory.EatingOut },
                { "astro burger", FinanceCategory.EatingOut },
                { "atm fee", FinanceCategory.Withdrawal },
                { "atm withdrawal", FinanceCategory.Withdrawal },
                { "audible", FinanceCategory.Entertainment },
                { "beans and brew", FinanceCategory.EatingOut },
                { "beans andbrews", FinanceCategory.EatingOut },
                { "beans &amp; brew", FinanceCategory.EatingOut },
                { "beauty", FinanceCategory.Beauty },
                { "bestbuy", FinanceCategory.Entertainment },
                { "best buy", FinanceCategory.Entertainment },
                { "burt brothers", FinanceCategory.CarMaintenance },
                { "bluffdale city", FinanceCategory.City },
                { "cafe rio", FinanceCategory.EatingOut },
                { "capital one", FinanceCategory.MoneyTransfer },
                { "carter's", FinanceCategory.Clothing },
                { "chevron", FinanceCategory.Gas },
                { "chick-fil-a", FinanceCategory.EatingOut },
                { "child support", FinanceCategory.ChildSupport },
                { "cookie cutters haircuts", FinanceCategory.Haircut },
                { "costco", FinanceCategory.Groceries },
                { "crown burger", FinanceCategory.EatingOut },
                { "cvs/pharmacy", FinanceCategory.Medical },
                { "deseret book", FinanceCategory.Entertainment },
                { "dickeys", FinanceCategory.EatingOut },
                { "dish ntwk", FinanceCategory.Television },
                { "dividend credit", FinanceCategory.Interest },
                { "dmv", FinanceCategory.Dmv },
                { "driver lic renewal", FinanceCategory.Dmv },
                { "dollar tree", FinanceCategory.Crafts },
                { "domino's", FinanceCategory.EatingOut },
                { "einstein bros bagels", FinanceCategory.EatingOut },
                { "exxonmobil", FinanceCategory.Gas },
                { "fotofly", FinanceCategory.Pictures },
                { "funds transfer", FinanceCategory.MoneyTransfer },
                { "granger medical clinic", FinanceCategory.Medical },
                { "great clips", FinanceCategory.Haircut },
                { "harmons", FinanceCategory.Groceries },
                { "holiday oil", FinanceCategory.Gas },
                { "home depot", FinanceCategory.HomeImprovement },
                { "humblebundl", FinanceCategory.Entertainment },
                { "ikea", FinanceCategory.HomeImprovement },
                { "itunes", FinanceCategory.Entertainment },
                { "john rowley", FinanceCategory.EatingOut },
                { "kay jewelers", FinanceCategory.Clothing },
                { "kindle", FinanceCategory.Entertainment },
                { "kneaders", FinanceCategory.EatingOut },
                { "gymboree", FinanceCategory.Entertainment },
                { "hallmark", FinanceCategory.Entertainment },
                { "hobby lobby", FinanceCategory.Crafts },
                { "hoffman &amp; co", FinanceCategory.Taxes },
                { "hulu", FinanceCategory.Streaming },
                { "iceberg", FinanceCategory.EatingOut },
                { "ihop", FinanceCategory.EatingOut },
                { "interest", FinanceCategory.Interest },
                { "jo-ann", FinanceCategory.Crafts },
                { "lahacienda", FinanceCategory.EatingOut },
                { "let's play music", FinanceCategory.LetsPlayMusic },
                { "lds church", FinanceCategory.Tithing },
                { "maverik", FinanceCategory.Gas },
                { "mcdonald's", FinanceCategory.EatingOut },
                { "megaplex", FinanceCategory.Entertainment },
                { "motherhood", FinanceCategory.Clothing },
                { "netflix", FinanceCategory.Streaming },
                { "nothing bundt cake", FinanceCategory.EatingOut },
                { "old navy", FinanceCategory.Clothing },
                { "office max", FinanceCategory.Entertainment },
                { "panda express", FinanceCategory.EatingOut },
                { "papa murphy's", FinanceCategory.EatingOut },
                { "parryfarmsh", FinanceCategory.Hoa },
                { "payment", FinanceCategory.Payment },
                { "pei wei", FinanceCategory.EatingOut },
                { "peterson's", FinanceCategory.Groceries },
                { "pretzelm", FinanceCategory.EatingOut },
                { "questar", FinanceCategory.NaturalGas },
                { "quilted bear", FinanceCategory.Crafts },
                { "red robin", FinanceCategory.EatingOut },
                { "reynolds and rey", FinanceCategory.IDS },
                { "s1 savings bank", FinanceCategory.Insurance },
                { "safeco", FinanceCategory.Insurance },
                { "salt lake library", FinanceCategory.Entertainment },
                { "sears", FinanceCategory.HomeImprovement },
                { "shakeology", FinanceCategory.Health },
                { "share draft", FinanceCategory.Check },
                { "smiths", FinanceCategory.Groceries },
                { "south valley sewer", FinanceCategory.Sewer },
                { "spirit halloween", FinanceCategory.Seasonal },
                { "sprouts", FinanceCategory.Groceries },
                { "starbucks", FinanceCategory.EatingOut },
                { "subway", FinanceCategory.EatingOut },
                { "tacotime", FinanceCategory.EatingOut },
                { "target", FinanceCategory.Clothing },
                { "team beachbody", FinanceCategory.BeachBody },
                { "u-swirl", FinanceCategory.EatingOut },
                { "usps", FinanceCategory.Mail },
                { "utahtaxrfd", FinanceCategory.TaxRefund },
                { "utah state fair", FinanceCategory.Entertainment },
                { "vzwrlss", FinanceCategory.Phone },
                { "wal-mart", FinanceCategory.Groceries },
                { "wendys", FinanceCategory.EatingOut },
                { "white elegance", FinanceCategory.Clothing },
                { "winco", FinanceCategory.EatingOut },
                { "wm supercenter", FinanceCategory.EatingOut },
                { "wood creations", FinanceCategory.Crafts },
            };


            internal void SetCategory()
            {
                string description = Description.ToLower();

                var match = DescriptionToCategory.Where(DescriptionToCategory => description.Contains(DescriptionToCategory.Key)).ToList();
                if (match.Count > 0)
                {
                    string financeCategoryString = match[0].Value.ToString();

                    FinanceCategory category;
                    Enum.TryParse(financeCategoryString, out category);
                    Category = category;
                }
            }
        }
    }

    internal enum FinanceType
    {
        NoMatch,
        Fee,
        Payment,
        Return,
        Sale
    }

    internal enum FinanceCategory
    {
        NoMatch,
        Beauty,
        BluffdaleCity,
        CarMaintenance,
        CarPayment,
        CarReplacement,
        Check,
        City,
        Clothing,
        Crafts,
        Dmv,
        EatingOut,
        EmergencyFund,
        Entertainment,
        FastOfferings,
        Gas,
        Groceries,
        Haircut,
        Health,
        Hoa,
        HomeImprovement,
        Insurance,
        Interest,
        Internet,
        LetsPlayMusic,
        Mail,
        Medical,
        MoneyTransfer,
        Mortgage,
        NaturalGas,
        Payment,
        Phone,
        Pictures,
        Power,
        Seasonal,
        Sewer,
        Streaming,
        Taxes,
        TaxRefund,
        Television,
        Tithing,
        Withdrawal,

        IDS,
        ChildSupport,
        BeachBody,
        MiscellaneousIncome
    }
}
