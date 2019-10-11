using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace skillMeDBL
{


    public partial class Form3 : Form
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        Form fishMeDBL;
        public Form3(Form _fishMeDBL)
        {
            InitializeComponent();
            this.fishMeDBL = _fishMeDBL;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            fishMeDBL.Close();
            this.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
           if(Form2.aTimer != null) Form2.aTimer.Enabled = false;
            Beep(500, 50);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Beep(500, 50);
                if (button1.Text == "START")
                {
                    button1.Text = "PAUSE";
                    Form2.SetTimer();
                    Form2.SetTimer2();
                }
                else
                {
                    button1.Text = "START";
                    if (Form2.aTimer.Enabled == true) Form2.aTimer.Enabled = false;
                    if (Form2.aTimer2.Enabled == true) Form2.aTimer2.Enabled = false;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
