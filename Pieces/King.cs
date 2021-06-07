using ChessBoard;
using ChessBoard.Enums;

namespace Pieces{
    class King : Piece{
        public King(Color color, Board board) : base(color, board){

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

            return possibleMovimentsOnBoardMatrix;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}