using Chesscape.Chess.Internals;
using System.Diagnostics;
using System.Runtime;
using System.Text;
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
                }
                else if (to.File - from.File == -2)
                {
                    CastleQueenside();
                } else
                {
                    StdMoveRoutine();
                }
            }
            else
            {
                StdMoveRoutine();
            }

            StorePosition(pretend);
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
            board.Squares[7][from.File - 3].Piece = null;
            to.Piece = from.Piece;
            from.Piece = null;
        }

        private void StorePosition(bool pretend)
        {
            if (!pretend)
            {
                Board b = Board.GetInstance();
                b.PreviousSetup = FEN.ToFEN(b.Squares);
            }
        }

        private void StdMoveRoutine()
        {
            Piece toMove = from.Piece;
            to.Piece = toMove;
            from.Piece = null;
        }
        

        public Square GetToSquare() { return to; }

        public override string ToString()
        {
            //TODO: Check if an identical piece can make the same move, this would change the notation. This applies to Knights and Rooks ONLY. (VERY IMPORTANT)

            // Safety measure if MakeMove() is called before ToString(), since the from square might be null
           StringBuilder sb= new StringBuilder();
            if (from.Piece.ToString().ToLower().Equals("p"))
            {
                sb.Append(to.ToString());
                if (to.PieceResident())
                {
                    sb.Append("x");
                    return sb.ToString();
                }
                return sb.ToString();
            }
            else
            {
                sb.Append(from.Piece.ToString());
            }
            if (to.PieceResident())
            {
                sb.Append("x");
                sb.Append(to.ToString());
            }
            else
            {
                sb.Append(to.ToString());
            }
           return sb.ToString();
        }
    }
}
