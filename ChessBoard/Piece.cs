namespace ChessBoard{
    class Piece{
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
    }
}