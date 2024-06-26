using Chesscape.Chess.Internals;
using Chesscape.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesscape.Puzzle
{
    public class PuzzleManager
    {
        private readonly string[] Easy;
        private readonly string[] Medium;
        private readonly string[] Hard;

        private static Random Random = new Random(); //uniform random object

        public PuzzleManager()
        {
            Easy = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Databases\1700-2100_elo.txt")));

            Medium = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Databases\2000-2100_elo.txt")));

            Hard = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Databases\2200-2600_elo.txt")));
        }

        public Puzzle GetArbitraryPuzzle(String diffic)
        {

            string[] difficAccessing;
            int ELOSend;

            if (diffic.Equals("easy"))
            {
                difficAccessing = Easy;
                ELOSend = 1900;
            }
            else if (diffic.Equals("medium"))
            {
                difficAccessing = Medium;
                ELOSend = 2050;
            }
            else
            {
                difficAccessing = Hard;
                ELOSend = 2400;
            }

            string newFEN;
            string[] MOVES;

            int randAccess = Random.Next(difficAccessing.Length);

            if (randAccess % 2 == 0)
            {
                newFEN = difficAccessing[randAccess];
                MOVES = difficAccessing[randAccess + 1].Split(' ');
            }
            else
            {
                newFEN = Easy[randAccess - 1];
                MOVES = Easy[randAccess].Split(' ');
            }

            return new Puzzle(newFEN, MOVES, ELOSend);
        }
    }
}
