using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public class GameGrid
    {

        public GameCell[,] grid;
        private int numRows;
        private int numCols;

        public GameGrid(int rows, int cols)
        {

            
            this.numCols = cols;
            this.numRows = rows;

            this.grid = new GameCell[rows, cols];

            int i, j;

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols; j++)
                {
                    grid[i, j] = new GameCell();
                }
            }
        }

        public void AddBombs(int numBombs)
        {
            for (int bombs = 0; bombs < numBombs; bombs ++)
            {
                Random rand = new Random();

                int row = rand.Next(0, this.numRows - 1);
                int col = rand.Next(0, this.numCols - 1);

                while (grid[row,col].GetIsBomb() == true)
                {
                    row = rand.Next(0, this.numRows - 1);
                    col = rand.Next(0, this.numCols - 1);
                }

                grid[row, col].SetIsBomb(true);



             }
        }

        public bool CheckForExplosion(int row, int col)
        {
            bool explosion = false;
            if (grid[row - 1, col - 1].GetIsBomb())
            {
                explosion = true;
                grid[row - 1, col - 1].IsExplodedBomb = true;
            }
            return explosion;
        }

        public void AddPlayer(int row, int col)
        {
            grid[row - 1, col - 1].IsPlayerHere = true;
        }

        public void RemovePlayer(int row, int col)
        {
            grid[row - 1, col - 1].IsPlayerHere = false;
        }
    }
}
