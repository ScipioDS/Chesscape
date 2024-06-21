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
        public Puzzle(string fEN, string[] moves, int puzzleELO)
        {
            FEN = fEN;
            this.moves = moves;
            this.puzzleELO = puzzleELO;
        }

        string FEN {  get; set; }
        string[] moves {  get; set; } 
        int puzzleELO {  get; set; }
        public bool checkMove()
        {
            throw new NotImplementedException();
        }

        public string[] getMoves() {  throw new NotImplementedException(); }
        public string getFEN() { return FEN;}
        public int getPuzzleELO() {  return puzzleELO;}
        


    }
}
