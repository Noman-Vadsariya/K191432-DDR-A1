using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public class MazeConsts
    {
        public enum state
        {
            Start,
            End,
            Blank,
            Hurdle,
            //Traversed,
            TraversedToSouth,
            TraversedToNorth,
            TraversedToEast,
            TraversedToWest,
            Backtracked,
            NoState
        };

        public enum dir
        {
            North,
            South,
            West,
            East,
            NA
        };

        public int SIZE { get; set; }
        public int START_POS { get; set; }
        public int END_POS { get; set; }

        public MazeConsts(int SIZE=20, int START_POS=0, int END_POS = 399)
        {
            this.SIZE = SIZE;
            this.START_POS = START_POS;
            this.END_POS = END_POS;
        }
    }
}
