using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{   
    public class Predator : Animal
    {
        private Random random;
        public new int viewDistance = 70;
        public Predator(int _x, int _y, Random rand) : base(_x, _y, rand)
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

            Entity tpreadtor = FindTarget(ref field);
            Direction direction = Direction.none;

            if (tpreadtor != null)
                direction = MoveToTarget(tpreadtor, x, y);
            if (direction == Direction.samePosition)
                DoChild(x, y, field, tpreadtor);
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
        }


        public override Entity CheckEat(int _x, int _y, ref Cell[,] field)
        {
            int tempX = x + _x;
            int tempY = y + _y;

            if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
            {
                if (field[tempX, tempY].animals.Count > 0)
                {
                    foreach (Animal a in field[tempX, tempY].animals)
                        if (a is Herbivore)
                            return a;
                }
            }

            return null;
        }
        private void DoChild(int _x, int _y, Cell[,] field, Entity h)
        {
            Predator c = new Predator(_x, _y, random);
            Predator ah = (Predator)h;
            c.changed = true;
            field[_x, _y].animals.Add(c);
            ah.timeLastChild = 150;
            timeLastChild = 200;
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
                        if (o is Predator && gender != o.gender && o.satiety >= 50 && o.timeLastChild == 0)
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
