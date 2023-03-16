using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public interface MazeSolvingBehavior
    {
        int SolveMaze(int currentPos, MazeSolver.state[,] states, out MazeSolver.dir dir);
    }
}
