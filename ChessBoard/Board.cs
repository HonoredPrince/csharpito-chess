using ChessBoard.Exceptions;

namespace ChessBoard{
    class Board{
        private Piece[,] _pieces;
        public int Lines { get; set; }
        public int Columns { get; set; }
        
        public Board(int lines, int columns){
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[lines , columns];
        }

        public Piece GetPiece(int line, int column){
            return _pieces[line, column];
        }

        public Piece GetPiece(Position position){
            return _pieces[position.Line, position.Column];
        }

        public bool PieceExistsOnPosition(Position position){
            ValidatePosition(position);
            return GetPiece(position) != null;
        } 

        public void PutPiece(Piece piece, Position position){
            if(PieceExistsOnPosition(position)){
                throw new BoardException("This position already have a piece on it");
            }
            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position){
            if(GetPiece(position) == null){
                return null;
            }
            Piece aux = GetPiece(position);
            aux.Position = null;
            _pieces[position.Line, position.Column] = null;
            return aux;
        }

        public bool IsPositionValid(Position position){
            if(position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns){
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position){
            if(!IsPositionValid(position)){
                throw new BoardException("Invalid Position");
            }
        }
    }
}