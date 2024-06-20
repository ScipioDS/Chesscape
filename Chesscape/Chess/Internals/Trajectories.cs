using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Chesscape.Chess
{
    public class Trajectories
    {

        private static readonly Board single = Board.GetInstance();
        private static HashSet<Square> fastIllegalSet = new HashSet<Square>(); 

        //----------------------------------------------Addition Trajectories----------------------------------------------

        public static HashSet<Move> PawnTrajectory(Square source)
        {
            Square[][] squares = single.Squares;
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

            Square[][] squares = single.Squares;
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
                    Square checking = squares[newRank][newFile];
                    if (!fastIllegalSet.Contains(checking))
                        AppendMove(source, squares[newRank][newFile], legalMoves);
                }
            }

            if (CanCastleKingside(source))
            {
                legalMoves.Add(new Move(source, squares[Rank][File + 2]));
            }
            if (CanCastleQueenside(source))
            {
                legalMoves.Add(new Move(source, squares[Rank][File - 2]));
            }

            return legalMoves;
        }


        public static HashSet<Move> DiagonalTrajectory(Square source)
        {

            Square[][] squares = single.Squares;
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
            Square[][] squares = single.Squares;
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
            Square[][] squares = single.Squares;
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

        //----------------------------------------------Utility----------------------------------------------

        public static void PreloadIllegalOfBlack()
        {
            fastIllegalSet = AllIllegalNoFrills(); fastIllegalSet.UnionWith(AroundBlackKing());
        }

        private static bool CheckValid(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }

        public static bool CanCastleQueenside(Square ofKing)
        {
            Square[][] squares = single.Squares;
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

                    if (fastIllegalSet.Contains(checking)) return false;
                }
                else
                {
                    return CastleHelper(checking);
                }
            }

            return false;
        }


        public static bool CanCastleKingside(Square ofKing)
        {
            Square[][] squares = single.Squares;
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
                    if (AllIllegalNoFrills().Contains(checking)) return false;
                }
                else
                {
                    return CastleHelper(checking);
                }
            }

            //reaching this means that there is an error
            return false;
        }

        public static bool CastleHelper(Square checking)
        {
            if (!checking.PieceResident()) return false;
            else if (!(checking.Piece is Rook)) return false;
            return !(checking.Piece as Rook).Moved();
        }

        private static HashSet<Square> AroundBlackKing()
        {
            HashSet<Square> nearby = new HashSet<Square>();

            Square ofKing = single.KingSquare(false);

            int row = ofKing.GetRankPhysical();
            int col = ofKing.File;


            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (CheckValid(i, j))
                    {
                        nearby.Add(single.Squares[i][j]);
                    }
                }
            }

            return nearby;
        }

        private static bool AppendMove(Square source, Square target, HashSet<Move> fill)
        {
            if ((target.PieceResident() && (source.Piece.White != target.Piece.White)) || !target.PieceResident())
            {
                Move toValidate = new Move(source, target);

                bool result = false;
                toValidate.MakeMove(true);
                target.Invisible = true;

                if (!AllIllegalNoFrills().Contains(single.KingSquare(true)))
                {
                    result = true;
                }

                single.Rollback();
                target.Invisible = false;

                if (result)
                {
                    fill.Add(toValidate);
                }

                return result;
            }
            return false;
        }

        //----------------------------------------------Legality Trajectories----------------------------------------------

        private static HashSet<Square> AllIllegalNoFrills()
        {
            HashSet<Square> retval = new HashSet<Square>();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    HashSet<Square> currentlyAppend = new HashSet<Square>();
                    Square square = single.Squares[i][j];
                    if (square.PieceResident() && !square.Piece.White)
                    {
                        string piece = square.Piece.FENNotation().ToLower();
                        switch (piece)
                        {
                            case "p":
                                if (CheckValid(i + 1, j + 1))
                                {
                                    currentlyAppend.Add(single.Squares[i + 1][j + 1]);
                                }
                                if (CheckValid(i + 1, j - 1))
                                {
                                    currentlyAppend.Add(single.Squares[i + 1][j - 1]);
                                }
                                break;
                            case "q":
                                currentlyAppend = ForthrightTrajectoryNoFrills(square);
                                currentlyAppend.UnionWith(DiagonalTrajectoryNoFrills(square));
                                break;
                            case "k":
                                break;
                            case "r":
                                currentlyAppend = ForthrightTrajectoryNoFrills(square);
                                break;
                            case "n":
                                currentlyAppend = GTrajectoryNoFrills(square);
                                break;
                            case "b":
                                currentlyAppend = DiagonalTrajectoryNoFrills(square);
                                break;
                        }
                        retval.UnionWith(currentlyAppend);
                    }
                }
            }
            return retval;
        }


        private static HashSet<Square> DiagonalTrajectoryNoFrills(Square source)
        {
            Square[][] squares = single.Squares;
            HashSet<Square> legalMoves = new HashSet<Square>();

            // bottom right
            for (int i = source.GetRankPhysical() + 1, j = source.File + 1; i < 8 && j < 8; ++i, ++j)
            {
                if (!AppendNoFrills(source, squares[i][j], legalMoves) || (squares[i][j].PieceResident() && source.Piece.White != squares[i][j].Piece.White)) break;

            }
            // top right
            for (int i = source.GetRankPhysical() - 1, j = source.File + 1; i >= 0 && j < 8; --i, ++j)
            {
                if (!AppendNoFrills(source, squares[i][j], legalMoves) || (squares[i][j].PieceResident() && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            //top left
            for (int i = source.GetRankPhysical() - 1, j = source.File - 1; i >= 0 && j >= 0; --i, --j)
            {
                if (!AppendNoFrills(source, squares[i][j], legalMoves) || (squares[i][j].PieceResident() && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            //bottom left
            for (int i = source.GetRankPhysical() + 1, j = source.File - 1; i < 8 && j >= 0; ++i, --j)
            {
                if (!AppendNoFrills(source, squares[i][j], legalMoves) || (squares[i][j].PieceResident() && source.Piece.White != squares[i][j].Piece.White)) break;
            }

            return legalMoves;
        }

        private static HashSet<Square> ForthrightTrajectoryNoFrills(Square source)
        {
            Square[][] squares = single.Squares;
            HashSet<Square> legalMoves = new HashSet<Square>();

            int sJ = source.File;
            int sI = source.GetRankPhysical();

            //up
            for (int i = source.GetRankPhysical() - 1; i >= 0; --i)
            {
                if (!AppendNoFrills(source, squares[i][sJ], legalMoves)
                    ||
                    (squares[i][sJ].PieceResident() && source.Piece.White != squares[i][sJ].Piece.White)) break;
            }

            //down
            for (int i = source.GetRankPhysical() + 1; i < 8; ++i)
            {
                if (!AppendNoFrills(source, squares[i][sJ], legalMoves)
                    ||
                    (squares[i][sJ].PieceResident() && source.Piece.White != squares[i][sJ].Piece.White)) break;
            }

            //left
            for (int j = source.File - 1; j >= 0; --j)
            {
                if (!AppendNoFrills(source, squares[sI][j], legalMoves)
                    ||
                    (squares[sI][j].PieceResident() && source.Piece.White != squares[sI][j].Piece.White)) break;
            }

            //right
            for (int j = source.File + 1; j < 8; ++j)
            {
                if (!AppendNoFrills(source, squares[sI][j], legalMoves)
                    ||
                    (squares[sI][j].PieceResident() && source.Piece.White != squares[sI][j].Piece.White)) break;

            }

            return legalMoves;
        }

        private static HashSet<Square> GTrajectoryNoFrills(Square source)
        {
            Square[][] squares = single.Squares;
            HashSet<Square> legalMoves = new HashSet<Square>();

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
                    AppendNoFrills(source, squares[newRank][newFile], legalMoves);
                }
            }

            return legalMoves;

        }

        private static bool AppendNoFrills(Square source, Square target, HashSet<Square> fill)
        {
            if ((target.PieceResident() && (source.Piece.White != target.Piece.White)) || !target.PieceResident())
                return fill.Add(target);

            return false;
        }
    }
}




