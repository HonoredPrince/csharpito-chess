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
        public bool IsMatchOnCheck { get; private set; }
        public Piece PieceIsVulnerableToEnPassant { get; private set; }

        public ChessMatch(){
            _piecesOnBoard = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            PieceIsVulnerableToEnPassant = null;
            
            Board = new Board(8, 8);
            Turn = 1;
            CurrentColorPlayer = Color.White;
            InsertPiecesOnBoard();
            IsMatchOver = false;
            IsMatchOnCheck = false;
        }

        public Piece ExecutePieceMovement(Position origin, Position destination){
            Piece movingPiece = Board.RemovePiece(origin);
            movingPiece.IncrementAmountOfMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PutPiece(movingPiece, destination);
            if(capturedPiece != null){
                _capturedPieces.Add(capturedPiece);
            }

            //King's Small Roque
            if(movingPiece is King && destination.Column == origin.Column + 2){
                Position towerOrigin = new Position(origin.Line, origin.Column + 3);
                Position towerDestination = new Position(origin.Line, origin.Column + 1);
                Piece roqueTower = Board.RemovePiece(towerOrigin);
                roqueTower.IncrementAmountOfMoves();
                Board.PutPiece(roqueTower, towerDestination);
            }

            //King's Big Roque
            if(movingPiece is King && destination.Column == origin.Column - 2){
                Position towerOrigin = new Position(origin.Line, origin.Column - 4);
                Position towerDestination = new Position(origin.Line, origin.Column - 1);
                Piece roqueTower = Board.RemovePiece(towerOrigin);
                roqueTower.IncrementAmountOfMoves();
                Board.PutPiece(roqueTower, towerDestination);
            }

            //Pawn En Passant
            if(movingPiece is Pawn){
                if(origin.Column != destination.Column && capturedPiece == null){
                    Position pawnToCapture;
                    if(movingPiece.Color == Color.White){
                        pawnToCapture = new Position(destination.Line + 1, destination.Column);
                    }else{
                        pawnToCapture = new Position(destination.Line - 1, destination.Column);
                    }
                    capturedPiece = Board.RemovePiece(pawnToCapture);
                    _capturedPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoPieceMovement(Position origin, Position destination, Piece capturedPiece){
            Piece movingPiece = Board.RemovePiece(destination);
            movingPiece.DecrementAmountOfMoves();
            if(capturedPiece != null){
                Board.PutPiece(capturedPiece, destination);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.PutPiece(movingPiece, origin);

            //King's Small Roque
            if(movingPiece is King && destination.Column == origin.Column + 2){
                Position towerOrigin = new Position(origin.Line, origin.Column + 3);
                Position towerDestination = new Position(origin.Line, origin.Column + 1);
                Piece roqueTower = Board.RemovePiece(towerDestination);
                roqueTower.DecrementAmountOfMoves();
                Board.PutPiece(roqueTower, towerOrigin);
            }

            //King's Big Roque
            if(movingPiece is King && destination.Column == origin.Column - 2){
                Position towerOrigin = new Position(origin.Line, origin.Column - 4);
                Position towerDestination = new Position(origin.Line, origin.Column - 1);
                Piece roqueTower = Board.RemovePiece(towerDestination);
                roqueTower.DecrementAmountOfMoves();
                Board.PutPiece(roqueTower, towerOrigin);
            }

            //Pawn En Passant
            if(movingPiece is Pawn){
                if(origin.Column != destination.Column && capturedPiece == PieceIsVulnerableToEnPassant){
                    Piece pawnToUncapture = Board.RemovePiece(destination);
                    Position originalPosition;
                    if(movingPiece.Color == Color.White){
                        originalPosition = new Position(3, destination.Column);
                    }else{
                        originalPosition = new Position(4, destination.Column);
                    }
                    Board.PutPiece(pawnToUncapture, originalPosition);
                    _capturedPieces.Add(capturedPiece);
                }
            }
        }

        public void StartTurn(Position origin, Position destination){
            Piece capturedPiece = ExecutePieceMovement(origin, destination);
            if(IsKingOnCheckByColor(CurrentColorPlayer)){
                UndoPieceMovement(origin, destination, capturedPiece);
                throw new BoardException("You wanna sacrifice your king dumbass?");
            }

            if(IsKingOnCheckByColor(GetAdversaryColor(CurrentColorPlayer))){
                IsMatchOnCheck = true;
            }else{
                IsMatchOnCheck = false;
            }

            if(IsKingOnCheckMateByColor(GetAdversaryColor(CurrentColorPlayer))){
                IsMatchOver = true;
            }else{
                Turn++;
                ChangeColorPlayer();
            }

            //Pawn's Special Play En Passant
            Piece movedPiece = Board.GetPiece(destination);
            if(movedPiece is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2)){
                PieceIsVulnerableToEnPassant = movedPiece;
            }else{
                PieceIsVulnerableToEnPassant = null;
            }
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

        private Color GetAdversaryColor(Color color){
            if(color == Color.White){
                return Color.Black;
            }else{
                return Color.White;
            }
        }

        private Piece GetKingByColor(Color color){
            foreach(Piece piece in GetCurrentPiecesOnBoardByColor(color)){
                if(piece is King){
                    return piece;
                }
            }
            return null;
        }
        
        public void PutNewPiece(char column, int line, Piece piece){
            Board.PutPiece(piece, new BoardPosition(column, line).ToNumberFormatPosition());
            _piecesOnBoard.Add(piece);
        }

        private void InsertPiecesOnBoard(){
            //Whites
            PutNewPiece('a', 1, new Tower(Color.White, Board));
            PutNewPiece('h', 1, new Tower(Color.White, Board));
            PutNewPiece('c', 1, new Bishop(Color.White, Board));
            PutNewPiece('f', 1, new Bishop(Color.White, Board));
            PutNewPiece('b', 1, new Horse(Color.White, Board));
            PutNewPiece('g', 1, new Horse(Color.White, Board));
            PutNewPiece('d', 1, new Queen(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board, this));
            
            PutNewPiece('a', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('b', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('c', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('d', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('e', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('f', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('g', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('h', 2, new Pawn(Color.White, Board, this));

            
            //Blacks
            PutNewPiece('a', 8, new Tower(Color.Black, Board));
            PutNewPiece('h', 8, new Tower(Color.Black, Board));
            PutNewPiece('c', 8, new Bishop(Color.Black, Board));
            PutNewPiece('f', 8, new Bishop(Color.Black, Board));
            PutNewPiece('b', 8, new Horse(Color.Black, Board));
            PutNewPiece('g', 8, new Horse(Color.Black, Board));
            PutNewPiece('d', 8, new Queen(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board, this));

            PutNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }
    
        public bool IsKingOnCheckByColor(Color color){
            Piece king = GetKingByColor(color);
            if(king == null){
                throw new BoardException("Game doesn't have a king of this color, where's the king man?");
            }

            foreach(Piece piece in GetCurrentPiecesOnBoardByColor(GetAdversaryColor(color))){
                bool[,] possibleMoves = piece.PossibleMoviments();
                if(possibleMoves[king.Position.Line, king.Position.Column]){
                    return true;
                }
            }
            return false;
        }

        public bool IsKingOnCheckMateByColor(Color color){
            if(!IsKingOnCheckByColor(color)){
                return false;
            }
            foreach(Piece piece in GetCurrentPiecesOnBoardByColor(color)){
                bool[,] possibleMovements = piece.PossibleMoviments();
                for(int i = 0; i < Board.Lines; i++){
                    for(int j = 0; j < Board.Columns; j++){
                        if(possibleMovements[i,j]){
                            Position origin = piece.Position;
                            Position destination = new Position(i,j);
                            Piece capturedPiece = ExecutePieceMovement(origin, destination);
                            bool checkTest = IsKingOnCheckByColor(color);
                            UndoPieceMovement(origin, destination, capturedPiece);
                            if(!checkTest){
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}