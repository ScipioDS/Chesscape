using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess.Internals
{
    public class ELO
    {
        private int puzzleELO {  get; set; }
        private int playerELO { get; set; }
        // Bool value, did player solve puzzle correctly
        private bool correctSolve { get; set; }
        // Constant, maximum possible adjustment per game, K16 for masters, K32 for weaker
        private int K { get; set; } = 16;
        // Constructor using Default K
        public ELO(int puzzleELO, int playerELO, bool correctSolve)
        {
            this.puzzleELO = puzzleELO;
            this.playerELO = playerELO;
            this.correctSolve = correctSolve;
        }
        // Constructor using Custom K
        public ELO(int puzzleELO, int playerELO, bool correctSolve, int K)
        {
            this.puzzleELO = puzzleELO;
            this.playerELO = playerELO;
            this.correctSolve = correctSolve;
            this.K = K;
        }
        /// <summary>
        ///     Calculates the new ELO of the player using the standar ELO calc formula.
        /// </summary>
        /// <returns> INT value, new player ELO </returns>
        public int calculatePlayerELO ()
        {
            //Expected score
            var E_B = 1 / (1 + Math.Pow(10, (puzzleELO - playerELO) / 400.0));
            if (correctSolve)
            {
                return (int)Math.Round(playerELO + K * (1 - E_B));
            } else
            {
                var newELO = (int)Math.Round(playerELO + K * (0 - E_B));
                if (newELO == playerELO)
                {
                    return newELO-1;
                }
                else
                {
                    return newELO;
                }
            }
        }
    }
}
