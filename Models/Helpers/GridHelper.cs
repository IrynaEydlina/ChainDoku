namespace Models.Helpers
{
    public static class GridHelper
    {
        static GridHelper()
        {
            SubGrid = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                int ix = i % 3 * 3;
                int iy = i / 3 * 3;
                for (int x = ix; x < ix + 3; x++)
                {
                    for (int y = iy; y < iy + 3; y++)
                    {
                        SubGrid[x, y] = i;
                    }
                }
            }
        }

        public static readonly int[,] SubGrid;
    }
}
