using Chesscape.Chess;
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
            pastmoves = new List<string>();
        }

        string FEN {  get; set; }
        string[] moves {  get; set; } 
        int puzzleELO {  get; set; }
        int current_move { get; set; } = 0;
        List<string> pastmoves { get; set; }
        
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
            pastmoves.Add(moves[current_move]);
            current_move++;
        }
        public List<string> getpastmoves()
        {
            return pastmoves;
        }
        

        
        public string getFEN() { return FEN;}
        public int getPuzzleELO() {  return puzzleELO;}
        


    }
}
