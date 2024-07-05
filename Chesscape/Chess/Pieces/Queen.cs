using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Queen : Piece
    {
        //TODO: Implement queen
        public Queen(bool isWhite) : base(isWhite)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\w_queen.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\b_queen.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW)
                :
                Image.FromFile(fullPathB);
        }

        public override string ToString()
        {
            return White ? "Q" : "q";
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        public override Image GetImageT()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\t_queen.png"));
            return Image.FromFile(fullPathT);
        }

        public override void SetFile(char file)
        {
            throw new NotImplementedException();
        }

        public override void SetRank(int rank)
        {
            throw new NotImplementedException();
        }

        public override void SetAddFile()
        {
            throw new NotImplementedException();
        }

        public override string FENNotation()
        {
            return White ? "Q" : "q";

        }

        public override void SetAddRank()
        {
            throw new NotImplementedException();
        }

        public override void SetPieceSet(string directive)
        {
            string wd = Directory.GetCurrentDirectory();

            PieceImage = White ? Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\w_queen.png")))
                        :
                        Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\b_queen.png")));
        }
    }
}
