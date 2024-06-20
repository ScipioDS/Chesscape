using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Puzzle
{
    public class Puzzle
    {
        public Puzzle(string fEN, string[] moves)
        {
            FEN = fEN;
            this.moves = moves;
        }

        string FEN {  get; set; }
        string[] moves {  get; set; } 
        public bool checkMove()
        {
            throw new NotImplementedException();
        }
        


    }
}
