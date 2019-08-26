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
using PingPongGame.Classes;

namespace PingPong
{
    public partial class Form1 : Form
    {
        // Declaring all variables
        bool mod1;
        bool mod2;
        bool ballup;
        bool balldown;
        bool ballleft;
        bool ballright;
        int red;
        int green;
        int xball;
        string playerName;
        int yball;
        int yplayer1;
        int yplayer2;
        int velplayer1;
        int scoreplayer1;
        int scoreplayer2;
        int extrahorvelball;
        int extravervelball;
        // Form1
        public Form1()
        {
            InitializeComponent();
            reset();
            Begin();
            Data.Save(new HighScore(), "hs1");

            var hs = Data.Load<HighScore>("hs1");
            lblHigh1.Text = $"{hs.Name} - {hs.Score}";
        }
        // Declaring the components within timer_Tick
        private void timer_Tick(object sender, EventArgs e)
        {
            Condition();
            Player1();
            Output();
            Refresh();
        }
        // Declaring the components within timer1_Tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            Win();
            GameOver();
        }
        // Declaring the components within timer2_Tick
        private void timer2_Tick(object sender, EventArgs e)
        {
            reset();
            Begin();
        }
        // Setting all the values and timers for when the form is loaded
        private void Begin()
        {
            scoreplayer1 = 0;
            scoreplayer2 = 0;
            extrahorvelball = 0;
            extravervelball = 0;
            timer.Enabled = false;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            youwin.Visible = false;
            pingpong.Visible = true;
            gameover.Visible = false;
        }
        private void StartGame()
        {

        }

        // Creating the Ball and it's movement
        private void Ball()
        {
            ball Ball = new ball();
            xball = Ball.MoveHorizontal(xball, ballright, ballleft, extrahorvelball);
            yball = Ball.MoveVertical(yball, ballup, balldown, extravervelball);
        }
        // Creating Player1 and their movement/boundries
        private void Player1()
        {
            if (yplayer1 + velplayer1 < 248 && yplayer1 + velplayer1 > 30) yplayer1 += velplayer1;
        }
        // Creating Player2 and their moevement/boundries
        private void Player2()
        {
            if (xball >= 300)
            {
                if (yball > yplayer2 + 33 && yplayer2 + 4 < 248) yplayer2 += 5;
                if (yball < yplayer2 + 33 && yplayer2 - 4 > 30) yplayer2 -= 5;
            }
            if (scoreplayer1 == 2)
            {
                if (xball >= 501)
                {
                    if (yball > yplayer2 + 33 && yplayer2 + 4 < 248) yplayer2 += 6;
                    if (yball < yplayer2 + 33 && yplayer2 - 4 > 30) yplayer2 -= 6;

                }
            }
        }
        // Creating the class for the ball and declaring its boundries/speed
        class ball
        {
            public int MoveHorizontal(int x, bool ballright, bool ballleft, int extrahorvelball)
            {
                if (ballright) return x + 8 + extrahorvelball;
                else return x - 8 - extrahorvelball;
            }
            public int MoveVertical(int y, bool ballup, bool balldown, int extravervelball)
            {
                if (ballup) return y - 7 - extravervelball;
                else return y + 7 + extravervelball;
            }
        }
        // Painting the objects onto the 
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush RedBrush = new SolidBrush(Color.FromArgb(151, 0, 0));
            SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 93, 253));
            SolidBrush WhiteBrush = new SolidBrush(Color.White);
            Rectangle ball = new Rectangle(xball, yball, 15, 15);
            Rectangle rect1 = new Rectangle(10, yplayer1, 10, 75);
            Rectangle rect2 = new Rectangle(564, yplayer2, 10, 75);
            e.Graphics.FillEllipse(WhiteBrush, ball);
            e.Graphics.FillRectangle(RedBrush, rect1);
            e.Graphics.FillRectangle(BlueBrush, rect2);
        }
        // Resetta le variabili;
        private void reset()
        {
            red = 40;
            green = 40;
            xball = 25;
            yball = 156;
            yplayer1 = 113;
            yplayer2 = 113;
            mod1 = true;
            mod2 = false;
            ballright = true;
            ballleft = false;
            if (ballup) { ballup = false; balldown = true; }
            else { ballup = true; balldown = false; }
        }
        // ;
        private void Output()
        {
            label1.Text = scoreplayer1.ToString();
            label2.Text = scoreplayer2.ToString();
        }
        // ;
        private void Condition()
        {
            if (xball + 4 >= 550 && ballright && yball + 15 >= yplayer2 && yball <= yplayer2 + 100) { ballleft = true; ballright = false; }
            if (xball + 4 >= 550 && yball + 15 < yplayer2 || xball + 4 >= 550 && yball > yplayer2 + 100) { scoreplayer1++; if (scoreplayer1 == 2) { extrahorvelball++; extravervelball++; } if (scoreplayer1 < 3) reset(); }
            if (xball - 4 <= 16 && ballleft && yball + 15 >= yplayer1 && yball <= yplayer1 + 100) { ballright = true; ballleft = false; }
            if (xball - 4 <= 16 && yball + 15 < yplayer1 || xball - 4 <= 16 && yball > yplayer1 + 100) { scoreplayer2++; if (scoreplayer2 < 3) reset(); }
            if (yball + 1 > 313 && balldown) { balldown = false; ballup = true; }
            if (yball - 1 < 30 && ballup) { balldown = true; ballup = false; }
        }
        // Game over
        private void GameOver()
        {
            if (scoreplayer2 == 3)
            {
                panel1.Visible = true;
                txtName.ReadOnly = false;
                txtName.Visible = true;
                timer.Enabled = false;
                timer2.Enabled = true;
                timer3.Enabled = false;
                timer3.Enabled = false;
                gameover.Visible = true;
                if (mod1) red+=5;
                if (mod2) red-=5;
                if (red == 255) { mod2 = true; mod1 = false; }
                if (red == 40) { mod1 = true; mod2 = false; }
                gameover.ForeColor = Color.FromArgb(red, 40, 40);
            }
        }
        // won
        private void Win() {
            if (scoreplayer1 == 3)
            {
                panel1.Visible = true;
                txtName.ReadOnly = false;
                txtName.Visible = true;
                timer.Enabled = false;
                timer2.Enabled = true;
                timer3.Enabled = false;
                timer4.Enabled = false;
                youwin.Visible = true;
                if (mod1) green += 5;
                if (mod2) green -= 5;
                if (green == 255) { mod2 = true; mod1 = false; }
                if (green == 40) { mod2 = false; mod1 = true; }
                youwin.ForeColor = Color.FromArgb(40, green, 40);
            }
        }

        private void up()
        {
            if (scoreplayer1 == 2)
            {
               timer3.Interval = 5;

            }
        }

        // Key Down events to move the paddle -/+6 in either direction
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if key is down then move player -6
            if (e.KeyCode == Keys.Up) velplayer1 = -6;
            if (e.KeyCode == Keys.Down) velplayer1 = 6;
        }
        // Key Up events to stop the paddle
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // if key is up then stop moving player
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) velplayer1 = 0;
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        // When Form1 is loaded 
        private void Form1_Load(object sender, EventArgs e)
        {
            // show a message box
            MessageBox.Show("Use your specific keys to move your paddle. " +
  "\n Blue |Up Arrow, Down arrow| " +
  "\n Don't let the ball reach your end. " +
  "\n Every ball that gets passed is a point." +
  "\n When you get 2 points, the ball pace will speed up. " +
  "\n \n Enter your name. " +
  "\n Click Start to begin", "Game Instructions");

            // turn all timers off until the game begins
            timer.Stop();
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
        }

        // when the start button is clicked
        private void mnuStart_Click(object sender, EventArgs e)
        {
            playerName = txtName.Text;


            if (Regex.IsMatch(playerName, @"^[a-zA-Z]+$"))//checks playerName for letters
            {
                //if playerName valid (only letters) 
                txtName.ReadOnly = true;
                txtName.Visible = false;
                MessageBox.Show("Player Ready! Starting...");
                timer.Enabled = true;
                timer1.Enabled = true;
                timer3.Enabled = true;
                timer4.Enabled = true;
                panel1.Visible = false;
                pingpong.Visible = false;
            }
            else
            {
                //invalid playerName, clear txtName and focus on it to try again
                MessageBox.Show("Please enter a name using letters only Red Team!");
                txtName.Clear();

                txtName.Focus();
            }
        }


        private void mnuStop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer1.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            panel1.Visible = true;
            pingpong.Visible = true;
            txtName.ReadOnly = false;
            txtName.Visible = true;
        }

        private void txtName_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Ball();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Player2();
        }
    }
}
