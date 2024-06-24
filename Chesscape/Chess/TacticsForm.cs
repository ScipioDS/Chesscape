﻿using Chesscape.Chess.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Chesscape.Chess
{
    public partial class TacticsForm : Form
    {

        private Board board;
        private Puzzle.Puzzle currentPuzzle {  get; set; }

        public TacticsForm(Puzzle.Puzzle puzzle)
        {
            InitializeComponent();
            DoubleBuffered = true;

            Square.SetFileTranslation();
            board = Board.GetInstance();
            board.SetPerspective(true);
            board.SetBoard(puzzle.getFEN());
            board.PreviousSetup = FEN.ToFEN(board.Squares);
            board.SetPuzzle(puzzle);
            board.setForm(this);
            this.currentPuzzle = puzzle;
            Invalidate();
        }

        private void TacticsForm_Paint(object sender, PaintEventArgs e)
        {
            board.DrawAllComponents(e.Graphics);
        }

        private void TacticsForm_Load(object sender, System.EventArgs e)
        {
            Trajectories.PreloadIllegalOfBlack();
        }

        private void TacticsForm_MouseDown(object sender, MouseEventArgs e)
        {
            board.Select(e.Location);
        }
        private void TacticsForm_MouseUp(object sender, MouseEventArgs e)
        {
            board.MakeMove(e.Location);

            // TODO CHECK MOVES => IF CORRECT DialogResult.Yes / ELSE IF TOO MANY MOVES DialogResult.No
            Invalidate();
        }
        private void TacticsForm_MouseMove(object sender, MouseEventArgs e)
        {
            board.Cursor = e.Location;
            Invalidate();
        }
        public void updateMoves()
        {
            lb1.Items.Clear();
            List<string> moves=currentPuzzle.getpastmoves();
            foreach(string s in moves)
            {
                lb1.Items.Add(s);
            }
        }

        private void timerforBlackMove_Tick(object sender, EventArgs e)
        {

        }
    }
}
