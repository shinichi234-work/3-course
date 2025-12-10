namespace HM9;

using System.Collections;

public class Atlas : IEnumerable<Location>
{
    private readonly List<Location> _locations;
    private readonly Dictionary<string, Location> _byId;

    public int Count => _locations.Count;

    public Atlas()
    {
        _locations = new List<Location>();
        _byId = new Dictionary<string, Location>();
    }

    public Location this[int index]
    {
        get
        {
            if (index < 0 || index >= _locations.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    "Индекс находится вне границ атласа"
                );
            }

            return _locations[index];
        }
    }

    public Location this[string id]
    {
        get
        {
            if (id == null)
            {
                throw new ArgumentNullException(
                    nameof(id),
                    "Id не может быть null"
                );
            }

            if (!_byId.ContainsKey(id))
            {
                throw new KeyNotFoundException(
                    $"Локация с Id '{id}' не найдена"
                );
            }

            return _byId[id];
        }
    }

    public void Add(Location location)
    {
        if (location == null)
        {
            throw new ArgumentNullException(nameof(location));
        }

        if (_byId.ContainsKey(location.Id))
        {
            throw new ArgumentException(
                $"Локация с Id '{location.Id}' уже существует"
            );
        }

        _locations.Add(location);
        _byId[location.Id] = location;
    }

    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= _locations.Count)
        {
            return false;
        }

        var location = _locations[index];
        _locations.RemoveAt(index);
        _byId.Remove(location.Id);

        return true;
    }

    public bool RemoveById(string id)
    {
        if (id == null || !_byId.ContainsKey(id))
        {
            return false;
        }

        var location = _byId[id];
        _byId.Remove(id);
        _locations.Remove(location);

        return true;
    }

    public IEnumerable<Location> EnumerateByRank(RegionRank minRank)
    {
        foreach (var location in _locations)
        {
            if (location.Rank >= minRank)
            {
                yield return location;
            }
        }
    }

    public IEnumerator<Location> GetEnumerator()
    {
        return _locations.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}