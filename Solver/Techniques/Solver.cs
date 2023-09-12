using Models;

namespace Solver.Techniques
{
    internal static partial class Solver
    {
        internal static readonly SolverTechnique[] _techniques = new[]
        {
            new SolverTechnique(OpenSingle, SolveType.OpenSingle),
            new SolverTechnique(HiddenSingle, SolveType.HiddenSingle),
            //new SolverTechnique(NakedPair, "https://hodoku.sourceforge.net/en/tech_naked.php#n2"),
            //new SolverTechnique(HiddenPair, "https://hodoku.sourceforge.net/en/tech_hidden.php#h2"),
            //new SolverTechnique(LockedCandidate, "https://hodoku.sourceforge.net/en/tech_intersections.php#lc1"),
            //new SolverTechnique(PointingTuple, "https://hodoku.sourceforge.net/en/tech_intersections.php#lc1"),
            //new SolverTechnique(NakedTriple, "https://hodoku.sourceforge.net/en/tech_naked.php#n3"),
            //new SolverTechnique(HiddenTriple, "https://hodoku.sourceforge.net/en/tech_hidden.php#h3"),
            //new SolverTechnique(XWing, "https://hodoku.sourceforge.net/en/tech_fishb.php#bf2"),
            //new SolverTechnique(Swordfish, "https://hodoku.sourceforge.net/en/tech_fishb.php#bf3"),
            //new SolverTechnique(YWing, "https://www.sudokuwiki.org/Y_Wing_Strategy"),
            //new SolverTechnique(XYZWing, "https://www.sudokuwiki.org/XYZ_Wing"),
            //new SolverTechnique(XYChain, "https://www.sudokuwiki.org/XY_Chains"),
            //new SolverTechnique(NakedQuadruple, "https://hodoku.sourceforge.net/en/tech_naked.php#n4"),
            //new SolverTechnique(HiddenQuadruple, "https://hodoku.sourceforge.net/en/tech_hidden.php#h4"),
            //new SolverTechnique(Jellyfish, "https://hodoku.sourceforge.net/en/tech_fishb.php#bf4"),
            //new SolverTechnique(UniqueRectangle, "https://hodoku.sourceforge.net/en/tech_ur.php"),
            //new SolverTechnique(HiddenRectangle, "https://hodoku.sourceforge.net/en/tech_ur.php#hr"),
            //new SolverTechnique(AvoidableRectangle, "https://hodoku.sourceforge.net/en/tech_ur.php#ar"),
        };

        private static bool OpenSingle(SudokuGrid puzzle)
        {
            foreach (var cell in puzzle)
            {
                if (cell.InternalCandidates.Count == 1)
                {
                    cell.ToggleValue(cell.InternalCandidates.First());
                    return true;
                }
            }
            return false;
        }

        private static bool HiddenSingle(SudokuGrid puzzle)
        {
            foreach (var regionTypes in puzzle.Regions)
            {
                foreach (var region in regionTypes)
                {
                    for (var i = 1; i <= 9; i++)
                    {
                        if (region.Count(c => c.InternalCandidates.Contains(i)) == 1)
                        {
                            var cell = region.First(c => c.InternalCandidates.Contains(i));
                            cell.ToggleValue(i);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
