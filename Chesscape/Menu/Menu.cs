﻿using Chesscape.Puzzle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Chesscape
{
    public partial class Menu : Form
    {
        PuzzleManager pm { get; set; }
        int ELO;
        string path = @"eloscore.txt";
        public Menu()
        {
            InitializeComponent();
            pm = new PuzzleManager();
            ELO = int.Parse(File.ReadAllText(path));
            lbl_ELO.Text = new StringBuilder("ELO: ").Append(File.ReadAllText(path)).ToString();
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
            File.WriteAllText(path, ELO.ToString());
            lbl_ELO.Text = new StringBuilder("ELO: ").Append(File.ReadAllText(path)).ToString();
        }

        private void btn_score_Click(object sender, EventArgs e)
        {
            string path = @"eloscore.txt";
            File.WriteAllText(path, "1600");

            lbl_ELO.Text = new StringBuilder("ELO: ").Append(File.ReadAllText(path)).ToString();
        }
    }
}
