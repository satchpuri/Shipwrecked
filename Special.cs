using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuneSquad_Shipwrecked
{
    //enum
    enum SpecialObjType
    {
        floor,
        stairs
    }

    class Special : WorldObj
    {
        //constructor
        public Special(int x, int y)
            : base(x, y)
        {
        }
    }
}