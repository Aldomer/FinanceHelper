using System;
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

        public MainWindow()
        {
            InitializeComponent();

            AddFinanceHeader(dgChase);

            ColumnDefinition gridCol1 = new ColumnDefinition();

            System.Windows.GridLength gridLength = new GridLength(100);

            gridCol1.Width = gridLength;

            ColumnDefinition gridCol2 = new ColumnDefinition();

            ColumnDefinition gridCol3 = new ColumnDefinition();

            gridTest.ColumnDefinitions.Add(gridCol1);

            gridTest.ColumnDefinitions.Add(gridCol2);

            gridTest.ColumnDefinitions.Add(gridCol3);

            Button tmp = new Button();

            tmp.Content = "Test1";
            tmp.Name = "Test1";
            tmp.Height = 25;
            tmp.Width = 90;

            gridTest.Children.Add(tmp);

            Button tmp2 = new Button();

            tmp2.Content = "Test2";
            tmp2.Name = "Test2";
            tmp2.Height = 25;
            tmp2.Width = 90;

            gridTest.Children.Add(tmp2);
            //gridTest.Children.Add(tmp);
            //gridTest.Children.Add(tmp);

            AddFinanceStatsHeader(dgChaseStats);

            for (int i = 0; i < 7; i++)
            {
                AddButtonTest(i);
            }
        }

        private void AddButtonTest(int test)
        {
            Button tmp = new Button();

            tmp.Content = "Test" + test;
            tmp.Name = "Test" + test;
            tmp.Height = 50;
            tmp.Width = 100;

            FinanceStatsDisplay financeStatsDisplay = new FinanceStatsDisplay();

            financeStatsDisplay.Category = tmp;
            financeStatsDisplay.Amount = "$500.00";
            financeStatsDisplay.Percent = "50%";

            dgChaseStats.Items.Add(financeStatsDisplay);
        }

        private void btnChase_Click(object sender, RoutedEventArgs e)
        {
            dgChase.Visibility = Visibility.Visible;

            _bankFileLoader.LoadChase();

            PopulateFinanceData(_bankFileLoader.ChaseFinanceData, dgChase);

            PopulateFinanceStats(_bankFileLoader.ChaseFinanceData, dgChaseStats);
        }

        private void btnCyprus_Click(object sender, RoutedEventArgs e)
        {
            //dgCyprus.Visibility = Visibility.Visible;
        }

        private void btnCapital_Click(object sender, RoutedEventArgs e)
        {
            //dgCapital.Visibility = Visibility.Visible;
        }

        private void PopulateFinanceData(List<FinanceData> financeData, DataGrid dataGrid)
        {
            foreach (FinanceData data in financeData)
            {
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
            }
        }

        private string FormatMoney(decimal amount)
        {
            return string.Format("{0:C}", amount);
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
            public Button Category;
            public string Amount { get; set; }
            public string Percent { get; set; }
        }

        private void PopulateFinanceStats(List<FinanceData> financeData, DataGrid dataGrid)
        {

        }

        #region FinanceHeader
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
            dgTextColumnCategory.Binding = new Binding("Category");
            dgTextColumnCategory.Width = 100;
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
