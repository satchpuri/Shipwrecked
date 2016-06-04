using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalMapMaker
{
    class GameObject
    {
        //Ints for position
        public int x; //Can be between 0 and 32
        public int y; //Can be between 0 and 24
        
        //Checks if the game object is currently selected
        public bool active;

        //All the different types of objects
        public enum Type
        {
            wall,
            dresser,
            rubble,
            bookcase,
            valve,
            lightSwitch,
            door,
            key,
            pitfall,
            shock,
            steam
        }

        Type type;

        public GameObject(Type t)
        {
            type = t;
            active = true;
            x = 0;
            y = 0;
        }

        //This method will eventually snap the game object into a valid position
        

    }
}
