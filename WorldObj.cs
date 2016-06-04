using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TuneSquad_Shipwrecked
{
    abstract class WorldObj
    {
        //fields
        private int x;
        private int y;
        private int prevX;
        private int prevY;
        private Rectangle rect;
        private Rectangle prevRect;
        private Texture2D texture;
        private String objType;
        private bool active;

        //constructor
        public WorldObj(int xPos, int yPos)
        {
            //set our xy coods in the world for our objects
            x = xPos;
            y = yPos;

            //setup our rectangle
            rect = new Rectangle(x, y, Game1.spriteWidth, Game1.spriteHeight);

            //set objtype default
            objType = "floor";

            active = true; //in reality only for hazards
        }

        //properties
        public int X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }

        public int PrevX
        {
            get { return prevX; }
            set
            {
                prevX = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }

        public int PrevY
        {
            get { return prevY; }
            set
            {
                prevY = value;
            }
        }
        public Rectangle Rect
        {
            get { return rect; }
            set
            {
                rect = value;
            }
        }

        public Rectangle PrevRect
        {
            get { return prevRect; }
            set
            {
                prevRect = value;
            }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
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
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
            }
        }

        //methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
