﻿using Chesscape.Chess.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Chesscape.Chess
{
    public class Trajectories
    {

        public static HashSet<Move> PawnTrajectory(Square source)
        {
            Square[][] squares = Board.GetInstance().Squares;
            HashSet<Move> legalMoves = new HashSet<Move>();
            HashSet<Square> IllegalKingMoves = new HashSet<Square>();

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
                    Square sq = squares[newRank][newFile];
                    if (!AroundBlackKing().Contains(sq) && !AllIllegalForPlayer().Contains(sq))
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


        public static bool CanCastleQueenside(Square ofKing)
        {
            Board b = Board.GetInstance();
            Square[][] squares = b.Squares;
            if ((ofKing.Piece as ICastleable).Moved()) return false;
            if (b.WhiteKingInCheck) return false;

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
                    return CastleHelper(checking);
                }
            }

            return false;
        }


        public static bool CanCastleKingside(Square ofKing)
        {
            Board b = Board.GetInstance();
            Square[][] squares = b.Squares;
            if ((ofKing.Piece as ICastleable).Moved()) return false;
            if (b.WhiteKingInCheck) return false;

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



        public static bool PieceStaring(Square checking)
        {
            bool imitate = false;

            if (!checking.PieceResident())
            {
                checking.Piece = new Pawn(true); // imitate a piece being placed to get the trajectory if square is without a piece.
                imitate = true;
            }

            HashSet<Move> legalsDiag = DiagonalTrajectory(checking);
            HashSet<Move> legalsForthRight = ForthrightTrajectory(checking);
            HashSet<Move> legalsG = GTrajectory(checking);
            HashSet<Move> legalsPawn = PawnTrajectory(checking);

            if (imitate)
            {
                checking.Piece = null;
            }

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

            foreach (Move move in legalsPawn)
            {
                if (!move.GetToSquare().PieceResident()) continue;
                Piece targetPiece = move.GetToSquare().Piece;
                bool pieceResidingBlack = !targetPiece.White;
                if (targetPiece is Pawn && pieceResidingBlack)
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

        private static HashSet<Square> AroundBlackKing()
        {
            HashSet<Square> nearby = new HashSet<Square>();
            Board b = Board.GetInstance();

            Square ofKing = b.KingSquare(false);

            int row = ofKing.GetRankPhysical();
            int col = ofKing.File;


            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (CheckValid(i, j))
                    {
                        nearby.Add(b.Squares[i][j]);
                    }
                }
            }

            foreach (var item in nearby)
            {
                Debug.WriteLine(item);
            }

            return nearby;
        }

        private static HashSet<Square> AllIllegalForPlayer()
        {

            HashSet<Square> retval = new HashSet<Square>();
            Board b = Board.GetInstance();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    HashSet<Square> currentlyAppend = new HashSet<Square>();
                    Square square = b.Squares[i][j];
                    if (square.PieceResident() && !square.Piece.White)
                    {
                        string piece = square.Piece.FENNotation().ToLower();
                        switch (piece)
                        {
                            case "p":
                                if (CheckValid(i + 1, j + 1))
                                {
                                    currentlyAppend.Add(b.Squares[i + 1][j+1]);
                                }
                                if (CheckValid(i + 1, j - 1))
                                {
                                    currentlyAppend.Add(b.Squares[i + 1][j-1]);
                                }
                                break;
                            case "q":
                                currentlyAppend = Trajectories.ForthrightTrajectory(square).Select(sq => sq.GetToSquare()).ToHashSet();
                                currentlyAppend.UnionWith(Trajectories.DiagonalTrajectory(square).Select(sq => sq.GetToSquare()).ToHashSet());
                                break;
                            case "k":
                                break;
                            case "r":
                                currentlyAppend = Trajectories.ForthrightTrajectory(square).Select(sq => sq.GetToSquare()).ToHashSet();
                                break;
                            case "n":
                                currentlyAppend = Trajectories.GTrajectory(square).Select(sq => sq.GetToSquare()).ToHashSet();
                                break;
                            case "b":
                                currentlyAppend = Trajectories.DiagonalTrajectory(square).Select(sq => sq.GetToSquare()).ToHashSet();
                                break;
                        }
                        retval.UnionWith(currentlyAppend);
                    }
                }
            }
            return retval;
        }



        private static bool AppendMove(Square source, Square target, HashSet<Move> fill)
        {
            Board inst = Board.GetInstance();
            if ((target.PieceResident() && (source.Piece.White != target.Piece.White)) || !target.PieceResident())
            {
                Move toValidate = new Move(source, target);
                if (inst.WhiteKingInCheck)
                {
                    toValidate.MakeMove(true);
                    target.Invisible = true;
                    inst.WhiteKingInCheck = false;
                    if (!PieceStaring(inst.KingSquare(true)))
                    {
                        inst.WhiteKingInCheck = true;
                        inst.Rollback();
                        target.Invisible = false;
                        return fill.Add(toValidate);
                    }
                    else
                    {
                        inst.WhiteKingInCheck = true;
                        inst.Rollback();
                        target.Invisible = false;
                        return false;
                    }
                }
                else return fill.Add(toValidate);
            }
            return false;
        }

    }
}


