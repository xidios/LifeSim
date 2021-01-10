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
        public readonly int rows;
        public readonly int cols;
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
                        //TotalOfOmnivores++;
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
                        //UpdatePredatorsInfo(x, y);
                        if (field[x, y].animals.Count > 0)
                        {
                            UpdateOmnivoresInfo(x, y);
                            List<Animal> rem2 = new List<Animal>();
                            List<Animal> add2 = new List<Animal>();
                            foreach (Animal o in field[x, y].animals)//движение
                            {
                                if (!o.changed)
                                {
                                    TotalOfAnimals++;
                                    o.DoSomething(field.GetLength(0), field.GetLength(1), field);
                                    add2.Add(o);

                                }
                                else
                                {
                                    rem2.Add(o);

                                }
                            }
                            field[x, y].animals = rem2;

                            foreach (Animal o in add2)
                            {
                                field[o.x, o.y].animals.Add(o);
                            }

                            field[x, y].animals.AddRange(field[x, y].achilds);
                            field[x, y].achilds.Clear();
                        }
                        //    List<Predator> rem1 = new List<Predator>();
                        //    List<Predator> add1 = new List<Predator>();
                        //    foreach (Predator p in field[x, y].predators)//движение
                        //    {
                        //        if (!p.changed)
                        //        {
                        //            TotalOfPredators++;
                        //            p.DoSomething(field.GetLength(0), field.GetLength(1), field);
                        //            add1.Add(p);

                        //        }
                        //        else
                        //        {
                        //            rem1.Add(p);

                        //        }
                        //    }
                        //    field[x, y].predators = rem1;

                        //    foreach (Predator p in add1)
                        //    {
                        //        field[p.x, p.y].predators.Add(p);
                        //    }

                        //    field[x, y].predators.AddRange(field[x, y].pchilds);
                        //    field[x, y].pchilds.Clear();
                        //}


                        //if (field[x, y].omnivores.Count > 0)
                        //{
                        //    UpdateOmnivoresInfo(x, y);
                        //    List<Omnivore> rem2 = new List<Omnivore>();
                        //    List<Omnivore> add2 = new List<Omnivore>();
                        //    foreach (Omnivore o in field[x, y].omnivores)//движение
                        //    {
                        //        if (!o.changed)
                        //        {
                        //            TotalOfOmnivores++;
                        //            o.DoSomething(field.GetLength(0), field.GetLength(1), field);
                        //            add2.Add(o);

                        //        }
                        //        else
                        //        {
                        //            rem2.Add(o);

                        //        }
                        //    }
                        //    field[x, y].omnivores = rem2;

                        //    foreach (Omnivore o in add2)
                        //    {
                        //        field[o.x, o.y].omnivores.Add(o);
                        //    }

                        //    field[x, y].omnivores.AddRange(field[x, y].ochilds);
                        //    field[x, y].ochilds.Clear();
                        //}


                        //if (field[x, y].humans.Count() > 0)
                        //{

                        //    UpdateHerbivoresInfo(x, y);
                        //    List<Herbivore> rem = new List<Herbivore>();
                        //    List<Herbivore> add = new List<Herbivore>();
                        //    foreach (Herbivore h in field[x, y].humans)//движение
                        //    {
                        //        if (!h.changed)
                        //        {
                        //            TotalOfHerbivores++;
                        //            h.DoSomething(field.GetLength(0), field.GetLength(1), field);
                        //            add.Add(h);

                        //        }
                        //        else
                        //        {
                        //            rem.Add(h);

                        //        }
                        //    }



                        //    field[x, y].humans = rem;

                        //    foreach (Herbivore h in add)
                        //    {
                        //        field[h.x, h.y].humans.Add(h);
                        //    }


                        //    field[x, y].humans.AddRange(field[x, y].childs);
                        //    field[x, y].childs.Clear();

                        //}

                    }
                }

            }
            CheckedToFalse();
            return field;
        }
        public Animal GetHuman(int _x, int _y)
        {
            if (field[_x, _y].animals.Count > 0)
                return field[_x, _y].animals[0];
            //else if(field[_x, _y].humans.Count>0 )
            //    return field[_x, _y].humans[0];            
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
                    //if (field[x, y].predators.Count() > 0)
                    //{
                    //    foreach (Predator b in field[x, y].predators)
                    //    {
                    //        b.changed = false;
                    //    }
                    //}
                    //if (field[x, y].omnivores.Count() > 0)
                    //{
                    //    foreach (Omnivore b in field[x, y].omnivores)
                    //    {
                    //        b.changed = false;
                    //    }
                    //}

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

        //private void UpdateHerbivoresInfo(int x, int y)
        //{
        //    List<Herbivore> checkAliveHumans = new List<Herbivore>();
        //    foreach (Herbivore h in field[x, y].humans)
        //    {
        //        if (h.satiety == 0)
        //            h.Dead();
        //        if (h.alive)
        //        {
        //            checkAliveHumans.Add(h);
        //        }

        //    }

        //    field[x, y].humans = checkAliveHumans;
        //}

        //private void UpdatePredatorsInfo(int x, int y)
        //{
        //    List<Predator> checkAliveHumans = new List<Predator>();
        //    foreach (Predator h in field[x, y].predators)
        //    {
        //        if (h.satiety == 0)
        //            h.Dead();
        //        if (h.alive)
        //        {
        //            checkAliveHumans.Add(h);
        //        }

        //    }

        //    field[x, y].predators = checkAliveHumans;
        //}
        private void UpdateOmnivoresInfo(int x, int y)
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

