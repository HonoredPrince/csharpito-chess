using ChessBoard;
using ChessBoard.Enums;
using Match;

namespace Pieces{
    class Pawn : Piece{
        
        private ChessMatch _gameMatch;

        public Pawn(Color color, Board board, ChessMatch gameMatch) : base(color, board){
            _gameMatch = gameMatch;
        }

        private bool IsPositionFree(Position position){
            return Board.GetPiece(position) == null;
        }
        protected bool CanPawnMoveToPosition(Position position){
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color != Color;
        }
    
        public override bool[,] PossibleMoviments(){
            bool [,] possibleMovimentsOnBoardMatrix = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            if(Color == Color.White){
                position.SetPosition(Position.Line - 1, Position.Column);
                if (Board.IsPositionValid(position) && IsPositionFree(position)) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }
                position.SetPosition(Position.Line - 2, Position.Column);
                Position position2 = new Position(Position.Line - 1, Position.Column);
                if (Board.IsPositionValid(position2) && IsPositionFree(position2) && Board.IsPositionValid(position) && IsPositionFree(position) && AmountOfMoves == 0) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }
                position.SetPosition(Position.Line - 1, Position.Column - 1);
                if (Board.IsPositionValid(position) && CanPawnMoveToPosition(position)) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }
                position.SetPosition(Position.Line - 1, Position.Column + 1);
                if (Board.IsPositionValid(position) && CanPawnMoveToPosition(position)) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }

                //En passant 
                if(Position.Line == 3){
                    Position leftPositionToPawn = new Position(Position.Line, Position.Column - 1);
                    if(Board.IsPositionValid(leftPositionToPawn) && CanMoveToPosition(leftPositionToPawn) && Board.GetPiece(leftPositionToPawn) == _gameMatch.PieceIsVulnerableToEnPassant){
                        possibleMovimentsOnBoardMatrix[leftPositionToPawn.Line - 1, leftPositionToPawn.Column] = true;
                    }
                    Position rightPositionToPawn = new Position(Position.Line, Position.Column + 1);
                    if(Board.IsPositionValid(rightPositionToPawn) && CanMoveToPosition(rightPositionToPawn) && Board.GetPiece(rightPositionToPawn) == _gameMatch.PieceIsVulnerableToEnPassant){
                        possibleMovimentsOnBoardMatrix[rightPositionToPawn.Line - 1, rightPositionToPawn.Column] = true;
                    }
                }

            }else{
                position.SetPosition(Position.Line + 1, Position.Column);
                if (Board.IsPositionValid(position) && IsPositionFree(position)) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }
                position.SetPosition(Position.Line + 2, Position.Column);
                Position position2 = new Position(Position.Line + 1, Position.Column);
                if (Board.IsPositionValid(position2) && IsPositionFree(position2) && Board.IsPositionValid(position) && IsPositionFree(position) && AmountOfMoves == 0) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }
                position.SetPosition(Position.Line + 1, Position.Column - 1);
                if (Board.IsPositionValid(position) && CanPawnMoveToPosition(position)) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }
                position.SetPosition(Position.Line + 1, Position.Column + 1);
                if (Board.IsPositionValid(position) && CanPawnMoveToPosition(position)) {
                    possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                }

                //En passant 
                if(Position.Line == 4){
                    Position leftPositionToPawn = new Position(Position.Line, Position.Column - 1);
                    if(Board.IsPositionValid(leftPositionToPawn) && CanMoveToPosition(leftPositionToPawn) && Board.GetPiece(leftPositionToPawn) == _gameMatch.PieceIsVulnerableToEnPassant){
                        possibleMovimentsOnBoardMatrix[leftPositionToPawn.Line + 1, leftPositionToPawn.Column] = true;
                    }
                    Position rightPositionToPawn = new Position(Position.Line, Position.Column + 1);
                    if(Board.IsPositionValid(rightPositionToPawn) && CanMoveToPosition(rightPositionToPawn) && Board.GetPiece(rightPositionToPawn) == _gameMatch.PieceIsVulnerableToEnPassant){
                        possibleMovimentsOnBoardMatrix[rightPositionToPawn.Line + 1, rightPositionToPawn.Column] = true;
                    }
                }
            }

            return possibleMovimentsOnBoardMatrix;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}