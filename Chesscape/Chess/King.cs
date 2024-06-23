using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class King : Piece, ICastleable
    {
        //TODO: Implement king

        private bool _Moved;

        public King(bool isWhite) : base(isWhite)
        {
            _Moved = false;

            string currentDirectory = Directory.GetCurrentDirectory();

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\w_king.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\b_king.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW)
                :
                Image.FromFile(fullPathB);
        }

        public override string FENNotation()
        {
            return White ? "K" : "k";

        }

        public override void refresh()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return White ?  "K" : "k";
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
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\t_king.png"));
            return Image.FromFile(fullPathT);
        }

        public override void setFile(char file)
        {
            throw new NotImplementedException();
        }

        public override void setRank(int rank)
        {
            throw new NotImplementedException();
        }

        public override void setAddFile()
        {
            throw new NotImplementedException();
        }

        public override void setAddRank()
        {
            throw new NotImplementedException();
        }
    }
}
