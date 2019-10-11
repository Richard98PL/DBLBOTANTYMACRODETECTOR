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
    
    public partial class Form6 : Form
    {

        static Random random = new Random();
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;


        public static System.Timers.Timer aTimer;

        public static Process[] processes = Process.GetProcessesByName("DBLClient");
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        public Form6()
        {
            Beep(500, 50);
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Beep(500, 50);

            if (button1.Text == "DIG ABOVE ME!")
            {
                button1.Text = "STOP!";
                digAboveMe();
                SetTimer();
            }
            else
            {
                button1.Text = "DIG ABOVE ME!";
                if (aTimer != null) aTimer.Stop();
            }
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Beep(500, 50);
            if (aTimer != null) aTimer.Stop();
        }
        private static void digAboveMe()
        {
            foreach (Process proc in processes)
            {

                
                int lag = random.Next(1, 200);
                Thread.Sleep(lag);
                SetForegroundWindow(proc.MainWindowHandle);
                Digger(860, 423);

            }
        }

        public static void Digger(int x, int y)
        {
            Random random = new Random();
            int lag = random.Next(1, 100);
            Thread.Sleep(lag);
            Point mycha = new Point(Cursor.Position.X, Cursor.Position.Y);
            SetCursorPos(x, y);
            Thread.Sleep(1);
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            SendKeys.SendWait("{F10}");
            Thread.Sleep(1);
            foreach (Process proc in processes)
            {
                SetForegroundWindow(proc.MainWindowHandle);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            }

            Thread.Sleep(1);
            SetCursorPos(mycha.X, mycha.Y);
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
            digAboveMe();
        }

    }
}
