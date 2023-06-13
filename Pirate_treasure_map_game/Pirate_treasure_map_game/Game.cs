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
    }
}
