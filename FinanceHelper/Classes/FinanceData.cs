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
                { "bath and body works", FinanceCategory.Groceries },
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
                { "country bakery of lehi", FinanceCategory.EatingOut },
                { "costco", FinanceCategory.Groceries },
                { "crown burger", FinanceCategory.EatingOut },
                { "cvs/pharmacy", FinanceCategory.Medical },
                { "darkhorse", FinanceCategory.Entertainment },
                { "deseret book", FinanceCategory.Entertainment },
                { "dickeys", FinanceCategory.EatingOut },
                { "dish network", FinanceCategory.Television },
                { "dish ntwk", FinanceCategory.Television },
                { "dividend credit", FinanceCategory.Interest },
                { "dmv", FinanceCategory.Dmv },
                { "driver lic renewal", FinanceCategory.Dmv },
                { "dollar tree", FinanceCategory.Crafts },
                { "dollartree", FinanceCategory.Crafts },
                { "domino's", FinanceCategory.EatingOut },
                { "doterra", FinanceCategory.Health },
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
                { "hee haw farms", FinanceCategory.Entertainment },
                { "hobby lobby", FinanceCategory.Crafts },
                { "hoffman &amp; co", FinanceCategory.Taxes },
                { "hulu", FinanceCategory.Hulu },
                { "iceberg", FinanceCategory.EatingOut },
                { "ihop", FinanceCategory.EatingOut },
                { "interest", FinanceCategory.Interest },
                { "jo-ann", FinanceCategory.Crafts },
                { "lahacienda", FinanceCategory.EatingOut },
                { "let's play music", FinanceCategory.Music },
                { "lds church", FinanceCategory.Tithing },
                { "lhm stockton #12", FinanceCategory.CarMaintenance },
                { "little adventures", FinanceCategory.Clothing },
                { "little caesars", FinanceCategory.EatingOut },
                { "maverik", FinanceCategory.Gas },
                { "mcdonald's", FinanceCategory.EatingOut },
                { "megaplex", FinanceCategory.Entertainment },
                { "michaels", FinanceCategory.Crafts },
                { "motherhood", FinanceCategory.Clothing },
                { "my growing season", FinanceCategory.Crafts },
                { "netflix", FinanceCategory.Netflix },
                { "nothing bundt cake", FinanceCategory.EatingOut },
                { "old navy", FinanceCategory.Clothing },
                { "office max", FinanceCategory.Entertainment },
                { "panda express", FinanceCategory.EatingOut },
                { "papa murphy's", FinanceCategory.EatingOut },
                { "parryfarmsh", FinanceCategory.Hoa },
                { "payment", FinanceCategory.Payment },
                { "pei wei", FinanceCategory.EatingOut },
                { "peterson's", FinanceCategory.Groceries },
                { "petersen family farm", FinanceCategory.Groceries },
                { "pizzeria limone", FinanceCategory.EatingOut },
                { "pretzelm", FinanceCategory.EatingOut },
                { "preventive pest control", FinanceCategory.PestControl },
                { "questar", FinanceCategory.NaturalGas },
                { "quilted bear", FinanceCategory.Crafts },
                { "red robin", FinanceCategory.EatingOut },
                { "redbox", FinanceCategory.Entertainment },
                { "reynolds and rey", FinanceCategory.IDS },
                { "s1 savings bank", FinanceCategory.Insurance },
                { "safeco", FinanceCategory.Insurance },
                { "salt lake library", FinanceCategory.Entertainment },
                { "samsclub", FinanceCategory.Groceries },
                { "sears", FinanceCategory.HomeImprovement },
                { "sees candy", FinanceCategory.EatingOut },
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
                { "vip nails", FinanceCategory.Pedicure },
                { "vzwrlss", FinanceCategory.Phone },
                { "wal-mart", FinanceCategory.Groceries },
                { "walmart", FinanceCategory.Groceries },
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
        AmazonPrime,
        AutoInsurance,
        Baby4Savings,
        Beauty,
        CarMaintenance,
        CarPayment,
        CarReplacement,
        Check,
        City,
        Clothing,
        Crafts,
        CreditCard,
        CreditCardChildSupport,
        Dmv,
        EatingOut,
        EliSavings,
        EmergencyFund,
        Entertainment,
        FastOfferings,
        Gas,
        Groceries,
        Haircut,
        Health,
        Hoa,
        HomeImprovement,
        HouseInsurance,
        Hulu,
        Insurance,
        Interest,
        Internet,
        Mail,
        Medical,
        MeganAllowance,
        Netflix,
        NickAllowance,
        LifeInsurance,
        MoneyTransfer,
        Mortgage,
        Music,
        NaturalGas,
        PaigeSavings,
        Payment,
        Pedicure,
        PestControl,
        PetCare,
        Phone,
        PennySavings,
        Pictures,
        Power,
        Seasonal,
        Sewer,
        Sports,
        Streaming,
        Taxes,
        TaxRefund,
        Television,
        Tithing,
        Vacation,
        Withdrawal,

        OtherExpenses,

        IDS,
        ChildSupport,
        BeachBody,
        MiscellaneousIncome
    }
}
