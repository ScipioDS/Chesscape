using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Bishop : Piece
    {
        //TODO: Implement bishop
        public Bishop(bool isWhite) : base(isWhite)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\w_bishop.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\b_bishop.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW) 
                : 
                Image.FromFile(fullPathB);
        }

        public override string ToString()
        {
            return White ? "B" : "b";
        }
    }
}
