using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesscape.Chess
{
    public class Trajectories
    {

        
        
        public static HashSet<Move> PawnTrajectory(Square source)
        {
            Square [] [] squares=Board.GetInstance().Squares;
            HashSet<Move> legalMoves = new HashSet<Move>();
            int File = source.File;
            int Rank = source.GetRankPhysical();
            if (Rank == 6 && squares[Rank - 1][File].PieceResident())
            {
                return legalMoves;
            }
            //eden cekor napred
            if (CheckValid(Rank - 1, File) && !squares[Rank - 1][File].PieceResident())
            {
                AppendMove(source, squares[Rank - 1][File], legalMoves);
            }
            //provervit dali e na staring line, pa aku e go dodavat i to
            if (Rank >= 6 && CheckValid(Rank - 2, File))
            {
                AppendMove(source, squares[Rank - 2][File], legalMoves);
            }
            //diagonalno od levo(ms?) provervit za capture
            if (CheckValid(File - 1, Rank - 1) && squares[Rank - 1][File - 1].PieceResident())
            {
                AppendMove(source, squares[Rank - 1][File - 1], legalMoves);
            }
            //diagonalno od desno(ne sum sig) provervit za capture
            if (CheckValid(Rank - 1, File + 1) && squares[Rank - 1][File + 1].PieceResident())
            {
                AppendMove(source, squares[Rank - 1][File + 1], legalMoves);
            }
            return legalMoves;
        }
        public static HashSet<Move> KingTrajectory(Square source)
        {
            Square[][] squares = Board.GetInstance().Squares;
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
                if (CheckValid(newRank, newFile))
                {
                    AppendMove(source, squares[newRank][newFile], legalMoves);
                }
            }

            if (Trajectories.CanCastleKingside(source))
            {
                legalMoves.Add(new Move(source, squares[Rank][File + 2]));
            }
            if (CanCastleQueenside(source))
            {
                legalMoves.Add(new Move(source, squares[Rank][File - 2]));
            }


            return legalMoves;
        }
        public static bool CanCastleQueenside(Square ofKing)
        {
            Square[][] squares = Board.GetInstance().Squares;
            if ((ofKing.Piece as ICastleable).Moved()) return false;

            int rankKing = ofKing.GetRankPhysical();
            int fileKing = ofKing.File;

            for (int j = fileKing - 1; j >= 0; --j)
            {
                Square checking = squares[rankKing][j];
                if (j != 0)
                {
                    if (checking.PieceResident())
                        return false;
                    if (PieceStaring(checking)) return false;
                }
                else
                {
                    return Trajectories.CastleHelper(checking);
                }
            }

            return false;
        }
        public static bool CanCastleKingside(Square ofKing)
        {
            Square[][] squares = Board.GetInstance().Squares;
            if ((ofKing.Piece as ICastleable).Moved()) return false;

            int rankKing = ofKing.GetRankPhysical();
            int fileKing = ofKing.File;

            for (int j = fileKing + 1; j < 8; ++j)
            {
                Square checking = squares[rankKing][j];
                if (j != 7)
                {
                    if (checking.PieceResident())
                        return false;
                    if (PieceStaring(checking)) return false;
                }
                else
                {
                    return Trajectories.CastleHelper(checking);
                }
            }

            //reaching this means that there is an error
            return false;
        }

        public static bool CastleHelper(Square checking)
        {
            Square[][] squares = Board.GetInstance().Squares;
            if (!checking.PieceResident()) return false;
            else if (!(checking.Piece is Rook)) return false;
            return !(checking.Piece as Rook).Moved();
        }

        public static bool PieceStaring(Square checking)
        {
            Square[][] squares = Board.GetInstance().Squares;
            checking.Piece = new Pawn(true); // Imitate a piece being placed to get the trajectory.

            HashSet<Move> legalsDiag = DiagonalTrajectory(checking);
            HashSet<Move> legalsForthRight = ForthrightTrajectory(checking);
            HashSet<Move> legalsG = GTrajectory(checking);
            HashSet<Move> legalsPawn = PawnTrajectory(checking);

            checking.Piece = null;

            foreach (Move move in legalsDiag)
            {
                if (!move.GetToSquare().PieceResident()) continue;
                Piece targetPiece = move.GetToSquare().Piece;
                bool pieceResidingBlack = !targetPiece.White;
                if ((targetPiece is Bishop || targetPiece is Queen) && pieceResidingBlack)
                {
                    return true;
                }
            }

            foreach (Move move in legalsForthRight)
            {
                if (!move.GetToSquare().PieceResident()) continue;
                Piece targetPiece = move.GetToSquare().Piece;
                bool pieceResidingBlack = !targetPiece.White;
                if ((targetPiece is Rook || targetPiece is Queen) && pieceResidingBlack)
                {
                    return true;
                }
            }

            foreach (Move move in legalsG)
            {
                if (!move.GetToSquare().PieceResident()) continue;
                Piece targetPiece = move.GetToSquare().Piece;
                bool pieceResidingBlack = !targetPiece.White;
                if (targetPiece is Knight && pieceResidingBlack)
                {
                    return true;
                }
            }



            return false;
        }
        public static HashSet<Move> DiagonalTrajectory(Square source)
        {
            Square[][] squares = Board.GetInstance().Squares;
            HashSet<Move> legalMoves = new HashSet<Move>();

            // bottom right
            for (int i = source.GetRankPhysical() + 1, j = source.File + 1; i < 8 && j < 8; ++i, ++j)
            {
                if (!AppendMove(source, squares[i][j], legalMoves) || (squares[i][j].Piece != null && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            // top right
            for (int i = source.GetRankPhysical() - 1, j = source.File + 1; i >= 0 && j < 8; --i, ++j)
            {
                if (!AppendMove(source, squares[i][j], legalMoves) || (squares[i][j].Piece != null && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            //top left
            for (int i = source.GetRankPhysical() - 1, j = source.File - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (!AppendMove(source, squares[i][j], legalMoves) || (squares[i][j].Piece != null && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            //bottom left
            for (int i = source.GetRankPhysical() + 1, j = source.File - 1; i < 8 && j >= 0; ++i, --j)
            {
                if (!AppendMove(source, squares[i][j], legalMoves) || (squares[i][j].Piece != null && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            return legalMoves;
        }
        public static HashSet<Move> GTrajectory(Square source)
        {
            Square[][] squares = Board.GetInstance().Squares;
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
                if (CheckValid(newRank, newFile))
                {
                    AppendMove(source, squares[newRank][newFile], legalMoves);
                }
            }
            return legalMoves;
        }
        public static HashSet<Move> ForthrightTrajectory(Square source)
        {
            Square[][] squares = Board.GetInstance().Squares;
            HashSet<Move> legalMoves = new HashSet<Move>();

            int sJ = source.File;
            int sI = source.GetRankPhysical();

            //up
            for (int i = source.GetRankPhysical() - 1; i >= 0; --i)
            {
                if (!AppendMove(source, squares[i][sJ], legalMoves)
                    ||
                    (squares[i][sJ].Piece != null && source.Piece.White != squares[i][sJ].Piece.White)) break;
            }

            //down
            for (int i = source.GetRankPhysical() + 1; i < 8; ++i)
            {
                if (!AppendMove(source, squares[i][sJ], legalMoves)
                    ||
                    (squares[i][sJ].Piece != null && source.Piece.White != squares[i][sJ].Piece.White)) break;
            }

            //left
            for (int j = source.File - 1; j >= 0; --j)
            {
                if (!AppendMove(source, squares[sI][j], legalMoves)
                    ||
                    (squares[sI][j].Piece != null && source.Piece.White != squares[sI][j].Piece.White)) break;
            }

            //right
            for (int j = source.File + 1; j < 8; ++j)
            {
                if (!AppendMove(source, squares[sI][j], legalMoves)
                    ||
                    (squares[sI][j].Piece != null && source.Piece.White != squares[sI][j].Piece.White)) break;

            }

            return legalMoves;
        }
        private static bool CheckValid(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }
        private static bool AppendMove(Square source, Square target, HashSet<Move> fill)
        {
            Square[][] squares = Board.GetInstance().Squares;
            if ((target.PieceResident() && (source.Piece.White != target.Piece.White)) || !target.PieceResident())
            {
                return fill.Add(new Move(source, target));
            }
            return false;
        }

    }
}


