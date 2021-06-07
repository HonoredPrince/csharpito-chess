using ChessBoard;
using System;

namespace View{
    class ScreenRenderer{
        public static void RenderBoard(Board board){
            for(int i = 0; i < board.Lines; i++){
                System.Console.Write(8 - i + " ");
                for(int j = 0; j < board.Columns; j++){
                    if(board.GetPiece(i,j) == null){
                        System.Console.Write("- ");
                    }else{
                        PrintColoredPiece(board.GetPiece(i,j));
                        System.Console.Write(" ");
                    }
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintColoredPiece(Piece piece){
            if(piece.Color == Color.White){
                System.Console.Write(piece);
            }else{
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}