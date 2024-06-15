using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Move
    {
        private readonly Square from;
        private readonly Square to;

        public Move(Square from, Square to)
        {
            this.from = from;
            this.to =   to;
        }

        public void MakeMove()
        {
            if (from.Piece.GetType().GetInterfaces().Length == 1)
            {
                (from.Piece as ICastleable).MakeIncastleable();
            }

            if (from.Piece.GetType() == typeof(King))
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

            //TODO: Check method safety
            Piece toMove = from.Piece;
            to.Piece = toMove;
            from.Piece = null;
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
            return to.PieceResident() ? $"{squareInString.Piece}x{to}" : $"{squareInString.Piece}{to}";
        }
    }
}
