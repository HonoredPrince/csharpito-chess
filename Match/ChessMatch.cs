using ChessBoard;
using Pieces;
using ChessBoard.Formater;
using ChessBoard.Exceptions;
using ChessBoard.Enums;
using System.Collections.Generic;

namespace Match{
    class ChessMatch{
        private HashSet<Piece> _piecesOnBoard;
        private HashSet<Piece> _capturedPieces;
        public Color CurrentColorPlayer { get; private set; }
        public int Turn { get; private set; }
        public Board Board { get; private set; }
        public bool IsMatchOver { get; private set; }
        public ChessMatch(){
            Board = new Board(8, 8);
            Turn = 1;
            CurrentColorPlayer = Color.White;
            _piecesOnBoard = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            InsertPiecesOnBoard();
            IsMatchOver = false;
        }

        public void ExecutePieceMovement(Position origin, Position destination){
            Piece movingPiece = Board.RemovePiece(origin);
            movingPiece.IncrementAmountOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(movingPiece, destination);
            if(capturedPiece != null){
                _capturedPieces.Add(capturedPiece);
            }
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

        public HashSet<Piece> GetCapturedPiecesByColorType(Color color){
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece capturedPiece in _capturedPieces){
                if(capturedPiece.Color == color){
                    aux.Add(capturedPiece);
                }
            }
            return aux;
        }

        public HashSet<Piece> GetCurrentPiecesOnBoardByColor(Color color){
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece pieceOnBoard in _piecesOnBoard){
                if(pieceOnBoard.Color == color){
                    aux.Add(pieceOnBoard);
                }
            }
            aux.ExceptWith(GetCapturedPiecesByColorType(color));
            return aux;
        }

        public void PutNewPiece(char column, int line, Piece piece){
            Board.PutPiece(piece, new BoardPosition(column, line).ToNumberFormatPosition());
            _piecesOnBoard.Add(piece);
        }

        private void InsertPiecesOnBoard(){
            //Whites
            PutNewPiece('a', 1, new Tower(Color.White, Board));
            PutNewPiece('h', 1, new Tower(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board));

            
            //Blacks
            PutNewPiece('a', 8, new Tower(Color.Black, Board));
            PutNewPiece('h', 8, new Tower(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board));
        }
    }
}