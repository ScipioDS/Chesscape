using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Bishop : Piece
    {
        //TODO: Implement bishop
        public Bishop(bool isWhite) : base(isWhite)
        {

        }

        public override string ToString()
        {
            return White ? "B" : "b";
        }
    }
}
