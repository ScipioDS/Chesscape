using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class King : Piece
    {
        //TODO: Implement king
        public King(bool isWhite) : base(isWhite)
        {

        }

        public override string ToString()
        {
            return White ? "K" : "k";
        }
    }
}
