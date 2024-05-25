using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public abstract class Piece
    {
        protected bool White { get; set; }

        protected Piece(bool isWhite)
        {
            White = isWhite;
        }

        //TODO: Add piece methods

        /// <summary>
        /// To be used for debugging purposes with Debug.WriteLine(square.Piece.ToString())
        /// </summary>
        /// <returns>A letter (string) denoting this piece. Ex. "P" is a white pawn, "p" is a black pawn.</returns>
        public abstract override string ToString();
        
    }
}
