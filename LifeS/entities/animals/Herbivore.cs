using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
      
    public class Herbivore : Animal
    {
        private Random random;
        private new int viewDistance = 10;
        public Herbivore(int _x, int _y, Random rand) : base (_x,_y,rand)
        {
            random = rand;
        }

        public override void DoSomething(int _x, int _y, Cell[,] field)
        {

            if (satiety <= 50)
            {
                SearchFood(field);

            }
            else if (satiety >= 70 && timeLastChild == 0)
            {
                SearchPartner(field);

            }
            else
            {
                MoveRandom(_x, _y);
            }

            if (timeLastChild > 0)
                timeLastChild--;
        }
       

        private void SearchPartner(Cell[,] field)
        {
            
            Entity therbivore = FindTarget(ref field);
            Direction direction = Direction.none;

            if (therbivore != null)
                direction = MoveToTarget(therbivore, x, y);
            if (direction == Direction.samePosition)
                DoChild(x, y, field, therbivore);
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
        }


        private void DoChild(int _x, int _y, Cell[,] field, Entity h)
        {
            Herbivore c = new Herbivore(_x,_y,random);
            Herbivore ah = (Herbivore)h;
            c.changed = true;
            field[_x, _y].animals.Add(c);
            ah.timeLastChild = 150;
            timeLastChild = 200;
        }
        public override Entity CheckEat(int _x, int _y, ref Cell[,] field)
        {
            int tempX = x + _x;
            int tempY = y + _y;

            if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
            {
                if (field[tempX, tempY].plant != null)
                {
                    return field[tempX, tempY].plant;
                }                
            }

            return null;
        }
        public override Entity CheckTarget(int _x, int _y, ref Cell[,] field)
        {
            int tempX = x + _x;
            int tempY = y + _y;

            if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
            {
                if (field[tempX, tempY].animals.Count > 0)
                {
                    foreach (Animal o in field[tempX, tempY].animals)
                    {
                        if (o is Herbivore && gender != o.gender && o.satiety >= 40 && o.timeLastChild == 0)
                        {
                            return o;
                        }
                    }
                }
            }

            return null;
        }

    }
}

