using ChessBoard;

namespace ChessGame{
    class ScreenRenderer{
        static public void RenderBoard(Board board){
            for(int i = 0; i < board.Lines; i++){
                for(int j = 0; j < board.Columns; j++){
                    if(board.GetPiece(i,j) == null){
                        System.Console.Write("- ");
                    }else{
                        System.Console.Write(board.GetPiece(i,j) + " ");
                    }
                }
                System.Console.WriteLine();
            }
        }
    }
}