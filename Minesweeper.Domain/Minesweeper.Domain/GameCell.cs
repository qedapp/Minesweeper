using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Domain
{
    public class GameCell
    {
        private bool isBomb;
        private bool hasBeenVisited;
            public bool IsExplodedBomb { get; set; }
        public bool IsPlayerHere { get; set; }

        public GameCell()
        {
            isBomb = false;
            hasBeenVisited = false;
        }

        public bool GetIsBomb()
        {
            return isBomb;
        }

        public void SetIsBomb(bool value)
        {
            isBomb = value;
        }


        public bool GetHasBeenVisited()
        {
            return hasBeenVisited;
        }

        public void SetHasBeenVisited(bool value)
        {
            hasBeenVisited = value;
        }
    }
}
