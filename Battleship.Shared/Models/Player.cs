namespace Battleship_Shared;

public class Player
{
    public string Name { get; set; } = string.Empty;
    public List<GridSpot> ShipLocations { get; set; } = new();
    public List<GridSpot> ShotGrid { get; set; } = new();

}