using Minesweeper.Domain;
using NUnit.Framework;

namespace Tests
{
    public class GridTests
    {

        private int numRows = 8;
        private int numCols = 10;
        private int numBombs = 10;

        private Game game;

        [SetUp]
        public void Setup()
        {
            game = new Game(numRows, numCols, numBombs);
        }

        [Test]
        public void TestForGridCreated()
        {

            Assert.AreEqual(numRows, game.Rows);
            Assert.AreEqual(numCols, game.Columns);

         }

        [Test]
        public void TestForNumberOfBombs()
        {


            int row, col;

            var countBombs = 0;

            for (row = 1; row <= numRows; row++)
            {
                for (col = 1; col <= numCols; col++)
                {
                    if (game.GameGrid.grid[row - 1, col -1].GetIsBomb())
                    {
                        countBombs++;
                    }
                }
            }

            Assert.AreEqual(numBombs, countBombs);
        }





    }
}