﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    public class Event
    {
        public bool exist = false;
        public int x;
        public int y;
        int distanse;
        Random rand;
        Direction direction;
        public Event(Random random, Cell[,] field)
        {
            exist = true;
            rand = random;
            x = rand.Next(field.GetLength(0));
            y = rand.Next(field.GetLength(1));
            distanse = rand.Next(100) + 5;
            direction = (Direction)rand.Next(4);
        }
        public void KillTheAll(Cell[,] field)
        {
            if (distanse == 0)
            {
                exist = false;
                return;
            }


            if (x < field.GetLength(0) && x >= 0 && y >= 0 && y < field.GetLength(1))
            {
                if (field[x, y].humans.Count > 0)
                {
                    foreach (Herbivore h in field[x, y].humans)
                    {
                        h.Dead();
                    }


                }
                if (field[x, y].plant != null && field[x, y].plant.alive)
                {
                    field[x, y].plant.Dead();
                }

                if (direction == Direction.right)
                    x++;
                else if (direction == Direction.left)
                    x--;
                else if (direction == Direction.up)
                    y--;
                else if (direction == Direction.down)
                    y++;

                distanse--;
            }
            else distanse = 0;


        }
    }
}
