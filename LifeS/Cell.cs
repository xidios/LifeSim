﻿using System;
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
        public Plant plant = null;        
        public List<Entity> animals = new List<Entity>();


        public Cell(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
            
        }        

    }


}
