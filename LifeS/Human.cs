using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
      
    public class Human
    {

        public int satiety = 100;
        public int x;
        public int y;
        public bool changed = false;
        public bool alive;
        public int timeLastChild = 100;
        private int viewDistance = 30;
        public Gender gender;
        
        Random random;


        public Human(int _x, int _y,Random rand)
        {
            x = _x;
            y = _y;
            random = rand;
            alive = true;

            if (rand.Next(2) == 0)
                gender = Gender.male;
            else
                gender = Gender.female;
        }
        public void DoSomething(int _x, int _y, Cell[,] field)
        {

            if (satiety <= 50)
            {
                SearchFood(field);

            }
            else if (satiety >= 70 && timeLastChild==0)
            {
                SearchPartner(field);
                
            }
            else
            {
                MoveRandom(_x, _y);
            }
            
            if(timeLastChild>0)
                timeLastChild--;
        }
        public void MoveRandom(int xSize, int ySize)
        {
            Direction direction = (Direction)random.Next(5);
            Move(xSize, ySize,direction);
        }
        public void PanicMove(int xSize, int ySize)
        {
            Direction direction = (Direction)random.Next(4);
            Move(xSize, ySize, direction);
        }
        private void Move(int xSize, int ySize, Direction direction)
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






        private void EatPlant(int _x, int _y,Cell[,] field)
        {
            
                if (field[_x, _y].plant!=null && field[_x, _y].plant.alive)
                {
                    satiety += 50;
                    field[_x, _y].plant.Dead();
                }
            
            
        }





        public void Dead()
        {           
            
                alive = false;           
        }
        private void SearchFood(Cell[,] field)
        {
            Direction direction = Direction.none;
            int distance = viewDistance;

            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].plant != null )
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

            Human thuman = null;
            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].humans != null)
                    {
                        foreach (Human hum in field[x - v, y].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                thuman = hum;
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
                        foreach (Human hum in field[x, y - v].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                thuman = hum;
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
                        foreach (Human hum in field[x + v, y].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                thuman = hum;
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
                        foreach (Human hum in field[x, y + v].humans)
                        {
                            if (hum.gender != gender && hum.satiety >= 50 && hum.timeLastChild == 0)
                            {
                                thuman = hum;
                                direction = Direction.down;
                                distance = v;
                                v = viewDistance;
                                break;
                            }

                        }                        
                    }
                }
            }


            if (distance==0&& thuman!=null)
            {
                DoChild(x, y, field,thuman);
            }
            else
            {
                if (direction == Direction.none)
                    PanicMove(field.GetLength(0), field.GetLength(1));
                else
                    Move(field.GetLength(0), field.GetLength(1), direction);

            }
        }

        private void MoveForTarget(int left,int right,int up,int down,Direction direction)
        {

        }

        private void DoChild(int _x, int _y, Cell[,] field,Human h)
        {
            Human c = new Human(_x,_y,random);
            c.changed = false;
            field[_x, _y].childs.Add(c);
            h.timeLastChild = 150;
            timeLastChild = 200;
        }


    }
}

