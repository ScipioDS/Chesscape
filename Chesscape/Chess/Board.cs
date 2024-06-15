using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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

        //Board matrix
        public Square[][] Squares { get; set; }
        //En passant target square
        public Square EnPassantTarget { get; set; }
        //Turn
        public bool WhiteToPlay { get; set; }
        //Selection
        public Piece SelectedPiece { get; set; } = null;
        public Point Cursor { get; set; }
        public Square FromSquare {  get; set; }
        HashSet<Move> legalMoves {  get; set; }

        /// <summary>
        /// Reserves memory for the Square matrix (the board itself). Private due to singleton.
        /// </summary>
        private Board()
        {
            Squares = new Square[8][];

            for (int i = 7; i >= 0; --i)
            {
                Squares[i] = new Square[8];
                for (int j = 0; j < 8; ++j)
                {
                    Squares[i][j] = new Square((byte)(7 - i), (byte)j, null);

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
        /// <param name="white">Pass true if the board perspective is from the white player.</param>
        public void SetPerspective(bool white)
        {
            int y_incrementer = 53;
            for (int i = 0; i < 8; ++i, y_incrementer += 64)
            {
                int x_incrementer = 53;
                for (int j = 0; j < 8; ++j, x_incrementer += 64)
                {
                    if (white)
                    {
                        SingleBoard.Squares[i][j].ColorDraw = Color.FromArgb(255, 173, 189, 143);
                    }
                    else
                    {
                        SingleBoard.Squares[i][j].ColorDraw = Color.FromArgb(255, 111, 143, 114);
                    }

                    SingleBoard.Squares[i][j].TopLeftCoord = new Point(x_incrementer, y_incrementer);

                    white = !white;
                }
                white = !white;
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

            return SingleBoard.Squares[7 - rank][file];
        }

        /// <summary>
        /// Computes legal moves that a rook would have (straight line moves)
        /// This is not to be used only by moves for rooks, Queens can reuse this.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves that a rook would have.</returns>
        public HashSet<Move> ForthrightTrajectory(Square source)
        {
            HashSet<Move> legalMoves = new HashSet<Move>();

            int sJ = source.File;
            int sI = source.GetRankPhysical();

            //up
            for (int i = source.GetRankPhysical() - 1; i >= 0; --i)
            {
                AppendMove(source, Squares[i][sJ], legalMoves);
            }

            //down
            for (int i = source.GetRankPhysical() + 1; i < 8; ++i)
            {
                AppendMove(source, Squares[i][sJ], legalMoves);
            }

            //left
            for (int j = source.File - 1; j >= 0; --j)
            {
                AppendMove(source, Squares[sI][j], legalMoves);
            }

            //right
            for (int j = source.File + 1; j < 8; ++j)
            {
                AppendMove(source, Squares[sI][j], legalMoves);
            }

            return legalMoves;
        }

        /// <summary>
        /// Computes the legal moves a knight would have for a given square.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>>A set containing legal moves for a knight.</returns>
        private HashSet<Move> GTrajectory(Square source)
        {
            HashSet<Move> legalMoves = new HashSet<Move>();
            int File = source.File;
            int Rank = source.GetRankPhysical();
            int[,] knightMoves = new int[,]
            {
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
            };
            //site vo edna pa neka bucit, neznam so tocno da pisam ovde
            //osvem deka site mozni 8 nasoki gi provervit i gi vrakat
            //tie so se legalni
            for (int i = 0; i < knightMoves.Length / 2; i++)
            {
                int newRank = Rank + knightMoves[i, 0];
                int newFile = File + knightMoves[i, 1];
                if (checkValid(newRank, newFile))
                {
                    AppendMove(source, Squares[newRank][newFile], legalMoves);
                }
            }
            return legalMoves;
        }
        private static bool checkValid(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }

        /// <summary>
        /// Computes the legal moves a pawn would have for a given square.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves for a pawn.</returns>
        public HashSet<Move> PawnTrajectory(Square source)
        {
            HashSet<Move> legalMoves = new HashSet<Move>();
            int File = source.File;
            int Rank = source.GetRankPhysical();
            //eden cekor napred
            if (checkValid(Rank - 1, File) && !Squares[Rank-1][File].PieceResident())
            {
                AppendMove(source, Squares[Rank - 1][File], legalMoves);
            }
            //provervit dali e na staring line, pa aku e go dodavat i to
            if (Rank == 6 && checkValid(Rank - 2, File))
            {
                AppendMove(source, Squares[Rank - 2][File], legalMoves);
            }
            //diagonalno od levo(ms?) provervit za capture
            if (checkValid(File - 1, Rank - 1) && File != 1 && Squares[Rank - 1][File-1].PieceResident())
            {
                AppendMove(source, Squares[Rank - 1][File - 1], legalMoves);
            }
            //diagonalno od desno(ne sum sig) provervit za capture
            if (checkValid(Rank - 1, File + 1) && File != 1 && Squares[Rank - 1][File+1].PieceResident())
            {
                AppendMove(source, Squares[Rank - 1][File + 1], legalMoves);
            }
            return legalMoves;
        }

        /// <summary>
        /// 
        /// This is not to be used only by moves for bishop, Queens can reuse this.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves for a bishop.</returns>
        public HashSet<Move> DiagonalTrajectory(Square source)
        {
            HashSet<Move> legalMoves = new HashSet<Move>();

            // bottom right
            for (int i = source.GetRankPhysical() + 1, j = source.File + 1; i < 8 && j < 8; ++i, ++j)
            {
                if (! AppendMove(source, Squares[i][j], legalMoves)) break;
            }

            // top right
            for (int i = source.GetRankPhysical() - 1, j = source.File + 1; i >= 0 && j < 8; --i, ++j)
            {
                Debug.WriteLine($"{i} {j}");
                if (!AppendMove(source, Squares[i][j], legalMoves)) break;
            }

            //top left
            for (int i = source.GetRankPhysical() - 1, j = source.File - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (!AppendMove(source, Squares[i][j], legalMoves)) break;
            }

            //bottom left
            for (int i = source.GetRankPhysical() + 1, j = source.File - 1; i < 8 && j >= 0 ; ++i, --j)
            {
                if (!AppendMove(source, Squares[i][j], legalMoves)) break;
            }

            return legalMoves;
        }
        public HashSet<Move> KingTrajectory(Square source)
        {
            HashSet<Move> legalMoves = new HashSet<Move>();
            int File = source.File;
            int Rank = source.GetRankPhysical();
            int[,] kingMoves = new int[,]
            {
                { 1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 0 }, { -1, 1 }, { -1, -1 }, { 0, 1 }, { 0, -1 }
            };
            for (int i = 0; i < kingMoves.GetLength(0); i++)
            {
                int newRank = Rank + kingMoves[i, 0];
                int newFile = File + kingMoves[i, 1];
                if (checkValid(newRank, newFile))
                {
                    AppendMove(source, Squares[newRank][newFile], legalMoves);
                }
            }
            return legalMoves;
        }

        /// <summary>
        /// Method that calls upon each square to draw itself.
        /// </summary>
        /// <param name="g">Graphics object obtained form Paint method arguments in TacticsForm.cs</param>
        /// <param name="topLeft">The top left point of the square itself.</param>
        public void DrawAllComponents(Graphics g)
        {
            Array.ForEach(Squares, rank => Array.ForEach(rank, square => square.Draw(g, SQUARE_SIZE)));
            if (SelectedPiece != null)
            {
                g.DrawImage(SelectedPiece.GetImage(), new Point(Cursor.X - 32, Cursor.Y - 32));
                foreach (Move i in legalMoves)
                {
                    i.getToSquare().Availabe = true;
                }
            }
        }


        /// <summary>
        /// Utility method to append legal moves to a set. To be called inside trajectory method loops.
        /// </summary>
        /// <param name="source">The source square in which the piece resides.</param>
        /// <param name="target">The target square to which the piece will move.</param>
        /// <param name="fill">A set that is filled with all legal moves from a given square.</param>
        /// <returns>True iff we dont encounter a blockade for the trajectory, false otherwise.</returns>
        private bool AppendMove(Square source, Square target, HashSet<Move> fill)
        {
            if ((target.PieceResident() && (source.Piece.White != target.Piece.White)) || !target.PieceResident())
            {
                return fill.Add(new Move(source, target));
            }
            return false;
        }

        /// <summary>
        /// Only for debugging purposes. To be called with Debug.WriteLine(board.ToString());
        /// </summary>
        /// <returns>An ASCII string representation of the board.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 8; ++i)
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

        /// <summary>
        /// Adds a duplicate of the piece over which the cursor is to selected piece. To be used with move
        /// </summary>
        public void Select(Point point)
        {
            Square square = getSquare(point);

            if (square.Piece != null) 
            {
                string piece = square.Piece.ToString().ToLower();
                switch (piece) {
                    case "p":
                        legalMoves = PawnTrajectory(square);
                        break;
                    case "q":
                        legalMoves = ForthrightTrajectory(square);
                        legalMoves.UnionWith(DiagonalTrajectory(square));
                        break;
                    case "k":
                        legalMoves = KingTrajectory(square);
                        break;
                    case "r":
                        legalMoves = ForthrightTrajectory(square);
                        break;
                    case "n":
                        legalMoves = GTrajectory(square);
                        break;
                    case "b":
                        legalMoves = DiagonalTrajectory(square);
                        break;
                }
                this.SelectedPiece = square.Piece;
                FromSquare = square;
            }
            
            this.Cursor = point;
        }
        /// <summary>
        /// Moves piece from square to square
        /// </summary>
        public void MakeMove(Point point)
        {
            Square square = getSquare(point);

            List<Square> moves = new List<Square>();
            if (legalMoves == null) return;
            foreach(Move i in legalMoves)
            {
                moves.Add(i.getToSquare());
            }

            if (moves.Contains(square) && FromSquare!=null)
            {
                square.Piece = this.SelectedPiece;
                FromSquare.Piece = null;
                FromSquare = null;
            }
            foreach (Move i in legalMoves)
            {
                i.getToSquare().Availabe = false;
            }
            legalMoves = null;
            this.SelectedPiece = null;
        }
        /// <summary>
        /// Returns the square over which a point (the cursor) is located.
        /// </summary>
        public Square getSquare(Point point)
        {
            int iI = 0, jJ = 0;
            int y_incrementer = 53;
            for (int i = 0; i < 8; ++i, y_incrementer += 64)
            {
                int x_incrementer = 53;
                for (int j = 0; j < 8; ++j, x_incrementer += 64)
                {
                    if (point.X >= x_incrementer && point.X <= x_incrementer + 64 && point.Y >= y_incrementer && point.Y <= y_incrementer + 64)
                    {
                        iI = i;
                        jJ = j;
                        break;
                    }

                }
            }
            return Squares[iI][jJ];
        }
    }
}
