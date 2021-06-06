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
    }
}