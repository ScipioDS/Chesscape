using Chesscape.Chess.Internals;
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

        public TacticsForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Square.SetFileTranslation();
            board = Board.GetInstance();
            board.SetPerspective(true);
            board.SetBoard("rnbqk1nr/pppp1ppp/8/4p3/1b2P3/3P4/PPP2PPP/RNBQKBNR w KQkq - 1 3");
            board.PreviousSetup = FEN.ToFEN(board.Squares);

            var ELO = new ELO(1500, 1400, true);
            Debug.WriteLine(ELO.calculatePlayerELO());

            Invalidate();
        }

        private void TacticsForm_Paint(object sender, PaintEventArgs e)
        {
            board.DrawAllComponents(e.Graphics);
        }

        private void TacticsForm_Load(object sender, System.EventArgs e)
        {

        }

        private void TacticsForm_MouseDown(object sender, MouseEventArgs e)
        {
            board.Select(e.Location);
            Invalidate();
        }
        private void TacticsForm_MouseUp(object sender, MouseEventArgs e)
        {
            board.MakeMove(e.Location);
            Invalidate();
        }
        private void TacticsForm_MouseMove(object sender, MouseEventArgs e)
        {
            board.Cursor = e.Location;
            Invalidate();
        }
    }
}
