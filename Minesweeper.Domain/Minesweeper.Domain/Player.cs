using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public class Player
    {
        private int numMoves;
        private int numBombsHit;
        private Point currentLocation;
        private int livesLeft;

        public Player(int numLives, int row, int col)
        {
            livesLeft = numLives;
            currentLocation = new Point(col, row);
            numMoves = 0;
        }

        public int GetLivesLeft()
        {
            return livesLeft;
        }

        public int DecNumLives()
        {
            livesLeft--;
            return livesLeft;
        }

        public int GetNumMoves()
        {
            return numMoves;
        }

        public void SetNumMoves(int value)
        {
            numMoves = value;
        }

        public void IncNumMoves()
        {
            numMoves++;
        }


        public int GetNumBombsHit()
        {
            return numBombsHit;
        }

        public void SetNumBombsHit(int value)
        {
            numBombsHit = value;
        }

        public void IncNumBombsHit()
        {
            numBombsHit++;
        }


        public Point GetcurrentLocation()
        {
            return currentLocation;
        }

        public void SetcurrentLocation(Point value)
        {
            currentLocation = value;
        }
    }
}
