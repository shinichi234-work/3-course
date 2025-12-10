namespace HM9;

public class Location
{
    private readonly List<PointOfInterest> _pois;

    public string Id { get; }
    public string Name { get; }
    public RegionRank Rank { get; }
    public IReadOnlyList<PointOfInterest> PointsOfInterest => _pois;

    public Location(string id, string name, RegionRank rank)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException(
                "Id локации не может быть пустым"
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Название локации не может быть пустым"
            );
        }

        Id = id;
        Name = name;
        Rank = rank;
        _pois = new List<PointOfInterest>();
    }

    public void AddPoi(PointOfInterest poi)
    {
        if (poi == null)
        {
            throw new ArgumentNullException(nameof(poi));
        }

        _pois.Add(poi);
    }

    public override string ToString()
    {
        return $"{Name} ({Rank}, POI: {_pois.Count})";
    }
}