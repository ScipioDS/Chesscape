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

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\w_bishop.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\b_bishop.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW) 
                : 
                Image.FromFile(fullPathB);
        }

        public override string ToString()
        {
            return White ? "B" : "b";
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        public override Image GetImageT()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, @"cburnett_pieces\t_bishop.png"));
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

        public override void SetAddRank()
        {
            throw new NotImplementedException();
        }

        public override string FENNotation()
        {
            return White ? "B" : "b";
        }

        public override void SetPieceSet(string directive)
        {
            string wd = Directory.GetCurrentDirectory();

            PieceImage = White ? Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\w_bishop.png")))
                        :
                        Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\b_bishop.png")));
        }
    }
}
