using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Pawn : Piece
    {
        //TODO: Implement pawn
        public Pawn(bool isWhite) : base(isWhite)
        {

        }

        public override string ToString()
        {
            return White ? "P" : "p";
        }
    }
}
