using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pirate_treasure_map_game
{
    public class Player
    {
        public int HP { get; set; }
        public bool IsPoisoned { get; set; }

        public Player()
        {
            HP = 100;
            IsPoisoned = false;
        }

        public void Damaged(int points)
        {
            HP -= points;
            if (HP < 0)
            {
                HP = 0;
            }
        }

    }
}
