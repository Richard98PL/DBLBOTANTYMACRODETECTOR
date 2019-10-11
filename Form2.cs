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
    public partial class Form2 : Form
    {
        
        public static System.Timers.Timer aTimer;
        public static System.Timers.Timer aTimer2;
        static int czas = 0;
        public static Boolean isFoodEnabled = false;
        static String comboChoice = "North 🠙";
        static Boolean isPowerDown = false;

        static String comboChoiceRes = "1920x1080";

        public static Process[] processes = Process.GetProcessesByName("DBLClient");
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        
        static Random random = new Random();

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_SHOWWINDOW = 0x0040;

        private const uint Restore = 9;
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        static Punkt[][] dblTablica = new Punkt[11][];
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 0;

            for (int i = 0; i < 11; i++)
            {
                dblTablica[i] = new Punkt[15];
            }

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    dblTablica[i][j] = new Punkt(300 + 80 * j, 100 + 80 * i);
                }
            }

            checkBox1.Checked = true;
            checkBox2.Checked = true;


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Beep(500, 50);
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Beep(500, 50);
            comboChoiceRes = comboBox2.GetItemText(comboBox2.SelectedItem);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 stoperLowienia = new Form3(this);
            stoperLowienia.Show();
            this.WindowState = FormWindowState.Minimized;
            Beep(500, 50);
        }

        public static void SetTimer()
        {
            //PowerDown();
            Thread.Sleep(5);
            FishMeDBL();
            
            //Thread.Sleep(5);
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            if (comboChoice.Equals("North 🠙")) antyKickDBL("^{DOWN}", "^{UP}");
            else if (comboChoice.Equals("West 🠘")) antyKickDBL(" ^{RIGHT}", "^{LEFT}");
            else if (comboChoice.Equals("East 🠚")) antyKickDBL(" ^{LEFT}", "^{RIGHT}");
            else if (comboChoice.Equals("South 🠛")) antyKickDBL(" ^{UP}", "^{DOWN}");

            

        }

        public static void SetTimer2()
        {
            if (isPowerDown)
            {
                int z = random.Next(0, 500);
                aTimer2 = new System.Timers.Timer(1700 + z);
                aTimer2.Elapsed += OnTimedEvent2;
                aTimer2.AutoReset = false;
                aTimer2.Enabled = true;
            }
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            FishMeDBL();
        }
        private static void OnTimedEvent2(Object source, ElapsedEventArgs e)
        {
            PowerDown();
        }

        private static void antyKickDBL(String s1, String s2)
        {
            SendKeys.SendWait(s1);
            Thread.Sleep(50);
            SendKeys.SendWait(s2);
            Thread.Sleep(5);
            if (isFoodEnabled) eatFood();
            Thread.Sleep(5);
        }

        private static void eatFood()
        {
            for (int i = 0; i < 20; i++) SendKeys.SendWait("{F8}");
        }

        static int ktoraFasola = 2;
        private static void PowerDown()
        {
                    foreach (Process proc in processes)
                    {
                         ktoraFasola++;
                        SetForegroundWindow(proc.MainWindowHandle);
                        Thread.Sleep(300);
                if (ktoraFasola == 3)
                {
                    SendKeys.SendWait("{F6}");
                    ktoraFasola = 0;
                }
                        Thread.Sleep(5);    
                        SendKeys.SendWait("{F7}");
                        Thread.Sleep(10);
                         if(aTimer2!=null) aTimer2.Enabled = false;
                         if (aTimer2 != null) aTimer2 = null;   
                        SetTimer2();
                        
                    }
        }

        private static void FishMeDBL()
        {


            

            czas += 2;
            if (czas == 600)
            {
                czas = 0;
                if (comboChoice.Equals("North 🠙")) antyKickDBL("^{DOWN}", "^{UP}");
                else if (comboChoice.Equals("West 🠘")) antyKickDBL(" ^{RIGHT}", "^{LEFT}");
                else if (comboChoice.Equals("East 🠚")) antyKickDBL(" ^{LEFT}", "^{RIGHT}");
                else if (comboChoice.Equals("South 🠛")) antyKickDBL(" ^{UP}", "^{DOWN}");

            }
           

            
            if (comboChoiceRes.Equals("1920x1080"))
            {
                foreach(Process proc in processes)
                {
                    SetForegroundWindow(proc.MainWindowHandle);

                    int x = random.Next(-3, 4);
                    int y = random.Next(2, 6);
                    Punkt kordy = dblTablica[5 + y][7 + x];

                    Fisher(kordy.getX(), kordy.getY());
                    
                }
                
            }
            else if (comboChoiceRes.Equals("1600x900"))
            {
                Fisher(1432, 148); // left hand
                Fisher(1506, 153); // right hand
                Fisher(1431, 186); // belt
                Fisher(1466, 140); // armor
                Fisher(1470, 172); // legs
                Fisher(1463, 212); // boots
            }
            else if (comboChoiceRes.Equals("1440x900"))
            {
                Fisher(1270, 148); // left hand
                Fisher(1347, 150); // right hand
                Fisher(1272, 183); // belt
                Fisher(1309, 140); // armor
                Fisher(1308, 179); // legs
                Fisher(1309, 211); // boots
            }
            else if (comboChoiceRes.Equals("1366x768"))
            {
                Fisher(1200, 150); // left hand
                Fisher(1273, 150); // right hand
                Fisher(1200, 183); // belt
                Fisher(1234, 141); // armor
                Fisher(1234, 176); // legs
                Fisher(1234, 215); // boots
            }
            
        }

        private static void Fisher(int x, int y)
        {
           
            int lag = random.Next(1, 400);
            Thread.Sleep(lag);
            Point mycha = new Point(Cursor.Position.X, Cursor.Position.Y);
            SetCursorPos(x,y);
            Thread.Sleep(1);
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            SendKeys.SendWait("{F9}");
            Thread.Sleep(1);
            foreach (Process proc in processes)
            {
                SetForegroundWindow(proc.MainWindowHandle);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            }
           
            Thread.Sleep(1);
            SetCursorPos(mycha.X,mycha.Y);
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            foreach (Process proc in processes)
            {
                SetForegroundWindow(proc.MainWindowHandle);
                SetTimer();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Beep(1000, 5);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (aTimer != null) aTimer.Stop();
            Beep(500, 50);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            isFoodEnabled = !isFoodEnabled;
            Beep(500, 50);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboChoice = comboBox1.GetItemText(comboBox1.SelectedItem);
        }

        public class Punkt
        {

            private int x;
            private int y;

            public Punkt(int _x, int _y)
            {
                this.x = _x;
                this.y = _y;
            }

            public void setX(int _x)
            {
                this.x = _x;
            }

            public void setY(int _y)
            {
                this.y = _y;
            }

            public int getX()
            {
                return this.x;
            }

            public int getY()
            {
                return this.y;
            }


        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            isPowerDown = !isPowerDown;
        }
    }
}
