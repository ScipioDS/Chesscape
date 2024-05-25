using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Knight : Piece
    {
        //TODO: Implement knight
        public Knight(bool isWhite) : base(isWhite)
        {

        }

        public override string ToString()
        {
            return White ? "N" : "n";
        }
    }
}
