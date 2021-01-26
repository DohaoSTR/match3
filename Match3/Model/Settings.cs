namespace Match3.Model
{
    public struct BoardSize
    {
        public int RowsCount { get; }

        public int ColumnsCount { get; }

        public BoardSize(int rows, int columns)
        {
            RowsCount = rows;
            ColumnsCount = columns;
        }
    }

    public class Settings
    {
        public BoardSize CurrentBoardSize { get; set; }

        public int GameTime { get; set; }

        public Settings(BoardSize currentBoardSize, int gameTime)
        {
            CurrentBoardSize = currentBoardSize;
            GameTime = gameTime;
        }
    }
}
