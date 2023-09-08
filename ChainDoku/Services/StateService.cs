using ChainDoku.Models;

namespace ChainDoku.Services;

internal class StateService : IDisposable
{
    private const string StorageKey = "StateKey";
    private readonly Stack<string> _history = new();
    private readonly SerializeService _serializer;

    public StateService(SerializeService serializer)
    {
        _serializer = serializer;
    }

    public void SaveState<T>(T[,] cells) where T : class
    {
        var data = _serializer.Serialize2(cells);
        _history.Push(data);
        Preferences.Set(StorageKey, data);
    }

    public bool TryGetState<T>(out T[,] cells) where T : class
    {
        if (_history.TryPop(out var state))
        {
            cells = _serializer.Deserialize2<T>(state);
            return true;
        }

        cells = default;
        return false;
    }

    public async Task<SudokuCell[,]> GetLastState()
    {
        var value = Preferences.Get(StorageKey, null);

        return _serializer.Deserialize2<SudokuCell>(value);
    }

    public bool LastStateExists() => Preferences.ContainsKey(StorageKey);

    public void ClearState()
    {
        Preferences.Clear(StorageKey);
    }

    public void Dispose()
    {
        _history.Clear();
    }
}
