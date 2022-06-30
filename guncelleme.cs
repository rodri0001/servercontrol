using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestHost___Server
{
    public partial class guncelleme : Form
    {
        public guncelleme()
        {
            InitializeComponent();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            string dosya_dizini = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resthost-server.exe";
            if (File.Exists(dosya_dizini) == true) // dizindeki dosya var mı ?
            {
                System.IO.File.Delete((System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resthost-server.exe"));

            }
            WebClient webClient = new WebClient(); // kullanılacak Sınız
            webClient.DownloadFile("https://devrimsoftreklamonayi.ml/resthost-server.zip", System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resthost-server.exe");
            Process.Start(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resthost-server.exe");
            var command = $"/c timeout 2 > NUL & del \"{Process.GetCurrentProcess().MainModule.FileName}\"";
            Process.Start(new ProcessStartInfo("cmd.exe", command) { CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden });
            Environment.Exit(0);
        }
    }
}
