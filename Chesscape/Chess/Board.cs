using Chesscape.Chess.Internals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Chesscape.Chess
{

    /// <summary>
    /// The main board class. Only one instance of the class may exist at any given moment, and is used by a separate form, opened from the Menu.
    /// IMPORTANT: Since the board is a 2D array, rows grow from top to bottom as black, and bottom-up as white, and we are keeping the board from the white perspective.
    /// </summary>
    public class Board
    {

        //LOGIC ATTRIBUTES
        //Unique and single board reference
        private static Board SingleBoard = null;
        //Board matrix
        public Square[][] Squares { get; set; }
        //En passant target square
        public Square EnPassantTarget { get; set; }
        //Turn
        public bool WhiteToPlay { get; set; }
        //Legal moves
        private HashSet<Move> LegalMoves { get; set; }
        private string previous_setup { get; set; }
        private Trajectories Trajectories { get; set; }


        //DRAWING ATTRIBUTES
        //Selection & Moving
        public Piece SelectedPiece { get; set; } = null;
        public Point Cursor { get; set; }
        public Square FromSquare { get; set; }

        //Drawing squares - Do NOT change value from 64 unless necessary.
        private static readonly int SQUARE_SIZE = 64;

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

        //----------------------------------LEGAL MOVE LOGIC METHODS----------------------------------

        /// <summary>
        /// Computes legal moves that a rook would have (straight line moves)
        /// This is not to be used only by moves for rooks, Queens can reuse this.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves that a rook would have.</returns>



        /// <summary>
        /// Computes the legal moves a knight would have for a given square.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>>A set containing legal moves for a knight.</returns>


        /// <summary>
        /// Computes the legal moves a pawn would have for a given square.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves for a pawn.</returns>


        /// <summary>
        /// Computes the legal moves a bishop would have for a given square.
        /// This is not to be used only by moves for bishop, Queens can reuse this.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves for a bishop.</returns>

        public Piece checkForPromotion()
        {
            int tempRank = 0;
            String piece = "a";
            for (int i = 0; i < 8; i++)
            {
                if (Squares[tempRank][i].Piece != null)
                {
                    piece = Squares[tempRank][i].Piece.ToString().ToLower();
                }
                if (Squares[tempRank][i].PieceResident() && piece.Equals("p"))
                {
                    Promotion tmp = new Promotion();
                    if (tmp.ShowDialog() == DialogResult.OK)
                    {
                        return tmp.piece;
                    }
                    else
                    {
                        return new Pawn(true);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Computes the legal moves a king would have for a given square.
        /// </summary>
        /// <param name="source">A square containing a piece.</param>
        /// <returns>A set containing legal moves for a king.</returns>


        //----------------------------------UTILITY METHODS----------------------------------

        /// <summary>
        /// Checks if your king can castle queenside.
        /// </summary>
        /// <param name="ofKing">Square of the king the player wants to castle.</param>
        /// <returns>Return true iff you can castle queenside.</returns>


        /// <summary>
        /// Checks if your king can castle kingside.
        /// </summary>
        /// <param name="ofKing">Square of the king the player wants to castle.</param>
        /// <returns>Return true iff you can castle kingside.</returns>





        /// <summary>
        /// Utility method to append legal moves to a set. To be called inside trajectory method loops.
        /// </summary>
        /// <param name="source">The source square in which the piece resides.</param>
        /// <param name="target">The target square to which the piece will move.</param>
        /// <param name="fill">A set that is filled with all legal moves from a given square.</param>
        /// <returns>True iff we dont encounter a blockade for the trajectory, false otherwise.</returns>


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

        //----------------------------------DRAWING METHODS----------------------------------

        /// <summary>
        /// Physically displaces a piece from one square to another on the chessboard.
        /// </summary>
        /// <param name="point">Point used to find the square which we are moving to.</param>
        public void MakeMove(Point point)
        {
            Square square = GetSquare(point);

            List<Square> moves = new List<Square>();
            if (LegalMoves == null) return;
            foreach (Move i in LegalMoves)
            {
                moves.Add(i.GetToSquare());
            }

            if (moves.Contains(square) && FromSquare != null)
            {
                //map move onto board
                previous_setup = FEN.ToFEN(Squares);
                new Move(FromSquare, square).MakeMove();
                Piece tmp = checkForPromotion();
                if (tmp != null)
                {
                    square.Piece = tmp;
                }
                FromSquare = null;
            }

            foreach (Move i in LegalMoves)
            {
                i.GetToSquare().Available = false;
            }
            LegalMoves = null;
            this.SelectedPiece = null;
        }
        public void Rollback()
        {
            SetBoard(previous_setup);
        }
        public void CheckForRooks(Square Rook1)
        {  
            Square Rook2 = null;
            int counter = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Squares[i][j].PieceResident())
                    {
                        Piece targetPiece = Squares[i][j].Piece;
                        if (Squares[i][j].Piece.ToString().ToLower().Equals("r") && targetPiece.White && Squares[i][j].ToString()!=Rook1.ToString())
                        {
                            counter++;
                            Rook2 = Squares[i][j];
                        }
                    }
                }
            }
            if (counter == 2)
            {
                if (Rook1.File == Rook2.File)
                {
                    Rook1.Piece.setRank(Rook1.GetRankPhysical());
                    Rook2.Piece.setRank(Rook2.GetRankPhysical());
                    Rook1.Piece.setAddRank();
                    Rook2.Piece.setAddRank();
                }
                else
                {
                    Rook2.Piece.setFile(Rook2.GetFileChar(Rook2.File));
                    Rook1.Piece.setFile(Rook1.GetFileChar(Rook1.File));
                    Rook1.Piece.setAddFile();
                    Rook2.Piece.setAddFile();
                }
            }
        }
        public void CheckForKnigths(Square knight1)
        {
            Square knight2 = null;
            int counter = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Squares[i][j].PieceResident())
                    {
                        Piece targetPiece = Squares[i][j].Piece;
                        if (Squares[i][j].Piece.ToString().ToLower().Equals("n") && targetPiece.White && Squares[i][j].ToString() != knight1.ToString())
                        {
                            counter++;
                            knight2 = Squares[i][j];
                        }
                    }
                }
            }
            if (counter == 2)
            {
                if (knight1.File == knight2.File)
                {
                    knight1.Piece.setRank(knight1.GetRankPhysical());
                    knight2.Piece.setRank(knight2.GetRankPhysical());
                    knight1.Piece.setAddRank();
                    knight2.Piece.setAddRank();
                }
                else
                {
                    knight2.Piece.setFile(knight2.GetFileChar(knight2.File));
                    knight1.Piece.setFile(knight1.GetFileChar(knight1.File));
                    knight1.Piece.setAddFile();
                    knight2.Piece.setAddFile();
                    int xd = 0;
                }
            }

        }

        /// <summary>
        /// Finds the square over which a point (the cursor) is located.
        /// </summary>
        /// <param name="point">Point used to find the square which we are moving to.</param>
        /// <returns>The square where we want to place the piece we have picked up.</returns>
        public Square GetSquare(Point point)
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
                g.DrawImage(SelectedPiece.GetImageT(), new Point(Cursor.X - 32, Cursor.Y - 32));
                if (LegalMoves != null)
                {
                    foreach (Move i in LegalMoves)
                    {
                        i.GetToSquare().Available = true;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a duplicate of the piece over which the cursor is to selected piece. To be used with move
        /// </summary>
        public void Select(Point point)
        {
            Square square = GetSquare(point);

            if (square.Piece != null)
            {
                string piece = square.Piece.ToString().ToLower();
                switch (piece)
                {
                    case "p":
                        LegalMoves = Trajectories.PawnTrajectory(square);
                        break;
                    case "q":
                        LegalMoves = Trajectories.ForthrightTrajectory(square);
                        LegalMoves.UnionWith(Trajectories.DiagonalTrajectory(square));
                        break;
                    case "k":
                        LegalMoves = Trajectories.KingTrajectory(square);
                        break;
                    case "r":
                        CheckForRooks(square);
                        LegalMoves = Trajectories.ForthrightTrajectory(square);
                        break;
                    case "n":
                        CheckForKnigths(square);
                        LegalMoves = Trajectories.GTrajectory(square);
                        break;
                    case "b":
                        LegalMoves = Trajectories.DiagonalTrajectory(square);
                        break;
                }

                this.SelectedPiece = square.Piece;
                FromSquare = square;
            }

            this.Cursor = point;
        }

    }
}
