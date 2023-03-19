using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public interface MazeSolvingBehavior
    {
        int SolveMaze(int currentPos, MazeConsts.state[,] states, out MazeConsts.dir dir);
    }
}
