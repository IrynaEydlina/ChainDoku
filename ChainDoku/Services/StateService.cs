using ChainDoku.Models;
using System.Text.Json;

namespace ChainDoku.Services;

internal class StateService
{
    private readonly Stack<string> _states = new();

    public void SaveState(List<SudokuCell> cells)
    {
        var data = JsonSerializer.Serialize(cells);
        _states.Push(data);
    }

    public bool TryGetState(out List<SudokuCell> cells)
    {
        if (_states.TryPop(out var state))
        {
            cells = JsonSerializer.Deserialize<List<SudokuCell>>(state);
            return true;
        }

        cells = null;
        return false;
    }

    public void Clear() => _states.Clear();
}
