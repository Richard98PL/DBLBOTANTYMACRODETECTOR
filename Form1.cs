using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace skillMeDBL{

    public partial class Form1 : Form{

        private static System.Timers.Timer aTimer;

        String comboChoice = "North 🠙";
        String comboChoiceRes = "1920x1080";
        Boolean isFoodEnabled = false;
        Boolean isRepairEnabled = false;
        Boolean isTesting = false;

        public static Process proc = Process.GetProcessesByName("DBLClient").FirstOrDefault();
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_SHOWWINDOW = 0x0040;

        private const uint Restore = 9;
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int X, int Y);

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


        static Random rnd = new Random();

        public Form1() {
            InitializeComponent();
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 0;
            checkBox1.Checked = true;    
        }

       
        private void Button1_Click(object sender, EventArgs e) {
            
            if (button1.Text.Equals("START")){
                SetTimer();
                button1.Text = "STOP";
                
            }
            else{
                resetApp();
            }
        }
        static int ThreeMinutes = 60 * 3 * 1000;
        static int lagToThreeMinutes = rnd.Next(0, ThreeMinutes);
        [STAThread]
        private void antyKick() {
                
                    ActivateWindow(proc.MainWindowHandle);
                    moveInGame(proc);
                    MinimizeWindow(proc.MainWindowHandle);


            this.WindowState = FormWindowState.Minimized;


            lagToThreeMinutes = rnd.Next(0, ThreeMinutes);
            Thread.Sleep(lagToThreeMinutes);
        }

        private void moveInGame(Process proc)
        {
            Thread.Sleep(100);
            if (comboChoice.Equals("North 🠙")) antyKickDBL("^{DOWN}", "^{UP}");
            else if (comboChoice.Equals("West 🠘")) antyKickDBL(" ^{RIGHT}", "^{LEFT}");
            else if (comboChoice.Equals("East 🠚")) antyKickDBL(" ^{LEFT}", "^{RIGHT}");
            else if (comboChoice.Equals("South 🠛")) antyKickDBL(" ^{UP}", "^{DOWN}");
            MinimizeWindow(proc.MainWindowHandle);


        }

        private void SetTimer(){
            antyKick();
            Thread.Sleep(5);
            

            int aTimerTime = 60 * 10 * 1000;
            aTimer = new System.Timers.Timer(aTimerTime);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void antyKickDBL(String s1, String s2){

            int lag = rnd.Next(0, 500);

            

            Thread.Sleep(300);
            SendKeys.SendWait(s1);
            Thread.Sleep(250+lag);
            SendKeys.SendWait(s2);
            Thread.Sleep(300);
            if (isFoodEnabled) eatFood();
            Thread.Sleep(5);
            if (isRepairEnabled) repairEQ();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e){
            antyKick();
        }

        private void eatFood(){
            for(int i=0;i<25;i++) SendKeys.SendWait("{F8}");
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e){
            
            resetApp();
            comboChoice = comboBox1.GetItemText(comboBox1.SelectedItem);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) {
            isFoodEnabled = !isFoodEnabled;
            resetApp();
            
        }

        private void Clicker(int x, int y) {
            SetCursorPos(x, y);
            Thread.Sleep(50);
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X,Y, 0, 0);
            Thread.Sleep(50);
                if (isTesting) Thread.Sleep(1000);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e){
            
            isRepairEnabled = !isRepairEnabled;
            resetApp();
        }

        private void ProgressBar1_Click(object sender, EventArgs e){

        }

        private void Timer1_Tick(object sender, EventArgs e){
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e){
            
            resetApp();
            comboChoiceRes = comboBox2.GetItemText(comboBox2.SelectedItem);
        }
      
        private void Button2_Click(object sender, EventArgs e){
            
           
            
                ActivateWindow(proc.MainWindowHandle);
                isTesting = true;
                repairEQ();
                MinimizeWindow(proc.MainWindowHandle);
            
            
        }

        private void resetApp(){

            if (aTimer != null) aTimer.Stop();
            button1.Text = "START";
            timer1.Stop();
            

        }

        private void repairEQ()
        {
            if (comboChoiceRes.Equals("1920x1080"))
            {
                Clicker(1750, 146); // left hand
                Clicker(1830, 147); // right hand
                Clicker(1750, 183); // belt
                Clicker(1790, 140); // armor
                Clicker(1790, 177); // legs
                Clicker(1790, 215); // boots
            }
            else if (comboChoiceRes.Equals("1600x900"))
            {
                Clicker(1432, 148); // left hand
                Clicker(1506, 153); // right hand
                Clicker(1431, 186); // belt
                Clicker(1466, 140); // armor
                Clicker(1470, 172); // legs
                Clicker(1463, 212); // boots
            }
            else if (comboChoiceRes.Equals("1440x900"))
            {
                Clicker(1270, 148); // left hand
                Clicker(1347, 150); // right hand
                Clicker(1272, 183); // belt
                Clicker(1309, 140); // armor
                Clicker(1308, 179); // legs
                Clicker(1309, 211); // boots
            }
            else if (comboChoiceRes.Equals("1366x768"))
            {
                Clicker(1200, 150); // left hand
                Clicker(1273, 150); // right hand
                Clicker(1200, 183); // belt
                Clicker(1234, 141); // armor
                Clicker(1234, 176); // legs
                Clicker(1234, 215); // boots
            }
            isTesting = false;
        }

        public static void ActivateWindow(IntPtr mainWindowHandle)
        {
            //check if already has focus
            if (mainWindowHandle == GetForegroundWindow()) return;

            //check if window is minimized
            if (IsIconic(mainWindowHandle))
            {
                ShowWindow(mainWindowHandle, Restore);
            }

            // Simulate a key press
            keybd_event(0, 0, 0, 0);

            SetForegroundWindow(mainWindowHandle);
        }

        public static void MinimizeWindow(IntPtr mainWindowHandle)
        {
            ShowWindow(mainWindowHandle, SW_MINIMIZE);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            if (aTimer != null) aTimer.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }


}
