using System;
using ChessBoard;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            ScreenRenderer.RenderBoard(board);
            Console.ReadLine();           
        }
    }
}
