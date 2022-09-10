namespace Match3.Model
{
    public class Settings
    {
        public BoardSize BoardSize { get; set; }

        public int GameTime { get; set; }

        public Settings(BoardSize boardSize, int gameTime)
        {
            BoardSize = boardSize;
            GameTime = gameTime;
        }
    }
}
