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

        public override string ToString()
        {
            return White ? "P" : "p";
        }
    }
}
