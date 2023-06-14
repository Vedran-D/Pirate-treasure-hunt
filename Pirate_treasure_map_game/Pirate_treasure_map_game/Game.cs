using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pirate_treasure_map_game
{
    [Serializable]
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
        public int PoisonDmg1 { get; set; }
        public int PoisonDmg2 { get; set; }
        public int PoisonDmg3 { get; set; }
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
            Status = -1;
            CurrentDamage = 0;
            PoisonDmg1 = 0;
            PoisonDmg2 = 0;
            PoisonDmg3 = 0;
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
                    CurrentDamage = Random.Next(5, 15);
                    Player.Damaged(CurrentDamage);
                }
                else if ((char)picture.Tag == 's')
                {
                    Status = 3;
                    Player.IsPoisoned = true;
                    PoisonDmg1 = Random.Next(5, 8);
                    PoisonDmg2 = Random.Next(5, 8);
                    PoisonDmg3 = Random.Next(5, 8);
                    //Player.Poisoned(PoisonDmg1, PoisonDmg2, PoisonDmg3);

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
                Cells[i].Hide();
                //Cells[i].Tag = null;
            }
            //Cells.Clear();
        }
    }
}
