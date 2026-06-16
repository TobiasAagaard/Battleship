namespace Battleship.Shared.Models;

public enum ShipType
{
    Battleship,
    Cruiser,
    Destroyer,
    Submarine,
    Carrier
}

public enum Orientation
{
    Horizontal,
    Vertical
}

public class Ships
{
    private readonly List<GridSpot> locations = new();
    public IReadOnlyList<GridSpot> Locations => locations;
    public ShipType Type { get; }
    public int Size => locations.Count;

    public bool IsSunk => Locations.All( s => s.Status == GridSpot.GridSpotStatus.Hit || s.Status == GridSpot.GridSpotStatus.Sunk );

    public Ships(ShipType type, IEnumerable<GridSpot> locations)
    {
        Type = type;
        this.locations = locations.ToList();
    }

    public static int SizeOf(ShipType type) => type switch
    {
        ShipType.Battleship => 4,
        ShipType.Cruiser => 3,
        ShipType.Destroyer => 2,
        ShipType.Submarine => 3,
        ShipType.Carrier => 5,
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown ship type: {type}")
    };

    public bool OccupiesSpot(GridSpot spot) => Locations.Any(s => s.Letter == spot.Letter && s.Number == spot.Number);

    public void Hit(GridSpot spot) => Locations.FirstOrDefault(s => s.Letter == spot.Letter && s.Number == spot.Number)?.MarkHit();
    public void MarkSunk()
    {
        for (int i = 0; i < Locations.Count; i++)
        {
            Locations[i].MarkSunk();
        }
    }
}