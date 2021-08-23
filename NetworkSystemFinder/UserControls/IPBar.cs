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
    public partial class IPBar : UserControl,ColorSetter
    {
        readonly Main main;
        Stopwatch stopWatch;
        CountdownEvent countdown;
        List<Machine> machines = new List<Machine>();
        public IPBar(Main main)
        {
            InitializeComponent();
            SetColor();
            Session.Instance.ChangeControlLanguage(this);
            this.main = main;
        }
  
        public void SetColor()
        {
            Theme theme = Session.Instance.theme;
            this.BackColor = theme.mainBackground;

            foreach (Button button in Controls.OfType<Button>())
            {
                theme.ColorButton(button);
            }
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                textBox.BackColor = theme.textBoxBackground;
                textBox.ForeColor = theme.textLine;
            }
            foreach (Label label in Controls.OfType<Label>())
            {
                label.ForeColor = theme.textLine;
            }


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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPinger.IsBusy) return;
            WriteLog("Starting...");
            main.SortableMachines.Clear();
            main.SetDataGrid();
            machines.Clear();
            //comboBoxFilterColumn.Items.Clear();
            progressBarSearch.Style = ProgressBarStyle.Marquee;
            backgroundWorkerPinger.RunWorkerAsync();
        }


        private void backgroundWorkerPinger_DoWork(object sender, DoWorkEventArgs e)
        {

            countdown = new CountdownEvent(1);
            stopWatch = new Stopwatch();
            stopWatch.Start();

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
                    string ip = ipBase + i.ToString();
                    try
                    {
                        Ping ping = new Ping();
                        ping.PingCompleted += new PingCompletedEventHandler(PingCompletion);

                        Machine machine = new Machine(ip);
                        machines.Add(machine);

                        countdown.AddCount();
                        ping.SendAsync(ip, 100, machine);
                    }
                    catch(Exception exception)
                    {
                        backgroundWorkerPinger.ReportProgress(2,(ip + " " + exception.Message));
                    }
                }
            }
            backgroundWorkerPinger.ReportProgress(1);
            if (!countdown.IsSet)
                countdown.Signal();
            countdown.Wait();
        }
        private void PingCompletion(object sender, PingCompletedEventArgs eventArgs)
        {
            try
            {
                Machine machine = (Machine)eventArgs.UserState;
                string ip = (string)machine.IP;
                if (eventArgs.Reply != null && eventArgs.Reply.Status == IPStatus.Success)
                {
                    machine.Status = Machine.StatusType.Alive;
                    if (ResolveNames)
                    {
                        try
                        {
                            IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                            machine.PcName = hostEntry.HostName;
                        }
                        catch (SocketException socketException) { }

                        backgroundWorkerPinger.ReportProgress(2, String.Format(ip + " (" + machine.PcName + ") is up: (" + eventArgs.Reply.RoundtripTime + " ms)"));
                        GetInformation(machine);
                    }
                    else
                    {
                        backgroundWorkerPinger.ReportProgress(2, String.Format(ip + " is up: (" + eventArgs.Reply.RoundtripTime + " ms)"));
                    }
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
                Console.WriteLine("ERROR-PCE:" + exception.Message);
            }
            finally
            {
                if(!countdown.IsSet)
                    countdown.Signal();
            }
        }
        private void GetInformation(Machine machine)
        {
            try
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();

                connectionOptions.Username = UserName;
                connectionOptions.Password = UserPassword;

                ManagementScope scope = new ManagementScope("\\\\" + machine.IP + "\\root\\cimv2", connectionOptions);
                scope.Connect();

                ObjectQuery queryCPU = new ObjectQuery("SELECT * FROM Win32_Processor");
                ObjectQuery queryGPU = new ObjectQuery("SELECT * FROM Win32_VideoController");
                ObjectQuery queryOS = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ObjectQuery queryBIOS = new ObjectQuery("SELECT * FROM Win32_BIOS");

                ManagementObjectSearcher searchGPU = new ManagementObjectSearcher(scope, queryGPU);
                ManagementObjectSearcher searchCPU = new ManagementObjectSearcher(scope, queryCPU);
                ManagementObjectSearcher searchOS = new ManagementObjectSearcher(scope, queryOS);
                ManagementObjectSearcher searchBIOS = new ManagementObjectSearcher(scope, queryBIOS);

                ManagementObjectCollection querys = searchCPU.Get();
                foreach (ManagementObject m in querys)
                {
                    machine.CPU = Convert.ToString(m["Name"]).Trim();
                    
                }
                querys = searchOS.Get();
                foreach (ManagementObject m in querys)
                {
                    machine.OS = Convert.ToString(m["Caption"]).Trim();
                    machine.RAM = Convert.ToString(int.Parse(m["TotalVisibleMemorySize"].ToString())/1024).Trim();

                }
                querys = searchGPU.Get();
                foreach (ManagementObject m in querys)
                {
                    machine.GPU = Convert.ToString(m["Name"]).Trim();
                }
                querys = searchBIOS.Get();
                foreach (ManagementObject m in querys)
                {
                    machine.SerialNumber = Convert.ToString(m["SerialNumber"]).Trim();
                }

                ObjectQuery queryMAC = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                ManagementObjectSearcher searchMAC = new ManagementObjectSearcher(scope, queryMAC);
                querys = searchMAC.Get();

                foreach (ManagementObject m in querys)
                {
                    machine.MAC = (m["MACAddress"].ToString());
                }

                backgroundWorkerPinger.ReportProgress(3);


            }
            catch (Exception e)
            {
                backgroundWorkerPinger.ReportProgress(2, String.Format(machine.PcName + " " + e.Message));
            }

        }
        private void backgroundWorkerPinger_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                main.SortableMachines = new SortableBindingList<Machine>(machines);
                main.SetDataGrid();
                SetFilters();
            }
            else if (e.ProgressPercentage == 2 && e.UserState != null)
            {
                WriteLog(e.UserState.ToString());
            }
            else if (e.ProgressPercentage == 3 && e.UserState.GetType() == typeof(Machine))
            {
               // main.Filter(textBoxFilter.Text.Trim(), comboBoxFilterColumn.SelectedIndex);
            }

        }
        private void backgroundWorkerPinger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarSearch.Style = ProgressBarStyle.Blocks;
            progressBarSearch.Value = 100;
            stopWatch.Stop();
            WriteLog("Ended " + stopWatch.ElapsedMilliseconds + "ms");
            for (int i = 0; i < machines.Count; i++)
            {
                if (machines[i].Status == Machine.StatusType.Dead)
                {
                    machines.RemoveAt(i);
                    i--;
                }
            }
            /*if(textBoxFilter.Text == "")
            {
                main.SortableMachines = new SortableBindingList<Machine>(machines);
                main.SetDataGrid();
            }*/
            
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

        Stack<UserControl> filterStack = new Stack<UserControl>();
        private void SetFilters()
        {
            if (filterStack.Count != 0) return;
            foreach(DataGridViewTextBoxColumn column in main.DataGridMain.Columns)
            {
                if(column.Name != "RAM")
                {
                    FilterString filterString = new FilterString();
                    filterString.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterString.Property = column.Name;
                    filterString.Index = column.Index;
                    filterString.GroupBox.Text = column.Name;
                    flowLayoutPanel1.Controls.Add(filterString);
                    filterStack.Push(filterString);
                }
                else
                {
                    FilterNumber filterNumber = new FilterNumber();
                    filterNumber.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterNumber.Property = column.Name;
                    filterNumber.Index = column.Index;
                    filterNumber.GroupBox.Text = column.Name;
                    flowLayoutPanel1.Controls.Add(filterNumber);
                    filterStack.Push(filterNumber);
                }
            
            }
        }

        private void SetFilterContents()
        {
            foreach(FilterString filterString in filterStack.OfType<FilterString>())
            {
                if(filterString.Property == "CPU")
                {
                    List<string> keys = new List<string>();
                    for(int i = 0; i < this.machines.Count; i++)
                    {
                        string[] split = this.machines[i].CPU.Split('-');
                        if (split[0].ToLower().StartsWith("intel") && !keys.Contains(split[0]))
                        {
                            keys.Add(split[0]);
                        }
                    }
                }
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            var filteredMachines = new SortableBindingList<Machine>(main.SortableMachines);
            foreach (Machine machine in main.SortableMachines)
            {
                bool hasDeleted = false;
                foreach (FilterString filterString in filterStack.OfType<FilterString>())
                {
                    if (hasDeleted) break;
                    if (filterString.Input == "") continue;
                    string[] names = filterString.Input.ToLower().Split(' ');
                    foreach(string str in names)
                    {
                        if(!machine.GetType().GetProperty(filterString.Property).GetValue(machine, null).ToString().ToLower().Contains(str))
                        {
                            filteredMachines.Remove(machine);
                            hasDeleted = true;
                            break;
                        }
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
    }
}
