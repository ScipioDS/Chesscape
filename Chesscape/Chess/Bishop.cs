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


        public override Image GetImageT()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\t_bishop.png"));
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
