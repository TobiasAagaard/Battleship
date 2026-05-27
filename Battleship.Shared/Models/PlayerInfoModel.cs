namespace Battleship_Shared.Models;

public class PlayerInfoModel
{
    public string Name { get; set; } = string.Empty;
    public List<GridSpotModel> ShipLocations { get; set; } = new();
    public List<GridSpotModel> ShotGrid { get; set; } = new();

}