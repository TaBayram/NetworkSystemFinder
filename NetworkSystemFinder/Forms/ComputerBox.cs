using NetworkSystemFinder.Helpers;
using NetworkSystemFinder.Models;
using NetworkSystemFinder.Models.Parts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace NetworkSystemFinder.Forms
{
    public partial class ComputerBox : Form
    {
        Computer computer;
        public ComputerBox(Computer computer)
        {
            this.computer = computer;
           
            InitializeComponent();
            Session.Instance.ChangeControlLanguage(this);
            Session.Instance.theme.ColorControl(this);
            this.BackColor = Session.Instance.theme.mainBackground;
            this.Text = computer.Name;

            TreeNode node = new TreeNode("Computer");
            node.Nodes.Add("IP " + computer.IP);
            node.Nodes.Add("Name "+computer.Name);
            node.Nodes.Add("Status "+computer.Status.ToString());
            node.Nodes.Add("Serial Number "+computer.OBIOS.SerialNumber);
            node.Nodes.Add("System Type " +computer.OBIOS.SystemType);
            treeView1.Nodes.Add(node);
            node = new TreeNode("OS");
            node.Nodes.Add("Model "+computer.OOS.Model);
            node.Nodes.Add("Architecture "+computer.OOS.OSArchitecture);
            node.Nodes.Add("Version "+computer.OOS.Version);
            node.Nodes.Add("User "+computer.OOS.RegisteredUser);
            treeView1.Nodes.Add(node);
            node = new TreeNode("CPU");
            node.Nodes.Add("Model "+computer.OCPU.Model);
            node.Nodes.Add("Cores "+computer.OCPU.NumberOfCores.ToString());
            node.Nodes.Add("Threads "+computer.OCPU.NumberOfThreads.ToString());
            node.Nodes.Add("Speed(Mhz) "+computer.OCPU.MaxClockSpeed.ToString());
            treeView1.Nodes.Add(node);
            node = new TreeNode("RAM");
            node.Nodes.Add("Total(GB) "+ computer.RAM.ToString());
            for(int i = 0; i < computer.RAMs.Count; i++)
            {
                RAM ram = computer.RAMs[i];
                TreeNode subNode = new TreeNode("Ram " +(i+1));
                subNode.Nodes.Add("Type " +ram.Type);
                subNode.Nodes.Add("Capacity(GB) " + ram.Capacity.ToString());
                subNode.Nodes.Add("Speed(Mhz) " +ram.Speed.ToString());
                node.Nodes.Add(subNode);
            }
            treeView1.Nodes.Add(node);
            node = new TreeNode("GPU");
            for (int i = 0; i < computer.GPUs.Count; i++)
            {
                GPU ram = computer.GPUs[i];
                TreeNode subNode = new TreeNode("GPU " + (i + 1));
                subNode.Nodes.Add("Model "+ram.Model);
                subNode.Nodes.Add("AdapterRAM(GB) " +ram.AdapterRAM.ToString());
                node.Nodes.Add(subNode);
            }
            treeView1.Nodes.Add(node);
            node = new TreeNode("Storage");
            node.Nodes.Add("SSD Capacity(GB) "+computer.SSD.ToString());
            node.Nodes.Add("HDD Capacity(GB) " + computer.HDD.ToString());
            for (int i = 0; i < computer.Storages.Count; i++)
            {
                Storage storage = computer.Storages[i];
                if(storage.Type == Storage.StorageType.SSD)
                {
                    TreeNode subNode = new TreeNode("SSD " + (i + 1));
                    subNode.Nodes.Add("ID "+storage.ID);
                    subNode.Nodes.Add("Model " +storage.Model);
                    subNode.Nodes.Add("Name " +storage.Name);
                    subNode.Nodes.Add("Capacity(GB) "+storage.Capacity.ToString());
                    node.Nodes.Add(subNode);
                }
            }
            for (int i = 0; i < computer.Storages.Count; i++)
            {
                Storage storage = computer.Storages[i];
                if (storage.Type == Storage.StorageType.HDD)
                {
                    TreeNode subNode = new TreeNode("HDD " + (i + 1));
                    subNode.Nodes.Add("ID " + storage.ID);
                    subNode.Nodes.Add("Model " + storage.Model);
                    subNode.Nodes.Add("Name " + storage.Name);
                    subNode.Nodes.Add("Capacity(GB) " + storage.Capacity.ToString());
                    node.Nodes.Add(subNode);
                }
            }
            treeView1.Nodes.Add(node);
            node = new TreeNode("Motherboard");
            node.Nodes.Add("Model " +computer.OMotherboard.Model);
            node.Nodes.Add("Manufacturer "+computer.OMotherboard.Manufacturer);
            treeView1.Nodes.Add(node);
            node = new TreeNode("Network");
            node.Nodes.Add("MAC "+ computer.ONetwork.MACAddress);
            node.Nodes.Add("ID "+computer.ONetwork.NetConnectionID);
            treeView1.Nodes.Add(node);





        }

        private void buttonOpenAll_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void buttonCloseAll_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void buttonQuerySearch_Click(object sender, EventArgs e)
        {
            ConnectionOptions connect = Session.Instance.connectionOptions;


            string output = "";
            try
            {
                ManagementScope scope = new ManagementScope("\\\\" + computer.IP + WMICHelper.PathCMIV2, connect);
                ObjectQuery queryNetwork = new ObjectQuery(textBoxQuery.Text.Trim().ToString());
                scope.Connect();
                ManagementObjectSearcher searchUser = new ManagementObjectSearcher(scope, queryNetwork);
                ManagementObjectCollection querys = searchUser.Get();

                string duplicate = "";
                bool repeated = false;
                foreach (ManagementObject mo in querys)
                {
                    if(repeated) output += "####\n";
                    repeated = false;
                    foreach (PropertyData prop in mo.Properties)
                    {
                        if (duplicate == prop.Name + prop.Value) continue;
                        duplicate = prop.Name + prop.Value;
                        output += String.Format("{0}: {1} \n", prop.Name, prop.Value);
                    }
                    repeated = true;
                }
                
            }
            catch(Exception ex)
            {
                output = ex.Message;
            }
            finally
            {
                InfoBox info = new InfoBox(output);
                info.TopMost = true;
                info.Activate();
                info.ShowDialog();
            }
        }

        //Excel
        string fileName;
        _Application excelApp;
        _Workbook workbook;
        _Worksheet worksheet;
        int rowCount = 0;

        private void buttonExcel_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel|*.xlsx|Excel Old|*.xls";
            saveFileDialog.Title = "Save File";
            saveFileDialog.FileName = "Computer "+computer.IP;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            fileName = saveFileDialog.FileName == "" ? "NetworkDevices" : saveFileDialog.FileName;

            backgroundWorkerExcel.RunWorkerAsync(fileName);
        }
        private void backgroundWorkerExcel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            double split = 0.1;
            string fileName = e.Argument.ToString();
           
            // creating Excel Application  
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbooks workbooks = excelApp.Workbooks;

            workbook = workbooks.Add(Type.Missing);
            worksheet = null;
            // see the excel sheet behind the program  
            excelApp.Visible = false;
            excelApp.ScreenUpdating = false;
            excelApp.DisplayAlerts = false;

            // get the reference of first sheet. By default its name is Sheet1.  
            worksheet = workbook.Sheets[1];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Computer";

            ExcelTree(treeView1.Nodes,1);
            
            // save the application  
            excelApp.ScreenUpdating = true;
            workbook.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            Marshal.ReleaseComObject(workbooks);
            Marshal.ReleaseComObject(worksheet);
            workbooks = null;
            worksheet = null;

        }

        public void ExcelTree(TreeNodeCollection treeNode,int col)
        {
            int i = 0;
            foreach (TreeNode collection in treeNode)
            {
                i++;
                rowCount = rowCount + 1;
                worksheet.Cells[rowCount, col].Value = collection.Text;
                if (collection.FirstNode != null)
                    ExcelTree(collection.Nodes, col + 1);
                if(col == 1)
                {
                    backgroundWorkerExcel.ReportProgress((int)(i*100 / treeNode.Count));
                }
            }
        }

        private void backgroundWorkerExcel_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBarExcel.Value = e.ProgressPercentage;
        }
        private void backgroundWorkerExcel_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Session session = Session.Instance;
            if (e.Error != null)
            {
                MessageBox.Show(session.TryTranslate("keyError") + ": " + progressBarExcel.Value + " - " + e.Error.Message, session.TryTranslate("keyFailed"), MessageBoxButtons.OK);
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
    }
}
