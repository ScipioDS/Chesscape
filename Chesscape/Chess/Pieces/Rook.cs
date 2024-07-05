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

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\w_rook.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\b_rook.png"));

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

        public override void Refresh()
        {
            this.addFile = false;
            this.addRank= false;
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

        public override void SetFile(char file)
        {
            this.File= file;
        }

        public override void SetRank(int rank)
        {
            this.Rank = rank;
        }

        public override void SetAddFile()
        {
            this.addFile = true;
        }

        public override void SetAddRank()
        {
            this.addRank = true;
        }

        public override string FENNotation()
        {
            return White ? "R" : "r";
        }

        public override void SetPieceSet(string directive)
        {
            string wd = Directory.GetCurrentDirectory();

            PieceImage = White ? Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\w_rook.png")))
                        :
                        Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\b_rook.png")));
        }
    }
}
