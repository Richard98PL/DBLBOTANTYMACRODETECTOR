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

   

    public partial class Form4 : Form
    {
       

        
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        public Form4()
        {
            InitializeComponent();

            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Beep(500, 50);
            Form2 lowienie = new Form2();
            lowienie.Show();
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Beep(500, 50);
            Form1 form = new Form1();
            form.Show();
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Beep(500, 50);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Beep(500, 50);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Beep(500, 50);
            Form5 foremka = new Form5();
            foremka.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            Form6 digger = new Form6();
            digger.Show();
        }
    }

    
}
