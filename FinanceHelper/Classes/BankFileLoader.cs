using System;
using System.Collections.Generic;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinanceHelper.Classes
{
    internal class BankFileLoader
    {
        internal FinanceData ChaseFinanceData
        {
            get
            {
                return _chaseFinanceData;
            }
        }

        private FinanceData _cyprusFinanceData = new FinanceData();

        internal FinanceData CyprusFinanceData
        {
            get
            {
                return _cyprusFinanceData;
            }
        }

        private FinanceData _capitalFinanceData = new FinanceData();

        internal FinanceData CapitalFinanceData
        {
            get
            {
                return _capitalFinanceData;
            }
        }

        private FinanceData _chaseFinanceData = new FinanceData();

        internal enum Banks
        {
            Capital,
            Chase,
            Cyprus
        }

        internal BankFileLoader()
        {

        }

        private bool LoadFileData(string fileName, Banks bank)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workBook = excelApp.Workbooks.Open(fileName);

                foreach (Excel.Worksheet workSheet in workBook.Worksheets)
                {
                    switch(bank)
                    {
                        case Banks.Capital:
                            break;
                        case Banks.Chase:
                            PopulateChaseData(workSheet);
                            break;
                        case Banks.Cyprus:
                            PopulateCyprusData(workSheet);
                            break;
                    }
                }

                excelApp.Quit();

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        #region Chase
        internal void LoadChase()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "CSV files (*.csv)|*.csv"
            };

            bool? result = fileDialog.ShowDialog();

            if (result != null && (bool)result)
            {
                string fileName = fileDialog.FileName;

                DisplayWaitCursor();

                bool fileDataLoaded = LoadFileData(fileName, Banks.Chase);

                PopulateFinanceData();

                DisplayRegularCursor();
            }
        }

        internal void LoadCyprus()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "CSV files (*.csv)|*.csv"
            };

            bool? result = fileDialog.ShowDialog();

            if (result != null && (bool)result)
            {
                string fileName = fileDialog.FileName;

                DisplayWaitCursor();

                bool fileDataLoaded = LoadFileData(fileName, Banks.Cyprus);

                //PopulateFinanceData();

                DisplayRegularCursor();
            }
        }

        private void DisplayWaitCursor()
        {
            Mouse.OverrideCursor = Cursors.Wait;
        }

        private void DisplayRegularCursor()
        {
            Mouse.OverrideCursor = null;
        }

        private void PopulateChaseData(Excel.Worksheet workSheet)
        {
            bool readData = false;

            foreach (Excel.Range row in workSheet.UsedRange.Rows)
            {
                int whichColumn = 1;

                if (!readData) // Skipping the first row which is just the column headers
                {
                    readData = true;
                    continue;
                }

                FinanceData.FinanceDataItem financeData = new FinanceData.FinanceDataItem();

                foreach (Excel.Range column in row.Columns)
                {
                    string columnValue = (column.Value2 != null) ? column.Value2.ToString() : String.Empty;

                    switch (whichColumn)
                    {
                        case 1: // Type
                            financeData.Type = EnumHelper.ParseEnum<FinanceType>(columnValue); ;
                            break;
                        case 2: // Trans Date
                            double excellTransDate = double.Parse(columnValue);
                            DateTime transDate = DateTime.FromOADate(excellTransDate);

                            financeData.TransDate = transDate;
                            break;
                        case 3: // Post Date
                            SetPostDate(columnValue, financeData);
                            break;
                        case 4: // Description
                            SetDescription(columnValue, financeData);
                            break;
                        case 5: // Amount
                            SetAmount(columnValue, financeData);
                            break;
                    }

                    whichColumn++;
                }

                financeData.SetCategory();
                _chaseFinanceData.AddFinanceDataItem(financeData);
            }
        }

        private void SetPostDate(string columnValue, FinanceData.FinanceDataItem financeData)
        {
            double excellTransDate = double.Parse(columnValue);
            DateTime postDate = DateTime.FromOADate(excellTransDate);

            financeData.PostDate = postDate;
        }

        private void SetDescription(string columnValue, FinanceData.FinanceDataItem financeData)
        {
            financeData.Description = columnValue;
        }

        private void SetAmount(string columnValue, FinanceData.FinanceDataItem financeData)
        {
            decimal excellAmount;
            Decimal.TryParse(columnValue, out excellAmount);

            financeData.Amount = excellAmount;
        }

        private void PopulateCyprusData(Excel.Worksheet workSheet)
        {
            bool readData = false;

            foreach (Excel.Range row in workSheet.UsedRange.Rows)
            {
                int whichColumn = 1;

                if (!readData) // Skipping the first row which is just the column headers
                {
                    readData = true;
                    continue;
                }

                FinanceData.FinanceDataItem financeData = new FinanceData.FinanceDataItem();

                foreach (Excel.Range column in row.Columns)
                {
                    string columnValue = (column.Value2 != null) ? column.Value2.ToString() : String.Empty;

                    switch (whichColumn)
                    {
                        case 1: //Account Number
                            break;
                        case 2: //Post Date
                            SetPostDate(columnValue, financeData);
                            break;
                        case 3: //Check Number
                            break;
                        case 4: // Description
                            SetDescription(columnValue, financeData);
                            break;
                        case 5: // Debit
                            if (columnValue.Trim() != String.Empty)
                                SetAmount("-" + columnValue, financeData);
                            break;
                        case 6: // Credit
                            if (columnValue.Trim() != String.Empty)
                                SetAmount(columnValue, financeData);
                            break;
                        case 7: // Status
                            break;
                        case 8: // Balance
                            break;
                    }

                    whichColumn++;
                }

                financeData.SetCategory();
                _cyprusFinanceData.AddFinanceDataItem(financeData);
            }
        }

        private void PopulateFinanceData()
        {

        }

        #endregion Chase
    }
}
