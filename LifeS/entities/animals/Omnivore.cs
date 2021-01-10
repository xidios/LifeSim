using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    public class Omnivore : Animal
    {
        private Random random;
        public int viewDistance = 40;
        public Omnivore(int _x, int _y, Random rand) : base(_x, _y, rand)
        {
            random = rand;
        }

        public override void  DoSomething(int _x, int _y, Cell[,] field)
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
        


        private void SearchFood(Cell[,] field)
        {
            Direction direction = Direction.none;
            int distance = viewDistance;
            Entity en = null;

            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].plant != null)
                    {
                        en = field[x - v, y].plant;
                        direction = Direction.left;
                        distance = v;
                        break;
                    }
                    if (field[x - v, y].animals.Count > 0)
                    {
                        foreach (Animal a in field[x - v, y].animals)
                        {
                            if (a is Herbivore)
                            {
                                en = (Herbivore)a;
                                direction = Direction.left;
                                distance = v;
                                break;
                            }
                        }
                        if (en != null)
                            break;
                    }

                }

                if (y - v >= 0)
                {
                    if (field[x, y-v].plant != null)
                    {
                        en = field[x , y-v].plant;
                        direction = Direction.up;
                        distance = v;
                        break;
                    }
                    if (field[x , y-v].animals.Count > 0)
                    {
                        foreach (Animal a in field[x , y-v].animals)
                        {
                            if (a is Herbivore)
                            {
                                en = (Herbivore)a;
                                direction = Direction.up;
                                distance = v;
                                break;
                            }
                        }
                        if (en != null)
                            break;
                    }
                }

                if (x + v < field.GetLength(0))
                {
                    if (field[x + v, y].plant != null)
                    {
                        en = field[x + v, y].plant;
                        direction = Direction.right;
                        distance = v;
                        break;
                    }
                    if (field[x + v, y].animals.Count > 0)
                    {
                        foreach (Animal a in field[x + v, y].animals)
                        {
                            if (a is Herbivore)
                            {
                                en = (Herbivore)a;
                                direction = Direction.right;
                                distance = v;
                                break;
                            }
                        }
                        if (en != null)
                            break;
                    }
                }
                if (y + v < field.GetLength(1))
                {
                    if (field[x, y + v].plant != null)
                    {
                        en = field[x, y + v].plant;
                        direction = Direction.down;
                        distance = v;
                        break;
                    }
                    if (field[x, y + v].animals.Count > 0)
                    {
                        foreach (Animal a in field[x, y + v].animals)
                        {
                            if (a is Herbivore)
                            {
                                en = (Herbivore)a;
                                direction = Direction.down;
                                distance = v;
                                break;
                            }
                        }
                        if (en != null)
                            break;
                    }
                }
            }





            if (distance == 0)
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

        private void SearchPartner(Cell[,] field)
        {
            Direction direction = Direction.none;
            int distance = viewDistance;

            Omnivore tomnivore = null;
            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].animals != null)
                    {
                        foreach (Animal p in field[x - v, y].animals)
                        {
                            if (p is Omnivore)
                            {
                                if (p.gender != gender && p.satiety >= 50 && p.timeLastChild == 0)
                                {
                                    tomnivore = (Omnivore)p;
                                    direction = Direction.left;
                                    distance = v;
                                    v = viewDistance;
                                    break;
                                }
                            }

                        }

                    }
                }
                if (y - v >= 0)
                {
                    if (field[x, y - v].animals != null)
                    {
                        foreach (Animal hum in field[x, y - v].animals)
                        {
                            if (hum is Omnivore)
                            {
                                if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                                {
                                    tomnivore = (Omnivore)hum;
                                    direction = Direction.up;
                                    distance = v;
                                    v = viewDistance;
                                    break;
                                }
                            }

                        }


                    }
                }
                if (x + v < field.GetLength(0))
                {
                    if (field[x + v, y].animals != null)
                    {
                        foreach (Animal hum in field[x + v, y].animals)
                        {
                            if (hum is Omnivore)
                            {
                                if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                                {
                                    tomnivore = (Omnivore)hum;
                                    direction = Direction.right;
                                    distance = v;
                                    v = viewDistance;
                                    break;
                                }
                            }

                        }


                    }
                }
                if (y + v < field.GetLength(1))
                {
                    if (field[x, y + v].animals != null)
                    {
                        foreach (Animal hum in field[x, y + v].animals)
                        {
                            if (hum is Omnivore)
                            {
                                if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                                {
                                    tomnivore = (Omnivore)hum;
                                    direction = Direction.down;
                                    distance = v;
                                    v = viewDistance;
                                    break;
                                }
                            }

                        }
                    }
                }
            }


            if (distance == 0 && tomnivore != null)
            {
                DoChild(x, y, field, tomnivore);
            }
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
        }


        private void DoChild(int _x, int _y, Cell[,] field, Omnivore h)
        {
            Omnivore o = new Omnivore(_x, _y, random);
            o.changed = false;
            field[_x, _y].achilds.Add(o);
            h.timeLastChild = 150;
            timeLastChild = 200;
        }
    } 
}

