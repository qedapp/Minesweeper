using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public class Game
    {

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int Bombs { get; set; }

        public GameGrid GameGrid { get; set; }

        public Player Player { get; set; }

        public Game(int rows, int columns, int bombs = 10)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Bombs = bombs;

            this.GameGrid = new GameGrid(rows, columns);
            
            AddBombs(bombs);
            AddPlayer();

        }

        private void AddPlayer()
        {
            Random rand = new Random();
            int row = rand.Next(1, this.Rows);
            int col = 1;

            while (this.GameGrid.grid[row -1, col - 1].GetIsBomb())
            {
                row = rand.Next(1, this.Rows);
            }

            this.GameGrid.AddPlayer(row, col);

            this.Player = new Player(5, row, col);


        }

        private void AddBombs(int bombs)
        {

            this.GameGrid.AddBombs(bombs);

        }

        public GameCell GetCellAtXY(int y, int x)
        {
            return this.GameGrid.grid[x - 1, y - 1];
        }

        public bool isComplete { get; set; }
        public bool IsOver { get; set; }

        public void End()
        {
            IsOver = true;
        }


    }
}
