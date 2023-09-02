namespace ChainDoku.Models;

public class SudokuGrid
{
    public SudokuGrid(List<SudokuCell> cells) => Cells = cells;

    public SudokuCell this[int row, int column] => Cells.FirstOrDefault(c => c.Row == row && c.Column == column);

    public List<SudokuCell> Cells { get; init; }
}

public class ChainDokuGrid : SudokuGrid
{
    public List<CellConnection> Connections { get; init; }
    public ChainDokuGrid(List<SudokuCell> cells, List<CellConnection> connections) : base(cells) => Connections = connections;
}

public struct IntCell {
    public IntCell()
    {
        
    }
    public Guid Id { get; } = Guid.NewGuid();
    public int Value { get; set; }
}

public record struct CellConnection(Guid Id, Guid Id2);

