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
    public partial class PrinterBar: UserControl, Bar
    {
        readonly Main main;
        Stopwatch stopWatch;
        CountdownEvent countdown;
        List<Computer> computers = new List<Computer>();
        List<Printer> printers = new List<Printer>();
        Stack<UserControl> filterStack = new Stack<UserControl>();
        int searchType = 0;
        int pingTotal = 0;
        int pingCurrent = 0;
        public PrinterBar(Main main)
        {
            InitializeComponent();
            this.main = main;
            Session session = Session.Instance;
            session.ChangeControlLanguage(this);
            session.theme.ColorControl(this);
            FillBoxes();
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
        public int RowCount
        {
            set { labelCount.Text = value + " " + Session.Instance.resourceManager.GetString("keyRows"); }
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
            main.SortablePrinters.Clear();
            main.SetDataGrid();
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
                    string[] ipStart = IPStart.Split('.');
                    string[] ipEnd = IPEnd.Split('.');
                    int[] ipStartInt = new int[4];
                    int[] ipEndInt = new int[4];
                    for (int i = 0; i < ipStart.Length; i++) int.TryParse(ipStart[i], out ipStartInt[i]);
                    for (int i = 0; i < ipEnd.Length; i++) int.TryParse(ipEnd[i], out ipEndInt[i]);

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

                ManagementScope scope = new ManagementScope("\\\\" + computer.IP + "\\root\\cimv2", connectionOptions);
                scope.Connect();

                ObjectQuery queryTCPIP = new ObjectQuery("SELECT * FROM Win32_TCPIPPrinterPort");
                ObjectQuery queryPrinter = new ObjectQuery("SELECT * FROM Win32_Printer");

                ManagementObjectSearcher searchIP = new ManagementObjectSearcher(scope, queryTCPIP);
                ManagementObjectSearcher searchPrinter = new ManagementObjectSearcher(scope, queryPrinter);

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
                        if (m["Caption"] != null)
                            printer.SetCaption(m["Caption"].ToString());
                        if (m["Name"] != null)
                            printer.Name = m["Name"].ToString();
                        if (m["PortNumber"] != null)
                            printer.MAC = m["PortNumber"].ToString();
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
                        foreach (Printer print in printers)
                        {
                            if (print.GetCaption() == m["Caption"].ToString() || print.Name == m["Name"].ToString())
                            {
                                if (m["DeviceID"] != null)
                                    print.SerialNumber = m["DeviceID"].ToString();
                                if (m["InstallDate"] != null)
                                    print.InstallDate = (DateTime)m["InstallDate"];
                                if (m["ServerName"] != null)
                                    print.ServerName = m["ServerName"].ToString();
                            }
                        }
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
                main.SortablePrinters = new SortableBindingList<Printer>(printers);
                main.SetDataGrid();
                SetFilters();
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
                // main.Filter(textBoxFilter.Text.Trim(), comboBoxFilterColumn.SelectedIndex);
            }
            else if (e.ProgressPercentage == 4)
            {
                pingCurrent++;
                progressBarSearch.Value = (int)(pingCurrent * 100 / pingTotal);
            }

        }
        private void backgroundWorkerPinger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarSearch.Style = ProgressBarStyle.Blocks;
            progressBarSearch.Value = 100;
            main.DataGridMain.Refresh();
            stopWatch.Stop();
            WriteLog("Ended " + stopWatch.ElapsedMilliseconds + "ms");
        }
        private void WriteLog(string text)
        {
            if (main.Logger != null)
            {
                main.Logger.Log = text;
            }
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
        private void SetFilters()
        {
            if (filterStack.Count != 0) return;
            foreach (DataGridViewTextBoxColumn column in main.DataGridMain.Columns)
            {
                if (column.Name == "Status")
                {
                    FilterCombobox filterCombobox = new FilterCombobox();
                    filterCombobox.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterCombobox.Property = column.Name;
                    filterCombobox.Index = column.Index;
                    filterCombobox.GroupBox.Text = column.Name;
                    flowLayoutPanelFilter.Controls.Add(filterCombobox);
                    filterStack.Push(filterCombobox);
                }
                else if (column.Name != "RAM" && column.Name != "HDD" && column.Name != "SSD")
                {
                    FilterString filterString = new FilterString();
                    filterString.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterString.Property = column.Name;
                    filterString.Index = column.Index;
                    filterString.GroupBox.Text = column.Name;
                    flowLayoutPanelFilter.Controls.Add(filterString);
                    filterStack.Push(filterString);
                }
                else
                {
                    FilterNumber filterNumber = new FilterNumber();
                    filterNumber.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterNumber.Property = column.Name;
                    filterNumber.Index = column.Index;
                    filterNumber.GroupBox.Text = column.Name;
                    flowLayoutPanelFilter.Controls.Add(filterNumber);
                    filterStack.Push(filterNumber);
                }

            }

            foreach (DataGridViewTextBoxColumn column in main.DataGridMain.Columns)
            {
                checkedListBoxColumns.Items.Add(column.Name);
                checkedListBoxColumns.SetItemChecked(checkedListBoxColumns.Items.Count - 1, true);
            }
        }
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            var filteredMachines = new SortableBindingList<Computer>(main.SortableComputers);
            foreach (Computer machine in main.SortableComputers)
            {
                bool hasDeleted = false;
                foreach (FilterCombobox filterCombobox in filterStack.OfType<FilterCombobox>())
                {
                    if (hasDeleted) break;
                    if (filterCombobox.SelectedItem == "" || filterCombobox.SelectedItem == "ALL") continue;
                    if (machine.GetType().GetProperty(filterCombobox.Property).GetValue(machine, null).ToString() != filterCombobox.SelectedItem)
                    {
                        filteredMachines.Remove(machine);
                        hasDeleted = true;
                        break;
                    }

                }
                if (hasDeleted) continue;
                foreach (FilterString filterString in filterStack.OfType<FilterString>())
                {
                    if (hasDeleted) break;
                    List<string> checkedList = filterString.CheckedList;
                    if (filterString.Input == "" && checkedList.Count == filterString.ListCount) continue;
                    string value = machine.GetType().GetProperty(filterString.Property).GetValue(machine, null).ToString().ToLower();

                    string[] names = filterString.Input.ToLower().Split(' ');
                    bool inputDelete = false;
                    foreach (string str in names)
                    {
                        if (!value.Contains(str))
                        {
                            inputDelete = true;
                            break;
                        }
                    }

                    bool itemDelete = true;
                    if (checkedList.Count != filterString.ListCount)
                    {
                        foreach (string item in checkedList)
                        {
                            if (value.StartsWith(item.ToLower()))
                            {
                                itemDelete = false;
                                break;
                            }
                        }
                    }
                    if ((filterString.Input != "" && inputDelete) || (checkedList.Count != filterString.ListCount && itemDelete))
                    {
                        filteredMachines.Remove(machine);
                        hasDeleted = true;
                    }
                }
                if (hasDeleted) continue;
                foreach (FilterNumber filterNumber in filterStack.OfType<FilterNumber>())
                {
                    if (hasDeleted) break;
                    if (filterNumber.InputMax == int.MaxValue && filterNumber.InputMin == 0) continue;
                    int value = 0;
                    bool hasParsed = int.TryParse(machine.GetType().GetProperty(filterNumber.Property).GetValue(machine, null).ToString().Trim(), out value);
                    if (!hasParsed) continue;

                    if (value < filterNumber.InputMin || value > filterNumber.InputMax)
                    {
                        filteredMachines.Remove(machine);
                        hasDeleted = true;
                        break;
                    }
                }
            }

            main.Filter(filteredMachines);

        }
        private void checkedListBoxColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool exists = false;
            string property = checkedListBoxColumns.Items[e.Index].ToString();
            DataGridViewTextBoxColumn column = null;
            foreach (DataGridViewTextBoxColumn col in main.DataGridMain.Columns)
            {
                if (col.Name == property)
                {
                    exists = true;
                    column = col;
                }
            }

            if (e.NewValue == CheckState.Unchecked && exists)
            {
                main.DataGridMain.Columns.Remove(column);
            }
            else if (e.NewValue == CheckState.Checked && !exists)
            {
                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = property;
                column.Name = property;
                main.DataGridMain.Columns.Add(column);
                main.DataGridMain.Columns[property].DisplayIndex = Math.Min(e.Index, main.DataGridMain.Columns.Count - 1);
            }

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
