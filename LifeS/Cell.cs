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
        public Plant plant = null;
        public List<Human> childs = new List<Human>();        



        public Cell(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
            
        }        

    }


}
