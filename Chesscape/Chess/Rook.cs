using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Rook : Piece, ICastleable
    {

        private bool _Moved;

        public Rook(bool isWhite) : base(isWhite)
        {
            _Moved = false;

            string currentDirectory = Directory.GetCurrentDirectory();

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\w_rook.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\b_rook.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW)
                :
                Image.FromFile(fullPathB);
        }

        public override string ToString()
        {
            return White ? "R" : "r";
        }

        public bool Moved()
        {
            return _Moved;
        }

        public void MakeIncastleable()
        {
            _Moved = true;
        }

        public override Image GetImageT()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\t_rook.png"));
            return Image.FromFile(fullPathT);
        }
    }
}
