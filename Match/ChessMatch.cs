using ChessBoard;
using Pieces;
using ChessBoard.Formater;
using ChessBoard.Exceptions;

namespace Match{
    class ChessMatch{
        public Color CurrentColorPlayer { get; private set; }
        public int Turn { get; private set; }
        public Board Board { get; private set; }
        public bool IsMatchOver { get; private set; }
        public ChessMatch(){
            Board = new Board(8, 8);
            Turn = 1;
            CurrentColorPlayer = Color.White;
            InsertPiecesOnBoard();
            IsMatchOver = false;
        }

        public void ExecutePieceMovement(Position origin, Position destination){
            Piece movingPiece = Board.RemovePiece(origin);
            movingPiece.IncrementAmountOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(movingPiece, destination);
        }

        public void StartTurn(Position origin, Position destination){
            ExecutePieceMovement(origin, destination);
            Turn++;
            ChangeColorPlayer();
        }

        public void OriginPositionValidation(Position position){
            if(Board.GetPiece(position) == null){
                throw new BoardException("There's not a piece on this position");
            }
            if(CurrentColorPlayer != Board.GetPiece(position).Color){
                throw new BoardException("This piece it's not yours");
            }
            if(!Board.GetPiece(position).IsAnyPossibleMovementAvaliable()){
                throw new BoardException("There's no moviments avaliable for this piece");
            }
        }

        public void DestinationPositionValidation(Position origin, Position destination){
            if(!Board.GetPiece(origin).IsPositionAllowedByPieceMovementRestriction(destination)){
                throw new BoardException("Destination choosen not valid for this piece");
            }
        }

        private void ChangeColorPlayer(){
            if(CurrentColorPlayer == Color.White){
                CurrentColorPlayer = Color.Black;
            }else{
                CurrentColorPlayer = Color.White;
            }
        }

        private void InsertPiecesOnBoard(){
            //Whites
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('a', 1).ToNumberFormatPosition());
            Board.PutPiece(new Tower(Color.White, Board), new BoardPosition('h', 1).ToNumberFormatPosition());
            Board.PutPiece(new King(Color.White, Board), new BoardPosition('e', 1).ToNumberFormatPosition());
            
            //Blacks
            Board.PutPiece(new Tower(Color.Black, Board), new BoardPosition('a', 8).ToNumberFormatPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new BoardPosition('h', 8).ToNumberFormatPosition());
            Board.PutPiece(new King(Color.Black, Board), new BoardPosition('e', 8).ToNumberFormatPosition());
        }
    }
}