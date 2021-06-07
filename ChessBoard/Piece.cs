using ChessBoard.Enums;

namespace ChessBoard{
    abstract class Piece{
        public Color Color { get; protected set; }
        public Position Position { get; set; }
        public Board Board { get; protected set; } 
        public int AmountOfMoves { get; protected set; }

        public Piece(Color color, Board board){
            Color = color;
            Position = null;
            Board = board;
            AmountOfMoves = 0; 
        } 

        protected bool CanMoveToPosition(Position position){
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public void IncrementAmountOfMoves(){
            AmountOfMoves++;
        }

        public bool IsAnyPossibleMovementAvaliable(){
            bool [,] possibleMovementsMatrix = PossibleMoviments();
            for(int i = 0; i < Board.Lines; i++){
                for(int j = 0; j < Board.Columns; j++){
                    if(possibleMovementsMatrix[i,j]){
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsPositionAllowedByPieceMovementRestriction(Position position){
            return PossibleMoviments()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMoviments();
    }
}