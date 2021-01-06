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
        public int timeLastChild = 0;
        public Gender gender;
        
        Random random;


        //public bool isAlive()
        //{
        //    return alive;
        //}
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
            else if (satiety >= 70 && timeLastChild>=100)
            {
                SearchPartner(field);
                
            }
            else
            {
                MoveRandom(_x, _y);
            }
            

            timeLastChild++;
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
            
                if (field[_x, _y].plants.Count()==1 && field[_x, _y].plants[0].alive)
                {
                    satiety += 50;
                    field[_x, _y].plants[0].alive = false;
                }
            
            
        }





        public void Dead()
        {
            if (satiety == 0)
            {
                alive = false;
            }
        }
        private void SearchFood(Cell[,] field)
        {
            int left= field.GetLength(0), up = field.GetLength(1), right = field.GetLength(0), down = field.GetLength(1);
            for (int i = x; i >= 0; i--)//слева
            {
                
                if (field[i, y].plants.Count() == 1 && field[i, y].plants[0].alive)
                {
                    left = x-i;
                    break;
                }
                
            }

            for (int i = x; i < field.GetLength(0); i++)//справа
            {
                if (field[i, y].plants.Count() == 1 && field[i, y].plants[0].alive)
                {
                    right = i - x;
                    break;
                }
                
            }
            for (int i = y; i >= 0; i--)//сверху
            {
                if (field[x, i].plants.Count() == 1 && field[x, i].plants[0].alive)
                {
                    up = y - i;
                    break;
                }
                
            }
            for (int i = y; i < field.GetLength(1); i++)//снизу
            {
                if (field[x, i].plants.Count() == 1 && field[x, i].plants[0].alive)
                {
                    down = i - y;
                    break;
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
                else if(down < left && down < right && down < up) 
                { direction = Direction.down; }
                

                Move(field.GetLength(0), field.GetLength(1), direction);

            }



        }

        private void SearchPartner(Cell[,] field)
        {
            int left = field.GetLength(0), up = field.GetLength(1), right = field.GetLength(0), down = field.GetLength(1);
            int _x= -1, _y= -1;
            Human h =null;
            for (int i = x; i >= 0; i--)//слева
            {
                if (h == null && field[i, y].humans != null)
                {
                    foreach (Human human in field[i, y].humans)
                    {
                        if (human.gender != gender && human.satiety >= 70 && human.timeLastChild >= 100)
                        {
                            left = x - i;
                            h = human;
                            break;
                        }

                    }
                }
                else break;
                //if (field[i, y].humans.Count() > 0 && field[i, y].humans[0].gender != gender && field[i, y].humans[0].satiety >= 70 && field[i, y].humans[0].timeLastChild >= 100)
                //{
                //    _x = i;
                //    _y = y;
                //    left = x - i;
                //    h = field[_x,_y].humans[0];
                //    break;
                //}

            }

            for (int i = x; i < field.GetLength(0); i++)//справа
            {

                if (h == null && field[i, y].humans != null)
                {
                    foreach (Human human in field[i, y].humans)
                    {
                        if (human.gender != gender && human.satiety >= 70 && human.timeLastChild >= 100)
                        {
                            right = x - i;
                            h = human;
                            break;
                        }

                    }
                }
                else break;
                //if (field[i, y].humans.Count() >0 && field[i, y].humans[0].gender != gender && field[i, y].humans[0].satiety>=70 && field[i, y].humans[0].timeLastChild>=100)
                //{
                //    _x = i;
                //    _y = y;
                //    right = i - x;
                //    h = field[_x, _y].humans[0];
                //    break;
                //}

            }
            for (int i = y; i >= 0; i--)//сверху
            {
                if (h == null && field[x, i].humans != null)
                {
                    foreach (Human human in field[i, y].humans)
                    {
                        if (human.gender != gender && human.satiety >= 70 && human.timeLastChild >= 100)
                        {
                            up = x - i;
                            h = human;
                            break;
                        }

                    }
                }
                else break;
                //if (field[x, i].humans.Count() >0 && field[x, i].humans[0].gender != gender && field[x, i].humans[0].satiety >= 70 && field[x, i].humans[0].timeLastChild >= 100)
                //{
                //    _x = x;
                //    _y = i;
                //    up = y - i;
                //    h = field[_x, _y].humans[0];
                //    break;
                //}

            }
            for (int i = y; i < field.GetLength(1); i++)//снизу
            {
                if (h == null && field[i, y].humans != null)
                {
                    foreach (Human human in field[i, y].humans)
                    {
                        if (human.gender != gender && human.satiety >= 70 && human.timeLastChild >= 100)
                        {
                            down = x - i;
                            h = human;
                            break;
                        }

                    }
                }
                else break;
                //if (field[x, i].humans.Count() >0 && field[x, i].humans[0].gender != gender && field[x, i].humans[0].satiety >= 70 && field[x, i].humans[0].timeLastChild >= 100)
                //{
                //    _x = x;
                //    _y = i;
                //    down = i - y;
                //    h = field[_x, _y].humans[0];
                //    break;
                //}

            }


            if ((left == 0 || right == 0 || up == 0 || down == 0 )&& h!=null) //left == 0 || right == 0 || up == 0 || down == 0
            {
                DoChild(x, y, field,h);
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

        private void DoChild(int _x, int _y, Cell[,] field,Human h)
        {
            Human c = new Human(_x,_y,random);
            c.changed = false;
            field[_x, _y].childs.Add(c);
            h.timeLastChild = 0;
            timeLastChild = 0;
        }


    }
}

//int left = field.GetLength(0), up = field.GetLength(1), right = field.GetLength(0), down = field.GetLength(1);
//for (int i = x; i >= 0; i--)//слева
//{

//    if (field[i, y].humans.Count() == 1 && field[i, y].humans[0].gender != gender && field[i, y].humans[0].satiety >= 70 && field[i, y].humans[0].timeLastChild >= 100)
//    {
//        left = x - i;
//        break;
//    }

//}

//for (int i = x; i < field.GetLength(0); i++)//справа
//{
//    if (field[i, y].humans.Count() == 1 && field[i, y].humans[0].gender != gender && field[i, y].humans[0].satiety >= 70 && field[i, y].humans[0].timeLastChild >= 100)
//    {
//        right = i - x;
//        break;
//    }

//}
//for (int i = y; i >= 0; i--)//сверху
//{
//    if (field[x, i].humans.Count() == 1 && field[x, i].humans[0].gender != gender && field[x, i].humans[0].satiety >= 70 && field[x, i].humans[0].timeLastChild >= 100)
//    {
//        up = y - i;
//        break;
//    }

//}
//for (int i = y; i < field.GetLength(1); i++)//снизу
//{
//    if (field[x, i].humans.Count() == 1 && field[x, i].humans[0].gender != gender && field[x, i].humans[0].satiety >= 70 && field[x, i].humans[0].timeLastChild >= 100)
//    {
//        down = i - y;
//        break;
//    }

//}


//if (gender == Gender.female && (left == 0 || right == 0 || up == 0 || down == 0)) //left == 0 || right == 0 || up == 0 || down == 0
//{
//    DoChild(x, y, field);
//}
//else if (gender == Gender.female && (left == 1 || right == 1 || up == 1 || down == 1 || left == 0 || right == 0 || up == 0 || down == 0))
//{
//    Move(field.GetLength(0), field.GetLength(1), Direction.none);
//    return;
//}
//else
//{
//    Direction direction = (Direction)random.Next(4);
//    if (left < right && left < up && left < down)
//    {
//        direction = Direction.left;
//    }
//    else if (right < left && right < up && right < down)
//    {
//        direction = Direction.right;
//    }
//    else if (up < left && up < right && up < down)
//    {
//        direction = Direction.up;
//    }
//    else if (down < left && down < right && down < up)
//    { direction = Direction.down; }


//    Move(field.GetLength(0), field.GetLength(1), direction);

//}
