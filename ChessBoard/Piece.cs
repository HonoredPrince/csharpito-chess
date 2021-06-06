namespace ChessBoard{
    class Piece{
        public Color Color { get; protected set; }
        public Position Position { get; set; }
        public Board Board { get; protected set; } 
        public int AmountOfMoves { get; protected set; }

        public Piece(Color color, Position position, Board board){
            Color = color;
            Position = position;
            Board = board;
            AmountOfMoves = 0; 
        } 
    }
}