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
            lb1.Items.Add("Rook");
            lb1.Items.Add("Queen");
            lb1.Items.Add("Knight");
            lb1.Items.Add("Bishop");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            piece = new Bishop(true);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            piece=new Knight(true);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            piece=new Queen(true);
            DialogResult = DialogResult.OK;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            piece=new Rook(true);
            DialogResult = DialogResult.OK;
        }

        private void pbBishop_MouseClick(object sender, MouseEventArgs e)
        {
            piece=new Bishop(true);
            DialogResult = DialogResult.OK;
        }

        private void pbKnight_MouseClick(object sender, MouseEventArgs e)
        {
            piece = new Knight(true);
            DialogResult = DialogResult.OK;
        }

        private void pbQueen_MouseClick(object sender, MouseEventArgs e)
        {
            piece = new Queen(true);
            DialogResult = DialogResult.OK;
        }

        private void pbRook_MouseClick(object sender, MouseEventArgs e)
        {
            piece=new Rook(true);
            DialogResult = DialogResult.OK;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if(lb1.SelectedItems.Count > 0)
            {
                DialogResult = DialogResult.OK;
                string piecename = lb1.SelectedItems[0] as string;
                if (piecename.Equals("Rook"))
                {
                    piece = new Rook(true);
                }
                else if (piecename.Equals("Bishop"))
                {
                    piece = new Bishop(true);
                }
                else if (piecename.Equals("Queen"))
                {
                    piece=new Queen(true);
                }
                else if (piecename.Equals("Knight"))
                {
                    piece = new Knight(true);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}
