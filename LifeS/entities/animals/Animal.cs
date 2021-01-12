using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    public abstract class Animal : Entity
    {
        public int satiety = 100;
        public bool changed = false;
        public int timeLastChild;        
        public Gender gender;
        private Random random;
        public Animal(int _x,int _y,Random rand):  base(_x,_y)
        {
            x = _x;
            y = _y;
            alive = true;
            random = rand;
            timeLastChild = random.Next(70) + 50;


            if (random.Next(2) == 0)
                gender = Gender.male;
            else
                gender = Gender.female;
        }
        public void MoveRandom(int xSize, int ySize)
        {
            Direction direction = (Direction)random.Next(5);
            Move(xSize, ySize, direction);
        }
        public void PanicMove(int xSize, int ySize)
        {
            Direction direction = (Direction)random.Next(4);
            Move(xSize, ySize, direction);
        }
        public void Move(int xSize, int ySize, Direction direction)
        {

            switch (direction)
            {
                case Direction.left:
                    if (x - 1 >= 0)
                    {
                        x--;
                        changed = true;
                    }

                    break;
                case Direction.up:
                    if (y - 1 >= 0)
                    {
                        y--;
                        changed = true;
                    }
                    break;
                case Direction.right:
                    if (x + 1 < xSize)
                    {
                        x++;
                        changed = true;
                    }
                    break;
                case Direction.down:
                    if (y + 1 < ySize)
                    {
                        y++;
                        changed = true;
                    }
                    break;
                case Direction.none:
                    break;
            }
            satiety--;
        }
        public abstract void DoSomething(int _x, int _y, Cell[,] field);


    }
}
