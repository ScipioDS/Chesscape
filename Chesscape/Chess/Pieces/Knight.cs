using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string fullPathW = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\w_knight.png"));
            string fullPathB = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\b_knight.png"));
            string fullPathT = Path.GetFullPath(Path.Combine(currentDirectory, $@"{Board.PieceSetDirective}\t_knight.png"));

            PieceImage = isWhite ? Image.FromFile(fullPathW)
                :
                Image.FromFile(fullPathB);

            TransparentImage = Image.FromFile(fullPathT);
        }

        public override string FENNotation()
        {
            return White ? "N" : "n";

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(White ? "N" : "n");
            if (this.addRank)
            {
                sb.Append(this.Rank);
            }
            else if (this.addFile)
            {
                sb.Append(this.File);
            }
            return sb.ToString();
        }


        public override Image GetImageT()
        {
            return TransparentImage;
        }

        public override void SetFile(char file)
        {
            this.File = file;
        }

        public override void SetRank(int rank)
        {
            this.Rank = rank;
        }

        public override void SetAddFile()
        {
            this.addFile = true;
        }

        public override void Refresh()
        {
            this.addRank = false;
            this.addFile = false;
        }

        public override void SetAddRank()
        {
           this.addRank = true;
        }

        public override void SetPieceSet(string directive)
        {
            string wd = Directory.GetCurrentDirectory();

            PieceImage = White ? Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\w_knight.png")))
                        :
                        Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\b_knight.png")));

            TransparentImage = Image.FromFile(Path.GetFullPath(Path.Combine(wd, $@"{directive}\t_knight.png")));
        }
    }
}
