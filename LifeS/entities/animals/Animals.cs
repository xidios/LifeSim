using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    abstract public class Animals<T, TFood> : Animal
        //where T : Animal
    {
        public Animals(int _x, int _y, Random rand) : base(_x, _y, rand)
        {

        }
        //public Entity CheckTarget(int _x, int _y, ref Cell[,] field)
        //{
        //    int tempX = x + _x;
        //    int tempY = y + _y;

        //    if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
        //    {
        //        if (field[tempX, tempY].animals.Count > 0)
        //        {
        //            foreach (Animal o in field[tempX, tempY].animals)
        //            {
        //                if (o is T && gender != o.gender && o.satiety >= 50 && o.timeLastChild == 0)
        //                {
        //                    return o;
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}
        //public Entity CheckEat(int _x, int _y, ref Cell[,] field)
        //{
        //    int tempX = x + _x;
        //    int tempY = y + _y;

        //    if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
        //    {
        //        if (typeof(T) == typeof(Omnivore) || typeof(T) == typeof(Herbivore) && field[tempX, tempY].plant != null)
        //        {
        //            return field[tempX, tempY].plant;
        //        }
        //        if (typeof(T) == typeof(Omnivore) || typeof(T) == typeof(Predator) && field[tempX, tempY].animals.Count > 0)
        //        {
        //            foreach (Animal a in field[tempX, tempY].animals)
        //            {
        //                if (a is Herbivore)
        //                {
        //                    return a;
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}
        public Entity CheckEat(int _x, int _y, ref Cell[,] field)
        {
            int tempX = x + _x;
            int tempY = y + _y;

            if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
            {
                if (field[tempX, tempY].plant != null && field[tempX, tempY].plant is TFood)
                {
                    return field[tempX, tempY].plant;
                }
                if (field[tempX, tempY].entity.Count > 0)
                {
                    foreach (Animal a in field[tempX, tempY].entity)
                    {
                        if (a is TFood)
                        {
                            return a;
                        }
                    }
                }
            }

            return null;
        }
        public Entity CheckTarget<Target>(int _x, int _y, ref Cell[,] field)
        {
            int tempX = x + _x;
            int tempY = y + _y;

            if (!(tempX < 0 || tempX >= field.GetLength(0) || tempY < 0 || tempY >= field.GetLength(1)))
            {
                if (field[tempX, tempY].entity.Count > 0)
                {
                    foreach (Animal o in field[tempX, tempY].entity)
                    {
                        if (o is Target && gender != o.gender && o.satiety >= 50 && o.timeLastChild == 0)
                        {
                            return o;
                        }
                    }
                }
            }

            return null;
        }
        public Entity FindEat(ref Cell[,] field)
        {
            int visibility = viewDistance;
            Entity target = null;
            for (int i = 0; i <= visibility; i++)
            {
                //up horiz
                for (int x = -i, y = x; x <= i; x++)
                {
                    target = CheckEat(x, y, ref field);
                    if (target != null)
                        return target;
                }
                //down horiz
                for (int x = -i, y = -x; x <= i; x++)
                {
                    target = CheckEat(x, y, ref field);
                    if (target != null)
                        return target;

                }

                //left vert
                for (int y = -i, x = -i; y < i; y++)
                {
                    target = CheckEat(x, y, ref field);
                    if (target != null)
                        return target;

                }

                //right vert
                for (int y = -i, x = i; y < i; y++)
                {
                    target = CheckEat(x, y, ref field);
                    if (target != null)
                        return target;

                }
            }

            return target;
        }
        public Entity FindTarget(ref Cell[,] field)
        {
            int visibility = viewDistance;
            Entity target = null;
            for (int i = 0; i <= visibility; i++)
            {
                //up horiz
                for (int x = -i, y = x; x <= i; x++)
                {
                    target = CheckTarget<T>(x, y, ref field);
                    if (target != null)
                        return target;
                }
                //down horiz
                for (int x = -i, y = -x; x <= i; x++)
                {
                    target = CheckTarget<T>(x, y, ref field);
                    if (target != null)
                        return target;

                }

                //left vert
                for (int y = -i + 1, x = -i; y < i; y++)
                {
                    target = CheckTarget<T>(x, y, ref field);
                    if (target != null)
                        return target;

                }

                //right vert
                for (int y = -i + 1, x = i; y < i; y++)
                {
                    target = CheckTarget<T>(x, y, ref field);
                    if (target != null)
                        return target;

                }
            }
            return target;
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

        public Direction MoveToTarget(Entity a, int _x, int _y)
        {
            if (a.x - _x < 0)
            {
                return Direction.left;
            }
            else if (a.x - _x > 0)
            {
                return Direction.right;
            }
            else if (a.y - _y < 0)
            {
                return Direction.up;
            }
            else if (a.y - _y > 0)
            {
                return Direction.down;
            }
            else if (a.y == _y && a.x == _x)
                return Direction.samePosition;
            return Direction.none;
        }

        public void EatSmth(int _x, int _y, Entity en)
        {
            if (en is Plant)
            {
                satiety += 50;
                en.Dead();
            }
            else
            {
                satiety += 70;
                en.Dead();
            }
        }

        public void SearchFood(Cell[,] field)
        {

            Entity en = null;

            en = FindEat(ref field);

            Direction direction = Direction.none;
            if (en != null)
                direction = MoveToTarget(en, x, y);
            if (direction == Direction.samePosition)
            {
                EatSmth(x, y, en);

            }
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
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

            Animal child = (Animal)Activator.CreateInstance(typeof(T), _x, _y, random);
            child.changed = true;
            Animal parent = (Animal)h;
            field[_x, _y].entity.Add(child);
            parent.timeLastChild = 150;
            timeLastChild = 200;
        }
        

    }
}
