using Chesscape.Puzzle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesscape
{
    public partial class Menu : Form
    {
        PuzzleManager pm { get; set; }
        int ELO = 1600;
        public Menu()
        {
            InitializeComponent();
            pm = new PuzzleManager();
            lbl_ELO.Text = new StringBuilder("ELO: ").Append(ELO).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Easy_Click(object sender, EventArgs e)
        {
            Puzzle.Puzzle puzzle = pm.GetArbitraryPuzzle("easy");
            var form = new Chess.TacticsForm(puzzle).ShowDialog();

            if(form == DialogResult.Yes)
            {
                ELO = new Chess.Internals.ELO(puzzle.GetPuzzleELO(), ELO, true).calculatePlayerELO();
            }
            else
            {
                ELO = new Chess.Internals.ELO(puzzle.GetPuzzleELO(), ELO, false).calculatePlayerELO();
            }
            updateLabel();
        }

        private void btn_Medium_Click(object sender, EventArgs e)
        {
            Puzzle.Puzzle puzzle = pm.GetArbitraryPuzzle("medium");
            var form = new Chess.TacticsForm(puzzle).ShowDialog();

            if (form == DialogResult.Yes)
            {
                ELO = new Chess.Internals.ELO(puzzle.GetPuzzleELO(), ELO, true).calculatePlayerELO();
            }
            else
            {
                ELO = new Chess.Internals.ELO(puzzle.GetPuzzleELO(), ELO, false).calculatePlayerELO();
            }
            updateLabel();
        }

        private void btn_Hard_Click(object sender, EventArgs e)
        {
            Puzzle.Puzzle puzzle = pm.GetArbitraryPuzzle("hard");
            var form = new Chess.TacticsForm(puzzle).ShowDialog();

            if (form == DialogResult.Yes)
            {
                ELO = new Chess.Internals.ELO(puzzle.GetPuzzleELO(), ELO, true).calculatePlayerELO();
            }
            else
            {
                ELO = new Chess.Internals.ELO(puzzle.GetPuzzleELO(), ELO, false).calculatePlayerELO();
            }
            updateLabel();
        }

        private void updateLabel()
        {
            lbl_ELO.Text = new StringBuilder("ELO: ").Append(ELO).ToString();
        }
    }
}
