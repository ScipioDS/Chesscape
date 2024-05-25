using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Queen : Piece
    {
        //TODO: Implement queen
        public Queen(bool isWhite) : base(isWhite)
        {

        }

        public override string ToString()
        {
            return White ? "Q" : "q";
        }
    }
}
