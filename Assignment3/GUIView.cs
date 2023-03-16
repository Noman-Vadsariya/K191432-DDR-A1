﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Assignment3
{
    public partial class frmMazeSolver : Form
    {
        MazeConsts MC = new MazeConsts();
        MazeSolver M = new MazeSolver();
        
        private List<Button> btnList = new List<Button>();

        public frmMazeSolver()
        {
            InitializeComponent();

            Font buttonFont = new Font("Arial", 8);
            this.SuspendLayout();
            for (int rowIndex = 0; rowIndex < MC.SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < MC.SIZE; ++colIndex)
                {
                    Button btn = new Button();
                    btn.Name = string.Format("btn{0}_{1}", rowIndex, colIndex);
                    btn.Parent = pnlParent;
                    btn.Location = new Point(colIndex * MC.SIZE,rowIndex * MC.SIZE);
                    btn.Size = new Size(MC.SIZE, MC.SIZE);
                    btn.Text = "";
                    btn.Font = buttonFont.Clone() as Font;
                    btn.Enabled = false;
                    btnList.Add(btn);
                }
            this.ResumeLayout();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int pos = 0;
            this.M.GenerateMaze();

            for( int rowIndex = 0; rowIndex < MC.SIZE; ++ rowIndex )
                for (int colIndex = 0; colIndex < MC.SIZE; ++colIndex)
                {
                    if (pos == MC.START_POS)
                    {
                        btnList[pos].Text = "S";
                    }

                    else if (pos == MC.END_POS)
                    {
                        btnList[pos].Text = "E";
                    }

                    else
                    {
                        btnList[pos].Text = "";
                        MazeSolver.state num = this.M.states[rowIndex,colIndex];
                        if (num == MazeSolver.state.Hurdle)
                        {
                            btnList[pos].BackColor = Color.Black;
                        }
                        else
                        {
                            btnList[pos].BackColor = SystemColors.Control;
                        }
                    }
                    pos++;
                }
            btnSolve.Enabled = true;
            lblProgress.Text = "Generated";
        }

        void ShowState(int position, MazeSolver.state newState)
        {
            Button btn = btnList[position];
            switch (newState)
            {
                case MazeSolver.state.Backtracked:
                    btn.BackColor = SystemColors.ControlDark;
                    btn.Text = "B";
                    break;
                case MazeSolver.state.TraversedToEast:
                    btn.Text = "\u2192";
                    break;
                case MazeSolver.state.TraversedToWest:
                    btn.Text = "\u2190";
                    break;
                case MazeSolver.state.TraversedToNorth:
                    btn.Text = "\u2191";
                    break;
                case MazeSolver.state.TraversedToSouth:
                    btn.Text = "\u2193";
                    break;
            }

            Application.DoEvents();
            Thread.Sleep(200);
        }

        void SetState(int position, MazeSolver.state newState)
        {
            // convert the current pos into row and col index;
            int rowIndex = position / MC.SIZE;
            int colIndex = position % MC.SIZE;

            this.M.states[rowIndex, colIndex] = newState;
            ShowState(position, newState);
        }

        MazeSolver.state GetStateFromDirection(MazeSolver.dir direction)
        {
            switch (direction)
            {
                case MazeSolver.dir.East: return MazeSolver.state.TraversedToEast;
                case MazeSolver.dir.West: return MazeSolver.state.TraversedToWest;
                case MazeSolver.dir.North: return MazeSolver.state.TraversedToNorth;
                case MazeSolver.dir.South: return MazeSolver.state.TraversedToSouth;
            }
            return MazeSolver.state.NoState;
        }

        void MoveNextPos(int currentPos, int nextPos, MazeSolver.dir direction)
        {
            MazeSolver.state currentState = this.M.states[currentPos / MC.SIZE, currentPos % MC.SIZE];
            MazeSolver.state nextState = this.M.states[nextPos / MC.SIZE, nextPos % MC.SIZE];

            if (nextState == MazeSolver.state.Blank || nextState == MazeSolver.state.End)
            {
                SetState(currentPos, GetStateFromDirection(direction));
            }
            if (nextState == MazeSolver.state.TraversedToNorth || nextState == MazeSolver.state.TraversedToSouth
                || nextState == MazeSolver.state.TraversedToEast || nextState == MazeSolver.state.TraversedToWest)
                SetState(currentPos, MazeSolver.state.Backtracked);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            lblProgress.Text = "Solving...";
            Application.DoEvents();
            int currentPos = MC.START_POS;
            while (currentPos != MC.END_POS)
            {
                MazeSolver.dir direction = MazeSolver.dir.NA;
                int nextPos = M.GetAvailablePos(currentPos, out direction);
                if (nextPos == -1)
                {
                    lblProgress.Text = "No solution!";
                    MessageBox.Show("No solution!");
                    break;
                }
                MoveNextPos(currentPos, nextPos, direction);
                currentPos = nextPos;
            }

            if (currentPos == MC.END_POS)
            {
                lblProgress.Text = "Solved!";
                MessageBox.Show("Solved!");
            }
            btnSolve.Enabled = false;
        }
        }
}
