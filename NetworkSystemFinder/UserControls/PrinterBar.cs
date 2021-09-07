using NetworkSystemFinder.Helpers;
using NetworkSystemFinder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkSystemFinder.UserControls
{
    public partial class PrinterBar
    #if DEBUG
            : AbstractHelper
    #else
            : Bar
    #endif
    {
        SortableBindingList<Printer> sortablePrinters = new SortableBindingList<Printer>();
        SortableBindingList<Printer> filteredPrinters = new SortableBindingList<Printer>();

        List<Computer> computers = new List<Computer>();
        List<Printer> printers = new List<Printer>();

        int searchType = 0;

        public PrinterBar(Main main)
        {
            InitializeComponent();
            Session session = Session.Instance;
            session.ChangeControlLanguage(this);
            session.theme.ColorControl(this);
            FillBoxes();

            base.Initialize(main,rowCount);
        }

        public string IPStart
        {
            get { return textBoxIPStart.Text.Trim(); }
        }
        public string IPEnd
        {
            get { return textBoxIPEnd.Text.Trim(); }
        }
        public string UserName
        {
            get { return textBoxUser.Text.Trim(); }
        }
        public string UserPassword
        {
            get { return textBoxPassword.Text.Trim(); }
        }
        public bool ResolveNames
        {
            get { return checkBoxResolveNames.Checked; }
        }

        public void AddPrinter(Printer printer)
        {
            bool alreadyExists = false;
            foreach(Printer print in printers)
            {
                if(printer.IP == print.IP)
                {
                    alreadyExists = true;
                    break;
                }
            }
            if (!alreadyExists)
            {
                printers.Add(printer);
            }
        }
        public void FillBoxes()
        {
            Session session = Session.Instance;
            textBoxIPStart.Text = session.remIPStart;
            textBoxIPEnd.Text = session.remIPEnd;
            textBoxUser.Text = session.remUser;
            textBoxPassword.Text = session.remPassword;
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPinger.IsBusy) return;
            searchType = 0;
            WriteLog("Starting...");
            sortablePrinters.Clear();
            LoadDataToGrid(sortablePrinters);
            computers.Clear();
            printers.Clear();
            this.tabControlLeft.SelectedIndex = 1;
            // progressBarSearch.Style = ProgressBarStyle.Marquee;
            backgroundWorkerPinger.RunWorkerAsync();

        }
        private void buttonPingDead_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPinger.IsBusy) return;
            searchType = 1;
            WriteLog("Starting Revival...");
            backgroundWorkerPinger.RunWorkerAsync();
        }
        private void backgroundWorkerPinger_DoWork(object sender, DoWorkEventArgs e)
        {
            countdown = new CountdownEvent(1);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            pingTotal = 0;
            pingCurrent = 0;
            if (searchType == 0)
            {
                if (textBoxMachineName.Text.Trim() == "")
                {
                    base.SplitIP(IPStart, out string[] ipStart, out int[] ipStartInt);
                    base.SplitIP(IPEnd, out string[] ipEnd, out int[] ipEndInt);

                    int fourthIPEnd = ipEndInt[3] == 0 ? 255 : ipEndInt[3];
                    int fourthIPStart = ipStartInt[3] == 0 ? 1 : ipStartInt[3];

                    string ipMain = ipStartInt[0] + "." + ipStartInt[1] + ".";
                    for (int j = ipStartInt[2]; j <= ipEndInt[2]; j++)
                    {
                        string ipBase = ipMain + j.ToString() + ".";
                        for (int i = fourthIPStart; i <= fourthIPEnd; i++)
                        {
                            pingTotal++;
                            string ip = ipBase + i.ToString();
                            try
                            {
                                PingMachine(ip);
                            }
                            catch (Exception exception)
                            {
                                backgroundWorkerPinger.ReportProgress(2, (ip + " " + exception.Message));
                            }
                        }
                    }
                }
                else
                {
                    PingMachine(textBoxMachineName.Text.Trim());
                }
                backgroundWorkerPinger.ReportProgress(1);
            }
            else if (searchType == 1)
            {
                try
                {
                    foreach (Computer computer in computers)
                    {
                        if (computer.Status == Machine.StatusType.Dead)
                        {
                            try
                            {
                                pingTotal++;
                                Ping ping = new Ping();
                                ping.PingCompleted += new PingCompletedEventHandler(PingCompletion);
                                countdown.AddCount();
                                ping.SendAsync(computer.IP, 100, computer);
                            }
                            catch (Exception exception)
                            {
                                backgroundWorkerPinger.ReportProgress(2, (computer.IP + " " + exception.Message));
                            }
                        }

                    }
                }
                catch (Exception exception)
                {
                    backgroundWorkerPinger.ReportProgress(2, ("Search 1 " + exception.Message));
                }
            }


            if (!countdown.IsSet)
                countdown.Signal();
            countdown.Wait();
        }
        private void PingMachine(string id)
        {
            Ping ping = new Ping();
            ping.PingCompleted += new PingCompletedEventHandler(PingCompletion);

            Computer machine = new Computer(id);
            computers.Add(machine);

            countdown.AddCount();
            ping.SendAsync(id, 100, machine);
        }
        private void PingCompletion(object sender, PingCompletedEventArgs eventArgs)
        {
            try
            {
                Computer computer = (Computer)eventArgs.UserState;
                if (eventArgs.Reply != null && eventArgs.Reply.Status == IPStatus.Success)
                {
                    GetInformation(computer);
                }
                else if (eventArgs.Reply == null || eventArgs.Reply.Status == IPStatus.TimedOut)
                {

                }
            }
            catch (Exception exception)
            {
                backgroundWorkerPinger.ReportProgress(2, String.Format("ERROR-PCE:" + exception.Message));
            }
            finally
            {
                if (!countdown.IsSet)
                    countdown.Signal();
                backgroundWorkerPinger.ReportProgress(4);

            }
        }
        private void GetInformation(Computer computer)
        {
            string errorQuery = "";
            try
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();

                connectionOptions.Username = UserName;
                connectionOptions.Password = UserPassword;

                ManagementScope scope = new ManagementScope("\\\\" + computer.IP + WMICHelper.PathCMIV2, connectionOptions);
                scope.Connect();

                ManagementObjectSearcher searchIP = new ManagementObjectSearcher(scope, WMICHelper.QueryTCPIPPrinter);
                ManagementObjectSearcher searchPrinter = new ManagementObjectSearcher(scope, WMICHelper.QueryPrinter);

                ManagementObjectCollection querys = null;
                List<Printer> printers = new List<Printer>();


                errorQuery = "TCPIP";
                try
                {
                    querys = searchIP.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["HostAddress"] == null) continue;
                        Printer printer = new Printer(m["HostAddress"].ToString());
                        if (m["Name"] != null)
                            printer.Name = m["Name"].ToString();
                        if (m["PortNumber"] != null)
                            printer.MAC = m["PortNumber"].ToString();

                        if (m["Caption"] != null)
                            printer.Caption = "C " + (m["Caption"].ToString());
                        if (m["Description"] != null)
                            printer.Caption += "|D|" + m["Description"].ToString();
                        if (m["SystemName"] != null)
                            printer.Caption += "|S|" + m["SystemName"].ToString();

                        printers.Add(printer);
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "Printer";
                try
                {
                    querys = searchPrinter.Get();
                    foreach (ManagementObject m in querys)
                    { 
                        if (m["DeviceID"] == null) continue;
                        Printer printer = new Printer(m["DeviceID"].ToString());
                        if (m["PNPDeviceID"] != null)
                            printer.SerialNumber = m["PNPDeviceID"].ToString();
                       //if (m["InstallDate"] != null && (DateTime)m["InstallDate"] != null)
                            //printer.InstallDate = (DateTime)m["InstallDate"];
                        if (m["ServerName"] != null)
                            printer.ServerName = m["ServerName"].ToString();

                        if(m["Caption"] != null)
                            printer.Caption2 = "C "+(m["Caption"].ToString());
                        if (m["Comment"] != null)
                            printer.Caption2 += "|C|" + m["Comment"].ToString();
                        if (m["Location"] != null)
                            printer.Caption2 += "|L|" + m["Location"].ToString();
                        if (m["DriverName"] != null)
                            printer.Caption2 += "|R|" + m["DriverName"].ToString();
                        if (m["Name"] != null)
                            printer.Caption2 += "|N|" + m["Name"].ToString();
                        if (m["SystemName"] != null)
                            printer.Caption2 += "|S|" + m["SystemName"].ToString();
                        if (m["ShareName"] != null)
                            printer.Caption2 += "|Sh|" + m["ShareName"].ToString();

                        printers.Add(printer);

                        /*foreach (Printer print in printers)
                        {
                            if (print.Caption == m["Caption"].ToString() || print.Name == m["Name"].ToString())
                            {
                                if (m["DeviceID"] != null)
                                    print.SerialNumber = m["DeviceID"].ToString();
                                if (m["InstallDate"] != null)
                                    print.InstallDate = (DateTime)m["InstallDate"];
                                if (m["ServerName"] != null)
                                    print.ServerName = m["ServerName"].ToString();
                                //print.Caption2 = (m["Caption"].ToString()) + "|||" + m["Comment"].ToString() + "|||" + m["DeviceID"].ToString() + "|||" + m["DriverName"].ToString() + "|||" + m["Name"].ToString() + "|||" + m["SystemName"].ToString();
                            }
                        }*/
                    }


                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                foreach(Printer printer1 in printers)
                {
                    AddPrinter(printer1);
                }
                backgroundWorkerPinger.ReportProgress(1);

                //backgroundWorkerPinger.ReportProgress(3, computer);

            }
            catch (Exception e)
            {
                backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " " + errorQuery + " " + e.Message));
            }

        }
        private void backgroundWorkerPinger_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                sortablePrinters = new SortableBindingList<Printer>(printers);
                LoadDataToGrid(sortablePrinters);
                SetFilters(flowLayoutPanelFilter,checkedListBoxColumns);
            }
            else if (e.ProgressPercentage == 2 && e.UserState != null)
            {
                WriteLog(e.UserState.ToString());
            }
            else if (e.ProgressPercentage == 3 && e.UserState != null && e.UserState.GetType() == typeof(Computer))
            {
                foreach (FilterString filterString in filterStack.OfType<FilterString>())
                {
                    if (filterString.Property == "CPU")
                    {
                        filterString.AddItem(((Computer)e.UserState).SplitName()[0]);
                    }
                }
                // Filter(textBoxFilter.Text.Trim(), comboBoxFilterColumn.SelectedIndex);
            }
            else if (e.ProgressPercentage == 4)
            {
                pingCurrent++;
                progressBarSearch.Value = (int)(pingCurrent * 100 / pingTotal);
                sortablePrinters = new SortableBindingList<Printer>(printers);
                LoadDataToGrid(sortablePrinters);
            }

        }
        private void backgroundWorkerPinger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarSearch.Style = ProgressBarStyle.Blocks;
            progressBarSearch.Value = 100;
            dataGrid.Refresh();
            stopWatch.Stop();
            WriteLog("Ended " + stopWatch.ElapsedMilliseconds + "ms");
        }
        private void buttonLog_Click(object sender, EventArgs e)
        {
            main.ToggleLog();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            backgroundWorkerPinger.CancelAsync();

            while (countdown != null && !countdown.IsSet && !countdown.Signal()) ;
        }
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            filteredPrinters = new SortableBindingList<Printer>(sortablePrinters);
            foreach (Printer printer in sortablePrinters)
            {
                if (base.CheckComboboxFilter(printer) || base.CheckStringFilter(printer) || base.CheckNumberFilter(printer))
                {
                    filteredPrinters.Remove(printer);
                    continue;
                }
            }

            LoadDataToGrid(filteredPrinters);
        }
        private void checkedListBoxColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            base.ToggleColumn(checkedListBoxColumns, e);
        }

        private void checkedListBoxColumns_MouseEnter(object sender, EventArgs e)
        {
            checkedListBoxColumns.Size = new Size(0, 90) + checkedListBoxColumns.Size;
        }

        private void checkedListBoxColumns_MouseLeave(object sender, EventArgs e)
        {
            timerMouseControl.Enabled = true;
        }

        private void timerMouseControl_Tick(object sender, EventArgs e)
        {
            Point pos = checkedListBoxColumns.PointToClient(Cursor.Position);
            if (!checkedListBoxColumns.DisplayRectangle.Contains(pos))
            {
                timerMouseControl.Enabled = false;
                checkedListBoxColumns.Size = new Size(0, -90) + checkedListBoxColumns.Size;
            }
        }
    }
}
