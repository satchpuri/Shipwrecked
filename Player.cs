using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TuneSquad_Shipwrecked
{
    class Player
    {
        //fields
        private int x;
        private int y;
        private int velocity;
        private int health;
        private Rectangle rect;
        private Rectangle interactRect;
        private Texture2D texture;
        private bool holding;

        //constructor
        public Player(int x, int y, int velocity)
        {
            this.x = x;
            this.y = y;
            velocity = this.velocity;
            health = 100;

            rect = new Rectangle(x, y, Game1.spriteWidth - 20 , Game1.spriteHeight - 20); //players rectangle
            interactRect = new Rectangle(x, y - 10, 1, 1); //rect for checking for an obj in front of the player
        }

        //properties
        public bool Holding
        {
            get { return holding; }
            set
            {
                holding = value;
            }
        }
        public Rectangle InteractRect
        {
            get { return interactRect; }
            set
            {
                interactRect = value;
            }
        }
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
            }
        }

        public int X
        {
            get { return x; }
            set
            {
                x = value;
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

        public int Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
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

        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
            }
        }

        //methods
        public void Damage(int damage)
        {
            //inflict damage
            health -= damage;
        }

        public bool IsDead()
        {
            //setup value to return
            bool dead = false;

            //check hp value
            if(health <= 0)
            {
                //update that the palyer is dead
                dead = true;
            }

            //return if player is dead or not
            return dead;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

    }
}
