using System;
namespace ChessBoard.Exceptions{
    class BoardException : Exception{
        public BoardException(string message) : base(message){
            
        }
    }
}