using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Chesscape.Chess.Internals;

namespace Chesscape.Chess
{

    /// <summary>
    /// The main board class. Only one instance of the class may exist at any given moment, and is used by a separate form, opened from the Menu.
    /// IMPORTANT: Since the board is a 2D array, rows grow from top to bottom as black, and bottom-up as white, and we are keeping the board from the white perspective.
    /// </summary>
    public class Board
    {
        //Unique and single board reference
        private static Board SingleBoard = null;

        //Do not change value 64 unless necessary.
        private static readonly int SQUARE_SIZE = 64;

        public Square[][] Squares { get; set; }
        public Square EnPassantTarget { get; set; }

        public bool WhiteToPlay { get; set; }

        /// <summary>
        /// Reserves memory for the Square matrix (the board itself). Private due to singleton.
        /// </summary>
        private Board()
        {
            Squares = new Square[8][];

            int reverseSetRank = 0;

            for (int i = 7; i >= 0; --i, reverseSetRank++)
            {
                Squares[i] = new Square[8];
                for (int j = 0; j < 8; ++j)
                {
                    Squares[i][j] = new Square((byte) (7 - i), (byte) j, null);
                }
            }
        }

        public static Board GetInstance()
        {
            // Board singleton
            SingleBoard = SingleBoard == null ? new Board() : SingleBoard;
            return SingleBoard;
        }

        /// <summary>
        /// Board perspective drawing logic. Sets the basis square colors from which the board will be drawn.
        /// </summary>
        /// <param name="startWhite">Pass true if the board perspective is from the white player.</param>
        public void SetPerspective(bool startWhite)
        {
            int y_incrementer = 53;
            for (int i = 0; i < 8; ++i, y_incrementer += 64)
            {
                int x_incrementer = 53;
                for (int j = 0; j < 8; ++j, x_incrementer += 64)
                {
                    if (startWhite)
                    {
                        SingleBoard.Squares[i][j].ColorDraw = Color.Bisque;
                    }
                    else
                    {
                        SingleBoard.Squares[i][j].ColorDraw = Color.FromArgb(100, 183, 136, 118);
                    }

                    SingleBoard.Squares[i][j].TopLeftCoord = new Point(x_incrementer, y_incrementer);

                    startWhite = !startWhite;
                }
                startWhite = !startWhite;
            }
        }

        /// <summary>
        /// Interface through which the board position is set using a Forsyth-Edwards Notation as a board identifier.
        /// </summary>
        /// <param name="FENString">A Forsyth-Edwards notation string.</param>
        public void SetBoard(string FENString)
        {
            FEN.Translate(FENString);
        }

        
        /// <summary>
        /// Returns the square in the Board matrix with the position passed as an argument
        /// </summary>
        /// <param name="position">A formally defined square position.</param>
        /// <returns>A Square object on the board matrix.</returns>
        public static Square PositionToSquare(string position)
        {
            int file = Square.FileToNumeric[position[0]];
            int rank = int.Parse(position[1].ToString()) - 1;
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (SingleBoard.Squares[i][j] == SingleBoard.Squares[7 - rank][file])
                    {
                        Debug.WriteLine($"{i} {j}");
                        break;
                    }
                }
            }
            return SingleBoard.Squares[7 - rank][file];
        }

        
        public void DrawAllComponents(Graphics g, Point topLeft)
        {
            Array.ForEach(Squares, rank => Array.ForEach(rank, square => square.Draw(g, SQUARE_SIZE)));
        }

        /// <summary>
        /// Debugging purposes. To be called with Debug.WriteLine(board.ToString());
        /// </summary>
        /// <returns>An ASCII string representation of the board.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    string toAppend = Squares[i][j].Piece == null ? "#" : Squares[i][j].Piece.ToString();
                    sb.Append(" ").Append(toAppend);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
