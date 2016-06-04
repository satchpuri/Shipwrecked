#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TuneSquad_Shipwrecked
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        Song menuMusic;
        
        //fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Manager manager;
        DrawManager drawManager;

        KeyboardState kbState;

        //game textures
        Texture2D sprBookcase;
        Texture2D sprDoor;
        Texture2D sprDresser;
        Texture2D sprFloor;
        Texture2D sprKey;
        Texture2D sprPitfall;
        Texture2D sprPlayer;
        Texture2D sprRuble;
        Texture2D sprShock;
        Texture2D sprStairs;
        Texture2D sprSteam;
        Texture2D sprSwitch;
        Texture2D sprValve;
        Texture2D sprWall;
        Texture2D playerup;
        Texture2D playerDown;
        Texture2D playerleft;
        Texture2D playertopright;
        Texture2D playertopleft;
        Texture2D playerbotleft;
        Texture2D playerbotRight;
        Texture2D playerLeftWalking1;
        Texture2D playerLeftWalking2;

        MainMenu mM;
        Collision collision;

        //menu textures
        Texture2D background;
        Texture2D play;
        Texture2D options;
        Texture2D exit;
        Texture2D instructions;
        Texture2D pauseInstrutctions;
        Texture2D pauseMenu;
        Texture2D pauseBackground;

        Rectangle water;
        Texture2D waterTex;
        int count;

        SpriteFont sF;

        Vector2 topCorner = new Vector2(0, 0);

        Player ply;

        //global fields yo
        public static int spriteWidth;
        public static int spriteHeight;

        //GameState
        GameState gS;

        //constructor
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //define the size of our sprites (we chnage them here to change where the size is used everywhere else
            spriteWidth = 64;
            spriteHeight = 64;

            //create the obj's we'll need
            manager = new Manager();
            drawManager = new DrawManager();
            ply = new Player(2 * spriteHeight, 2 * spriteWidth, 0);

            mM = new MainMenu(800, 600);

            gS = GameState.MainMenu;

            count = 0;

            //make collision object and give it what it needs
            collision = new Collision(manager.Items, manager.StaticObjs, manager.Hazards, manager.Specials, ply);
        }
       
        
        //methods
        protected override void Initialize() //setup stuff needed before starting game
        {
            water = new Rectangle(0, GraphicsDevice.Viewport.Height - 10, 1, 10);

            base.Initialize();
        }

        public void MovementProcessing()
        {            
            //get keyboard state
            KeyboardState kbState = Keyboard.GetState();

            moveBookcase();

            //integer for movespead (and direction based on +/-)
            int move = 5;

            foreach (Item obj in manager.Items)
            {
                obj.PrevRect = obj.Rect;
                obj.PrevX = obj.X;
                obj.PrevY = obj.Y;
            }

            foreach (StaticObj obj in manager.StaticObjs)
            {
                obj.PrevRect = obj.Rect;
                obj.PrevX = obj.X;
                obj.PrevY = obj.Y;
            }

            foreach (Hazard obj in manager.Hazards)
            {
                obj.PrevRect = obj.Rect;
                obj.PrevX = obj.X;
                obj.PrevY = obj.Y;
            }
            foreach (Special obj in manager.Specials)
            {
                obj.PrevRect = obj.Rect;
                obj.PrevX = obj.X;
                obj.PrevY = obj.Y;
            }

            //check D
            if (kbState.IsKeyDown(Keys.D))
            {
                if (ply.Holding == false)
                {
                    //give player texture
                    ply.Texture = sprPlayer;

                    //set interaction rectangle to the right of the players rect
                    ply.InteractRect = new Rectangle(ply.X + 45, ply.Y + 22, 5, 5);
                }
                //move objects in world
                foreach (Item obj in manager.Items)
                {
                    obj.X -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }

                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.X -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }

                foreach (Hazard obj in manager.Hazards)
                {
                    obj.X -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.X -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }    
            }  
        
            //check S
            if (kbState.IsKeyDown(Keys.S))
            {
                if (ply.Holding == false)
                {
                    //set player texture
                    ply.Texture = playerDown;

                    //set interaction rectangle to below the players rect
                    ply.InteractRect = new Rectangle(ply.X + 22, ply.Y + 45, 5, 5);
                }
                //move
                foreach (Item obj in manager.Items)
                {
                    obj.Y -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);                      
                }
    
                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.Y -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
    
                foreach (Hazard obj in manager.Hazards)
                {
                    obj.Y -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.Y -= move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                    
            }
            
            //check W
            if (kbState.IsKeyDown(Keys.W))
            {
                if (ply.Holding == false)
                {
                    //give ply texture
                    ply.Texture = playerup;

                    //set interaction rectangle to below the players rect
                    ply.InteractRect = new Rectangle(ply.X + 22, ply.Y - 6, 5, 5);
                }
                //move
                foreach (Item obj in manager.Items)
                {
                    obj.Y += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }

                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.Y += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
    
                foreach (Hazard obj in manager.Hazards)
                {
                    obj.Y += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.Y += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
            }
            
            //check A
            if (kbState.IsKeyDown(Keys.A))
            {
                if (ply.Holding == false)
                {
                    //give ply texture
                    ply.Texture = playerleft;

                    //set interaction rectangle to the right of the players rect
                    ply.InteractRect = new Rectangle(ply.X - 6, ply.Y + 22, 5, 5);
                }
                //move
                foreach (Item obj in manager.Items)
                {
                    obj.X += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                    
                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.X += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                    
                foreach (Hazard obj in manager.Hazards)
                {
                    obj.X += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.X += move;
                    obj.Rect = new Rectangle(obj.X, obj.Y, spriteWidth, spriteHeight);
                }  
            }
            //check collision
            List<WorldObj> objs = manager.Cast("staticObjs"); //cast to worldObj
            if (collision.CheckList(objs))
            {
                foreach (Item obj in manager.Items)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }

                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }

                foreach (Hazard obj in manager.Hazards)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY; ;
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }
            }
            //reset move
            move = 5;
            if (ply.Holding == false)
            {
                if ((kbState.IsKeyDown(Keys.W)) && (kbState.IsKeyDown(Keys.D)))
                {
                    ply.Texture = playertopright;
                }

                if ((kbState.IsKeyDown(Keys.W)) && (kbState.IsKeyDown(Keys.A)))
                {
                    ply.Texture = playertopleft;
                }

                if ((kbState.IsKeyDown(Keys.S)) && (kbState.IsKeyDown(Keys.D)))
                {
                    ply.Texture = playerbotRight;
                }

                if ((kbState.IsKeyDown(Keys.S)) && (kbState.IsKeyDown(Keys.A)))
                {
                    ply.Texture = playerbotleft;
                }
            }
        }

        public void UpdateGameState()
        {
            //get current keyboard state
            KeyboardState kbState = Keyboard.GetState();
            
            if (gS == GameState.MainMenu)
            {
                if (mM.Selected() == 1 && kbState.IsKeyDown(Keys.Enter))
                {
                    gS = GameState.Loading;
                }

                else if (mM.Selected() == 2 && kbState.IsKeyDown(Keys.Enter))
                {
                    gS = GameState.OptionsMenu;
                }

                else if (mM.Selected() == 3 && kbState.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (gS == GameState.OptionsMenu)
            {
                if (kbState.IsKeyDown(Keys.Back))
                {
                    gS = GameState.MainMenu;
                }

                if(kbState.IsKeyDown(Keys.Space))
                {
                    gS = GameState.Loading;
                }
            }

            if(gS == GameState.Loading)
            {
                //clear lists of game objects
                manager.Items.Clear();
                manager.Hazards.Clear();
                manager.StaticObjs.Clear();
                manager.Specials.Clear();
                
                //check for first run
                if(manager.Level == 0)
                {
                    //goto a new level - updates lists
                    manager.NextLevel();
                    water.Width = water.Width - 200;
                    if (water.Width < 0)
                    {
                        water.Width = 0;
                    }
                }
                else if (manager.CurrentLevel == manager.Level)//check currentlevel vs level
                {
                    //goto a new level - updates lists
                    manager.NextLevel();
                    water.Width = water.Width - 200;
                    if (water.Width < 0)
                    {
                        water.Width = 0;
                    }
                }
                else
                {
                    //goto a previous level - updates lists
                    manager.LastLevel();
                    water.Width = water.Width + 100;
                }

                //set textures for all the new objects
                GiveTextures();

                //change state to game
                gS = GameState.Game;
            }

            if (gS == GameState.Game)
            {
                //check if player wants to pause
                if (kbState.IsKeyDown(Keys.P))
                {
                    gS = GameState.PauseMenu;
                }

                //check if player is dead
                if(ply.IsDead())
                {
                    //change state to end game
                    gS = GameState.EndGame;
                }

                //play the game
                MovementProcessing();

                //check collision for hazards
                List<WorldObj> objs = manager.Cast("hazards"); //cast to worldObj
                if (collision.CheckList(objs))
                {
                    //make collidedObj a hazard
                    collision.CollidedObj = (Hazard)collision.CollidedObj;

                    //check if its a pitfall and we arent on the first level
                    if(collision.CollidedObj.ObjType == "pitfall" && manager.CurrentLevel > 0)
                    {
                        //drop current level
                        manager.CurrentLevel--;

                        //goto laod state
                        gS = GameState.Loading;
                    }
                    //check if its active
                    else if(collision.CollidedObj.Active)
                    {
                        //oh man they touched a hazard, damage the player
                        ply.Damage(100);
                    }
                }

                List<WorldObj> objSpecials = manager.Cast("specials"); //cast to worldObj
                if (collision.CheckList(objSpecials))
                {
                    if (collision.CollidedObj.ObjType == "stairs")
                    {
                        //update currentlevel
                        manager.CurrentLevel++;
                        
                        //move into loading gamestate
                        gS = GameState.Loading;
                    }
                }

                List<WorldObj> objItems = manager.Cast("items"); //cast to worldObj
                if (collision.CheckList(objItems))
                {

                    //process collisions for switches and valves
                    collideSwitch();
                }

                //check for using of items
                UseItem();
            }

            if (gS == GameState.PauseMenu)
            {
                if (kbState.IsKeyDown(Keys.Q))
                {
                    gS = GameState.MainMenu;
                }

                if (kbState.IsKeyDown(Keys.Enter))
                {
                    gS = GameState.Game;
                }

                if (kbState.IsKeyDown(Keys.O))
                {
                    gS = GameState.PausedOptionsMenu;
                }

            }

            if (gS == GameState.PausedOptionsMenu)
            {
                if (kbState.IsKeyDown(Keys.Back))
                {
                    gS = GameState.PauseMenu;
                }
            }

            if (gS == GameState.EndGame)
            {
                //goto main menu when you hit space
                if (kbState.IsKeyDown(Keys.Back))
                {
                    gS = GameState.MainMenu;

                    //reset level
                    manager.Level = 0;
                    manager.CurrentLevel = 0;

                    //reset ply death
                    ply.Health = 100;
                }
            }
        }

        public void collideSwitch()
        {
            if (collision.CollidedObj.ObjType == "valve" || collision.CollidedObj.ObjType == "switch")
            {
                foreach (Item obj in manager.Items)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }

                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }

                foreach (Hazard obj in manager.Hazards)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY; ;
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }
            }
        }

        public void moveBookcase()
        {
            ply.Holding = false;
            List<WorldObj> objItems = manager.Cast("items"); //cast to worldObj
            if (kbState.IsKeyDown(Keys.Space))
            {
                foreach (Item obj in manager.Items)
                {
                    if (obj.Rect.Intersects(ply.InteractRect) && obj.ObjType == "bookcase")
                    {
                        obj.Rect = obj.PrevRect;
                        obj.X = obj.PrevX;
                        obj.Y = obj.PrevY;
                        ply.Holding = true;
                    }
                }
            }
            else if (collision.CheckList(objItems) && collision.CollidedObj.ObjType == "bookcase")
            {
                foreach (Item obj in manager.Items)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }

                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }

                foreach (Hazard obj in manager.Hazards)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY; ;
                }
                foreach (Special obj in manager.Specials)
                {
                    obj.Rect = obj.PrevRect;
                    obj.X = obj.PrevX;
                    obj.Y = obj.PrevY;
                }
            }
        }

        public void UseItem()
        {
            //check for interactions with an item
            foreach(Item obj in manager.Items)
            {
                //check for intersection and action key being hit(space)
                if (ply.InteractRect.Intersects(obj.Rect) && kbState.IsKeyDown(Keys.Space))
                {
                    //check cases for diff type objs
                    if(obj.ObjType == "valve")
                    {
                        //run through each steam hazard and change active bool
                        foreach(Hazard steam in manager.Hazards)
                        {
                            //check if steam
                            if(steam.ObjType == "steam")
                            {
                                //change active bool
                                //steam.changeActive();
                                steam.Active = false;
                            } 
                        }
                    }
                    else if(obj.ObjType == "switch")
                    {
                        //run through each shock hazard and change active bool
                        foreach (Hazard shock in manager.Hazards)
                        {
                            //check if shock
                            if (shock.ObjType == "shock")
                            {
                                //change active bool
                                //shock.changeActive();
                                shock.Active = false;
                            }
                        }
                    }
                }
            }
        }

        public void GiveTextures()
        {
            //run through objs and set their textures
            foreach (Special obj in manager.Specials) //specials
            {
                if (obj.ObjType == "floor") //floor
                {
                    obj.Texture = sprFloor;
                }
                else //stairs
                {
                    obj.Texture = sprStairs;
                }
            }

            foreach (StaticObj obj in manager.StaticObjs) //static Objects
            {
                if (obj.ObjType == "wall")
                {
                    obj.Texture = sprWall;
                }
                else if (obj.ObjType == "dresser")
                {
                    obj.Texture = sprDresser;
                }
                else //ruble
                {
                    obj.Texture = sprRuble;
                }
            }

            foreach (Item obj in manager.Items) //items
            {
                if (obj.ObjType == "bookcase")
                {
                    obj.Texture = sprBookcase;
                }
                else if (obj.ObjType == "valve")
                {
                    obj.Texture = sprValve;
                }
                else if (obj.ObjType == "switch")
                {
                    obj.Texture = sprSwitch;
                }
                else if (obj.ObjType == "door")
                {
                    obj.Texture = sprDoor;
                }
                else //key
                {
                    obj.Texture = sprKey;
                }
            }

            foreach (Hazard obj in manager.Hazards) //hazards
            {
                if (obj.ObjType == "pitfall")
                {
                    obj.Texture = sprPitfall;
                }
                else if (obj.ObjType == "shock")
                {
                    obj.Texture = sprShock;
                }
                else //steam
                {
                    obj.Texture = sprSteam;
                }
            }
        }
       
        protected override void LoadContent() //loads all content to be used
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //menuMusic = Content.Load<Song>("Song/BOAT.mp3");
           // MediaPlayer.Play(menuMusic);
            //textures for the menus
            background = this.Content.Load<Texture2D>("background.jpg");
            play = this.Content.Load<Texture2D>("play.png");
            options = this.Content.Load<Texture2D>("options.png");
            exit = this.Content.Load<Texture2D>("exit.png");
            instructions = this.Content.Load<Texture2D>("Instructions.png");
            pauseInstrutctions = this.Content.Load<Texture2D>("defualtpauseoptions.png");
            pauseBackground = this.Content.Load<Texture2D>("pause.png");
            pauseMenu = this.Content.Load<Texture2D>("pausedText.png");

            // TODO: use this.Content to load your game content here
            sprPlayer = this.Content.Load<Texture2D>("Player.png");
            playerleft = this.Content.Load<Texture2D>("LeftStill.png");
            playertopleft = this.Content.Load<Texture2D>("TopLeftStill.png");
            playertopright = this.Content.Load<Texture2D>("TopRightSTill.png");
            playerbotRight = this.Content.Load<Texture2D>("BotRightStill.png");
            playerbotleft = this.Content.Load<Texture2D>("BotLeftStill");
            playerDown = this.Content.Load<Texture2D>("BotStill.png");
            playerup = this.Content.Load<Texture2D>("TopStill.png");
            playerLeftWalking1 = this.Content.Load<Texture2D>("LeftWalking1");
            playerLeftWalking2 = this.Content.Load<Texture2D>("LeftWalking2");


            sprBookcase = this.Content.Load<Texture2D>("bookcase.png"); //1
            sprDoor = this.Content.Load<Texture2D>("floor.png");         //2
            sprDresser = this.Content.Load<Texture2D>("dresser.png");   //3
            sprFloor = this.Content.Load<Texture2D>("floor2.png");       //4
            sprKey = this.Content.Load<Texture2D>("key.png");           //5
            sprPitfall = this.Content.Load<Texture2D>("pitfall.png");   //6
            sprRuble = this.Content.Load<Texture2D>("ruble.png");       //7
            sprShock = this.Content.Load<Texture2D>("shock.png");       //8
            sprStairs = this.Content.Load<Texture2D>("stairs.png");     //9
            sprSteam = this.Content.Load<Texture2D>("steam.png");       //10
            sprSwitch = this.Content.Load<Texture2D>("switch.png");     //11  
            sprValve = this.Content.Load<Texture2D>("valve.png");       //12
            sprWall = this.Content.Load<Texture2D>("wall.png");         //13

            waterTex = this.Content.Load<Texture2D>("rectangle.jpg");

            sF = this.Content.Load<SpriteFont>("TimesNewRoman12");
           
            //set texture of player
            ply.Texture = sprPlayer;
           
          
                 
        }

        protected override void UnloadContent() //unloads content needed for the game (dont think we'll use)
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) //runs through game logic and updates them
        {
            //quit game if you hit escape key or back button on controller
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            
            kbState = Keyboard.GetState();
                
            // TODO: Add your update logic here----------------

            //run the games input processing method
            UpdateGameState();
            
            //check gamestate
            if (gS == GameState.MainMenu) //we are at main menu
            {
                
                //check which button is selected
                mM.Selected();

                //reset the water level
                count = 0;
            }

            if (gS == GameState.Game)
            {
                count++;

                if (count == 10)
                {
                    water.Width++;
                    count = 0;
                }

                if (water.Width >= GraphicsDevice.Viewport.Width)
                {
                    gS = GameState.EndGame;
                }
            }

            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) //draw the game 
        {
            //set window BG
            GraphicsDevice.Clear(Color.SlateBlue);

            spriteBatch.Begin();

            if (gS == GameState.MainMenu)
            {
                mM.Draw(spriteBatch, background, play, options, exit);
            }

            if (gS == GameState.OptionsMenu)
            {
                spriteBatch.Draw(background, topCorner, Color.White);
                spriteBatch.Draw(instructions, topCorner, Color.White);
            }

            if (gS == GameState.PauseMenu)
            {
                spriteBatch.Draw(pauseBackground, topCorner, Color.White);
                spriteBatch.Draw(pauseMenu, topCorner, Color.White);
            }

            if (gS == GameState.PausedOptionsMenu)
            {
                spriteBatch.Draw(pauseBackground, topCorner, Color.White);
                spriteBatch.Draw(pauseInstrutctions, topCorner, Color.White);
            }

            if (gS == GameState.Game)
            {
                //draw manager havin' issues, gonna do it this way. 

                //draw all specials
                foreach (Special obj in manager.Specials)
                {
                    obj.Draw(spriteBatch);
                }

                //draw all the static objects
                foreach (StaticObj obj in manager.StaticObjs)
                {
                    obj.Draw(spriteBatch);
                }
               

                //draw all the hazards
                foreach (Hazard obj in manager.Hazards)
                {
                    //check if its active
                    if(obj.Active)
                    {
                        //only draw if it is
                        obj.Draw(spriteBatch);
                    }
                }

                //draw all the items
                foreach (Item obj in manager.Items)
                {
                    obj.Draw(spriteBatch);
                }

                //draw ply last so its on top
                ply.Draw(spriteBatch);

                spriteBatch.Draw(waterTex, water, Color.Blue);
            }

            if (gS == GameState.EndGame)
            {
                int floor = manager.CurrentLevel + 1;
                int max = manager.Level;

                spriteBatch.Draw(background, topCorner, Color.White);
                spriteBatch.DrawString(sF, "Game Over", new Vector2(180, 200), Color.White);
                spriteBatch.DrawString(sF, "The current water level was: " + water.Width, new Vector2(180, 230), Color.White);
                spriteBatch.DrawString(sF, "The highest deck you reached was: " + max, new Vector2(180, 260), Color.White);
                spriteBatch.DrawString(sF, "You're currently on deck " + floor, new Vector2(180, 290), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public enum GameState
        {
            MainMenu,
            OptionsMenu,
            PausedOptionsMenu,
            Game,
            PauseMenu,
            Loading,
            EndGame
        }
    }
}
