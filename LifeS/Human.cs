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
        public void MoveRandom(int _x, int _y)
        {
            Direction direction = (Direction)random.Next(5);
            Move(_x, _y,direction);
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
            int left = field.GetLength(0), up = field.GetLength(1), right = field.GetLength(0), down = field.GetLength(1);            
            Plant tplant = null;
            for (int v = 0; v < viewDistance; v++)
            {
                if (x - v >= 0)
                {
                    if (field[x - v, y].plant != null )
                    {
                        tplant = field[x - v, y].plant;
                        left = v;
                        break;
                    }
                }
                if (y - v >= 0)
                {
                    if (field[x, y - v].plant != null)
                    {
                        tplant = field[x, y - v].plant;
                        up = v;
                        break;
                    }
                }
                if (x + v < field.GetLength(0))
                {
                    if (field[x + v, y].plant != null)
                    {
                        tplant = field[x + v, y].plant;
                        right = v;
                        break;
                    }
                }
                if (y + v < field.GetLength(1))
                {
                    if (field[x, y + v].plant != null)
                    {
                        tplant = field[x, y + v].plant;
                        down = v;
                        break;
                    }
                }
            }
           
           



            if (left == 0 || right == 0 || up == 0 || down == 0)
            {
                EatPlant(x, y, field);

            }
            else
            {
                Direction direction = (Direction)random.Next(4);
                if (left < right && left < up && left < down)
                {
                    direction = Direction.left;
                }
                else if (right < left && right < up && right < down)
                {
                    direction = Direction.right;
                }
                else if (up < left && up < right && up < down)
                {
                    direction = Direction.up;
                }
                else if (down < left && down < right && down < up)
                { direction = Direction.down; }


                Move(field.GetLength(0), field.GetLength(1), direction);

            }



        }

        private void SearchPartner(Cell[,] field)
        {
            int left = field.GetLength(0), up = field.GetLength(1), right = field.GetLength(0), down = field.GetLength(1);           
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
                                left = v;
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
                                up = v;
                                v=viewDistance;
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
                                right = v;
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
                                down = v;
                                v = viewDistance;
                                break;
                            }

                        }                        
                    }
                }
            }


            if (((left == 0 && left<viewDistance)|| (right == 0 && right< viewDistance) || (up == 0 && up< viewDistance )|| (down==0&& down < viewDistance))&& thuman!=null) //left == 0 || right == 0 || up == 0 || down == 0
            {
                DoChild(x, y, field,thuman);
            }
            else
            {
                Direction direction = (Direction)random.Next(4);
                if (left < right && left < up && left < down)
                {
                    direction = Direction.left;
                }
                else if (right < left && right < up && right < down)
                {
                    direction = Direction.right;
                }
                else if (up < left && up < right && up < down)
                {
                    direction = Direction.up;
                }
                else if (down < left && down < right && down < up)
                { direction = Direction.down; }


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

