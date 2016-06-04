using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace TuneSquad_Shipwrecked
{
    

    class Manager
    {
        //fields
        private List<Item> items;
        private List<StaticObj> staticObjs;
        private List<Hazard> hazards;
        private List<Special> specials;

        private int rowStart;
        private int columnStart;

        private int direction;
        private int lastDirection;

        private int level;
        private int currentLevel;

        private String levelRooms;
        private List<String> levels;

        private bool lastRoom;

        //constrctor
        public Manager()
        {
            //make our lists
            items = new List<Item>();
            staticObjs = new List<StaticObj>();
            hazards = new List<Hazard>();
            specials = new List<Special>();

            //set rowstart and columnstart 
            rowStart = 0;
            columnStart = 0;

            //set level junk
            level = 0;
            currentLevel = level;
            levelRooms = "";
            levels = new List<String>();

            //preset last firection so it can be found if its the first run
            lastDirection = -1;
        }

        //properties
        public List<Item> Items
        { 
            get { return items; } 
        }

        public List<StaticObj> StaticObjs
        { 
            get { return staticObjs; } 
        }

        public List<Hazard> Hazards
        {
            get { return hazards; }
        }

        public List<Special> Specials
        {
            get { return specials; }
        }

        public int Level
        {
            get { return level; }
            set 
            {
                level = value;
            }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                currentLevel = value;
            }
        }

        //methods
        public void NextLevel()
        {
            //reset row and column start
            rowStart = 0;
            columnStart = 0;

            //havent made any rooms yet so lastroom is false
            lastRoom = false;

            //create our rooms based on level
            int rooms = level + 3; //each level will have 1 more room than the last
            String filepath = (@"../TuneSquad_Shipwrecked/Assets/Rooms/" + RoomSelector()); //@ symbol so it knows this is a filepath
            for (int i = 0; i < rooms; i++)
            {
                //check if were on the lastRoom
                if(( i + 1) == rooms)
                {
                    //we are on the last room
                    lastRoom = true;
                }

                //load in the newest room
                LoadRoom(level, filepath);
            }

            //save the current level
            levels.Add(levelRooms);

            //update level count and reset lasTDirection
            lastDirection = -1;
            level++;
        }

        public void LastLevel()
        {
            //reset lastroom
            lastRoom = false;

            //reset row and column start
            rowStart = 0;
            columnStart = 0;

            //set a level to request
            int lvl = currentLevel;

            //get the data from the requested level
            String line = levels[lvl];

            //split the data for the requested level
            String[] lvlData = line.Split('-');

            //create rooms based on the data in the array
            int rooms = level + 3; //each level will have 1 more room than the last
            for (int i = 0; i < lvlData.Length; i++)
            {
                //check if were on the lastRoom
                if ((i + 1) == rooms)
                {
                    //we are on the last room
                    lastRoom = true;
                }

                LoadRoom(lvl, lvlData[i]); //load in a room
            }

        }

        private String RoomSelector()
        {
            //premake our return string
            String room;

            //randomize a room number
            Random rand = new Random();
            int roomNum = rand.Next(0, 15);

            //give room number to our room string
            room = "room_" + roomNum + ".txt";

            //update which rooms are in a level
            levelRooms = levelRooms + "-" + room;

            //return the selected room
            return room;
        }

        private void LoadRoom(int lvl, String data) 
        {
            String filepath = data; //setup where our reder will get data

            //load a file of our map
            StreamReader reader;
            try
            {
                reader = new StreamReader(filepath);
            }
            catch(Exception)
            {
                //path was wrong we will work with a null map then
                reader = new StreamReader(RoomSelector()); //use the ones in bin
                //reader = null;
            }
            
            //split each line of the file into a list
            String line = null;
            List<String> rows = new List<String>();

            while((line = reader.ReadLine()) != null) //loop through file by line
            {
                rows.Add(line);
            }

            //run through each row
            String column = null;

            //chose directions to build, right or down
            Random rand = new Random();
            direction = rand.Next(0,2); //0 or 1
            
            for (int x = 0; x < rows.Count; x++) //run through each row
            {
                //get string of current row
                column = rows[x];

                for (int i = 0; i < column.Length; i++) //run through each char in the row
                {
                    //get char
                    char c = column[i]; //strings already have an index so this works

                    //send row, column, and character to the update list method
                    UpdateList(x, i, c);
                }
            }

            //close stream
            reader.Close();

            //update start pos of rows and columns
            NextRoomLoc(rows.Count, column.Length);
        }

        private void NextRoomLoc(int row, int column)
        {
            //save last direction
            lastDirection = direction;

            //updaet rows and collumns for sprite sizes
            row = row * Game1.spriteHeight;
            column = column * Game1.spriteWidth;

            if(direction == 0)
            {
                //build right
                rowStart = rowStart + row;
            }
            else
            {
                //build down
                columnStart = columnStart + column;
            }
        }

        private void UpdateList(int row, int column, char c)
        {
            //save row and column for future use
            int x = row;
            int y = column;

            //update rows and columns to refelct the size of the sprite
            row = row * Game1.spriteHeight;
            column = column * Game1.spriteWidth;

            //update row and column if other rooms have been loaded before
            row += rowStart;
            column += columnStart;

            //figure out if the worldObj is static, an item, or a hazard
            if(c == 'W' || c == 'F' || c == 'R') //static
            {

                //create our static object
                StaticObj staticObject = new StaticObj(column, row);

                //setup static with proper type
                //note: wanted to use enums here, but was having trouble so im using a string property
                switch(c)
                {
                    case 'W':
                        staticObject.ObjType = "wall";
                        break;
                    case 'F':
                        staticObject.ObjType = "dresser";
                        break;
                    case 'R':
                        staticObject.ObjType = "ruble";
                        break;
                }

                //add to our list
                staticObjs.Add(staticObject);
            }
            else if(c == 'B' || c == 'V' || c == 'S' || c == 'D' || c == 'K') //items
            {
                //create our item object
                Item item = new Item(column, row, 0);

                //setup item with proper type
                //note: wanted to use enums here, but was having trouble so im using a string property
                switch (c)
                {
                    case 'B':
                        item.ObjType = "bookcase";
                        break;
                    case 'V':
                        item.ObjType = "valve";
                        break;
                    case 'S':
                        item.ObjType = "switch";
                        break;
                    case 'D':
                        item.ObjType = "door";
                        break;
                }
                
                //add to our list
                items.Add(item);

                //build floor under it
                Special spec = new Special(column, row);
                spec.ObjType = "floor";
                specials.Add(spec); //add floor to list
            }
            else if(c == 'P' || c == 'Z' || c == 'T') //hazards
            {
                //create our item object
                Hazard hazard = new Hazard(column, row);

                //setup hazard with proper type
                //note: wanted to use enums here, but was having trouble so im using a string property
                switch (c)
                {
                    case 'P':
                        hazard.ObjType = "pitfall";
                        break;
                    case 'Z':
                        hazard.ObjType = "shock";
                        break;
                    case 'T':
                        hazard.ObjType = "steam";
                        break;
                }

                //add to our list
                hazards.Add(hazard);

                //build floor under it
                Special spec = new Special(column, row);
                spec.ObjType = "floor";
                specials.Add(spec); //add floor to list
            }
            else if(c == '-' || c == 'X') //the floor
            {
                
                //setup special with proper type
                //note: wanted to use enums here, but was having trouble so im using a string property
                switch (c)
                {
                    case '-':
                        //create our special object
                        Special spec = new Special(column, row);

                        spec.ObjType = "floor";

                        //add to our list
                        specials.Add(spec);
                        break;

                    case 'X':
                        //check cases for what X could be 
                        if(lastDirection == -1) //frist run through ------------------------------------
                        {
                            if(y == 0 || x == 0) //top 2 X cases
                            {
                                //create our static object
                                StaticObj staticObject = new StaticObj(column, row);

                                //give objType
                                staticObject.ObjType = "wall";

                                //add too list
                                staticObjs.Add(staticObject);
                            }
                            else if(direction == 0) //right side build dir
                            {
                                if(x == 15) //right side x case
                                {
                                    //create our special object
                                    Special special = new Special(column, row);

                                    //floor so you cna leave
                                    special.ObjType = "floor";

                                    //add to our list
                                    specials.Add(special);
                                }
                                else //bottom x case
                                {
                                    //create our static object
                                    StaticObj staticObject = new StaticObj(column, row);

                                    //give objType
                                    staticObject.ObjType = "wall";

                                    //add too list
                                    staticObjs.Add(staticObject);
                                }
                            }
                            else //down build dir
                            {
                                if (x == 15) //right side x case
                                {
                                    //create our static object
                                    StaticObj staticObject = new StaticObj(column, row);

                                    //give objType
                                    staticObject.ObjType = "wall";

                                    //add too list
                                    staticObjs.Add(staticObject);
                                }
                                else //bottom x case
                                {
                                    //create our special object
                                    Special special = new Special(column, row);

                                    //floor so you cna leave
                                    special.ObjType = "floor";

                                    //add to our list
                                    specials.Add(special);
                                }
                            }
                        }
                        else if(lastDirection == 0) //---------------------------------------------------
                        {
                            if(y == 0)
                            {
                                //create our static object
                                StaticObj staticObject = new StaticObj(column, row);

                                //give objType
                                staticObject.ObjType = "wall";

                                //add too list
                                staticObjs.Add(staticObject);
                            }
                            else if(x == 0)
                            {
                                //create our special object
                                Special special = new Special(column, row);

                                //floor so you can enter
                                special.ObjType = "floor";

                                //add to our list
                                specials.Add(special);
                            }
                            else if (lastRoom)
                            {
                                //create our special object
                                Special special = new Special(column, row);

                                //floor so you can enter
                                special.ObjType = "stairs";

                                //add to our list
                                specials.Add(special);
                            }
                            else if (direction == 0) //right side build dir
                            {
                                if (x == 15) //right side x case
                                {
                                    //create our special object
                                    Special special = new Special(column, row);

                                    //floor so you cna leave
                                    special.ObjType = "floor";

                                    //add to our list
                                    specials.Add(special);
                                }
                                else //bottom x case
                                {
                                    //create our static object
                                    StaticObj staticObject = new StaticObj(column, row);

                                    //give objType
                                    staticObject.ObjType = "wall";

                                    //add too list
                                    staticObjs.Add(staticObject);
                                }
                            }
                            else //down build dir
                            {
                                if (x == 15) //right side x case
                                {
                                    //create our static object
                                    StaticObj staticObject = new StaticObj(column, row);

                                    //give objType
                                    staticObject.ObjType = "wall";

                                    //add too list
                                    staticObjs.Add(staticObject);
                                }
                                else //bottom x case
                                {
                                    //create our special object
                                    Special special = new Special(column, row);

                                    //floor so you cna leave
                                    special.ObjType = "floor";

                                    //add to our list
                                    specials.Add(special);
                                }
                            }
                        }
                        else if(lastDirection == 1) //build down -------------------------------------------------------
                        {
                            if (y == 0)
                            {
                                //create our special object
                                Special special = new Special(column, row);

                                //floor so you can enter
                                special.ObjType = "floor";

                                //add to our list
                                specials.Add(special);
                            }
                            else if (x == 0)
                            {
                                //create our static object
                                StaticObj staticObject = new StaticObj(column, row);

                                //give objType
                                staticObject.ObjType = "wall";

                                //add too list
                                staticObjs.Add(staticObject);  
                            }
                            else if (lastRoom)
                            {
                                //create our special object
                                Special special = new Special(column, row);

                                //floor so you can enter
                                special.ObjType = "stairs";

                                //add to our list
                                specials.Add(special);
                            }
                            else if (direction == 0) //right side build dir
                            {
                                if (x == 15) //right side x case
                                {
                                    //create our special object
                                    Special special = new Special(column, row);

                                    //floor so you cna leave
                                    special.ObjType = "floor";

                                    //add to our list
                                    specials.Add(special);
                                }
                                else //bottom x case
                                {
                                    //create our static object
                                    StaticObj staticObject = new StaticObj(column, row);

                                    //give objType
                                    staticObject.ObjType = "wall";

                                    //add too list
                                    staticObjs.Add(staticObject);
                                }
                            }
                            else //down build dir
                            {
                                if (x == 15) //right side x case
                                {
                                    //create our static object
                                    StaticObj staticObject = new StaticObj(column, row);

                                    //give objType
                                    staticObject.ObjType = "wall";

                                    //add too list
                                    staticObjs.Add(staticObject);
                                }
                                else //bottom x case
                                {
                                    //create our special object
                                    Special special = new Special(column, row);

                                    //floor so you cna leave
                                    special.ObjType = "floor";

                                    //add to our list
                                    specials.Add(special);
                                }
                            }
                        }
                    break;
                }
            }

            
        }

        public List<WorldObj> Cast(String type)
        {
            //make list of world objs
            List<WorldObj> objs = new List<WorldObj>();

            //check which type to cast into world obj
            if(type == "items")
            {
                foreach(Item obj in items)
                {
                    objs.Add(obj);
                }
            }
            else if(type == "staticObjs")
            {
                foreach (StaticObj obj in staticObjs)
                {
                    objs.Add(obj);
                }
            }
            else if(type == "hazards")
            {
                foreach (Hazard obj in hazards)
                {
                    objs.Add(obj);
                }
            }
            else
            {
                foreach (Special obj in specials)
                {
                    objs.Add(obj);
                }
            }

            //return the list
            return objs;
        }
    }
}
