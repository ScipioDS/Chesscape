using Chesscape.Chess.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


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
        //En passant target justMoved
        public Square EnPassantTarget { get; set; }
        //Turn
        public bool WhiteToPlay { get; set; }
        //Legal moveTo
        private HashSet<Move> LegalMoves { get; set; }
        public bool KingInCheck { get; set; }
        public bool WhiteKingInCheck { get; set; }
        public string PreviousSetup { get; set; }


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

            KingInCheck = false;
            WhiteKingInCheck = false;

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
        /// Board perspective drawing logic. Sets the basis justMoved colors from which the board will be drawn.
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
                        SingleBoard.Squares[i][j].ColorDraw = Color.FromArgb(255, 232, 235, 239);
                    }
                    else
                    {
                        SingleBoard.Squares[i][j].ColorDraw = Color.FromArgb(255, 125, 135, 150);
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

        public Piece CheckForPromotion(Square check)
        {
            if(check.GetRankPhysical() == 0)
            {
                Promotion tmp = new Promotion();
                if(tmp.ShowDialog() == DialogResult.OK)
                {
                    
                    return tmp.piece;
                }
            }
            return null;
        }

        //----------------------------------UTILITY METHODS----------------------------------

        public Square KingSquare(bool white)
        {
            Square ofKing = null;
            for (byte i = 0; i < 8; ++i)
            {
                for (byte j = 0; j < 8; ++j)
                {
                    if (Squares[i][j].PieceResident() && Squares[i][j].Piece is King && (Squares[i][j].Piece.White == white))
                    {
                        ofKing = Squares[i][j]; break;
                    }
                }
                if (ofKing != null) break;
            }
            return ofKing;
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
                    string toAppend = Squares[i][j].Piece == null ? "#" : Squares[i][j].Piece.FENNotation();
                    sb.Append(" ").Append(toAppend);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public void Rollback()
        {
            SetBoard(PreviousSetup);
        }

        //TODO: adapt for usage to remove red squares (king checked highlight, or keep it)
        private void CheckScan(Square justMoved)
        {
            throw new NotImplementedException();
        }

        public void RemoveCheck()
        {
            for (byte i = 0; i < 8; ++i)
            {
                for (byte j = 0; j < 8; ++j)
                {
                    if (Squares[i][j].IsInCheck())
                    {
                        KingInCheck = false;
                        Squares[i][j].KingChecked(false);
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// Returns the justMoved in the Board matrix with the position passed as an argument
        /// </summary>
        /// <param name="position">A formally defined justMoved position.</param>
        /// <returns>A Square object on the board matrix.</returns>
        public static Square PositionToSquare(string position)
        {
            int file = Square.FileToNumeric[position[0]];
            int rank = int.Parse(position[1].ToString()) - 1;

            return SingleBoard.Squares[7 - rank][file];
        }

        //----------------------------------DRAWING METHODS----------------------------------

        /// <summary>
        /// Physically displaces a piece from one justMoved to another on the chessboard.
        /// </summary>
        /// <param name="point">Point used to find the justMoved which we are moving to.</param>
        public void MakeMove(Point point)
        {
            if (LegalMoves == null) return;

            Square square = GetSquare(point);
            List<Square> moveTo = new List<Square>();

            foreach (Move i in LegalMoves)
            {
                moveTo.Add(i.GetToSquare());
            }

            string preloadCheckPos = null;

            if (moveTo.Contains(square) && FromSquare != null)
            {
                preloadCheckPos = FromSquare.ToString();

                //map move onto board
                new Move(FromSquare, square).MakeMove(false);
                if (square.Piece.ToString().ToLower().Equals("p"))
                {
                    Piece tmp = CheckForPromotion(square);
                    if (tmp != null)
                    {
                        square.Piece = tmp;
                        PreviousSetup = FEN.ToFEN(Squares);
                    }
                }

                FromSquare = null;
            }

            foreach (Move i in LegalMoves)
            {
                i.GetToSquare().Available = false;
            }

            if (preloadCheckPos != null && !preloadCheckPos.Equals(square.ToString()))
                Trajectories.PreloadIllegalOfBlack();

            LegalMoves = null;
            this.SelectedPiece = null;
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
                        if (Squares[i][j].Piece.ToString().ToLower().Equals("r") && targetPiece.White && Squares[i][j].ToString() != Rook1.ToString())
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
                }
            }

        }

        /// <summary>
        /// Finds the justMoved over which a point (the cursor) is located.
        /// </summary>
        /// <param name="point">Point used to find the justMoved which we are moving to.</param>
        /// <returns>The justMoved where we want to place the piece we have picked up.</returns>
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
        /// Method that calls upon each justMoved to draw itself.
        /// </summary>
        /// <param name="g">Graphics object obtained form Paint method arguments in TacticsForm.cs</param>
        /// <param name="topLeft">The top left point of the justMoved itself.</param>
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
                string piece = square.Piece.FENNotation().ToLower();
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


                this.Cursor = point;
            }

        }
    }
}
