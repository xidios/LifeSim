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
        public int viewDistance = 40;
        public Herbivore(int _x, int _y, Random rand) : base (_x,_y,rand)
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

        private void EatPlant(int _x, int _y,Cell[,] field)
        {
            
                if (field[_x, _y].plant!=null && field[_x, _y].plant.alive)
                {
                    satiety += 50;
                    field[_x, _y].plant.Dead();
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
                    if (field[x - v, y].plant != null)
                    {
                        direction = Direction.left;
                        distance = v;
                        break;
                    }
                }
                if (y - v >= 0)
                {
                    if (field[x, y - v].plant != null)
                    {
                        direction = Direction.up;
                        distance = v;
                        break;
                    }
                }
                if (x + v < field.GetLength(0))
                {
                    if (field[x + v, y].plant != null)
                    {
                        direction = Direction.right;
                        distance = v;
                        break;
                    }
                }
                if (y + v < field.GetLength(1))
                {
                    if (field[x, y + v].plant != null)
                    {
                        direction = Direction.down;
                        distance = v;
                        break;
                    }
                }
            }





            if (distance == 0)
            {
                EatPlant(x, y, field);

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
            int distance=viewDistance;

            Herbivore therbivore = null;
            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].humans != null)
                    {
                        foreach (Herbivore hum in field[x - v, y].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                therbivore = hum;
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
                        foreach (Herbivore hum in field[x, y - v].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                therbivore = hum;
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
                        foreach (Herbivore hum in field[x + v, y].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                therbivore = hum;
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
                        foreach (Herbivore hum in field[x, y + v].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                therbivore = hum;
                                direction = Direction.down;
                                distance = v;
                                v = viewDistance;
                                break;
                            }

                        }                        
                    }
                }
            }


            if (distance==0&& therbivore != null)
            {
                DoChild(x, y, field, therbivore);
            }
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
        }

        private void DoChild(int _x, int _y, Cell[,] field, Herbivore h)
        {
            Herbivore c = new Herbivore(_x,_y,random);
            c.changed = false;
            field[_x, _y].childs.Add(c);
            h.timeLastChild = 150;
            timeLastChild = 200;
        }


    }
}

