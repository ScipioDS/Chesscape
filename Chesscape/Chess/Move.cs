using Chesscape.Chess.Internals;
using System.Runtime;
using System.Windows.Forms;

namespace Chesscape.Chess
{
    public class Move
    {
        private readonly Square from;
        private readonly Square to;

        public Move(Square from, Square to)
        {
            this.from = from;
            this.to = to;
        }

        public void MakeMove(bool pretend)
        {
            if (from.Piece is ICastleable)
            {
                (from.Piece as ICastleable).MakeIncastleable();
            }

            if (from.Piece is King)
            {
                if (to.File - from.File == 2)
                {
                    CastleKingside();
                    return;
                }
                else if (to.File - from.File == -2)
                {
                    CastleQueenside();
                    return;
                }
            }

            Piece toMove = from.Piece;
            to.Piece = toMove;
            from.Piece = null;

            if (!pretend)
            {
                Board b = Board.GetInstance();
                b.PreviousSetup = FEN.ToFEN(b.Squares);
            }
        }

        private void CastleKingside()
        {
            Board board = Board.GetInstance();
            board.Squares[7][from.File + 1].Piece = new Rook(true);

            (board.Squares[7][from.File + 1].Piece as ICastleable).MakeIncastleable();

            board.Squares[7][from.File + 3].Piece = null;
            to.Piece = from.Piece;
            from.Piece = null;
        }

        private void CastleQueenside()
        {
            Board board = Board.GetInstance();
            board.Squares[7][from.File - 1].Piece = new Rook(true);

            (board.Squares[7][from.File - 1].Piece as ICastleable).MakeIncastleable();

            board.Squares[7][from.File - 4].Piece = null;
            to.Piece = from.Piece;
            from.Piece = null;
        }

        public Square GetToSquare() { return to; }

        public override string ToString()
        {
            //TODO: Check if an identical piece can make the same move, this would change the notation. This applies to Knights and Rooks ONLY. (VERY IMPORTANT)

            // Safety measure if MakeMove() is called before ToString(), since the from square might be null
            Square squareInString = from.PieceResident() ? from : to;

            string fixPieceNotation = squareInString.Piece.ToString().Substring(0, 1).ToUpper() + squareInString.Piece.ToString().Substring(1);

            return to.PieceResident() ? $"{fixPieceNotation}x{to}" : $"{fixPieceNotation}{to}";
        }
    }
}
