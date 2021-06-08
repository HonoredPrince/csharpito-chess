using ChessBoard;
using ChessBoard.Enums;
using Match;

namespace Pieces{
    class King : Piece{
        
        private ChessMatch _gameMatch;

        public King(Color color, Board board, ChessMatch gameMatch) : base(color, board){
            _gameMatch = gameMatch;
        }

        private bool TestTowerPositionForRoque(Position position){
            Piece piece = Board.GetPiece(position);
            return piece != null && piece is Tower && piece.Color == Color && piece.AmountOfMoves == 0;
        }

        public override bool[,] PossibleMoviments(){
            bool [,] possibleMovimentsOnBoardMatrix = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //North
            position.SetPosition(Position.Line - 1, Position.Column);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //Northeast
            position.SetPosition(Position.Line - 1, Position.Column + 1);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //East
            position.SetPosition(Position.Line, Position.Column + 1);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //Southeast
            position.SetPosition(Position.Line + 1, Position.Column + 1);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //South
            position.SetPosition(Position.Line + 1, Position.Column);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //Southwest
            position.SetPosition(Position.Line + 1, Position.Column - 1);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //West
            position.SetPosition(Position.Line, Position.Column - 1);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }
            //Northwest
            position.SetPosition(Position.Line - 1, Position.Column - 1);
            if(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
            }

            if(AmountOfMoves == 0 && !_gameMatch.IsMatchOnCheck){
                //King's Small Roque
                Position rightTowerPosition = new Position(Position.Line, Position.Column + 3);
                if(TestTowerPositionForRoque(rightTowerPosition)){
                    Position onePositionAfterKing = new Position(Position.Line, Position.Column + 1);
                    Position twoPositionAfterKing = new Position(Position.Line, Position.Column + 2);
                    if(Board.GetPiece(onePositionAfterKing) == null && Board.GetPiece(twoPositionAfterKing) == null){
                        possibleMovimentsOnBoardMatrix[Position.Line, Position.Column + 2] = true;
                    }
                }

                //King's Big Roque
                Position leftTowerPosition = new Position(Position.Line, Position.Column - 4);
                if(TestTowerPositionForRoque(leftTowerPosition)){
                    Position onePositionBeforeKing = new Position(Position.Line, Position.Column - 1);
                    Position twoPositionBeforeKing = new Position(Position.Line, Position.Column - 2);
                    Position threePositionBeforeKing = new Position(Position.Line, Position.Column - 3);
                    if(Board.GetPiece(onePositionBeforeKing) == null && Board.GetPiece(twoPositionBeforeKing) == null && Board.GetPiece(threePositionBeforeKing) == null){
                        possibleMovimentsOnBoardMatrix[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return possibleMovimentsOnBoardMatrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}