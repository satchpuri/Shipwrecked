using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuneSquad_Shipwrecked
{
    //enum
    enum StaticObjType
    {
        wall,
        dresser,
        ruble
    }

    class StaticObj : WorldObj
    {
        //constructor
        public StaticObj(int x, int y)
            : base(x, y)
        {
        }
    }
}
