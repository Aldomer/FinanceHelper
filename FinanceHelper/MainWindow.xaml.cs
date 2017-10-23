using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FinanceHelper.Classes;
using System.Collections.Generic;

namespace FinanceHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BankFileLoader _bankFileLoader = new BankFileLoader();

        private BudgetData _budgetData = new BudgetData();

        private bool _chaseLoaded = false;
        private bool _cyprusLoaded = false;
        private bool _capitalLoaded = false;

        public MainWindow()
        {
            InitializeComponent();

            HideAllDataGrids();

            AddBudgetHeader(dgFinances);

            AddFinanceHeader(dgChase);
            AddFinanceHeader(dgCyprus);
            AddFinanceHeader(dgCapital);

            AddFinanceStatsHeader(dgChaseStats);
            AddFinanceStatsHeader(dgCyprusStats);
            AddFinanceStatsHeader(dgCapitalStats);

            AddFinanceStatsHeader(dgAllStats);
        }

        private void btnChase_Click(object sender, RoutedEventArgs e)
        {
            HideAllDataGrids();

            dgChase.Visibility = Visibility.Visible;
            dgChaseStats.Visibility = Visibility.Visible;

            if (!_chaseLoaded)
            {
                _bankFileLoader.LoadChase();

                PopulateFinanceData(_bankFileLoader.ChaseFinanceData.FinanceDataList, dgChase);

                PopulateFinanceStats(_bankFileLoader.ChaseFinanceData, dgChaseStats);

                PopulateAllStats(dgAllStats);

                _chaseLoaded = true;
            }
        }

        private void btnCyprus_Click(object sender, RoutedEventArgs e)
        {
            HideAllDataGrids();

            dgCyprus.Visibility = Visibility.Visible;
            dgCyprusStats.Visibility = Visibility.Visible;

            if (!_cyprusLoaded)
            {
                _bankFileLoader.LoadCyprus();

                PopulateFinanceData(_bankFileLoader.CyprusFinanceData.FinanceDataList, dgCyprus);

                PopulateFinanceStats(_bankFileLoader.CyprusFinanceData, dgCyprusStats);

                PopulateAllStats(dgAllStats);

                _cyprusLoaded = true;
            }
        }

        private void btnCapital_Click(object sender, RoutedEventArgs e)
        {
            HideAllDataGrids();

            dgCapital.Visibility = Visibility.Visible;
            dgCapitalStats.Visibility = Visibility.Visible;

            if (!_capitalLoaded)
            {
                //_bankFileLoader.LoadCapital();

                //PopulateFinanceData(_bankFileLoader.CapitalFinanceData.FinanceDataList, dgCapital);

                //PopulateFinanceStats(_bankFileLoader.CapitalFinanceData, dgCapitalStats);

                //PopulateAllStats(dgAllStats);

                //_capitalLoaded = true;
            }
        }

        private void btnFinances_Click(object sender, RoutedEventArgs e)
        {
            HideAllDataGrids();

            dgFinances.Visibility = Visibility.Visible;

            _budgetData.CombineCategoryTotalAmount(_bankFileLoader);

            PopulateBudgetData(dgFinances);
        }

        private void HideAllDataGrids()
        {
            dgChase.Visibility = Visibility.Hidden;
            dgCyprus.Visibility = Visibility.Hidden;
            dgCapital.Visibility = Visibility.Hidden;

            dgChaseStats.Visibility = Visibility.Hidden;
            dgCyprusStats.Visibility = Visibility.Hidden;
            dgCapitalStats.Visibility = Visibility.Hidden;

            dgFinances.Visibility = Visibility.Hidden;
        }

        private void PopulateBudgetData(DataGrid dataGrid)
        {
            decimal budgetIncome = 0.00m, budgetExpenses = 0.00m;

            bool isIncome = true;

            foreach (BudgetItem budgetItem in _budgetData.BudgetItemList)
            {
                decimal plannedAmount = 0.00m;

                if (budgetItem.Header == "Expenses")
                    isIncome = false;
                
                if (budgetItem.Header.StartsWith("EmptyLine"))
                    AddBudgetItemToGrid(dataGrid, String.Empty);
                else if (budgetItem.Header == "Total Income")
                    AddBudgetItemToGrid(dataGrid, budgetItem.Header, budgetIncome);
                else if (budgetItem.Header == "Total Expenses")
                    AddBudgetItemToGrid(dataGrid, budgetItem.Header, budgetExpenses);
                else if (budgetItem.Header == "Left to Budget")
                    AddBudgetItemToGrid(dataGrid, budgetItem.Header, budgetIncome - budgetExpenses);
                else if (budgetItem.Header != String.Empty)
                    AddBudgetItemToGrid(dataGrid, budgetItem.Header);
                else
                    AddBudgetItemToGrid(dataGrid, budgetItem.Category, budgetItem.PlannedAmount, budgetItem.SpentAmount, budgetItem.RemainingAmount);

                if (isIncome)
                    budgetIncome += budgetItem.PlannedAmount;
                else
                    budgetExpenses += budgetItem.PlannedAmount;
            }
        }

        private void AddBudgetItemToGrid(DataGrid dataGrid, string header)
        {
            BudgetDataDisplay item = new BudgetDataDisplay
            {
                Category = header,
                PlannedAmount = String.Empty,
                SpentAmount = String.Empty,
                RemainingAmount = String.Empty
            };

            dataGrid.Items.Add(item);
        }

        private void AddBudgetItemToGrid(DataGrid dataGrid, string header, decimal plannedAmount)
        {
            BudgetDataDisplay item = new BudgetDataDisplay
            {
                Category = header,
                PlannedAmount = FormatMoney(plannedAmount),
                SpentAmount = String.Empty,
                RemainingAmount = String.Empty
            };

            dataGrid.Items.Add(item);
        }

        private void AddBudgetItemToGrid(DataGrid dataGrid, FinanceCategory category, decimal plannedAmount, decimal spentAmount = 0, decimal remainingAmount = 0)
        {
            BudgetDataDisplay item = new BudgetDataDisplay
            {
                Category = category.ToString(),
                PlannedAmount = FormatMoney(plannedAmount),
                SpentAmount = FormatMoney(spentAmount),
                RemainingAmount = FormatMoney(remainingAmount)
            };

            dataGrid.Items.Add(item);
        }

        private void PopulateFinanceData(List<FinanceData.FinanceDataItem> financeData, DataGrid dataGrid)
        {
            foreach (FinanceData.FinanceDataItem data in financeData)
            {
                //if (data.Category == FinanceCategory.NoMatch)
                //{ 
                FinanceDataDisplay item = new FinanceDataDisplay
                {
                    Type = data.Type.ToString(),
                    TransDate = data.TransDate.ToString("MM/dd/yy"),
                    PostDate = data.PostDate.ToString("MM/dd/yy"),
                    Description = data.Description,
                    Amount = FormatMoney(data.Amount),
                    Category = data.Category.ToString()
                };

                dataGrid.Items.Add(item);
                //}
            }
        }

        private string FormatMoney(decimal amount)
        {
            return string.Format("{0:C}", amount);
        }

        private string FormatPercent(decimal percent)
        {
            string formattedPercent = string.Format("{0:P2}", percent);

            if (formattedPercent.Length == 6)
                formattedPercent = "0" + formattedPercent;

            return formattedPercent;
        }

        public class BudgetDataDisplay
        {
            public string Category { get; set; }
            public string PlannedAmount { get; set; }
            public string SpentAmount { get; set; }
            public string RemainingAmount { get; set; }
        }

        public class FinanceDataDisplay
        {
            public string Type { get; set; }
            public string TransDate { get; set; }
            public string PostDate { get; set; }
            public string Description { get; set; }
            public string Amount { get; set; }
            public string Category { get; set; }
        }

        public class FinanceStatsDisplay
        {
            public string Category { get; set; }
            public string Amount { get; set; }
            public string Percent { get; set; }
        }

        private void PopulateFinanceStats(FinanceData financeData, DataGrid dataGrid)
        {
            foreach (FinanceCategory financeCategory in Enum.GetValues(typeof(FinanceCategory)))
            {
                if (financeData.CategoryTotalAmount[financeCategory] != 0)
                {
                    FinanceStatsDisplay financeCategoryItem = new FinanceStatsDisplay
                    {
                        Category = financeCategory.ToString(),
                        Amount = FormatMoney(financeData.CategoryTotalAmount[financeCategory]),
                        Percent = FormatPercent((financeData.CategoryTotalAmount[financeCategory] / financeData.TotalAmount))
                    };

                    dataGrid.Items.Add(financeCategoryItem);
                }
            }

            FinanceStatsDisplay totalAmountItem = new FinanceStatsDisplay
            {
                Category = "Total Amount",
                Amount = FormatMoney(financeData.TotalAmount),
                Percent = String.Empty
            };

            dataGrid.Items.Add(totalAmountItem);
        }

        private void PopulateAllStats(DataGrid dataGrid)
        {
            FinanceData allData = new FinanceData();

            foreach (FinanceCategory financeCategory in Enum.GetValues(typeof(FinanceCategory)))
            {
                allData.CategoryTotalAmount[financeCategory] += _bankFileLoader.ChaseFinanceData.CategoryTotalAmount[financeCategory];
            }

            allData.TotalAmount += _bankFileLoader.ChaseFinanceData.TotalAmount;

            PopulateFinanceStats(allData, dataGrid);
        }

        #region FinanceHeader
        private void AddBudgetHeader(DataGrid dataGrid)
        {
            DataGridTextColumn dgTextColumnCategory = new DataGridTextColumn();
            dgTextColumnCategory.Header = "Category";
            dgTextColumnCategory.Width = 100;
            dgTextColumnCategory.Binding = new Binding("Category");
            dataGrid.Columns.Add(dgTextColumnCategory);

            DataGridTextColumn dgTextColumnPlannedAmount = new DataGridTextColumn();
            dgTextColumnPlannedAmount.Header = "Planned Amount";
            dgTextColumnPlannedAmount.Width = 100;
            dgTextColumnPlannedAmount.Binding = new Binding("PlannedAmount");
            dataGrid.Columns.Add(dgTextColumnPlannedAmount);

            DataGridTextColumn dgTextColumnSpentAmount = new DataGridTextColumn();
            dgTextColumnSpentAmount.Header = "Spent Amount";
            dgTextColumnSpentAmount.Width = 100;
            dgTextColumnSpentAmount.Binding = new Binding("SpentAmount");
            dataGrid.Columns.Add(dgTextColumnSpentAmount);

            DataGridTextColumn dgTextColumnRemainingAmount = new DataGridTextColumn();
            dgTextColumnRemainingAmount.Header = "Remaining Amount";
            dgTextColumnRemainingAmount.Width = 100;
            dgTextColumnRemainingAmount.Binding = new Binding("RemainingAmount");
            dataGrid.Columns.Add(dgTextColumnRemainingAmount);
        }

        private void AddFinanceHeader(DataGrid dataGrid)
        {
            DataGridTextColumn dgTextColumnType = new DataGridTextColumn();
            dgTextColumnType.Header = "Type";
            dgTextColumnType.Binding = new Binding("Type");
            dgTextColumnType.Width = 100;
            dataGrid.Columns.Add(dgTextColumnType);

            DataGridTextColumn dgTextColumnTranDate = new DataGridTextColumn();
            dgTextColumnTranDate.Header = "Transaction Date";
            dgTextColumnTranDate.Width = 100;
            dgTextColumnTranDate.Binding = new Binding("TransDate");
            dataGrid.Columns.Add(dgTextColumnTranDate);

            DataGridTextColumn dgTextColumnPostDate = new DataGridTextColumn();
            dgTextColumnPostDate.Header = "Post Date";
            dgTextColumnPostDate.Width = 100;
            dgTextColumnPostDate.Binding = new Binding("PostDate");
            dataGrid.Columns.Add(dgTextColumnPostDate);

            DataGridTextColumn dgTextColumnDescription = new DataGridTextColumn();
            dgTextColumnDescription.Header = "Description";
            dgTextColumnDescription.Width = 300;
            dgTextColumnDescription.Binding = new Binding("Description");
            dataGrid.Columns.Add(dgTextColumnDescription);

            DataGridTextColumn dgTextColumnAmount = new DataGridTextColumn();
            dgTextColumnAmount.Header = "Amount";
            dgTextColumnAmount.Width = 100;
            dgTextColumnAmount.Binding = new Binding("Amount");
            dataGrid.Columns.Add(dgTextColumnAmount);

            DataGridTextColumn dgTextColumnCategory = new DataGridTextColumn();
            dgTextColumnCategory.Header = "Category";
            dgTextColumnCategory.Width = 100;
            dgTextColumnCategory.Binding = new Binding("Category");
            dataGrid.Columns.Add(dgTextColumnCategory);
        }

        private void AddFinanceStatsHeader(DataGrid dataGrid)
        {
            DataGridTextColumn dgTextColumnCategory = new DataGridTextColumn();
            dgTextColumnCategory.Header = "Category";
            dgTextColumnCategory.Width = 200;
            dgTextColumnCategory.Binding = new Binding("Category");
            dataGrid.Columns.Add(dgTextColumnCategory);

            DataGridTextColumn dgTextColumnAmount = new DataGridTextColumn();
            dgTextColumnAmount.Header = "Amount";
            dgTextColumnAmount.Width = 100;
            dgTextColumnAmount.Binding = new Binding("Amount");
            dataGrid.Columns.Add(dgTextColumnAmount);

            DataGridTextColumn dgTextColumnPercent = new DataGridTextColumn();
            dgTextColumnPercent.Header = "Percent";
            dgTextColumnPercent.Width = 100;
            dgTextColumnPercent.Binding = new Binding("Percent");
            dataGrid.Columns.Add(dgTextColumnPercent);
        }
        #endregion FinanceHeader
    }
}
