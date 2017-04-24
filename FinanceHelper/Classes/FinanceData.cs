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
                { "artic circle", FinanceCategory.Food },
                { "astro burger", FinanceCategory.Food },
                { "atm fee", FinanceCategory.Withdrawal },
                { "atm withdrawal", FinanceCategory.Withdrawal },
                { "beans and brew", FinanceCategory.Food },
                { "beans &amp; brew", FinanceCategory.Food },
                { "beauty", FinanceCategory.Beauty },
                { "bestbuy", FinanceCategory.Entertainment },
                { "best buy", FinanceCategory.Entertainment },
                { "burt brothers", FinanceCategory.CarMaintenance },
                { "bluffdale city", FinanceCategory.City },
                { "cafe rio", FinanceCategory.Food },
                { "capital one", FinanceCategory.MoneyTransfer },
                { "carter's", FinanceCategory.Clothing },
                { "chevron", FinanceCategory.Gas },
                { "chick-fil-a", FinanceCategory.Food },
                { "child support", FinanceCategory.ChildSupport },
                { "cookie cutters haircuts", FinanceCategory.Haircut },
                { "costco", FinanceCategory.Food },
                { "crown burger", FinanceCategory.Food },
                { "cvs/pharmacy", FinanceCategory.Medical },
                { "deseret book", FinanceCategory.Entertainment },
                { "dish ntwk", FinanceCategory.Television },
                { "dividend credit", FinanceCategory.Interest },
                { "dmv", FinanceCategory.Dmv },
                { "driver lic renewal", FinanceCategory.Dmv },
                { "dollar tree", FinanceCategory.Crafts },
                { "domino's", FinanceCategory.Food },
                { "einstein bros bagels", FinanceCategory.Food },
                { "exxonmobil", FinanceCategory.Gas },
                { "fotofly", FinanceCategory.Pictures },
                { "funds transfer", FinanceCategory.MoneyTransfer },
                { "granger medical clinic", FinanceCategory.Medical },
                { "great clips", FinanceCategory.Haircut },
                { "harmons", FinanceCategory.Food },
                { "humblebundl", FinanceCategory.Entertainment },
                { "itunes", FinanceCategory.Entertainment },
                { "john rowley", FinanceCategory.Food },
                { "kay jewelers", FinanceCategory.Clothing },
                { "kindle", FinanceCategory.Entertainment },
                { "kneaders", FinanceCategory.Food },
                { "gymboree", FinanceCategory.Entertainment },
                { "hallmark", FinanceCategory.Entertainment },
                { "hobby lobby", FinanceCategory.Crafts },
                { "hoffman &amp; co", FinanceCategory.Taxes },
                { "hulu", FinanceCategory.Streaming },
                { "iceberg", FinanceCategory.Food },
                { "interest", FinanceCategory.Interest },
                { "jo-ann", FinanceCategory.Crafts },
                { "lahacienda", FinanceCategory.Food },
                { "let's play music", FinanceCategory.LetsPlayMusic },
                { "lds church", FinanceCategory.Tithing },
                { "maverik", FinanceCategory.Gas },
                { "mcdonald's", FinanceCategory.Food },
                { "megaplex", FinanceCategory.Entertainment },
                { "motherhood", FinanceCategory.Clothing },
                { "netflix", FinanceCategory.Streaming },
                { "nothing bundt cake", FinanceCategory.Food },
                { "old navy", FinanceCategory.Clothing },
                { "office max", FinanceCategory.Entertainment },
                { "papa murphy's", FinanceCategory.Food },
                { "parryfarmsh", FinanceCategory.Hoa },
                { "payment", FinanceCategory.Payment },
                { "pei wei", FinanceCategory.Food },
                { "peterson's", FinanceCategory.Food },
                { "pretzelm", FinanceCategory.Food },
                { "questar", FinanceCategory.NaturalGas },
                { "red robin", FinanceCategory.Food },
                { "reynolds and rey", FinanceCategory.IDS },
                { "s1 savings bank", FinanceCategory.Insurance },
                { "safeco", FinanceCategory.Insurance },
                { "salt lake library", FinanceCategory.Entertainment },
                { "south valley sewer", FinanceCategory.Sewer },
                { "share draft", FinanceCategory.Check },
                { "smiths", FinanceCategory.Food },
                { "starbucks", FinanceCategory.Food },
                { "subway", FinanceCategory.Food },
                { "tacotime", FinanceCategory.Food },
                { "target", FinanceCategory.Clothing },
                { "team beachbody", FinanceCategory.BeachBody },
                { "u-swirl", FinanceCategory.Food },
                { "usps", FinanceCategory.Mail },
                { "utahtaxrfd", FinanceCategory.TaxRefund },
                { "vzwrlss", FinanceCategory.Phone },
                { "wal-mart", FinanceCategory.Food },
                { "wendys", FinanceCategory.Food },
                { "white elegance", FinanceCategory.Clothing },
                { "winco", FinanceCategory.Food },
                { "wm supercenter", FinanceCategory.Food },
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
        EmergencyFund,
        Entertainment,
        FastOfferings,
        Food,
        Gas,
        Haircut,
        Hoa,
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
