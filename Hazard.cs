using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuneSquad_Shipwrecked
{
    //enums
    enum HazardType
    {
        pitfall,
        shock,
        steam
    }

    class Hazard : WorldObj
    {
        //constructor
        public Hazard(int x, int y)
            :base(x, y)
        {
        }
    }
}
