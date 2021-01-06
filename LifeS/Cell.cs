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
        public List<Human> humans = new List<Human>();
        public List<Plant> plants = new List<Plant>();
        public List<Human> childs = new List<Human>();
        public bool plant = false;



        public Cell(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
            
        }
        public void CreatePlant(Random rand)
        {
            if (!plant)
            {   
                if(rand.Next(3000)==0)
                    plant = true;
            }
        }

    }


}
