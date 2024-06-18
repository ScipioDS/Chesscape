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
            addFile = false;
            addRank = false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(White ? "R" : "r");
            if (this.addRank)
            {
                sb.Append(this.Rank);
                this.addRank = false;
            }
            else if (this.addFile) { 
                sb.Append(this.File);
                this.addFile = false;
            }
            return sb.ToString();
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

        public override void setFile(char file)
        {
            this.File= file;
        }

        public override void setRank(int rank)
        {
            this.Rank = rank;
        }

        public override void setAddFile()
        {
            this.addFile = true;
        }

        public override void setAddRank()
        {
            this.addRank = true;
        }
    }
}
