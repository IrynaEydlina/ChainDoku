using System.Text.Json;

namespace ChainDoku.Services;

internal class StateService
{
    private readonly Stack<string> _states = new();

    public void SaveState<T>(T cells)
    {
        return;
        // todo implement proper solution
        var data = JsonSerializer.Serialize(cells);
        _states.Push(data);
    }

    public bool TryGetState<T>(out T cells)
    {
        if (_states.TryPop(out var state))
        {
            cells = JsonSerializer.Deserialize<T>(state);
            return true;
        }

        cells = default;
        return false;
    }

    public void Clear() => _states.Clear();
}
