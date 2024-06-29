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
        //TODO: Implement pawn
        public Pawn(bool isWhite) : base(isWhite)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            Debug.WriteLine(currentDirectory);

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\w_pawn.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\b_pawn.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW)
                :
                Image.FromFile(fullPathB);
        }

        public override string FENNotation()
        {
            return White ? "P" : "p";

        }

        public override void refresh()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return White ? "P" : "p";
        }

        public override Image GetImageT()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\t_pawn.png"));
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
