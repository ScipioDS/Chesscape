using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace Chesscape.Chess
{
    /// <summary>
    /// Note: byte (unsigned 8-bit integer) is used as a type to reduce waste on memory by using int (unsigned 8-bit integer);
    /// 0-indexed values are used for File and Rank (i.e., File and Rank are in range [0,7] for ease of access in the Board matrix, 
    ///                                              but are formally defined as [a-h] and [1-8] respectively)
    /// </summary
    public class Square
    {
        //Logic attributes
        public readonly byte File;
        private readonly byte RankLogical;
        public Piece Piece;
        public readonly static Dictionary<int, char> NumericToFile = new Dictionary<int, char>();
        public readonly static Dictionary<char, int> FileToNumeric = new Dictionary<char, int>();

        //Drawing attributes
        public Point TopLeftCoord { get; set; }
        public Color ColorDraw { get; set; }
        private PictureBox PiecePic;

        /// <summary>
        /// Full identification and definition of a square.
        /// </summary>
        /// <param name="File">The File of the Square</param>
        /// <param name="RankLogical">The Rank of the Square</param>
        /// <param name="Piece">Piece resident on this Square</param>
        public Square(byte RankLogical, byte File, Piece Piece)
        {
            Trace.Assert(File <= 7 && RankLogical >= 0); // insurance the File is in the range [0, 7]
            Trace.Assert(RankLogical <= 7 && RankLogical >= 0); // insurance the Rank is in the range [0, 7]

            this.File = File; // the file is indexed by j in the board matrix (columns)
            this.RankLogical = RankLogical; // the rank is indexed by i in the board matrix (rows)
            this.Piece = Piece;
        }

        /// <summary>
        /// Indicator stating if a piece is on this square.
        /// </summary>
        /// <returns>True if a piece is currently on the square, false otherwise.</returns>
        public bool PieceResident()
        {
            return Piece != null;
        }


        /// <summary>
        /// Sets dictionaries to translate letters to unsigned integers to help index the board. (described in class summary)
        /// </summary>
        public static void SetFileTranslation()
        {
            for (char i = 'a'; i <= 'h'; ++i)
            {
                NumericToFile.Add((i - 'a'), i);
                FileToNumeric.Add(i, (i - 'a'));
            }
        }


        /// <summary>
        /// Using dictionaries, makes up a formally defined string represenation of a square("a6", "e4", ...)
        /// </summary>
        /// <returns>A formal notation representing a square.</returns>
        public override string ToString()
        {
            return $"{NumericToFile[File]}{RankLogical + 1}";
        }

        /// <summary>
        /// Very important method! Use only this method to index squares by row in the board matrix.
        /// </summary>
        /// <returns>The actual index of the row in the Board matrix.</returns>
        public int GetRankPhysical()
        {
            return 7 - RankLogical;
        }

        /// <summary>
        /// Sets the PiecePic field according to which piece lives on the Square. To be used for drawing.
        /// </summary>
        public void SetImage(Graphics g)
        {
            if (Piece == null) return;
            g.DrawImage(Piece.GetImage(), TopLeftCoord);
        }

        /// <summary>
        /// Drawing logic of a square.
        /// </summary>
        /// <param name="g">Graphics object to draw square.</param>
        /// <param name="size">Width and height of a square.</param>
        public void Draw(Graphics g, int size)
        {
            using (Brush fillSquare = new SolidBrush(ColorDraw))
                g.FillRectangle(fillSquare, TopLeftCoord.X, TopLeftCoord.Y, size, size);

            SetImage(g);
        }
    }
}