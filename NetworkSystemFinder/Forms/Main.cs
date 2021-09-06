using System;
using System.Windows.Forms;
using NetworkSystemFinder.Models;
using NetworkSystemFinder.Helpers;
using NetworkSystemFinder.UserControls;
using System.Linq;
using NetworkSystemFinder.Forms;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace NetworkSystemFinder
{
    public partial class Main : Form
    {
        //Forms
        Logger logger;
        Settings settings;
        // User Controls
        Stack<Control> leftBarStack = new Stack<Control>();
        Control currentBarControl;
        ComputerBar computerBar;
        PrinterBar printerBar;
        //Excel
        string fileName;
        Microsoft.Office.Interop.Excel._Application excelApp;
        Microsoft.Office.Interop.Excel._Workbook workbook;


        public Main()
        {
            InitializeComponent();
            PushLeftBar(panelMainButtons);
            Session.Instance.ChangeControlLanguage(this);
            Session.Instance.theme.ColorControl(this);
        }
        public Logger Logger { get => logger; }
        public DataGridView DataGridMain { get => dataViewMain; }
        public Panel RightPanel { get => panelHome; }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            PopLeftBar();
        }
        private void PushLeftBar(Control control)
        {
            leftBarStack.Push(control);
            HideCurrentShowThis(control);
            buttonBack.Visible = leftBarStack.Count > 1;
        }
        private Control PopLeftBar()
        {
            Control control = leftBarStack.Pop();
            buttonBack.Visible = leftBarStack.Count > 1;
            HideCurrentShowThis(leftBarStack.Peek());
            return control;
        }
        private void HideCurrentShowThis(Control control)
        {
            if (currentBarControl != null)
            {
                currentBarControl.Visible = false;
            }
            currentBarControl = control;
            currentBarControl.Visible = true;
        }
        private void buttonLANDevices_Click(object sender, EventArgs e)
        {
            if(computerBar == null)
            {
                computerBar = new ComputerBar(this);
                computerBar.Parent = panelLeftBar;
                computerBar.Dock = DockStyle.Fill;
            }
            PushLeftBar(computerBar);
        }
        private void buttonPrinters_Click(object sender, EventArgs e)
        {
            if (printerBar == null)
            {
                printerBar = new PrinterBar(this);
                printerBar.Parent = panelLeftBar;
                printerBar.Dock = DockStyle.Fill;
            }
            PushLeftBar(printerBar);
        }
        public void ToggleLog()
        {
            if(Logger == null)
            {
                logger = new Logger();
                logger.TopMost = true;
                logger.Activate();
                logger.Show();
            }
            else
            {
                logger.Close();
                logger = null;
            }
        }
        private void buttonExcel_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel|*.xlsx|Excel Old|*.xls";
            saveFileDialog.Title = "Save File";
            saveFileDialog.FileName = "NetworkDevices";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            fileName = saveFileDialog.FileName == "" ? "NetworkDevices" : saveFileDialog.FileName;

            backgroundWorkerExcel.RunWorkerAsync(fileName);
        }
        private void backgroundWorkerExcel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            double split = 0.1;
            string fileName = e.Argument.ToString();
            DataTable dataTable = new DataTable();
            foreach (DataGridViewColumn col in dataViewMain.Columns)
            {
                dataTable.Columns.Add(col.Name);
            }

            for (int i = 0; i < dataViewMain.Rows.Count; i++)
            {
                DataRow dRow = dataTable.NewRow();
                foreach (DataGridViewCell cell in dataViewMain.Rows[i].Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dataTable.Rows.Add(dRow);
                backgroundWorkerExcel.ReportProgress((int)((float)i / (dataViewMain.Rows.Count-1) * 100 * split));
            }

            // creating Excel Application  
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = excelApp.Workbooks;

            workbook = workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            excelApp.Visible = false;
            excelApp.ScreenUpdating = false;
            excelApp.DisplayAlerts = false;

            // get the reference of first sheet. By default its name is Sheet1.  
            worksheet = workbook.Sheets[1];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Network Devices";

            // storing header part in Excel  
            for (int i = 1; i < dataTable.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataTable.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i].ItemArray[j].ToString();
                }
                backgroundWorkerExcel.ReportProgress((int)(split*100)+ (int)((float)i / (dataTable.Rows.Count-1) * 100 * (1-split)));
            }
            // save the application  
            excelApp.ScreenUpdating = true;
            workbook.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            Marshal.ReleaseComObject(workbooks);
            Marshal.ReleaseComObject(worksheet);
            workbooks = null;
            worksheet = null;

        }
        private void backgroundWorkerExcel_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBarExcel.Value = e.ProgressPercentage;
        }
        private void backgroundWorkerExcel_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Session session = Session.Instance;
            if(e.Error != null)
            {
                MessageBox.Show(session.TryTranslate("keyError")+": "+progressBarExcel.Value+" - "+e.Error.Message, session.TryTranslate("keyFailed"), MessageBoxButtons.OK);
            }
            else if (MessageBox.Show(session.TryTranslate("keyExcelPositive"), session.TryTranslate("keyCompleted"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                excelApp.Visible = true;
            }
            else
            {
                workbook.Close(0);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
                Marshal.ReleaseComObject(workbook);
                workbook = null;
                excelApp = null;
            }

            progressBarExcel.Value = 0;
        }
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (settings == null || settings.IsDisposed)
            {
                settings = new Settings();
                settings.TopMost = true;
                settings.FormClosed += Settings_FormClosed;
                settings.ShowDialog();
 
            }
        }
        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            settings = null;
            //Session.Instance.ChangeControlLanguage(this);
            Session.Instance.theme.ColorControl(this);
        }
    }


}

