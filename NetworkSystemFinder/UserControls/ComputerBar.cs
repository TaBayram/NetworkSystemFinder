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
    public partial class ComputerBar: UserControl, Bar
    {
        readonly Main main;
        Stopwatch stopWatch;
        CountdownEvent countdown;
        List<Computer> computers = new List<Computer>();
        Stack<UserControl> filterStack = new Stack<UserControl>();
        int searchType = 0;
        int pingTotal = 0;
        int pingCurrent = 0;
        public ComputerBar(Main main)
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
            set { labelCount.Text = value + " "+Session.Instance.resourceManager.GetString("keyRows"); }
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
            main.SortableComputers.Clear();
            main.SetDataGrid();
            computers.Clear();
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
            if(searchType == 0)
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
            else if(searchType == 1)
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
                Computer machine = (Computer)eventArgs.UserState;
                string ip = (string)machine.IP.Trim();
                if (eventArgs.Reply != null && eventArgs.Reply.Status == IPStatus.Success)
                {
                    IPHostEntry hostEntry;
                    machine.Status = Computer.StatusType.Alive;
                    if (ResolveNames)
                    {
                        try
                        {
                            hostEntry = Dns.GetHostEntry(ip);
                            machine.Name = hostEntry.HostName;
                        }
                        catch (SocketException socketException) { }

                        backgroundWorkerPinger.ReportProgress(2, String.Format(ip + " (" + machine.Name + ") is up: (" + eventArgs.Reply.RoundtripTime + " ms)"));
                    }
                    else
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(ip + " is up: (" + eventArgs.Reply.RoundtripTime + " ms)"));
                    }
                    machine.SetIP();
                    MacPairer macPairer = new MacPairer();
                    machine.MAC = macPairer.getMacByIp(machine.IP);
                    GetInformation(machine);
                }
                else if (eventArgs.Reply == null)
                {

                }
                else if (eventArgs.Reply.Status == IPStatus.TimedOut)
                {

                }
            }
            catch (Exception exception)
            {
                backgroundWorkerPinger.ReportProgress(2,String.Format("ERROR-PCE:" + exception.Message));
            }
            finally
            {
                if(!countdown.IsSet)
                    countdown.Signal();
                backgroundWorkerPinger.ReportProgress(4);

            }
        }
        private void GetInformation(Computer machine)
        {
            string errorQuery = "";
            try
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();

                connectionOptions.Username = UserName;
                connectionOptions.Password = UserPassword;

                ManagementScope scope = new ManagementScope("\\\\" + machine.IP + "\\root\\cimv2", connectionOptions);
                ManagementScope scope2 = new ManagementScope("\\\\" + machine.IP + "\\root\\Microsoft\\Windows\\Storage", connectionOptions);

                scope.Connect();

                ObjectQuery queryUser = new ObjectQuery("SELECT * FROM Win32_Account");
                ObjectQuery queryCPU = new ObjectQuery("SELECT * FROM Win32_Processor");
                ObjectQuery queryGPU = new ObjectQuery("SELECT * FROM Win32_VideoController");
                ObjectQuery queryOS = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ObjectQuery queryBIOS = new ObjectQuery("SELECT * FROM Win32_BIOS");
                ObjectQuery queryStorage = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
                ObjectQuery queryStorageNew = new ObjectQuery("SELECT * FROM MSFT_PhysicalDisk");
                ObjectQuery queryMAC = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                ObjectQuery queryMotherboard = new ObjectQuery("SELECT * FROM Win32_BaseBoard");

                ManagementObjectSearcher searchUser = new ManagementObjectSearcher(scope, queryUser);
                ManagementObjectSearcher searchGPU = new ManagementObjectSearcher(scope, queryGPU);
                ManagementObjectSearcher searchCPU = new ManagementObjectSearcher(scope, queryCPU);
                ManagementObjectSearcher searchOS = new ManagementObjectSearcher(scope, queryOS);
                ManagementObjectSearcher searchBIOS = new ManagementObjectSearcher(scope, queryBIOS);
                ManagementObjectSearcher searchMAC = new ManagementObjectSearcher(scope, queryMAC);
                ManagementObjectSearcher searchMotherboard = new ManagementObjectSearcher(scope, queryMotherboard);

                ManagementObjectCollection querys = null;

                if (machine.Name == "?")
                {
                    errorQuery = "User";
                    try
                    {
                        querys = searchUser.Get();
                        foreach (ManagementObject m in querys)
                        {
                            if(m["Domain"] != null)
                                machine.Name = Convert.ToString(m["Domain"]);
                        }
                    }
                    catch (Exception e)
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                    }
                }

                errorQuery = "CPU";
                try
                {
                    querys = searchCPU.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Name"] != null)
                            machine.CPU = Convert.ToString(m["Name"]).Trim();
                    }
                }
                catch(Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "OS";
                try
                {
                    querys = searchOS.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Caption"] != null)
                            machine.OS = Convert.ToString(m["Caption"]).Trim();
                        if (m["TotalVisibleMemorySize"] != null)
                            machine.RAM = Convert.ToString(int.Parse(m["TotalVisibleMemorySize"].ToString())/1024).Trim();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "GPU";
                try
                {
                    querys = searchGPU.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Name"] != null)
                            machine.GPU = Convert.ToString(m["Name"]).Trim();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                }
                errorQuery = "BIOS";
                try
                {
                    querys = searchBIOS.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["SerialNumber"] != null && Convert.ToString(m["SerialNumber"]).Trim() != "")
                            machine.SerialNumber = Convert.ToString(m["SerialNumber"]).Trim();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                }
                
                errorQuery = "MAC";
                try
                {
                    querys = searchMAC.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if(m["MACAddress"] != null)
                            machine.MAC = m["MACAddress"].ToString();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                }
                
                errorQuery = "Storage";
                bool isNew = true;
                ManagementObjectSearcher searchStorage;
                try
                {
                    scope2.Connect();
                    searchStorage = new ManagementObjectSearcher(scope2, queryStorageNew);
                    querys = searchStorage.Get();
                }
                catch(Exception e)
                {
                    isNew = false;
                }

                if(!isNew)
                {
                    try
                    {
                        searchStorage = new ManagementObjectSearcher(scope, queryStorage);
                        querys = searchStorage.Get();
                        foreach (ManagementObject m in querys)
                        {
                            if (m["Size"] != null)
                                machine.HDD = (Convert.ToUInt64(machine.HDD) + (Convert.ToUInt64(m["Size"]) / (1024 * 1024 * 1024))).ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " IO" + errorQuery + " " + e.Message));
                    }
            
                }
                else
                {
                    try
                    {
                        foreach (ManagementObject m in querys)
                        {
                            if (m["MediaType"] == null || m["BusType"] == null || Convert.ToUInt16(m["BusType"]) == 7) continue;
                            switch (Convert.ToInt16(m["MediaType"]))
                            {
                                case 4:
                                    if (m["Size"] != null)
                                        machine.SSD = (Convert.ToUInt64(machine.SSD) + (Convert.ToUInt64(m["Size"]) / (1024 * 1024 * 1024))).ToString();
                                    break;
                                default:
                                    if (m["Size"] != null)
                                        machine.HDD = (Convert.ToUInt64(machine.HDD) + (Convert.ToUInt64(m["Size"]) / (1024 * 1024 * 1024))).ToString();
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " IN" + errorQuery + " " + e.Message));
                    }
                }

                errorQuery = "Motherboard";
                try
                {
                    querys = searchMotherboard.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Manufacturer"] != null)
                            machine.Motherboard = m["Manufacturer"].ToString().Substring(0,Math.Min(m["Manufacturer"].ToString().Length,12)) +" : " +m["Product"].ToString();
                        if (m["SerialNumber"] != null && m["SerialNumber"].ToString().Trim() != "" && (machine.SerialNumber == "?" || machine.SerialNumber == "NONE" || machine.SerialNumber.ToLower().StartsWith("default") || machine.SerialNumber.ToLower().StartsWith("to be filled") || machine.SerialNumber.ToLower().StartsWith("system serial") || machine.SerialNumber.ToLower().StartsWith("not")))
                            machine.SerialNumber = "MB>"+m["SerialNumber"].ToString();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " I" + errorQuery + " " + e.Message));
                }



                backgroundWorkerPinger.ReportProgress(3,machine);


            }
            catch (Exception e)
            {
                backgroundWorkerPinger.ReportProgress(2, String.Format(machine.Name + " "+errorQuery +" "+ e.Message));
            }

        }
        private void backgroundWorkerPinger_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                main.SortableComputers = new SortableBindingList<Computer>(computers);
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
                    if(filterString.Property == "CPU")
                    {
                        filterString.AddItem(((Computer)e.UserState).SplitName()[0]);
                    }
                }
                // main.Filter(textBoxFilter.Text.Trim(), comboBoxFilterColumn.SelectedIndex);
            }
            else if(e.ProgressPercentage == 4)
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
            if(main.Logger != null)
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

            while (countdown != null && !countdown.IsSet &&!countdown.Signal());
        }
        private void SetFilters()
        {
            if (filterStack.Count != 0) return;
            foreach(DataGridViewTextBoxColumn column in main.DataGridMain.Columns)
            {
                if(column.Name == "Status")
                {
                    FilterCombobox filterCombobox = new FilterCombobox();
                    filterCombobox.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterCombobox.Property = column.Name;
                    filterCombobox.Index = column.Index;
                    filterCombobox.GroupBox.Text = column.Name;
                    flowLayoutPanelFilter.Controls.Add(filterCombobox);
                    filterStack.Push(filterCombobox);
                }
                else if(column.Name != "RAM" && column.Name != "HDD" && column.Name != "SSD")
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

                    if(value < filterNumber.InputMin || value > filterNumber.InputMax)
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
                if(col.Name == property)
                {
                    exists = true;
                    column = col;
                }
            }
            
            if (e.NewValue == CheckState.Unchecked && exists)
            {
                main.DataGridMain.Columns.Remove(column);
            }
            else if(e.NewValue == CheckState.Checked && !exists)
            {
                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = property;
                column.Name = property;
                main.DataGridMain.Columns.Add(column);
                main.DataGridMain.Columns[property].DisplayIndex = Math.Min(e.Index, main.DataGridMain.Columns.Count-1);
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
