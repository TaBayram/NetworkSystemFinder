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
    public partial class ComputerBar
    #if DEBUG
        : AbstractHelper
    #else 
        : Bar
    #endif
    {
        List<Computer> computers = new List<Computer>();
        int searchType = 0;

        SortableBindingList<Computer> sortableComputers = new SortableBindingList<Computer>();
        SortableBindingList<Computer> filteredComputers = new SortableBindingList<Computer>();

        public ComputerBar(Main main)
        {
            InitializeComponent();
            Session session = Session.Instance;
            session.ChangeControlLanguage(this);
            session.theme.ColorControl(this);
            FillBoxes();

            base.Initialize(main,labelCount);
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
            sortableComputers.Clear();
            computers.Clear();
            LoadDataToGrid(sortableComputers);            
            this.tabControlLeft.SelectedIndex = 1;
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
                    pingTotal++;
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
        private void GetInformation(Computer computer)
        {
            string errorQuery = "";
            try
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();

                connectionOptions.Username = UserName;
                connectionOptions.Password = UserPassword;

                ManagementScope scope = new ManagementScope("\\\\" + computer.IP + "\\root\\cimv2", connectionOptions);
                ManagementScope scope2 = new ManagementScope("\\\\" + computer.IP + "\\root\\Microsoft\\Windows\\Storage", connectionOptions);

                scope.Connect();

                ObjectQuery queryUser = new ObjectQuery("SELECT * FROM Win32_Account");
                ObjectQuery queryCPU = new ObjectQuery("SELECT * FROM Win32_Processor");
                ObjectQuery queryGPU = new ObjectQuery("SELECT * FROM Win32_VideoController");
                ObjectQuery queryOS = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ObjectQuery queryRAM = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                ObjectQuery queryBIOS = new ObjectQuery("SELECT * FROM Win32_BIOS");
                ObjectQuery queryStorage = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
                ObjectQuery queryStorageNew = new ObjectQuery("SELECT * FROM MSFT_PhysicalDisk");
                ObjectQuery queryMAC = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                ObjectQuery queryMotherboard = new ObjectQuery("SELECT * FROM Win32_BaseBoard");

                ManagementObjectSearcher searchUser = new ManagementObjectSearcher(scope, queryUser);
                ManagementObjectSearcher searchGPU = new ManagementObjectSearcher(scope, queryGPU);
                ManagementObjectSearcher searchCPU = new ManagementObjectSearcher(scope, queryCPU);
                ManagementObjectSearcher searchRAM = new ManagementObjectSearcher(scope, queryRAM);
                ManagementObjectSearcher searchOS = new ManagementObjectSearcher(scope, queryOS);
                ManagementObjectSearcher searchBIOS = new ManagementObjectSearcher(scope, queryBIOS);
                ManagementObjectSearcher searchMAC = new ManagementObjectSearcher(scope, queryMAC);
                ManagementObjectSearcher searchMotherboard = new ManagementObjectSearcher(scope, queryMotherboard);

                ManagementObjectCollection querys = null;

                if (computer.Name == "?")
                {
                    errorQuery = "User";
                    try
                    {
                        querys = searchUser.Get();
                        foreach (ManagementObject m in querys)
                        {
                            if(m["Domain"] != null)
                                computer.Name = Convert.ToString(m["Domain"]);
                        }
                    }
                    catch (Exception e)
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                    }
                }

                errorQuery = "CPU";
                try
                {
                    querys = searchCPU.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Name"] != null)
                            computer.CPU = Convert.ToString(m["Name"]).Trim();
                    }
                }
                catch(Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "OS";
                try
                {
                    querys = searchOS.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Caption"] != null)
                            computer.OS = Convert.ToString(m["Caption"]).Trim();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "RAM";
                try
                {
                    querys = searchRAM.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Capacity"] != null)
                            computer.RAM += (int)Math.Round(Int64.Parse(m["Capacity"].ToString()) / 1024d / 1024 / 1024);
                        if (m["MemoryType"] != null && ((computer.RAMType == "Unknown" || computer.RAMType == "?" || computer.RAMType == "Undefined") && (m["MemoryType"].ToString()) != "0"))
                            computer.RAMType = GetRamType(int.Parse(m["MemoryType"].ToString()));                            
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "GPU";
                try
                {
                    querys = searchGPU.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Name"] != null)
                            computer.GPU = Convert.ToString(m["Name"]).Trim();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }
                errorQuery = "BIOS";
                try
                {
                    querys = searchBIOS.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["SerialNumber"] != null && Convert.ToString(m["SerialNumber"]).Trim() != "")
                            computer.SerialNumber = Convert.ToString(m["SerialNumber"]).Trim();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }
                
                errorQuery = "MAC";
                try
                {
                    querys = searchMAC.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if(m["MACAddress"] != null)
                            computer.MAC = m["MACAddress"].ToString();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
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
                                computer.HDD = (int)(Convert.ToUInt64(computer.HDD) + (Convert.ToUInt64(m["Size"]) / (1024 * 1024 * 1024)));
                        }
                    }
                    catch (Exception e)
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " IO" + errorQuery + " " + e.Message));
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
                                        computer.SSD = (int)(Convert.ToUInt64(computer.SSD) + (Convert.ToUInt64(m["Size"]) / (1024 * 1024 * 1024)));
                                    break;
                                default:
                                    if (m["Size"] != null)
                                        computer.HDD = (int)(Convert.ToUInt64(computer.HDD) + (Convert.ToUInt64(m["Size"]) / (1024 * 1024 * 1024)));
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " IN" + errorQuery + " " + e.Message));
                    }
                }

                errorQuery = "Motherboard";
                try
                {
                    querys = searchMotherboard.Get();
                    foreach (ManagementObject m in querys)
                    {
                        if (m["Manufacturer"] != null)
                            computer.Motherboard = m["Manufacturer"].ToString().Substring(0,Math.Min(m["Manufacturer"].ToString().Length,12)) +" : " +m["Product"].ToString();
                        if (m["SerialNumber"] != null && m["SerialNumber"].ToString().Trim() != "" && (computer.SerialNumber == "?" || computer.SerialNumber == "NONE" || computer.SerialNumber.ToLower().StartsWith("default") || computer.SerialNumber.ToLower().StartsWith("to be filled") || computer.SerialNumber.ToLower().StartsWith("system serial") || computer.SerialNumber.ToLower().StartsWith("not")))
                            computer.SerialNumber = "MB>"+m["SerialNumber"].ToString();
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }



                backgroundWorkerPinger.ReportProgress(3,computer);


            }
            catch (Exception e)
            {
                backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " "+errorQuery +" "+ e.Message));
            }

        }
        private void backgroundWorkerPinger_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                sortableComputers = new SortableBindingList<Computer>(computers);
                LoadDataToGrid(sortableComputers);
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
            while (countdown != null && !countdown.IsSet &&!countdown.Signal());
        }
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            filteredComputers = new SortableBindingList<Computer>(sortableComputers);
            foreach (Computer computer in sortableComputers)
            {
                if (base.CheckComboboxFilter(computer) || base.CheckStringFilter(computer) || base.CheckNumberFilter(computer))
                {
                    filteredComputers.Remove(computer);
                    continue;
                }
            }

            LoadDataToGrid(filteredComputers);

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

        private void ComputerBar_VisibleChanged(object sender, EventArgs e)
        {
            dataGrid.Visible = Visible;
        }
    }
}
