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
        private string[] Easy { get; set; } = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Databases\1700-2100_elo.txt")));
        private string[] Medium { get; set; } = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Databases\2000-2100_elo.txt")));
        private string[] Hard { get; set; } = File.ReadAllLines(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Databases\2200-2600_elo.txt")));
        
        private static Random Random = new Random();
        public PuzzleManager() {

        } 
        
        public Puzzle getEasyPuzzle()
        {
            int num = Random.Next(Easy.Length);

            string newFEN = null;
            string[] MOVES = null;

            if (num %2 == 0)
            {
                newFEN = Easy[num];
                MOVES = Easy[num + 1].Split(' ');
            } else
            {
                newFEN = Easy[num - 1];
                MOVES = Easy[num].Split(' ');
            }

            return new Puzzle(newFEN, MOVES, 1900);
        }

        public Puzzle getMediumPuzzle()
        {
            int num = Random.Next(Medium.Length);

            string newFEN = null;
            string[] MOVES = null;

            if (num % 2 == 0)
            {
                newFEN = Medium[num];
                MOVES = Medium[num + 1].Split(' ');
            }
            else
            {
                newFEN = Medium[num - 1];
                MOVES = Medium[num].Split(' ');
            }

            return new Puzzle(newFEN, MOVES, 2050);
        }

        public Puzzle getHardPuzzle()
        {
            int num = Random.Next(Hard.Length);

            string newFEN = null;
            string[] MOVES = null;

            if (num % 2 == 0)
            {
                newFEN = Hard[num];
                MOVES = Hard[num + 1].Split(' ');
            }
            else
            {
                newFEN = Hard[num - 1];
                MOVES = Hard[num].Split(' ');
            }

            return new Puzzle(newFEN, MOVES, 2400);
        }

    }
}
