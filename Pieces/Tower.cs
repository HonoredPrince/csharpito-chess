using ChessBoard;
using ChessBoard.Enums;

namespace Pieces{
    class Tower : Piece{
        public Tower(Color color, Board board) : base(color, board){

        }

        public override bool[,] PossibleMoviments(){
            bool [,] possibleMovimentsOnBoardMatrix = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //North
            position.SetPosition(Position.Line - 1, Position.Column);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Line -= 1;
            }
            //South
            position.SetPosition(Position.Line + 1, Position.Column);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Line += 1;
            }
            //East
            position.SetPosition(Position.Line,  Position.Column + 1);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Column += 1;
            }
            //West
            position.SetPosition(Position.Line,  Position.Column - 1);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Column -= 1;
            }

            return possibleMovimentsOnBoardMatrix;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}