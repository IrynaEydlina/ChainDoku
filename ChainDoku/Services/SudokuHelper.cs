namespace ChainDoku.Services;

internal static class SudokuHelper
{
    public static int Size = 9;

    static SudokuHelper()
    {
        SubSquares = new int[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                var startRow = i / 3;
                var startColumn = j / 3;
                SubSquares[i, j] = startRow * 3 + startColumn;
            }
        }
    }

    public static int[,] SubSquares { get; private set; }
}
