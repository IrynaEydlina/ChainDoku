using System.Text.Json;

namespace ChainDoku.Services;

internal class SerializeService
{
    public string Serialize2<T>(T[,] data) where T : class
    {
        var oneLength = data.GetLength(0) * data.GetLength(1);
        T[] array = new T[oneLength];
        var index = 0;
        for (int row = 0; row < data.GetLength(0); row++)
        {
            for (int column = 0; column < data.GetLength(1); column++)
            {
                array[index++] = data[row, column];
            }
        }
        return JsonSerializer.Serialize(array);
    }

    public T[,] Deserialize2<T>(string data) where T : class
    {
        var array = JsonSerializer.Deserialize<T[]>(data);
        int size = (int)Math.Sqrt(array.Length);
        var result = new T[size, size];
        var index = 0;
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                result[row,column] = array[index++];
            }
        }
        return result;
    }
}
