using ChessBoard;
using Pieces;
using ChessBoard.Formater;

namespace GameLogic{
    class ChessMatch{
        //private int _turn;
        private Color _currentColorPlayer;
        public Board Board { get; private set; }
        public bool IsMatchOver { get; private set; }
        public ChessMatch(){
            Board = new Board(8, 8);
            //_turn = 1;
            _currentColorPlayer = Color.White;
            InsertPiecesOnBoard();
            IsMatchOver = false;
        }

        public void ExecutePieceMovement(Position origin, Position destination){
            Piece movingPiece = Board.RemovePiece(origin);
            movingPiece.IncrementAmountOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(movingPiece, destination);
        }

        private void InsertPiecesOnBoard(){
            //Whites
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('c', 1).ToNumberFormatPosition());
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('c', 2).ToNumberFormatPosition());
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('d', 2).ToNumberFormatPosition());
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('e', 2).ToNumberFormatPosition());
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('e', 1).ToNumberFormatPosition());
            Board.PutPiece(new King(Color.White, Board), new BoardPosition('d', 1).ToNumberFormatPosition());
            
            //Blacks
            //Board.PutPiece(new Tower(Color.Black, Board), new BoardPosition('a', 8).ToNumberFormatPosition());
            //Board.PutPiece(new Tower(Color.Black, Board), new BoardPosition('h', 8).ToNumberFormatPosition());
            //Board.PutPiece(new King(Color.Black, Board), new BoardPosition('e', 8).ToNumberFormatPosition());
        }
    }
}