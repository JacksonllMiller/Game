using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Game
{
    public partial class Form1 : Form
    {
        bool isLUpPressed, isLDownPressed, isRUpPressed, isRDownPressed;
        string playerName;
        string playerName1;
        Bitmap btm;

        Graphics g;
        Graphics SCG;

        Thread th;

        Rectangle ball = Rectangle.Empty;
        Rectangle LSide = Rectangle.Empty;
        Rectangle RSide = Rectangle.Empty;

        int ball_speed = 7;
        int ball_speedY = 7;
        int move_speed = 7;

        Point moveTo = Point.Empty;
        Point ballMove = Point.Empty;

        bool drawing = true;



        public Form1()
        {
            InitializeComponent();
            Data.Save("Hello", "test");
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

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                isLUpPressed = false;
            if (e.KeyCode == Keys.S)
                isLDownPressed = false;
            if (e.KeyCode == Keys.Up)
                isRUpPressed = false;
            if (e.KeyCode == Keys.Down)
                isRDownPressed = false;
        }

        private void mainloop_Tick(object sender, EventArgs e)
        {

        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            playerName = txtName.Text;


            if (Regex.IsMatch(playerName, @"^[a-zA-Z]+$"))//checks playerName for letters
            {
                //if playerName valid (only letters) 
                MessageBox.Show("Red Team is Ready!");
            }
            else
            {
                //invalid playerName, clear txtName and focus on it to try again
                MessageBox.Show("Please enter a name using letters only Red Team!");
                txtName.Clear();

                txtName.Focus();
            }

            playerName1 = txtName1.Text;


            if (Regex.IsMatch(playerName1, @"^[a-zA-Z]+$"))//checks playerName for letters
            {
                //if playerName valid (only letters) 
                MessageBox.Show("Blue Team Ready! Starting...");
            }
            else
            {
                //invalid playerName, clear txtName and focus on it to try again
                MessageBox.Show("Please enter a name using letters only Blue Team!");
                txtName1.Clear();

                txtName1.Focus();
            }

        }

        private void pnlGame_Paint(object sender, PaintEventArgs e)
        {
            btm = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(btm);
            SCG = this.CreateGraphics();

            ball = new Rectangle(this.Width / 2, this.Height / 2, 40, 40);
            RSide = new Rectangle(this.Width - 50, this.Height / 2, 30, 150);
            LSide = new Rectangle(5, this.Height / 2, 30, 150);

            th = new Thread(draw);
            th.IsBackground = true;

            th.Start();
        }

        public void draw()
        {
            while (drawing)
            {
                if (moveTo.Y > RSide.Y + 100) RSide.Y += move_speed;
                if (moveTo.Y > RSide.Y + 100) RSide.Y -= move_speed;

                if (ball.Y > LSide.Y + 100) LSide.Y += move_speed;
                if (ball.Y > LSide.Y + 100) LSide.Y -= move_speed;

                ball.X += ball_speed;

                if (ballMove.Y > ball.Y + 100) ball.Y += ball_speedY;
                if (ballMove.Y > ball.Y + 100) ball.Y -= ball_speedY;

                if (RSide.IntersectsWith(ball))
                {
                    ball_speed *= -1;
                }

                if (LSide.IntersectsWith(ball))
                {
                    ball_speed *= -1;
                }

                if (ball.Y < 20) ballMove.Y = this.Height;
                if (ball.Y > this.Height - 80) ballMove.Y = 0;

                if (ball.X < -40) ball.X = this.Width / 2;
                if (ball.X > this.Width) ball.X = this.Width / 2;
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                isLUpPressed = true;
            if (e.KeyCode == Keys.S)
                isLDownPressed = true;
            if (e.KeyCode == Keys.Up)
                isRUpPressed = true;
            if (e.KeyCode == Keys.Down)
                isRDownPressed = true;
        }
    }
}
    
