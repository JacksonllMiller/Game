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

        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use the left and right arrow keys to move the spaceship. " +
                "\n Don't get hit by the planets! " +
                "\n Every planet that gets past scores a point." +
                "\n If a planet hits a spaceship a life is lost! " +
                "\n \n Enter your Name press tab and enter the number of lives. " +
                "\n Click Start to begin", "Game Instructions");
            txtName.Focus();

        }
    }
}
