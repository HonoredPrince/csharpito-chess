using ChessBoard;
using System;
using ChessBoard.Enums;
using Match;
using System.Collections.Generic;

namespace View{
    class ScreenRenderer{
        public static void RenderBoard(Board board){
            for(int i = 0; i < board.Lines; i++){
                System.Console.Write(8 - i + " ");
                for(int j = 0; j < board.Columns; j++){
                    PrintColoredPiece(board.GetPiece(i, j));
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
        }

        public static void RenderBoard(Board board, bool[,] possibleMoves){
            
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor moveLocationBackground = ConsoleColor.DarkGray;

            for(int i = 0; i < board.Lines; i++){
                System.Console.Write(8 - i + " ");
                for(int j = 0; j < board.Columns; j++){
                    if(possibleMoves[i, j]){
                        Console.BackgroundColor = moveLocationBackground;
                    }else{
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintColoredPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static void RenderGameMatch(ChessMatch gameMatch){
            ScreenRenderer.RenderBoard(gameMatch.Board);
            System.Console.WriteLine();

            RenderCapturedPieces(gameMatch);

            System.Console.WriteLine();
            System.Console.WriteLine("Turn:" + gameMatch.Turn);
            System.Console.WriteLine($"Waiting play from: {gameMatch.CurrentColorPlayer}");
        }

        public static void RenderCapturedPieces(ChessMatch gameMatch){
            System.Console.WriteLine("Captured Pieces:");
            System.Console.Write("Whites: ");
            RenderCapturedPiecesBySet(gameMatch.GetCapturedPiecesByColorType(Color.White));
            System.Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Write("Blacks: ");
            RenderCapturedPiecesBySet(gameMatch.GetCapturedPiecesByColorType(Color.Black));
            Console.ForegroundColor = aux;
            System.Console.WriteLine();
        }

        public static void RenderCapturedPiecesBySet(HashSet<Piece> pieceSet){
            System.Console.Write("[");
            foreach(Piece piece in pieceSet){
                System.Console.Write(piece + " ");
            }
            System.Console.Write("]");
        }

        public static void PrintColoredPiece(Piece piece){
            if(piece == null){
                System.Console.Write("- ");
            }else{
                if(piece.Color == Color.White){
                    System.Console.Write(piece);
                }else{
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                System.Console.Write(" ");
            }
        }

    }
}