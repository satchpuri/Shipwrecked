using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace TuneSquad_Shipwrecked
{
    public class MainMenu
    {
        //rectangles for buttons
        private Rectangle play;
        private Rectangle options;
        private Rectangle exit;
        private Rectangle background;

        //colors of buttons
        private Color playColor;
        private Color optionsColor;
        private Color exitColor;

        //keyboard states
        KeyboardState prevkb;
        KeyboardState kb;

        private int num;

        public MainMenu(int width, int height)
        {
            play = new Rectangle(200, 200, 100, 50);
            options = new Rectangle(200, 250, 100, 50);
            exit = new Rectangle(200, 300, 100, 50);
            background = new Rectangle(0, 0, width, height);

            playColor = Color.White;
            optionsColor = Color.White;
            exitColor = Color.White;

            kb = Keyboard.GetState();
            prevkb = Keyboard.GetState();

            num = 0;
        }

        public Color PlayColor
        {
            get { return playColor; }
        }

        public Color OptionsColor
        {
            get { return optionsColor; }
        }

        public Color ExitColor
        {
            get { return exitColor; }
        }

        public int Num
        {
            get { return num; }
        }

        public int Selected()
        {
                if (SingleKeyPress(Keys.Up))
                {
                    num--;
                }

                else if (SingleKeyPress(Keys.Down))
                {
                    num++;
                }

                if (num > 2)
                {
                    num = 0;
                }

                else if (num < 0)
                {
                    num = 2;
                }

                if (num == 0)
                {
                    playColor = Color.Blue;
                    optionsColor = Color.White;
                    exitColor = Color.White;
                    return 1;
                }

                if (num == 1)
                {
                    playColor = Color.White;
                    optionsColor = Color.Blue;
                    exitColor = Color.White;
                    return 2;
                }

                if (num == 2)
                {
                    playColor = Color.White;
                    optionsColor = Color.White;
                    exitColor = Color.Blue;
                    return 3;
                }
                else
                {
                    return 0;
                }

                
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D backgroundTex, Texture2D playTex, Texture2D optionsTex, Texture2D exitTex)
        {
            spriteBatch.Draw(backgroundTex, background, Color.White);
            spriteBatch.Draw(playTex, play, PlayColor);
            spriteBatch.Draw(optionsTex, options, OptionsColor);
            spriteBatch.Draw(exitTex, exit, ExitColor);
        }

        public bool SingleKeyPress(Keys key)
        {
            kb = Keyboard.GetState();

            if (kb.IsKeyUp(Keys.Up) && kb.IsKeyUp(Keys.Down))
            {
                prevkb = Keyboard.GetState();
                return false;
            }

            else if (kb.IsKeyDown(key) == true && prevkb.IsKeyDown(key) == false)
            {
                prevkb = kb;
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
