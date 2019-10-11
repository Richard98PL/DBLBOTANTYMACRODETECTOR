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
    public partial class Form5 : Form
    {

        static Random random = new Random();

        public static System.Timers.Timer aTimer;

        public static Process[] processes = Process.GetProcessesByName("DBLClient");
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        
        public Form5()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Beep(500, 50);

            if (button1.Text == "EAT!")
            {
                button1.Text = "STOP!";
                eatSenzu();
                SetTimer();
            }
            else
            {
                button1.Text = "EAT!";
                if(aTimer!=null)aTimer.Stop();
            }
        }
        private static void eatSenzu()
        {
            foreach (Process proc in processes)
            {
                
                
                int lag = random.Next(1, 400);
                Thread.Sleep(lag);
                SetForegroundWindow(proc.MainWindowHandle);
                SendKeys.SendWait("{F7}");
            }
        }

        public static void SetTimer()
        {
            
            Thread.Sleep(5);
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            

        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            eatSenzu();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (aTimer != null) aTimer.Stop();
        }
    }
}
