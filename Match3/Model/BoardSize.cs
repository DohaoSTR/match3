namespace Match3.Model
{
    public struct BoardSize
    {
        public int RowCount { get; }

        public int ColumnCount { get; }

        public BoardSize(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
        }
    }
}