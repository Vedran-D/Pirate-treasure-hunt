using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pirate_treasure_map_game
{
    public partial class Form1 : Form
    {
        public Player Player { get; set; }
        public List<PictureBox> Cells { get; set; }
        public bool gameOver { get; set; }
        public Random Random { get; set; }
        public List<char> Content { get; set; }
        public char? CurrentChar { get; set; }
        public PictureBox CurrentPicture { get; set; }
        public Form1()
        {
            InitializeComponent();
            Content = new List<char>() { 't', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'
             , 'p' , 'p' , 'p' , 'p' , 'p' , 'p', 's', 's', 's', 's', 's', 's'};
            Player = new Player();
            Cells = new List<PictureBox>();
            CurrentChar = null;
            Random = new Random();
            LoadCells();
        }

        public void LoadCells()
        {
            int top = 35;
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
                Cells.Add(pictureBox);

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
            RestartGame();
        }

        public void pictureBox_Click(object sender, EventArgs e)
        {
            if (gameOver)
            {
                return;
            }

            CurrentPicture = sender as PictureBox;
            if (CurrentPicture.Tag != null && CurrentPicture.Image == null)
            {
                CurrentPicture.Image = Image.FromFile(CurrentPicture.Tag + ".png");
                CurrentChar = (char)CurrentPicture.Tag;
            }

            CheckCell(CurrentPicture);


        }
        // encounter
        // stumble accross
        // came across
        public void CheckCell(PictureBox picture)
        {
            if ((char)picture.Tag == 't')
            {
                gameOver = true;
                if (MessageBox.Show("Congratulations matey! Ya found the treasure", "Start another adventure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RestartGame();
                }
            }
            else if ((char)picture.Tag == 'e')
            {
                logs.Text += "You came across an empty road and carry on walking safely.\n";
            }
            else if ((char)picture.Tag == 'p')
            {
                int damage = Random.Next(5,15);
                logs.Text += "You fell down a trap! You are impaled and take " + damage + " damage\n";
            }
        }

        public void RestartGame()
        {
            List<char> randomizedList = new List<char>();
            Random random = new Random();
            for (int i = Content.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = Content[i];
                Content[i] = Content[j];
                Content[j] = temp;
            }
            //int indeks = random.Next(Content.Count);
            //int indeks = Content.Where(z => z == 't').Index();
            int indeks = Array.IndexOf(Content.ToArray(), 't');
            Cells[indeks].Image = null;
            Cells[indeks].Tag = 't';
            for (int i = 0; i < Cells.Count; i++)
            {
                if (indeks != i)
                {
                    Cells[i].Image = null;
                    Cells[i].Tag = Content[i];
                }

            }

            gameOver = false;
            logs.Text = "";
        }

        public void GameOver()
        {

        }


    }
}
