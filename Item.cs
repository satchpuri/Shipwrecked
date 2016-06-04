using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuneSquad_Shipwrecked
{
    //enums
    enum ItemType
    {
        bookcase,
        valve,
        power,
        door,
    }

    class Item : WorldObj
    {
        //fields
        private int velocity; //direction

        //constructor
        public Item(int x, int y, int velocity)
            : base(x, y)
        {
            velocity = this.velocity; //we'll get this from the input of obj locations
        }

        //properties
        public int Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
            }
        }
    }
}
