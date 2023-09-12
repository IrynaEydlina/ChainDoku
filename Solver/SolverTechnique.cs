using Models;
using Solver.Techniques;

namespace Solver;

internal sealed record class SolverTechnique(Func<SudokuGrid, bool> Function, SolveType Type);