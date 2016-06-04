#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace TuneSquad_Shipwrecked
{
    class DrawManager : Manager
    {
        //fields

        //constructor
        public DrawManager()
        {

        }

        //properties

        //methods
        public void Draw(SpriteBatch spriteBatch) //handles drawing of all world objects
        {
            //draw all specials
            foreach (Special obj in Specials)
            {
                obj.Draw(spriteBatch);
            }

            //draw all the static objects
            foreach(StaticObj obj in StaticObjs)
            {
                obj.Draw(spriteBatch);
            }

            //draw all the items
            foreach(Item obj in Items)
            {
                obj.Draw(spriteBatch);
            }

            //draw all the hazards
            foreach(Hazard obj in Hazards)
            {
                obj.Draw(spriteBatch);
            }
        }
    }
}
