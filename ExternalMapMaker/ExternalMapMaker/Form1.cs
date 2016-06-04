using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExternalMapMaker
{
    public partial class Form1 : Form
    {
        //A grid to store all the data
        Grid room = new Grid();

        //A new thread to run the cursor following method
        private System.Threading.Thread thread;

        //A point that will track the location of the cursor relative to the form
        Point CursorLocation;
        public Form1()
        {
            InitializeComponent();

            //Makes it possible for methods on different threads to communicate
            CheckForIllegalCrossThreadCalls = false;

            //Starts the FollowCursor method on the new thread
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(FollowCursor));
            thread.Start();

            //Hides the label that will follow the cursor
            label17.Visible = false;
        }

        private void FollowCursor()
        {
            //This loop will run as long as the program is running
            bool running = true;
            do
            {
                //Sets a point to the cursor position relative to the top left corner of the form
                Point relativePoint = this.PointToClient(Cursor.Position);

                //Sets that point to another point and changes the first point by a little bit
                //This will allow the label that follows the cursor to not get in the way of the cursor
                Point relativePoint2 = relativePoint;
                relativePoint.X+=2;
                relativePoint.Y+=2;
                //This is the part that finally will make the label track the cursor
                label17.Location = relativePoint;
                //Sets the point in the main thread to the calculated relative position
                CursorLocation = relativePoint2;
            } while (running == true);
        }

        private void UpdateRoom()
        {
            //Updates the text for each label to the corresponding row of characters in the grid

            //Label 1
            string row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[0, i] + " ";
            }
            label1.Text = row;

            //Label 2
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[1, i] + " ";
            }
            label2.Text = row;

            //Label 3
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[2, i] + " ";
            }
            label3.Text = row;

            //Label 4
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[3, i] + " ";
            }
            label4.Text = row;

            //Label 5
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[4, i] + " ";
            }
            label5.Text = row;

            //Label 6
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[5, i] + " ";
            }
            label6.Text = row;

            //Label 7
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[6, i] + " ";
            }
            label7.Text = row;

            //Label 8
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[7, i] + " ";
            }
            label8.Text = row;

            //Label 9
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[8, i] + " ";
            }
            label9.Text = row;

            //Label 10
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[9, i] + " ";
            }
            label10.Text = row;

            //Label 11
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[10, i] + " ";
            }
            label11.Text = row;

            //Label 12
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[11, i] + " ";
            }
            label12.Text = row;

            //Label 13
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[12, i] + " ";
            }
            label13.Text = row;

            //Label 14
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[13, i] + " ";
            }
            label14.Text = row;

            //Label 15
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[14, i] + " ";
            }
            label15.Text = row;

            //Label 16
            row = "";
            for (int i = 0; i < 16; i++)
            {
                row += " " + room.cell[15, i] + " ";
            }
            label16.Text = row;
        }

        //These next methods change the text of the follower label to represent different objects
        private void button16_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  W";
            UpdateRoom();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  F";
            UpdateRoom();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  R";
            UpdateRoom();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  B";
            UpdateRoom();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  V";
            UpdateRoom();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  S";
            UpdateRoom();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  D";
            UpdateRoom();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  K";
            UpdateRoom();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  P";
            UpdateRoom();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  Z";
            UpdateRoom();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  T";
            UpdateRoom();
        }

        //Loads a Form2 to save and prompts the user to check if the level works
        private void button13_Click(object sender, EventArgs e)
        {
            label17.Visible = false;
            MessageBox.Show("Remember to make sure the room is playable before saving!");
            Form2 Number = new Form2(room);
            Number.Show();
        }

        //Clears the room to the default
        private void button14_Click(object sender, EventArgs e)
        {
            label17.Visible = false;
            room.Clear();
            UpdateRoom();
        }
        
        //When the form loads, update the room to the default
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateRoom();
        }

        //Works the same as the other object buttons, - means empty space
        private void button1_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            label17.Text = "  -";
            UpdateRoom();
        }

        //When the grid is clicked, if the floating label is visible, it will put the character in the floating label into the grid based on where the cursor is
        private void panel1_Click(object sender, EventArgs e)
        {
            if (124 < CursorLocation.X && CursorLocation.X < 460 && 82 < CursorLocation.Y && CursorLocation.Y < 466 && label17.Visible == true)
            {
                int x = (CursorLocation.Y - 58) / 24;
                int y = (CursorLocation.X - 100) / 24;
                room.cell[x, y] = label17.Text.Last<char>();
                UpdateRoom();
            }
        }

        
    }
}
