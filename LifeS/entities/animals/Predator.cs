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
        public int viewDistance = 20;
        public Predator(int _x, int _y, Random rand) : base(_x, _y, rand)
        {
            random = rand;
        }

        public void DoSomething(int _x, int _y, Cell[,] field)
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


        public void EatHerbivore(int _x, int _y, Cell[,] field)
        {
            if (field[_x, _y].humans.Count>0 && field[_x, _y].humans[0].alive)
            {
                satiety += 50;
                field[_x, _y].humans[0].Dead();
            }
        }



        private void SearchFood(Cell[,] field)
        {
            Direction direction = Direction.none;
            int distance = viewDistance;

            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].humans.Count > 0)
                    {
                        direction = Direction.left;
                        distance = v;
                        break;
                    }
                }
                if (y - v >= 0)
                {
                    if (field[x, y - v].humans.Count > 0)
                    {
                        direction = Direction.up;
                        distance = v;
                        break;
                    }
                }
                if (x + v < field.GetLength(0))
                {
                    if (field[x + v, y].humans.Count > 0)
                    {
                        direction = Direction.right;
                        distance = v;
                        break;
                    }
                }
                if (y + v < field.GetLength(1))
                {
                    if (field[x, y + v].humans.Count>0)
                    {
                        direction = Direction.down;
                        distance = v;
                        break;
                    }
                }
            }





            if (distance == 0)
            {
                EatHerbivore(x, y, field);

            }
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }



        }

        private void SearchPartner(Cell[,] field)
        {
            Direction direction = Direction.none;
            int distance = viewDistance;

            Predator tpredator = null;
            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].humans != null)
                    {
                        foreach (Predator p in field[x - v, y].predators)
                        {
                            if (p.gender != gender && p.satiety >= 50 && p.timeLastChild == 0)
                            {
                                tpredator = p;
                                direction = Direction.left;
                                distance = v;
                                v = viewDistance;
                                break;
                            }

                        }

                    }
                }
                if (y - v >= 0)
                {
                    if (field[x, y - v].humans != null)
                    {
                        foreach (Predator hum in field[x, y - v].predators)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                tpredator = hum;
                                direction = Direction.up;
                                distance = v;
                                v = viewDistance;
                                break;
                            }

                        }


                    }
                }
                if (x + v < field.GetLength(0))
                {
                    if (field[x + v, y].humans != null)
                    {
                        foreach (Predator hum in field[x + v, y].predators)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                tpredator = hum;
                                direction = Direction.right;
                                distance = v;
                                v = viewDistance;
                                break;
                            }

                        }


                    }
                }
                if (y + v < field.GetLength(1))
                {
                    if (field[x, y + v].humans != null)
                    {
                        foreach (Predator hum in field[x, y + v].predators)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                tpredator = hum;
                                direction = Direction.down;
                                distance = v;
                                v = viewDistance;
                                break;
                            }

                        }
                    }
                }
            }


            if (distance == 0 && tpredator != null)
            {
                DoChild(x, y, field, tpredator);
            }
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
        }


        private void DoChild(int _x, int _y, Cell[,] field, Predator h)
        {
            Predator c = new Predator(_x, _y, random);
            c.changed = false;
            field[_x, _y].pchilds.Add(c);
            h.timeLastChild = 150;
            timeLastChild = 200;
        }
    }

}
