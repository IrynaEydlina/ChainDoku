using System.Collections;

namespace Models;

public sealed class Region : IEnumerable<Cell>
{
    private readonly IReadOnlyList<Cell> _cells;

    public Cell this[int index] => _cells[index];

    public Region(IReadOnlyList<Cell> cells)
    {
        _cells = cells;
    }


    public IEnumerator<Cell> GetEnumerator()
    {
        return _cells.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _cells.GetEnumerator();
    }
}
