using System;
using ChessBoard;
using ChessBoard.Exceptions;
using Pieces;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                Board board = new Board(8, 8);
            
                board.PutPiece(new Tower(Color.Black, board), new Position(0, 0));
                board.PutPiece(new Tower(Color.Black, board), new Position(1, 3));
                board.PutPiece(new King(Color.Black, board), new Position(2, 4));
                board.PutPiece(new King(Color.Black, board), new Position(-1, 0));

                ScreenRenderer.RenderBoard(board);

            }catch(BoardException e){
                System.Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
