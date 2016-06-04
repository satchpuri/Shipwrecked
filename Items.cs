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
        key
    }

    class Items : WorldObj
    {
        //fields
        private int dir; //direction
        private String objType;

        

        //constructor
        public Items(int x, int y, int dir)
            : base(x, y)
        {
            dir = this.dir; //we'll get this from the input of obj locations
            objType = "bookcase";
        }

        //properties
        public int Dir
        {
            get { return dir; }
            set
            {
                dir = value;
            }
        }

        public String ObjType
        {
            get { return objType; }
            set
            {
                objType = value;
            }
        }

        //methods
        public void Action()
        {

        }

        public void BookcaseAction()
        {

        }

        public void ValveAction()
        {
            
        }

        public void PowerSwitchAction()
        {

        }
    }
}
