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
    public partial class Form2 : Form
    {
        Grid grid;

        //The constructor takes the grid in Form1 as a parameter so it can save it
        public Form2(Grid g)
        {
            InitializeComponent();
            grid = g;
        }

        public void Save(int roomNumber)
        {
            //saves the current room to a file with the specified number
            string name = "room_" + roomNumber + ".txt";
            System.IO.StreamWriter output = new System.IO.StreamWriter(name);
            //Writes the contents of every cell of the grid to the file
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    output.Write(grid.cell[i, j]);
                }
                //Enters a new line after each row
                output.WriteLine("");
            }
            output.Close();
            //Shows a message to tell the user the file was successfully saved
            MessageBox.Show("Room saved");
        }

        //Checks if the number entered is valid, then closes the form
        private void button1_Click(object sender, EventArgs e)
        {
            string number = textBox1.Text.Trim();
            try
            {
                int num = int.Parse(number);
                Save(num);
                this.Close();
            }
            catch (Exception ex)
            {
                label1.Text = "Enter a NUMBER:";
            }
        }
    }
}
