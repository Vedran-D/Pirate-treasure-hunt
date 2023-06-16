using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pirate_treasure_map_game
{
    public partial class Form1 : Form
    {
        public Game Game { get; set; }
        public int Ticks { get; set; }
        public Form1()
        {
            InitializeComponent();
            Ticks = 0;
            logs.Text = "";
            Game = new Game();
            UpdateStatusStrip();
            LoadCells();
        }

        public void LoadCells()
        {
            int top = 125;
            int left = 20;
            int rows = 0;
            for (int i = 0; i < 25; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Height = 100;
                pictureBox.Width = 100;
                pictureBox.BackColor = Color.LightYellow;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Click += pictureBox_Click;
                Game.Cells.Add(pictureBox);

                if (rows < 5)
                {
                    rows += 1;
                    pictureBox.Left = left;
                    pictureBox.Top = top;
                    this.Controls.Add(pictureBox);
                    left += 110;
                }

                if (rows == 5)
                {
                    left = 20;
                    top += 110;
                    rows = 0;
                }
            }
            Game.RestartGame();
        }

        public void pictureBox_Click(object sender, EventArgs e)
        {
            if (Game.gameOver)
            {
                return;
            }
            if (Game.Player.IsPoisoned)
            {
                return;
            }
            Game.CurrentPicture = sender as PictureBox;
            if (Game.CurrentPicture.Tag != null && Game.CurrentPicture.Image == null)
            {
                Game.CurrentPicture.Image = Image.FromFile(Game.CurrentPicture.Tag + ".png");
                Game.CheckCell(Game.CurrentPicture);
                CheckStatus();
                UpdateStatusStrip();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        public void CheckStatus()
        {
            if (Game.Status == 0)
            {
                YouWon();
            }
            else if (Game.Status == 1)
            {
                logs.Text += "You came across an empty road and carry on walking safely.\n";
            }
            else if (Game.Status == 2)
            {
                logs.Text += "You fell down a trap! You are impaled and take " + Game.CurrentDamage + " damage.\n";
            }
            else if (Game.Status == 3)
            {
                logs.Text += "You encounter a deadly den of spiders! You are poisoned! You take damage over time and can not make another move until poison wears off.\n";
                timer1.Start();
            }
            if (Game.Status == 4)
            {
                YouDied();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Ticks)
            {
                case 0: logs.Text += "You take " + Game.PoisonDmg1 + " poison damage over time!\n";
                    Ticks++; Game.Player.Damaged(Game.PoisonDmg1); break;
                case 1: logs.Text += "You take " + Game.PoisonDmg2 + " poison damage over time!\n";
                    Ticks++; Game.Player.Damaged(Game.PoisonDmg2); break;
                case 2: logs.Text += "You take " + Game.PoisonDmg3 + " poison damage over time!\n";
                    Ticks++; Game.Player.Damaged(Game.PoisonDmg3); break;
                default: 
                    Ticks = 0;
                    timer1.Stop();
                    Game.Player.IsPoisoned = false;
                    break;
            }
            UpdateStatusStrip();
            if (Game.Player.HP == 0)
            {
                timer1.Stop();
                YouDied();
            }
            
        }

        public void UpdateStatusStrip()
        {
            playerStatus.Text = $"Player's health: {Game.Player.HP.ToString()}";
        }

        public void NewGame()
        {
            timer1.Stop();
            Ticks = 0;
            Game.ClearCells();
            Game = new Game();
            UpdateStatusStrip();
            LoadCells();
            logs.Text = "";
        }

        public void YouDied()
        {
            logs.Text += "You died!";
            UpdateStatusStrip();
            if (MessageBox.Show("Ya died, you landlubber!", "Your health reaches 0. You die. Do you take the challange again?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NewGame();
            }
        }

        public void YouWon()
        {
            logs.Text += "Good job landlubber!";
            if (MessageBox.Show("Congratulations matey! Ya found the treasure", "Start another adventure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NewGame();
            }
        }


    }
}
