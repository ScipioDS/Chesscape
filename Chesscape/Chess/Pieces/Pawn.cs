using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Pawn : Piece
    {
        public Pawn(bool isWhite) : base(isWhite)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            Debug.WriteLine(currentDirectory);

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\w_pawn.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\b_pawn.png"));
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\t_pawn.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW)
                :
                Image.FromFile(fullPathB);

            TransparentImage = Image.FromFile(fullPathT);
        }

        public override string FENNotation()
        {
            return White ? "P" : "p";

        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return White ? "P" : "p";
        }

        public override Image GetImageT()
        {
            return TransparentImage;
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

        public override void SetAddRank()
        {
            throw new NotImplementedException();
        }

        public override void SetPieceSet(string directive)
        {
            string wd = Directory.GetCurrentDirectory();

            PieceImage = White ? Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\w_pawn.png")))
                        :
                        Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\b_pawn.png")));

            TransparentImage = Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\t_pawn.png")));
        }
    }
}
