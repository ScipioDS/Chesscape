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
        public static PictureBox[][] picBoxes = new PictureBox[8][];

        public TacticsForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Square.SetFileTranslation();
            board = Board.GetInstance();
            board.SetPerspective(true);
            board.SetBoard("1R6/2bnQP1K/br1N1BP1/nPkp1P2/2p1P1P1/4Ppqp/p1r1ppp1/1PNR3B w - - 0 1");

            Debug.WriteLine(board);

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
        }
        private void TacticsForm_MouseUp(object sender, MouseEventArgs e)
        {
            board.SelectedPiece = null;
        }
        private void TacticsForm_MouseMove(object sender, MouseEventArgs e)
        {
            board.Cursor = e.Location;
            Invalidate();
        }
    }
}
