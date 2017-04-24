﻿using System;
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
                { "beauty", FinanceCategory.Beauty }
            };


            internal void SetCategory()
            {
                string description = Description.ToLower();

                if (description.Contains("beauty"))
                    Category = FinanceCategory.Beauty;
                else if (description.Contains("burt brothers"))
                    Category = FinanceCategory.CarMaintenance;
                else if (description.Contains("bluffdale city"))
                    Category = FinanceCategory.City;
                else if (description.Contains("carter's") || description.Contains("motherhood") || description.Contains("target"))
                    Category = FinanceCategory.Clothing;
                else if (description.Contains("hobby lobby") || description.Contains("dollar tree"))
                    Category = FinanceCategory.Crafts;
                else if (description.Contains("dmv") || description.Contains("driver lic renewal"))
                    Category = FinanceCategory.Dmv;
                else if (description.Contains("kindle") || description.Contains("amazon") || description.Contains("megaplex") || description.Contains("itunes") || description.Contains("bestbuy") || description.Contains("humblebundl") || description.Contains("salt lake library"))
                    Category = FinanceCategory.Entertainment;
                else if (description.Contains("harmons") || description.Contains("costco") || description.Contains("wendys") || description.Contains("wm supercenter") || description.Contains("astro burger") || description.Contains("red robin") || description.Contains("domino's") || description.Contains("beans and brew") || description.Contains("cafe rio") || description.Contains("einstein bros bagels") || description.Contains("chick-fil-a") || description.Contains("pretzelm") || description.Contains("kneaders") || description.Contains("smiths") || description.Contains("papa murphy's"))
                    Category = FinanceCategory.Food;
                else if (description.Contains("maverik"))
                    Category = FinanceCategory.Gas;
                else if (description.Contains("parryfarmsh"))
                    Category = FinanceCategory.Hoa;
                else if (description.Contains("safeco"))
                    Category = FinanceCategory.Insurance;
                else if (description.Contains("interest"))
                    Category = FinanceCategory.Interest;
                else if (description.Contains("let's play music"))
                    Category = FinanceCategory.LetsPlayMusic;
                else if (description.Contains("questar"))
                    Category = FinanceCategory.NaturalGas;
                else if (description.Contains("payment"))
                    Category = FinanceCategory.Payment;
                else if (description.Contains("south valley sewer"))
                    Category = FinanceCategory.Sewer;
                else if (description.Contains("hulu") || description.Contains("netflix"))
                    Category = FinanceCategory.Streaming;
                else if (description.Contains("dish ntwk"))
                    Category = FinanceCategory.Television;

                FinanceCategory test;

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
        City,
        Clothing,
        Crafts,
        Dmv,
        EmergencyFund,
        Entertainment,
        FastOfferings,
        Food,
        Gas,
        Hoa,
        Insurance,
        Interest,
        Internet,
        LetsPlayMusic,
        Mortgage,
        NaturalGas,
        Payment,
        Phone,
        Power,
        Sewer,
        Streaming,
        Television,
        Tithing,

        IDS,
        ChildSupport,
        BeachBody,
        MiscellaneousIncome
    }
}
