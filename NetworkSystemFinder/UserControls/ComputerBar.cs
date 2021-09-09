using NetworkSystemFinder.Helpers;
using NetworkSystemFinder.Models;
using NetworkSystemFinder.Models.Parts;
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
                Computer computer = (Computer)eventArgs.UserState;
                string ip = (string)computer.IP.Trim();
                if (eventArgs.Reply != null && eventArgs.Reply.Status == IPStatus.Success)
                {
                    IPHostEntry hostEntry;
                    computer.Status = Computer.StatusType.Alive;
                    if (ResolveNames)
                    {
                        try
                        {
                            hostEntry = Dns.GetHostEntry(ip);
                            computer.Name = hostEntry.HostName;
                        }
                        catch (SocketException socketException) { }

                        backgroundWorkerPinger.ReportProgress(2, String.Format(ip + " (" + computer.Name + ") is up: (" + eventArgs.Reply.RoundtripTime + " ms)"));
                    }
                    else
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(ip + " is up: (" + eventArgs.Reply.RoundtripTime + " ms)"));
                    }
                    computer.SetIP();
                    MacPairer macPairer = new MacPairer();
                    computer.MAC = macPairer.getMacByIp(computer.IP);
                    GetInformation(computer);
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
                Session.Instance.connectionOptions = connectionOptions;

                ManagementScope scope = new ManagementScope("\\\\" + computer.IP + WMICHelper.PathCMIV2, connectionOptions);
                ManagementScope scope2 = new ManagementScope("\\\\" + computer.IP + WMICHelper.PathSTORAGE, connectionOptions);
                scope.Connect();

                ManagementObjectSearcher searchUser = new ManagementObjectSearcher(scope, WMICHelper.QueryAccount);
                ManagementObjectSearcher searchGPU = new ManagementObjectSearcher(scope, WMICHelper.QueryGPU);
                ManagementObjectSearcher searchCPU = new ManagementObjectSearcher(scope, WMICHelper.QueryCPU);
                ManagementObjectSearcher searchRAM = new ManagementObjectSearcher(scope, WMICHelper.QueryRAM);
                ManagementObjectSearcher searchOS = new ManagementObjectSearcher(scope, WMICHelper.QueryOS);
                ManagementObjectSearcher searchBIOS = new ManagementObjectSearcher(scope, WMICHelper.QueryBIOS);
                ManagementObjectSearcher searchMAC = new ManagementObjectSearcher(scope, WMICHelper.QueryNetwork);
                ManagementObjectSearcher searchMotherboard = new ManagementObjectSearcher(scope, WMICHelper.QueryMotherboard);
                ManagementObjectSearcher searchComputerSystem = new ManagementObjectSearcher(scope, WMICHelper.QueryComputerSystem);

                ManagementObjectCollection querys = null;

                errorQuery = "User";
                try
                {
                    Account mainAccount = new Account();
                    querys = searchUser.Get();
                    foreach (ManagementObject m in querys)
                    {
                        Account account = new Account();
                        account.GetInformation(m);
                        if(!account.Disabled && account.Status == "OK" && account.PasswordRequired)
                        {
                            mainAccount = account;
                            break;
                        }
                    }

                    computer.OAccount = mainAccount;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }
                

                errorQuery = "CPU";
                try
                {
                    CPU mainCPU = new CPU();
                    querys = searchCPU.Get();
                    foreach (ManagementObject m in querys)
                    {
                        CPU cPU = new CPU();
                        cPU.GetInformation(m);
                        mainCPU = cPU;
                        break;
                    }
                    computer.OCPU = mainCPU;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "OS";
                try
                {
                    OS mainOS = new OS();
                    querys = searchOS.Get();
                    foreach (ManagementObject m in querys)
                    {
                        OS oS = new OS();
                        oS.GetInformation(m);
                        mainOS = oS;
                        break;
                    }
                    computer.OOS = mainOS;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "RAM";
                try
                {
                    List<RAM> rams = new List<RAM>();
                    querys = searchRAM.Get();
                    foreach (ManagementObject m in querys)
                    {
                        RAM ram = new RAM();
                        ram.GetInformation(m);
                        rams.Add(ram);
                    }
                    computer.RAMs = rams;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "GPU";
                try
                {
                    List<GPU> gPUs = new List<GPU>();
                    querys = searchGPU.Get();
                    foreach (ManagementObject m in querys)
                    {
                        GPU gpu = new GPU();
                        gpu.GetInformation(m);
                        gPUs.Add(gpu);
                    }
                    computer.GPUs = gPUs;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }
                errorQuery = "BIOS";
                try
                {
                    BIOS mainBIOS = new BIOS();
                    querys = searchBIOS.Get();
                    foreach (ManagementObject m in querys)
                    {
                        BIOS bIOS = new BIOS();
                        bIOS.GetInformation(m);
                        mainBIOS = bIOS;
                        break;
                    }
                    computer.OBIOS = mainBIOS;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }
                errorQuery = "Computer System";
                try
                {
                    querys = searchComputerSystem.Get();
                    foreach (ManagementObject m in querys)
                    {
                        computer.OBIOS.GetSystemInformation(m);
                        break;
                    }
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }

                errorQuery = "MAC";
                try
                {
                    Network mainNetwork = new Network();
                    querys = searchMAC.Get();
                    foreach (ManagementObject m in querys)
                    {
                        Network network = new Network();
                        network.GetInformation(m);
                        if(network.NetEnabled && network.PhysicalAdapter)
                        {
                            mainNetwork = network;
                            break;
                        }
                    }
                    computer.ONetwork = mainNetwork;
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
                    searchStorage = new ManagementObjectSearcher(scope2, WMICHelper.QueryStorageNew);
                    querys = searchStorage.Get();
                }
                catch (Exception e)
                {
                    isNew = false;
                }

                if (!isNew)
                {
                    try
                    {
                        searchStorage = new ManagementObjectSearcher(scope, WMICHelper.QueryStorage);
                        querys = searchStorage.Get();
                        foreach (ManagementObject m in querys)
                        {
                            Storage storage = new Storage();
                            storage.GetInformation(m, false);
                            computer.Storages.Add(storage);
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
                            Storage storage = new Storage();
                            storage.GetInformation(m, true);
                            computer.Storages.Add(storage);
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
                    Motherboard mainMotherboard = new Motherboard();
                    querys = searchMotherboard.Get();
                    foreach (ManagementObject m in querys)
                    {
                        Motherboard motherboard = new Motherboard();
                        motherboard.GetInformation(m);
                        mainMotherboard = motherboard;
                        break;
                    }
                    computer.OMotherboard = mainMotherboard;
                }
                catch (Exception e)
                {
                    backgroundWorkerPinger.ReportProgress(2, String.Format(computer.Name + " I" + errorQuery + " " + e.Message));
                }



                backgroundWorkerPinger.ReportProgress(3, computer);


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
                    else if(filterString.Property == "OS")
                    {
                        filterString.AddItem(((Computer)e.UserState).OS);
                    }
                    else if (filterString.Property == "RAMType")
                    {
                        filterString.AddItem(((Computer)e.UserState).RAMType);
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
