using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pirate_treasure_map_game
{
    public class Game
    {
        public Player Player { get; set; }
        public List<PictureBox> Cells { get; set; }
        public bool gameOver { get; set; }
        public Random Random { get; set; }
        public List<char> Content { get; set; }
        public char? CurrentChar { get; set; }
        public PictureBox CurrentPicture { get; set; }
        public int Status { get; set; }
        public int CurrentDamage { get; set; }
        public Game()
        {
            Player = new Player();
            Cells = new List<PictureBox>();
            gameOver = false;
            Random = new Random();
            Content = new List<char>() { 't', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'
             , 'p' , 'p' , 'p' , 'p' , 'p' , 'p', 's', 's', 's', 's', 's', 's'};
            CurrentChar = null;
            CurrentPicture = null;
            Status = 0;
            CurrentDamage = 0;
        }

        

        public void RestartGame()
        {
            Random random = new Random();
            for (int i = Content.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = Content[i];
                Content[i] = Content[j];
                Content[j] = temp;
            }
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
        }

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
                Status = 1;
                //logs.Text += "You came across an empty road and carry on walking safely.\n";
            }
            else if ((char)picture.Tag == 'p')
            {
                Status = 2;
                int damage = Random.Next(5, 15);
                CurrentDamage = damage;
                Player.Damaged(damage);
                //logs.Text += "You fell down a trap! You are impaled and take " + damage + " damage.\n";
            }
            else if ((char)picture.Tag == 's')
            {
                Status = 3;
                Player.IsPoisoned = true;
                int damage1 = Random.Next(5, 8);
                int damage2 = Random.Next(5, 8);
                int damage3 = Random.Next(5, 8);
                Player.Poisoned(damage1, damage2, damage3);
                //logs.Text += "You encounter a deadly den of spiders! You are poisoned and take " + damage + " damage overtime.\n";
            }
            if (Player.HP == 0)
            {
                //logs.Text += "Your health reaches 0. You die.\n";
                Status = 4;
                gameOver = true;
                if (MessageBox.Show("Ya died, you landlubber!", "Do you take the challange again?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RestartGame();
                }
            }
        }
    }
}
