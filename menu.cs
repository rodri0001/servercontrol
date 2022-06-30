using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RestHost___Araçlar
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
      
        Ping p = new Ping();
        PingReply r;
        string s;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                guna2TextBox6.Text += string.Format("{0}", queryObj["Name"]);
            }
            ManagementObjectSearcher Search = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");

            foreach (ManagementObject Mobject in Search.Get())
            {
                double Ram_Bytes = (Convert.ToDouble(Mobject["TotalPhysicalMemory"]));
                double ramgb = Ram_Bytes / 1073741824;
                double islem = Math.Ceiling(ramgb);
                guna2TextBox4.Text = islem.ToString() + " GB";
            }
            ManagementObjectSearcher ekran = new ManagementObjectSearcher("Select * From Win32_VideoController");

            foreach (ManagementObject Mobject in ekran.Get())
            {

                guna2TextBox7.Text = Mobject["name"].ToString() + " " + Mobject["AdapterRam"].ToString();
            }
            guna2TextBox5.Text = System.Environment.OSVersion.ToString();
            String macadress = string.Empty;
            string mac = null;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                OperationalStatus ot = nic.OperationalStatus;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macadress = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            for (int i = 0; i <= macadress.Length - 1; i++)
            {
                mac = mac + ":" + macadress.Substring(i, 2);

                i++;
            }
            mac = mac.Remove(0, 1);
            guna2TextBox9.Text = mac;
            timer1.Start();
            ra.Stop();
            tra.Stop();
            guna2Button1.ShadowDecoration.Enabled = true;
            guna2Button2.ShadowDecoration.Enabled = false;
            _sistem.Visible = true;
            _network.Visible = false;
     
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            WebClient web_client = new WebClient();
            StreamReader sr = new StreamReader(web_client.OpenRead("https://icanhazip.com/"));

            guna2TextBox2.Text = sr.ReadToEnd();
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            guna2TextBox1.Text = localIP.ToString();
            ra.Start();
            tra.Start();
            guna2Button1.ShadowDecoration.Enabled = false;
            guna2Button2.ShadowDecoration.Enabled = true;
            _sistem.Visible = false;
            _network.Visible = true;
    
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            ra.Stop();
            tra.Stop();
            guna2Button1.ShadowDecoration.Enabled = false;
            guna2Button2.ShadowDecoration.Enabled = false;
            _sistem.Visible = false;
            _network.Visible = false;
   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            j = Convert.ToInt32(myCounter.NextValue());
            //Console.WriteLine(j);
            guna2CircleProgressBar2.Value = j;
            ManagementObjectSearcher searcher =
                  new ManagementObjectSearcher("root\\CIMV2",
                  "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                guna2TextBox6.Text += string.Format("{0}", queryObj["Name"]);
            }
            ManagementObjectSearcher Search = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");

            foreach (ManagementObject Mobject in Search.Get())
            {
                double Ram_Bytes = (Convert.ToDouble(Mobject["TotalPhysicalMemory"]));
                double ramgb = Ram_Bytes / 1073741824;
                double islem = Math.Ceiling(ramgb);
                guna2TextBox4.Text = islem.ToString() + " GB";
            }
            ManagementObjectSearcher ekran = new ManagementObjectSearcher("Select * From Win32_VideoController");

            foreach (ManagementObject Mobject in ekran.Get())
            {

                guna2TextBox7.Text = Mobject["name"].ToString() + " " + Mobject["AdapterRam"].ToString();
            }
            guna2TextBox5.Text = System.Environment.OSVersion.ToString();
            String macadress = string.Empty;
            string mac = null;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                OperationalStatus ot = nic.OperationalStatus;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macadress = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            for (int i = 0; i <= macadress.Length - 1; i++)
            {
                mac = mac + ":" + macadress.Substring(i, 2);

                i++;
            }
            mac = mac.Remove(0, 1);
            guna2TextBox9.Text = mac;

            timer1.Start();
            guna2Button1.ShadowDecoration.Enabled = true;
            _sistem.Visible = true;
            _network.Visible = false;
    
        }

        public static IPAddress GetDefaultGateway()
        {
            try
            {
                return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null)
                // .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                // .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Modem arayüz adresi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                acs.RunWorkerAsync();
            }
            catch
            {

            }

        }

       
        void kontrol()
        {
            double[] speeds = new double[5];
            for (int i = 0; i < 5; i++)
            {
                int jQueryFileSize = 261; //Size of File in KB.
                WebClient client = new WebClient();
                DateTime startTime = DateTime.Now;
                client.DownloadFile("http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.js", @"C:\Windows\Temp\~");
                DateTime endTime = DateTime.Now;
                speeds[i] = Math.Round((jQueryFileSize / (endTime - startTime).TotalSeconds));
            }
            guna2CircleProgressBar3.Value = Convert.ToInt32(speeds.Average());
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            cacak();
        }


        void cacak()
        {

            s = GetDefaultGateway().ToString();
            r = p.Send(s);

            if (r.Status == IPStatus.Success)
            {
                guna2CircleProgressBar1.Value = Convert.ToInt32(r.RoundtripTime);
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            kontrol();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                bacs.RunWorkerAsync();
            }
            catch 
            {

      
            }

        }
        void calstr()
        {
            float cpu = performanceCounter1.NextValue();

           guna2CircleProgressBar4.Value = Convert.ToInt16(cpu);

            float ram = performanceCounter2.NextValue();
            guna2CircleProgressBar5.Value = Convert.ToInt16(ram);
        }
        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {

        }
        public PerformanceCounter myCounter =
           new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        public Int32 j = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            j = Convert.ToInt32(myCounter.NextValue());
            //Console.WriteLine(j);
            guna2CircleProgressBar2.Value = j;
            float cpu = performanceCounter1.NextValue();

            guna2CircleProgressBar4.Value = Convert.ToInt16(cpu);

            float ram = performanceCounter2.NextValue();
            guna2CircleProgressBar5.Value = Convert.ToInt16(ram);
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }

