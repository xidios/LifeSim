using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    class Map
    {
        
        private Cell[,] field;
        public List<Event> mapEvents=new List<Event>();
        public readonly int rows;
        public readonly int cols;
        Random random = new Random();

        public int CurrentGeneration { get; private set; }
        public int TotalOfHumans { get; private set; }
       
      
        public Map(int rows, int cols, int density)
        {
            Random random = new Random();
            this.rows = rows;
            this.cols = cols;
            var hasLife = false;
            field = new Cell[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    hasLife = random.Next(density) == 0;
                    field[x, y] = new Cell(cols, rows);
                    if (hasLife)
                    {                                              
                        Human rand = new Human(x, y,random);                       
                        field[x, y].humans.Add(rand);
                        TotalOfHumans++;
                    }
                    if (random.Next(200) == 0)
                    {                        
                        field[x, y].plant = new Plant(x, y);
                    }
                }
            }
            CurrentGeneration++;
        }
        public Cell[,] NextGeneration()
        {
            StartEvent();
            CurrentGeneration++;
            TotalOfHumans = 0;
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {                    
                    CreateRandomPlant(x, y, random);
                    UpdatePlantsInfo(x, y);

                    if (field[x, y].humans.Count() > 0)
                    {
                        
                        UpdateHumansInfo(x, y);                       

                        List<Human> rem = new List<Human>();
                        List<Human> add = new List<Human>();
                        foreach (Human h in field[x, y].humans)//движение
                        {
                            if (!h.changed)
                            {
                                TotalOfHumans++;
                                h.DoSomething(field.GetLength(0), field.GetLength(1), field);
                                add.Add(h);

                            }
                            else
                            {
                                rem.Add(h);
                               
                            }
                        }
                        field[x, y].humans = rem;
                        
                        foreach (Human h in add)
                        {
                            field[h.x, h.y].humans.Add(h);
                        }
                        field[x, y].humans.AddRange(field[x, y].childs);
                        field[x, y].childs.Clear();
                    }
                    
                }
            }

            
            
           

            CheckedToFalse();
            return field;

        }
        public Human GetHuman(int _x, int _y)
        {       
            if(field[_x, _y].humans.Count>0 )
                return field[_x, _y].humans[0];            
            return null;
        }
        private void CheckedToFalse()
        {
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (field[x, y].humans.Count() > 0)
                    {
                        foreach (Human b in field[x, y].humans)
                        {
                            b.changed = false;
                        }
                    }

                }
            }
        }

        private void CreateRandomPlant(int x,int y,Random _random)
        {

            if (field[x, y].plant==null)
            {
                int countPlantsAround = 0;
                if (x - 1 >= 0 && field[x - 1, y].plant!= null)
                {
                    countPlantsAround++;
                }
                if (y - 1 >= 0 && field[x, y - 1].plant != null)
                {
                    countPlantsAround++;
                }
                if (x + 1 < field.GetLength(0) && field[x + 1, y].plant != null)
                {
                    countPlantsAround++;
                }
                if (y + 1 < field.GetLength(1) && field[x, y + 1].plant != null)
                {
                    countPlantsAround++;
                }

                if (countPlantsAround > 0 && _random.Next(60 / countPlantsAround) == 0)
                {
                    field[x, y].plant = new Plant(x, y);
                }
            }
        }
        private void UpdatePlantsInfo(int x, int y)
        {
            if (field[x, y].plant!=null && !field[x, y].plant.alive)
            {                     
                    field[x, y].plant=null;
            }
        }
        private void UpdateHumansInfo(int x, int y)
        {
            List<Human> checkAliveHumans = new List<Human>();
            foreach (Human h in field[x, y].humans)
            {
                if (h.satiety == 0)
                    h.Dead();
                if (h.alive)
                {
                    checkAliveHumans.Add(h);
                }

            }

            field[x, y].humans = checkAliveHumans;
        }

        private void StartEvent()
        {
            List<Event> _mapEvents = new List<Event>();
            if (random.Next(50) == 0)
            {
                Event _event = new Event(random, field);
                mapEvents.Add(_event);
            }
            foreach (Event e in mapEvents)
            {
                if (e.exist)
                {
                    e.KillTheAll(field);
                    _mapEvents.Add(e);
                }

            }
            mapEvents = _mapEvents;
        }
        public void CreateEvent(int x, int y)
        {
            Event ev = new Event(random,field);
            mapEvents.Add(ev);
            ev.x = x;
            ev.y = y;
            ev.KillTheAll(field);
        }

    }
}

