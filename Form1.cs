using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | 
                BindingFlags.Instance | BindingFlags.NonPublic, null, pnlGame, new object[] { true });
        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Use your specific keys to move your paddle. " +
    "\n Blue Team: |Up Arrow, Down arrow| |Numpad Arrow Up + Down| " +
    "\n Red Team: |W Key, S Key| |H Key, N Key| " +
    "\n Don't let the ball reach your end. " +
    "\n Every ball that gets passed is a point." +
    "\n Keep and eye on the time as the pace might start to pick up. " +
    "\n \n Enter your respective team names. " +
    "\n Click Start to begin", "Game Instructions");
            txtName.Focus();

        }

        private void mnuStart_Click(object sender, EventArgs e)
        {

        }
    }
}
