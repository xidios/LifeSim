using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeS
{
    public class Cell
    {
        public int x;
        public int y;
        //public List<Herbivore> humans = new List<Herbivore>();
        public Plant plant = null;
        //public List<Herbivore> childs = new List<Herbivore>();
        //public List<Predator> predators = new List<Predator>();
        //public List<Predator> pchilds = new List<Predator>();
        //public List<Omnivore> omnivores = new List<Omnivore>();
        //public List<Omnivore> ochilds = new List<Omnivore>();
        public List<Animal> animals = new List<Animal>();
        public List<Animal> achilds = new List<Animal>();



        public Cell(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
            
        }        

    }


}
