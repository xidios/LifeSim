using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    public abstract class Animal : Entity
        
    {
        public int satiety;
        public bool changed = false;
        public int timeLastChild;        
        public Gender gender;
        public int viewDistance = 40;
        private Random random;
        public Animal(int _x,int _y,Random rand):  base(_x,_y)
        {
            x = _x;
            y = _y;
            alive = true;
            random = rand;
            timeLastChild = random.Next(70) + 50;
            satiety = random.Next(30) + 70;


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

        private void SearchTarget(Cell[,] field, int _x, int _y)
        {
            for (int i = 0; i < viewDistance; i++)
                for (int x = _x; x <= i; x++)
                    for (int y = _y; y <= i; y++)
                    {
                        if (field[x, y].animals.Count > 0)
                        {
                            foreach (Animal a in field[x, y].animals) { }
                        }
                    }
        }
        public Direction MoveToTarget(Entity a,int _x,int _y)
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
        public abstract void DoSomething(int _x, int _y, Cell[,] field);
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
                    target = CheckTarget(x, y, ref field);
                    if (target != null)
                        return target;
                }
                //down horiz
                for (int x = -i, y = -x; x <= i; x++)
                {
                    target = CheckTarget(x, y, ref field);
                    if (target != null)
                        return target;

                }

                //left vert
                for (int y = -i + 1, x = -i; y < i; y++)
                {
                    target = CheckTarget(x, y, ref field);
                    if (target != null)
                        return target;

                }

                //right vert
                for (int y = -i + 1, x = i; y < i; y++)
                {
                    target = CheckTarget(x, y, ref field);
                    if (target != null)
                        return target;

                }
            }
            return target;
        }
        public abstract Entity CheckEat(int _x, int _y, ref Cell[,] field);
        public abstract Entity CheckTarget(int _x, int _y, ref Cell[,] field);


    }
}
