using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuneSquad_Shipwrecked
{
    class Door : Item
    {
        //fields
        bool locked;
        Key key;

        //constuctor
        public Door(int x, int y, int dir, bool locked)
            :base(x, y, dir)
        {
            locked = this.locked;
        }

        //properties
        public bool Locked
        {
            get { return locked; }
            set
            {
                locked = value;
            }
        }

        //methods
        public void UseKey(bool locked, Key key)
        {
            //see if player has proper key if any

            //unlock or lock door depending on current state
        }
    }
}
