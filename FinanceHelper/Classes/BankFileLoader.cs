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

        private bool _chaseLoaded;

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

                ResetChaseInformationLoaded();

                bool fileDataLoaded = LoadFileData(fileName, Banks.Chase);

                PopulateFinanceData();

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

        private void ResetChaseInformationLoaded()
        {
            _chaseLoaded = false;
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
                            double excellPostDate = double.Parse(columnValue);
                            DateTime postDate = DateTime.FromOADate(excellPostDate);

                            financeData.PostDate = postDate;
                            break;
                        case 4: // Description
                            financeData.Description = columnValue;
                            break;
                        case 5: // Amount
                            decimal excellAmount;
                            Decimal.TryParse(columnValue, out excellAmount);

                            financeData.Amount = excellAmount;
                            break;
                    }

                    whichColumn++;
                }

                financeData.SetCategory();
                _chaseFinanceData.AddFinanceDataItem(financeData);
            }
        }

        private void PopulateFinanceData()
        {

        }

        #endregion Chase
    }
}
