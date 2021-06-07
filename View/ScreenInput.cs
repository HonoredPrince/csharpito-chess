
using ChessBoard.Formater;
using System;

namespace View{
    class ScreenInput{
        public static BoardPosition ReadBoardPosition(){
            string positionText = Console.ReadLine();
            char column = positionText[0];
            int line = int.Parse(positionText[1] + "");
            return new BoardPosition(column, line);
        }
    }
}