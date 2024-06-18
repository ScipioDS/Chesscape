using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess.Internals
{

    public class FEN
    {
        /// <summary>
        /// Translates a Forsyth-Edwards Notation string and defines the board according to it.
        /// </summary>
        /// <param name="FEN">A Forsyth-Edwards Notation string.</param>
        public static void Translate(string FEN)
        {
            string[] parts = FEN.Split(' ');

            Board single = Board.GetInstance();

            SquareSetup(parts[0], single);

            single.WhiteToPlay = parts[1].Equals("w");

            //single.EnPassantTarget = parts[3].Equals("-") ? null : Board.PositionToSquare(parts[3]);

            //TODO: Add available castling rights;
        }


        public static string ToFEN(Square[][] Squares)
        {
            StringBuilder fen = new StringBuilder();

            for (int i = 0; i < 8; ++i)
            {
                int emptyCount = 0;
                for (int j = 0; j < 8; ++j) {
                
                    if (!Squares[i][j].PieceResident())
                    {
                        emptyCount++;
                    }
                    else
                    {
                        if (emptyCount > 0)
                        {
                            fen.Append(emptyCount);
                            emptyCount = 0;
                        }
                        fen.Append(Squares[i][j].Piece.FENNotation());
                    }
                }
                if (emptyCount > 0)
                {
                    fen.Append(emptyCount);
                }
                if (i < 7)
                {
                    fen.Append('/');
                }
            }


            string activeColor = "w"; // 'w' for white to move, 'b' for black to move
            string castlingAvailability = "KQkq";
            string enPassantTarget = "-"; // Modify based on actual en passant target square
            int halfmoveClock = 0; // Number of halfmoves since the last capture or pawn move
            int fullmoveNumber = 1;
            fen.Append($" {activeColor} {castlingAvailability} {enPassantTarget} {halfmoveClock} {fullmoveNumber}");
            Debug.WriteLine(fen);
            return fen.ToString();
        }

        /// <summary>
        /// Gets the first part of FEN string, and using that, translates the string to pieces on the board.
        /// </summary>
        /// <param name="firstPartFEN">The piece positions part of the FEN string.</param>
        /// <param name="onto">The board onto which we are placing pieces. (the Singleton reference)</param>
        private static void SquareSetup(string firstPartFEN, Board onto)
        {

            byte indexFEN = 0; // used to index the string

            for (int i = 7; i >= 0; --i)
            {
                bool nextRow = false; // used to know if we can continue to the next row IF AND ONLY IF we are not on the "a" file inside the inner loop (j = 0)

                for (int j = 0; j < 8; ++j)
                {
                    Square squareSetting = onto.Squares[7 - i][j];
                    char checkEl = firstPartFEN[indexFEN++];

                    switch (checkEl)
                    {
                        case 'P':
                            squareSetting.Piece = new Pawn(true);
                            break;
                        case 'p':
                            squareSetting.Piece = new Pawn(false);
                            break;
                        case 'B':
                            squareSetting.Piece = new Bishop(true);
                            break;
                        case 'b':
                            squareSetting.Piece = new Bishop(false);
                            break;
                        case 'Q':
                            squareSetting.Piece = new Queen(true);
                            break;
                        case 'q':
                            squareSetting.Piece = new Queen(false);
                            break;
                        case 'R':
                            squareSetting.Piece = new Rook(true);
                            break;
                        case 'r':
                            squareSetting.Piece = new Rook(false);
                            break;
                        case 'N':
                            squareSetting.Piece = new Knight(true);
                            break;
                        case 'n':
                            squareSetting.Piece = new Knight(false);
                            break;
                        case 'K':
                            squareSetting.Piece = new King(true);
                            break;
                        case 'k':
                            squareSetting.Piece = new King(false);
                            break;
                        case '/':
                            if (j != 0) nextRow = true;
                            else j--;
                            break;
                        default:
                            int forward = int.Parse(char.ToString(checkEl)) - 1;
                            int k = j;
                            j += forward++;
                            while(forward != 0)
                            {
                                onto.Squares[7 - i][k].Piece = null;
                                k++;
                                forward--;
                            }
                            break;
                    }

                    if (nextRow) break;
                }
            }
        }
    }
}
