using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestHost___Araçlar
{
    public partial class load : Form
    {
        public load()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2ProgressBar1.Value += 2;
            if (guna2ProgressBar1.Value == 100)
            {
                timer2.Start();
                timer1.Stop();
                timer1.Enabled = false;
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            menu f = new menu();
            f.Show();
            timer2.Stop();
            timer2.Enabled = false;
            this.Hide();
        }
    }
}
