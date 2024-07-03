using Chesscape.Chess.Internals;
using Chesscape.Chess.Internals;
using Chesscape.Chess.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public global::Chesscape.Puzzle.Puzzle currentPuzzle { get; set; }
        public TacticsForm tf { get; set; }


        //DRAWING ATTRIBUTES
        //Selection & Moving
        private bool stopAnnoying = false;
        private Color CWhite = Color.FromArgb(255, 255, 253, 208);
        private Color CBlack = Color.FromArgb(255, 210, 180, 140);
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

        public void SetColorsDef()
        {

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"board_colors.txt"));

            File.WriteAllText(path, "255,253,208\n");
            File.AppendAllText(path, "210, 180, 140\n");

            ReadColorsFromDefaultDirective();

        }

        public void SetColors()
        {

            if (!stopAnnoying)
            {
                MessageBox.Show("First, select a color for the white squares, then the black squares.", "Color Selection", MessageBoxButtons.OK);
                stopAnnoying = true;
            }

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"board_colors.txt"));

            string appendWhite = $"{CWhite.R},{CWhite.G},{CWhite.B}";
            string appendBlack = $"{CBlack.R},{CBlack.G},{CBlack.B}";

            ColorDialog cdWhite = new ColorDialog();
            ColorDialog cdBlack = new ColorDialog();

            if (cdWhite.ShowDialog() == DialogResult.OK)
            {
                Color forWhite = cdWhite.Color;
                if (forWhite != null)
                    appendWhite = $"{forWhite.R},{forWhite.G},{forWhite.B}";
            }

            if (cdBlack.ShowDialog() == DialogResult.OK)
            {
                Color forBlack = cdBlack.Color;
                if (forBlack != null)
                    appendBlack = $"{forBlack.R},{forBlack.G},{forBlack.B}";
            }

            File.WriteAllText(path, appendWhite + "\n");
            File.AppendAllText(path, appendBlack + "\n");

            ReadColorsFromDefaultDirective();

        }

        public void SetPuzzle(Puzzle.Puzzle puzzle)
        {
            this.currentPuzzle = puzzle;
        }

        public void SetForm(TacticsForm tf)
        {
            this.tf = tf;
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
        public void SetSquaresTheme()
        {
            bool white = true;

            ReadColorsFromDefaultDirective();

            int y_incrementer = 53;
            for (int i = 0; i < 8; ++i, y_incrementer += 64)
            {
                int x_incrementer = 53;
                for (int j = 0; j < 8; ++j, x_incrementer += 64)
                {
                    if (white)
                    {
                        SingleBoard.Squares[i][j].ColorDraw = CWhite;
                    }
                    else
                    {
                        SingleBoard.Squares[i][j].ColorDraw = CBlack;
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
            if (check.GetRankPhysical() == 0)
            {
                Promotion tmp = new Promotion();
                if (tmp.ShowDialog() == DialogResult.OK)
                {

                    return tmp.piece;
                }
            }
            return null;
        }

        //----------------------------------UTILITY METHODS----------------------------------

        private void ReadColorsFromDefaultDirective() {

            string currentDirectory = Directory.GetCurrentDirectory();
            string[] currentColors = File.ReadAllLines(Path.GetFullPath(Path.Combine(currentDirectory, @"board_colors.txt")));

            List<int> whiteColors = currentColors[0].Split(',').ToList<string>().Select(val => int.Parse(val)).ToList<int>();
            List<int> blackColors = currentColors[1].Split(',').ToList<string>().Select(val => int.Parse(val)).ToList<int>();

            CWhite = Color.FromArgb(255, whiteColors[0], whiteColors[1], whiteColors[2]);
            CBlack = Color.FromArgb(255, blackColors[0], blackColors[1], blackColors[2]);

        }

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
        public async void MakeMove(Point point)
        {
            if (LegalMoves == null) return;
            Square square = GetSquare(point);
            List<Square> moveTo = new List<Square>();

            foreach (Move i in LegalMoves)
            {
                moveTo.Add(i.GetToSquare());
            }
            bool promotion = false;


            string preloadCheckPos = null;
            StringBuilder currentplayermove = new StringBuilder();
            Move player_Move = null;
            string next_move = currentPuzzle.GetNextMove();
            if (next_move.Contains("+") || next_move.Contains("#"))
            {
                next_move = next_move.Substring(0, next_move.Length - 1);
            }
            if (moveTo.Contains(square) && FromSquare != null)
            {
                preloadCheckPos = FromSquare.ToString();

                //map move onto board
                player_Move = new Move(FromSquare, square);
                currentplayermove.Append(player_Move);
                if (knight_rook_check(next_move, next_move.Substring(0, 1)))
                {
                    currentplayermove = new StringBuilder();
                    currentplayermove.Append(player_Move.ToString().Substring(0, 1));
                    currentplayermove.Append(FromSquare.GetFileChar(FromSquare.File));
                    currentplayermove.Append(player_Move.ToString().Substring(1, player_Move.ToString().Length - 1));
                }
                if (FromSquare.Piece.ToString().ToLower().Equals("p"))
                {
                    Piece check = CheckForPromotion(square);
                    if (check != null)
                    {
                        currentplayermove.Append("/" + check.ToString());
                        FromSquare.Piece = check;
                        promotion = true;
                    }
                }
                if (currentplayermove.ToString().Equals(next_move))
                {
                    player_Move.MakeMove(false);
                    if (square.Piece.ToString().ToLower().Equals("p"))
                    {
                        EnPassantCheck(square);
                    }
                    currentPuzzle.Increment();
                    foreach (Move i in LegalMoves)
                    {
                        i.GetToSquare().Available = false;
                    }

                    if (preloadCheckPos != null && !preloadCheckPos.Equals(square.ToString()))
                        Trajectories.PreloadIllegalOfBlack();
                    LegalMoves = null;
                    this.SelectedPiece = null;
                    FromSquare = null;
                    await Task.Delay(750);
                    next_move = currentPuzzle.GetNextMove();
                    if (next_move.Equals("GAME OVER"))
                    {
                        tf.UpdateMoves();
                        MessageBox.Show("Puzzle Completed! Good Job!");
                        DialogResult result = MessageBox.Show("Would you like a problem of the same category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            tf.menu.update_elo(this.currentPuzzle);
                            tf.generate_next();
                            return;
                        }
                        else if (result == DialogResult.No)
                        {
                            tf.DialogResult = DialogResult.Yes;
                            return;
                        }
                    }
                    if (next_move.Contains("+") || next_move.Contains("#"))
                    {
                        next_move = next_move.Substring(0, next_move.Length - 1);
                    }
                    BlackMove(next_move);
                    currentPuzzle.Increment();
                    if (EnPassantTarget != null)
                    {
                        EnPassantTarget = null;
                    }
                    tf.UpdateMoves();
                    return;
                }
                else
                {
                    foreach (Move i in LegalMoves)
                    {
                        i.GetToSquare().Available = false;
                    }

                    if (preloadCheckPos != null && !preloadCheckPos.Equals(square.ToString()))
                        Trajectories.PreloadIllegalOfBlack();
                    LegalMoves = null;
                    this.SelectedPiece = null;
                    if (promotion)
                    {
                        FromSquare.Piece = new Pawn(true);
                    }
                    if (EnPassantTarget != null)
                    {
                        EnPassantTarget = null;
                    }
                    MessageBox.Show("Wrong Move! Try again.");
                    return;
                }
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
        public bool knight_rook_check(string move, string piece)
        {
            if ((piece.ToLower().Equals("r") || piece.ToLower().Equals("n")) && char.IsLetter(move[1]) && char.IsLetter(move[2]) && !move[1].Equals('x'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void BlackMove(string move)
        {
            string piece = getPiece(move.Substring(0, 1));
            bool rook_knight_check = false;
            if (knight_rook_check(move, piece))
            {
                rook_knight_check = true;
            }
            string square = move.Substring(move.Length - 2);
            Piece blackpiece = null;
            Square toMove = null;
            Square todelete = null;
            bool edge_case = true;
            int xi = 0;
            int xy = 0;
            for (int i = 0; i < 8; i++)
            {
                if (blackpiece != null && toMove != null)
                {
                    break;
                }
                for (int j = 0; j < 8; j++)
                {
                    if (Squares[i][j].PieceResident())
                    {
                        if (!Squares[i][j].Piece.White && Squares[i][j].Piece.ToString().ToLower().Equals(piece.ToLower()))
                        {
                            HashSet<Move> LegalMoves = null;
                            switch (piece.ToLower())
                            {
                                case "q":
                                    blackpiece = Squares[i][j].Piece;
                                    Squares[i][j].Piece = null;
                                    LegalMoves = null;
                                    break;
                                case "k":
                                    LegalMoves = null;
                                    blackpiece = Squares[i][j].Piece;
                                    Squares[i][j].Piece = null;
                                    break;
                                case "r":
                                    LegalMoves = Trajectories.ForthrightTrajectory(Squares[i][j]);
                                    break;
                                case "n":
                                    LegalMoves = Trajectories.GTrajectory(Squares[i][j]);
                                    break;
                                case "b":
                                    LegalMoves = Trajectories.DiagonalTrajectory(Squares[i][j]);
                                    break;
                                case "p":
                                    string piece_square = Squares[i][j].ToString().Substring(0, 1);
                                    string to_find = move.Substring(0, 1);
                                    if (piece_square.Equals(to_find))
                                    {
                                        blackpiece = Squares[i][j].Piece;
                                        Squares[i][j].Piece = null;
                                    }
                                    break;
                            }
                            if (LegalMoves != null)
                            {
                                foreach (Move tmp in LegalMoves)
                                {
                                    if (rook_knight_check)
                                    {
                                        StringBuilder currentplayermove = new StringBuilder();
                                        currentplayermove.Append(tmp.ToString().Substring(0, 1));
                                        currentplayermove.Append(tmp.GetFromSquare().GetFileChar(tmp.GetFromSquare().File));
                                        currentplayermove.Append(tmp.ToString().Substring(1, tmp.ToString().Length - 1));
                                        if (currentplayermove.ToString().Equals(move.ToLower()))
                                        {
                                            blackpiece = Squares[i][j].Piece;
                                            todelete = Squares[i][j];
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (tmp.ToString().Equals(move.ToLower()))
                                        {
                                            blackpiece = Squares[i][j].Piece;
                                            Squares[i][j].Piece = null;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Squares[i][j].ToString().Equals(square))
                    {
                        toMove = Squares[i][j];
                    }
                }
            }
            if (todelete != null)
            {
                todelete.Piece = null;
            }
            rook_knight_check = false;
            toMove.Piece = blackpiece;
            PreviousSetup = FEN.ToFEN(Squares);
            if (piece.Equals("p"))
            {
                EnPassantCheck(toMove);
            }
        }
        public void EnPassantCheck(Square check)
        {
            if (Squares[check.File][check.GetRankPhysical() - 1].PieceResident())
            {
                if (Squares[check.File][check.GetRankPhysical() - 1].Piece.ToString().ToLower().Equals("p"))
                {
                    EnPassantTarget = check;
                }
            }
            if (Squares[check.File][check.GetRankPhysical() + 1].PieceResident())
            {
                if (Squares[check.File][check.GetRankPhysical() + 1].Piece.ToString().ToLower().Equals("p"))
                {
                    EnPassantTarget = check;
                }
            }
        }

        public string getPiece(string move)
        {
            string to_return = "";
            switch (move.ToLower())
            {
                case "q":
                    to_return = "q";
                    break;
                case "k":
                    to_return = "k";
                    break;
                case "r":
                    to_return = "r";
                    break;
                case "n":
                    to_return = "n";
                    break;
                case "b":
                    to_return = "b";
                    break;
                default:
                    to_return = "p";
                    break;
            }
            return to_return;
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
