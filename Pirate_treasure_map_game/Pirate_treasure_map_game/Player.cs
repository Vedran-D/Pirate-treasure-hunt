﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pirate_treasure_map_game
{
    public class Player
    {
        public int HP { get; set; }
        public int Status { get; set; }
        public bool IsPoisoned { get; set; }

        public Player()
        {
            HP = 100;
            Status = 0;
            IsPoisoned = false;
        }

        public void Damaged(int points)
        {
            HP -= points;
            CheckHealth();
        }

        public void Poisoned(int dmg1, int dmg2, int dmg3)
        {
            if (IsPoisoned)
            {
                HP -= dmg1;
                HP -= dmg2;
                HP -= dmg3;
            }
            CheckHealth();
        }

        public void CheckHealth()
        {
            if (HP < 0)
            {
                HP = 0;
            }
        }

    }
}
