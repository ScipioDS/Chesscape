using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base(isWhite)
        {
        }

        public override string ToString()
        {
            return White ? "R" : "r";
        }
    }
}
