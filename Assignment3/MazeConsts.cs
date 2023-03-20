using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K191432_DDR_A1
{
    /// <summary>
    /// Code Smell : Data Clumps
    /// Refactoring : Same data items are used across multiple classes so using
    /// "Extract Class" to put these data items in a class and turn them into an object.
    /// </summary>
    public class MazeConsts
    {
        public enum state
        {
            Start,
            End,
            Blank,
            Hurdle,
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

        public class NextStep
        {
            public MazeConsts.state newstate;
            public int nextPos;

            public NextStep(MazeConsts.state newstate, int nextPos)
            {
                this.newstate = newstate;
                this.nextPos = nextPos;
            }
        }

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
