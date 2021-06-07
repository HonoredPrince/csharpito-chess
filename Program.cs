using System;
using GameLogic;
using ChessBoard;
using ChessBoard.Exceptions;
using View;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                ChessMatch gameMatch = new ChessMatch();
                while(!gameMatch.IsMatchOver){
                    Console.Clear();
                    ScreenRenderer.RenderBoard(gameMatch.Board);

                    System.Console.WriteLine();
                    System.Console.Write("Origin: ");
                    Position origin = ScreenInput.ReadBoardPosition().ToNumberFormatPosition();

                    bool[,] possibleMovesForSelectedPiece = gameMatch.Board.GetPiece(origin).PossibleMoviments();

                    Console.Clear();
                    ScreenRenderer.RenderBoard(gameMatch.Board, possibleMovesForSelectedPiece);

                    System.Console.WriteLine();
                    System.Console.Write("Destination: ");
                    Position destination = ScreenInput.ReadBoardPosition().ToNumberFormatPosition();

                    gameMatch.ExecutePieceMovement(origin, destination);
                }
            }catch(BoardException e){
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
