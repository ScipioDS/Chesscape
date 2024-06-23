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
        int current_move { get; set; } = 0;
        
        public string GetNextMove()
        {
            if (!check_pointer()) { 
                return moves[current_move];
            }
            else {
                return "GAME OVER";
            }
        }
        public bool check_pointer()
        {
            if(current_move >= moves.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void increment()
        {
            current_move++;
        }

        
        public string getFEN() { return FEN;}
        public int getPuzzleELO() {  return puzzleELO;}
        


    }
}
