using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalMapMaker
{
    public class Grid
    {
        //Creates a 16 by 16 grid of chars
        public char[,] cell = new char[16,16];

        //The constructor for the grid clears the grid
        public Grid()
        {
            Clear();
        }

        //Sets the default values for every cell
        public void Clear()
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    //If the cell is on the outer edge it is a wall
                    if (i == 0 || i == 15 || j == 0 || j == 15)
                    {
                        cell[i, j] = 'W';
                    }
                    //Everything else initially becomes an empty space
                    else
                    {
                        cell[i, j] = '-';
                    }
                }
            }
            //Sets 4 specified cells to contain exits
            cell[0, 12] = 'X';
            cell[12, 0] = 'X';
            cell[15, 12] = 'X';
            cell[12, 15] = 'X';
        }
    }
}
