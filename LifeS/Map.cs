using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    class Map
    {
     
        //public struct Border
        //{
        //    public int left;
        //    public int right;
        //    public int top;
        //    public int bot;
        //};
        private Cell[,] field;
        public List<Event> mapEvents=new List<Event>();
        public readonly int rows=1000;
        public readonly int cols=1000;
        Random random = new Random();

        public int CurrentGeneration { get; private set; }
        public int TotalOfHerbivores { get; private set; }
        public int TotalOfPredators { get; private set; }
        public int TotalOfAnimals { get; private set; }


        public Map(int rows, int cols, int density)
        {
            Random random = new Random();
            this.rows = rows;
            this.cols = cols;
            field = new Cell[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {                   
                    field[x, y] = new Cell(cols, rows);
                    if (random.Next(density) == 0)
                    {
                        Herbivore rand = new Herbivore(x, y,random);                       
                        field[x, y].animals.Add(rand);
                        TotalOfHerbivores++;
                    }
                    if (random.Next(density*4) == 0)
                    {
                        Omnivore rand = new Omnivore(x, y, random);
                        field[x, y].animals.Add(rand);
                    }
                    if (random.Next(density*5) == 0)
                    {
                        Predator rand = new Predator(x, y, random);
                        field[x, y].animals.Add(rand);
                        TotalOfPredators++;
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
            TotalOfAnimals = 0;
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    CreateRandomPlant(x, y, random);
                    UpdatePlantsInfo(x, y);

                    if (field[x, y].animals.Count > 0)
                    {
                        UpdateCellInformation(x, y);
                    }


                }

            }
            
            CheckedToFalse();
            return field;
        }
        private void UpdateCellInformation(int x,int y)
        {
            UpdateAnimalsInfo(x, y);
            List<Animal> rem = field[x, y].animals.OfType<Animal>().Where(animal => animal.changed).ToList();
            List<Animal> add = field[x, y].animals.OfType<Animal>().Where(animal => !animal.changed).ToList();

            field[x, y].animals = rem;

            foreach (Animal o in add)
            {
                TotalOfAnimals++;
                o.DoSomething(field.GetLength(0), field.GetLength(1), field);
                field[o.x, o.y].animals.Add(o);
            }

            field[x, y].animals.AddRange(field[x, y].achilds);
            field[x, y].achilds.Clear();
        }
        public Animal GetHuman(int _x, int _y)
        {
            if (field[_x, _y].animals.Count > 0)
                return field[_x, _y].animals[0];          
            return null;
        }
        private void CheckedToFalse()
        {
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (field[x, y].animals.Count() > 0)
                    {
                        foreach (Animal b in field[x, y].animals)
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
      
        private void UpdateAnimalsInfo(int x, int y)
        {
            List<Animal> checkAliveOmnivores = new List<Animal>();
            foreach (Animal o in field[x, y].animals)
            {
                if (o.satiety == 0)
                    o.Dead();
                if (o.alive)
                {
                    checkAliveOmnivores.Add(o);
                }

            }

            field[x, y].animals = checkAliveOmnivores;
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

