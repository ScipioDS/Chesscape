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
        public Puzzle(string FEN, string[] Moves, int PuzzleELO)
        {
            this.FEN = FEN;
            this.Moves = Moves;
            this.PuzzleELO = PuzzleELO;
            PastMoves = new List<string>();
        }

        string FEN {  get; set; }
        string[] Moves {  get; set; } 
        int PuzzleELO {  get; set; }
        int CurrentMove { get; set; } = 0;
        List<string> PastMoves { get; set; }
        
        public string GetNextMove()
        {
            if (!CheckPointer()) { 
                return Moves[CurrentMove];
            }
            else {
                return "GAME OVER";
            }
        }

        public bool CheckPointer()
        {
            if(CurrentMove >= Moves.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Increment()
        {
            PastMoves.Add(Moves[CurrentMove]);
            CurrentMove++;
        }

        public List<string> GetPastMoves()
        {
            return PastMoves;
        }
       
        
        public string GetFEN() { return FEN;}
        public int GetPuzzleELO() {  return PuzzleELO;}
        

    }
}
