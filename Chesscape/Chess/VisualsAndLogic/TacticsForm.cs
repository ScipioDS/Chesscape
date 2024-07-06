using Chesscape.Chess.Internals;
using Chesscape.Puzzle;
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

        private Puzzle.Puzzle CurrentPuzzle { get; set; }

        public Menu menu;
        private Puzzle.Puzzle currentPuzzle { get; set; }

        public TacticsForm(Puzzle.Puzzle puzzle,Menu menu)
        {
            InitializeComponent();

            DoubleBuffered = true;
            this.menu = menu;
            Square.SetFileTranslation();
            board = Board.GetInstance();

            board.SetSquaresTheme();
            board.SetBoard(puzzle.GetFEN());
            board.PreviousSetup = FEN.ToFEN(board.Squares);
            board.SetPuzzle(puzzle);
            board.SetForm(this);

            cbPieceSet.Items.Add("CBurnett");
            cbPieceSet.Items.Add("Tia");

            CurrentPuzzle = puzzle;

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

            // TODO: CHECK MOVES => IF CORRECT DialogResult.Yes / ELSE IF TOO MANY MOVES DialogResult.No
            Invalidate();
        }
        private void TacticsForm_MouseMove(object sender, MouseEventArgs e)
        {
            board.Cursor = e.Location;
            Invalidate();
        }

        public void UpdateMoves()
        {
            lbDoneMoves.Items.Clear();

            CurrentPuzzle.GetPastMoves()
                .ForEach(puzzle => lbDoneMoves.Items.Add(puzzle));
        }
        public void generate_next()
        {
            Puzzle.Puzzle tmp = menu.generate_next_puzzle();
            board.SetBoard(tmp.GetFEN());
            board.SetPuzzle(tmp);
            CurrentPuzzle = tmp;
            lbDoneMoves.Items.Clear();
        }

        private void bMakeCustomTheme_click(object sender, EventArgs e)
        {
            board.SetColors();
            board.SetSquaresTheme();
            Invalidate();
        }

        private void bResetTheme_click(object sender, EventArgs e)
        {
            board.SetColorsDef();
            board.SetSquaresTheme();
            Invalidate();
        }

        private void cbPieceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbPieceSet.SelectedItem.ToString())
            {
                case "CBurnett": board.ChangePieceSet("cburnett_pieces"); Board.PieceSetDirective = "cburnett_pieces"; break;
                case "Tia": board.ChangePieceSet("tia_pieces"); Board.PieceSetDirective = "tia_pieces"; break;
            }

            Invalidate();
        }
    }
}
