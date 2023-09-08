using ChainDoku.Models;
using ChainDoku.Models.Enums;

namespace ChainDoku.Services;

internal class SudokuGridGenerator
{
    public SudokuGrid GenerateGrid(Difficulty difficulty = Difficulty.Middle)
    {
        var cells = new SudokuCell[9, 9];
        var tempGrid = GenerateSudoku(ToDifficulty(difficulty));
        for (int row = 0; row < 9; row++)
            for (int col = 0; col < 9; col++)
                cells[row, col] = new SudokuCell(row, col, tempGrid[row, col]);

        return new SudokuGrid(cells);
    }

    static int[,] GenerateSudoku(int difficulty = 30)
    {
        int[,] grid = new int[9, 9];
        SolveSudoku(grid); // Generate a complete grid

        Random random = new Random();
        for (int i = 0; i < 81 - difficulty; i++)
        {
            int row = random.Next(9);
            int col = random.Next(9);
            while (grid[row, col] == 0)
            {
                row = random.Next(9);
                col = random.Next(9);
            }
            int backup = grid[row, col];
            grid[row, col] = 0;

            int[,] tempGrid = (int[,])grid.Clone();
            int countSolutions = 0;

            void CountUniqueSolutions(int r, int c)
            {
                if (r == 9)
                {
                    countSolutions++;
                    return;
                }

                int nextRow = (c + 1) / 9 == 1 ? r + 1 : r;
                int nextCol = (c + 1) % 9;

                if (tempGrid[r, c] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (IsValid(tempGrid, r, c, num))
                        {
                            tempGrid[r, c] = num;
                            CountUniqueSolutions(nextRow, nextCol);
                            if (countSolutions > 1)
                                return;
                            tempGrid[r, c] = 0;
                        }
                    }
                }
                else
                {
                    CountUniqueSolutions(nextRow, nextCol);
                }
            }

            CountUniqueSolutions(0, 0);

            if (countSolutions != 1)
                grid[row, col] = backup;
        }

        return grid;
    }

    static bool SolveSudoku(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (grid[row, col] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (IsValid(grid, row, col, num))
                        {
                            grid[row, col] = num;
                            if (SolveSudoku(grid))
                                return true;
                            grid[row, col] = 0; // Backtrack
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }

    static bool IsValid(int[,] grid, int row, int col, int num)
    {
        // Check if 'num' is valid in the given row and column
        for (int i = 0; i < 9; i++)
        {
            if (grid[row, i] == num || grid[i, col] == num)
                return false;
        }

        // Check if 'num' is valid in the 3x3 subgrid
        int startRow = 3 * (row / 3);
        int startCol = 3 * (col / 3);
        for (int i = startRow; i < startRow + 3; i++)
        {
            for (int j = startCol; j < startCol + 3; j++)
            {
                if (grid[i, j] == num)
                    return false;
            }
        }

        return true;
    }

    static int ToDifficulty(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Junior => 80,
            Difficulty.Trinee => 60,
            Difficulty.Middle => 45,
            Difficulty.Senior => 30,
            Difficulty.Techlead => 20,
            _ => 1
        };
    }
}
