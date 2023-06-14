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
            //for (int i = 0; i < 25; i++)
            //{
            //    Cells[1].Image = null;
            //    Cells[1].Tag = 'o';
            //    Cells.ElementAt(i).Image = null;
            //}
            gameOver = false;
            Random = new Random();
            Content = new List<char>() { 't', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e'
             , 'p' , 'p' , 'p' , 'p' , 'p' , 'p', 's', 's', 's', 's', 's', 's'};
            CurrentChar = null;
            CurrentPicture = null;
            Status = -1;
            CurrentDamage = 0;
        }

        

        public void RestartGame()
        {
            for (int i = Content.Count - 1; i > 0; i--)
            {
                int j = Random.Next(i + 1);
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
                Status = 0;
                gameOver = true;
            }
            else if ((char)picture.Tag == 'e')
            {
                Status = 1;
                
            }
            else if ((char)picture.Tag == 'p')
            {
                Status = 2;
                int damage = Random.Next(5, 15);
                CurrentDamage = damage;
                Player.Damaged(damage);
            }
            else if ((char)picture.Tag == 's')
            {
                Status = 3;
                Player.IsPoisoned = true;
                int damage1 = Random.Next(5, 8);
                int damage2 = Random.Next(5, 8);
                int damage3 = Random.Next(5, 8);
                Player.Poisoned(damage1, damage2, damage3);
                
            }
            if (Player.HP == 0)
            {
                Status = 4;
                gameOver = true;
            }
        }
        public void ClearCells()
        {
            for (int i = 0; i < 25; i++)
            {
                Cells[i].Image = null;
                //Cells[i].Tag = null;
            }
            //Cells.Clear();
        }
    }
}
