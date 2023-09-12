using Models;

namespace Solver
{
    public class SolverService
    {
        public bool TrySolve(SudokuGrid grid)
        {
            // TODO: Determine type of solving technique to have a posibility to show it on UI
            foreach (var tech in Techniques.Solver._techniques)
            {
                FillInternalCandidates(grid);
                var res = tech.Function(grid);
                ClearInternalCandidates(grid);
                if (res)
                {
                    return res;
                }
            }
            return false;
        }

        private void ClearInternalCandidates(SudokuGrid grid)
        {
            foreach (var cell in grid)
            {
                cell.InternalCandidates.Clear();
            }
        }

        private static void FillInternalCandidates(SudokuGrid grid)
        {
            foreach (var cell in grid.Where(c => !c.HasValue))
            {
                for (int val = 1; val <= 9; val++)
                {
                    if (!grid.Any(c => c.SameRegion(cell) && c.Value == val))
                    {
                        cell.InternalCandidates.Add(val);
                    }
                }
            }
        }
    }
}
