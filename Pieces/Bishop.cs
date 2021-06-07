using ChessBoard;
using ChessBoard.Enums;

namespace Pieces{
    class Bishop : Piece{
        public Bishop(Color color, Board board) : base(color, board){

        }

        public override bool[,] PossibleMoviments(){
            bool [,] possibleMovimentsOnBoardMatrix = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //Northeast
            position.SetPosition(Position.Line - 1, Position.Column + 1);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Line -= 1;
                position.Column += 1;
            }
            //Southeast
            position.SetPosition(Position.Line + 1, Position.Column + 1);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Line += 1;
                position.Column += 1;
            }
            //Northwest
            position.SetPosition(Position.Line - 1,  Position.Column - 1);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Line -= 1;
                position.Column -= 1;
            }
            //Southwest
            position.SetPosition(Position.Line + 1,  Position.Column - 1);
            while(Board.IsPositionValid(position) && CanMoveToPosition(position)){
                possibleMovimentsOnBoardMatrix[position.Line, position.Column] = true;
                if(Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color){
                    break;
                }
                position.Line += 1;
                position.Column -= 1;
            }

            return possibleMovimentsOnBoardMatrix;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}