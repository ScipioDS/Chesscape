using Chesscape.Chess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesscape
{
    public partial class Promotion : Form
    {
        public Piece piece { get; set; }
        public Promotion()
        {
            InitializeComponent();
            piece = null;
        }

        private void queen_btn_Click(object sender, EventArgs e)
        {
            piece = new Queen(true);
            DialogResult = DialogResult.OK;
        }

        private void bishop_btn_Click(object sender, EventArgs e)
        {
            piece = new Bishop(true);
            DialogResult = DialogResult.OK;
        }

        private void rook_btn_Click(object sender, EventArgs e)
        {
            piece = new Rook(true);
            DialogResult = DialogResult.OK;
        }

        private void knight_btn_Click(object sender, EventArgs e)
        {
            piece = new Knight(true);
            DialogResult = DialogResult.OK;
        }

        private void Promotion_Load(object sender, EventArgs e)
        {

        }
    }
}
