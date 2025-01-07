using System.Text.Json;

public class JsonDataService<T>
{
    private readonly string _path;

    public JsonDataService(string path)
    {
        _path = path;
    }

    public IEnumerable<T> GetAll()
    {
        if (!File.Exists(_path))
        {
            return Enumerable.Empty<T>();
        }

        var json = File.ReadAllText(_path);
        Console.WriteLine(json);
        return JsonSerializer.Deserialize<List<T>>(json) ?? [];
    }

    public T? GetById(string id)
    {
        var data = GetAll();
        return data.FirstOrDefault(item => GetId(item) == id);
    }

    private string GetId(T item)
    {
        var property = typeof(T).GetProperty("Id");
        if (property == null)
        {
            throw new InvalidOperationException("O tipo T não possui uma propriedade 'Id'.");
        }

        return (string)(property.GetValue(item) ?? throw new InvalidOperationException("A propriedade 'Id' não pode ser nula."));
    }

    public void Save(T item)
    {
        if (item == null) return;

        var data = GetAll().ToList();
        var id = GetId(item);

        var index = data.FindIndex(i => GetId(i) == id);
        if (index >= 0)
        {
            data[index] = item;
        }
        else
        {
            data.Add(item);
        }

        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(_path, json);
    }

    public void Delete(string id)
    {
        var data = GetAll().ToList();
        var index = data.FindIndex(i => GetId(i) == id);
        if (index >= 0)
        {
            data.RemoveAt(index);
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(_path, json);
        }
    }
}
