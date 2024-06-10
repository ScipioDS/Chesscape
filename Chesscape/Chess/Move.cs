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
            //TODO: Check method safety
            Piece toMove = from.Piece;
            to.Piece = toMove;
            from.Piece = null;
        }

        public override string ToString()
        {
            //TODO: Check if an identical piece can make the same move, this would change the notation. This applies to Knights and Rooks ONLY. (VERY IMPORTANT)

            // Safety measure if MakeMove() is called before ToString(), since the from square might be null
            Square squareInString = from.PieceResident() ? from : to;
            return to.PieceResident() ? $"{squareInString.Piece}x{to}" : $"{squareInString.Piece}{to}";
        }
    }
}
