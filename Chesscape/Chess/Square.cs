using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

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
        public readonly byte Rank;
        public Piece Piece;
        public readonly static Dictionary<int, char> NumericToFile = new Dictionary<int, char>();
        public readonly static Dictionary<char, int> FileToNumeric = new Dictionary<char, int>();

        //Drawing attributes
        public Point TopLeftCoord { get; set; }
        public Color ColorDraw { get; set; }

        /// <summary>
        /// Full identification and definition of a square.
        /// </summary>
        /// <param name="File">The File of the Square</param>
        /// <param name="Rank">The Rank of the Square</param>
        /// <param name="Piece">Piece resident on this Square</param>
        public Square(byte Rank, byte File, Piece Piece)
        {
            Trace.Assert(File <= 7 && Rank >= 0); // insurance the File is in the range [0, 7]
            Trace.Assert(Rank <= 7 && Rank >= 0); // insurance the Rank is in the range [0, 7]

            this.File = File; // the file is indexed by j in the board matrix (columns)
            this.Rank = Rank; // the rank is indexed by i in the board matrix (rows)
            this.Piece = Piece;
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
            return $"{NumericToFile[File]}{Rank + 1}";
        }

        /// <summary>
        /// Drawing logic of a square.
        /// </summary>
        /// <param name="g">Graphics object to draw square.</param>
        /// <param name="size">Width and height of a square.</param>
        public void Draw(Graphics g, int size)
        {
            Brush fillSquare = new SolidBrush(ColorDraw);
            g.FillRectangle(fillSquare, TopLeftCoord.X, TopLeftCoord.Y, size, size);
        }
    }
}