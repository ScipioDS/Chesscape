
using Microsoft.Win32;
using System.Drawing;

namespace Chesscape.Chess
{
    public abstract class Piece
    {
        public bool White { get; set; }

        protected Image PieceImage;
        protected Image TransparentImage;
        public bool addRank { get; set; }
        public bool addFile { get; set; }
        public char File { get; set; }
        public int Rank { get; set; }

        protected Piece(bool isWhite)
        {
            White = isWhite;
        }
        /// <summary>
        /// To be used for debugging purposes with Debug.WriteLine(square.Piece.ToString())
        /// </summary>
        /// <returns>A letter (string) denoting this piece. Ex. "P" is a white pawn, "p" is a black pawn.</returns>
        public abstract override string ToString();

        public abstract string FENNotation();

        public Image GetImage()
        {
            return PieceImage;
        }

        public abstract void SetPieceSet(string directive);
        public abstract void SetFile(char file);
        public abstract void SetRank(int rank);
        public abstract void SetAddFile();
        public abstract void SetAddRank();
        public abstract void Refresh();
        public abstract Image GetImageT();
    }
}
