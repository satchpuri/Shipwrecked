using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuneSquad_Shipwrecked
{
    class Collision
    {
        //fields
        private List<Item> items;
        private List<StaticObj> staticObjs;
        private List<Hazard> hazards;
        private List<Special> specials;
        private Player ply;

        private WorldObj collidedObj;


        //constructor
        public Collision(List<Item> items, List<StaticObj> staticObjs, List<Hazard> hazards, List<Special> specials, Player ply)
        {
            this.specials = specials;
            this.staticObjs = staticObjs;
            this.hazards = hazards;
            this.items = items;
            this.ply = ply; //
        }

        //properties
        public WorldObj CollidedObj
        {
            get { return collidedObj; }
            set
            {
                collidedObj = value;
            }
        }

        //method
        public bool CheckAll() //checks for collision for any object
        {
            //preset return value
            bool collision = false;

            //check for collisions
            foreach (Item obj in items)
            {
                if (ply.Rect.Intersects(obj.Rect))
                {
                    collision = true;
                }
            }

            foreach (StaticObj obj in staticObjs)
            {
                if (ply.Rect.Intersects(obj.Rect))
                {
                    collision = true;
                }
            }

            foreach (Hazard obj in hazards)
            {
                if (ply.Rect.Intersects(obj.Rect))
                {
                    collision = true;
                }
            }

            //specials not listed bc the floors are special, so we dont wanna check a collision for them. 

            //return bool
            return collision;
        }

        public bool CheckList(List<WorldObj> objs) //checks for collision fo object of specified type
        {
            //pre set collision bool
            bool collision = false;

            //check for collision
            foreach(WorldObj obj in objs)
            {
                if(ply.Rect.Intersects(obj.Rect))
                {
                    collision = true;
                    collidedObj = obj; //tell us which object was collided with
                }
            }

            //return bool
            return collision;
        }
    }
}
