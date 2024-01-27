using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_Man
{
    public partial class Form1 : Form
    {

        bool goup, godown, goleft, goright, isGameOver;

        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;


        DateTime startTime;
        TimeSpan elapsedTime;
        bool isTimerStarted;




        public Form1()
        {
            InitializeComponent();

            resetGame();
            
            this.FormClosing += Form1_FormClosing;

        }

        public void keyisdown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }




        }

        public void keyisup(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if(e.KeyCode == Keys.Enter)
            {
                resetGame();
            }



        }

        public void mainGameTimer(object sender, EventArgs e)
        {

            txtScore.Text = "Очки: " + score;


            if (!isTimerStarted)
            {
                startTime = DateTime.Now;
                isTimerStarted = true;
            }

            elapsedTime = DateTime.Now - startTime;
            label1.Text = "Время: " + elapsedTime.ToString(@"mm\:ss");








            if (goleft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left1;
            }
            if (goright == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right1;
            }
            if (godown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down1;
            }
            if (goup == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up1;
            }

            if (pacman.Left < -10)
            {
                pacman.Left = 680;
            }
            if (pacman.Left > 680)
            {
                pacman.Left = -10;
            }

            if (pacman.Top < -10)
            {
                pacman.Top = 550;
            }
            if (pacman.Top > 550)
            {
                pacman.Top = 0;
            }

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {

                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {

                        if(pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }


                    }

                    if((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("Вы проиграли!\nОчень жаль ;(");
                        }



                        if(pinkGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhostX = -pinkGhostX;
                        }



                    }


                    if((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("Вы проиграли!\nОчень жаль ;(");
                        }
                    }

                }
            }


            //moving ghosts

            redGhost.Left += redGhostSpeed;

            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) || redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }


            yellowGhost.Left -= yellowGhostSpeed;

            if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds) || yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }


            pinkGhost.Left -=pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            if (pinkGhost.Top < 0  || pinkGhost.Top > 520)
            {
                pinkGhostY = -pinkGhostY;
            }
            if (pinkGhost.Left < 0 || pinkGhost.Left > 620)
            {
                pinkGhostX = -pinkGhostX;
            }






            if (score == 165)
            {
                gameOver("Вы победили!\nПоздравляем :)");
            }


        }

        public void resetGame()
        {

            txtScore.Text = "Очки: 0";
            score = 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;

            isGameOver = false;

            pacman.Left = 86;
            pacman.Top = 163;

            redGhost.Left = 271;
            redGhost.Top = 39;

            yellowGhost.Left = 439;
            yellowGhost.Top = 466;

            pinkGhost.Left = 516;
            pinkGhost.Top = 101;


            // Инициализация переменных времени
            startTime = DateTime.Now;
            elapsedTime = TimeSpan.Zero;
            isTimerStarted = false;

            // Отображение времени в начале игры
            label1.Text = "Время: 00:00";







            foreach (Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    x.Visible = true;
                }
            }


            gameTimer.Start();



        }

        public void gameOver(string message)
        {

            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Очки: " + score + Environment.NewLine + message;




        }



        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
