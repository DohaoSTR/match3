namespace Match3.Model
{
    public static class Utility
    {
        public static void SwapTiles<T>(ref T left, ref T right)
        {
            T temporaryTile = left;
            left = right;
            right = temporaryTile;
        }
    }
}