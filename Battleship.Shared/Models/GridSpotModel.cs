namespace Battleship_Shared.Models;

public class GridSpotModel
{
    public string SpotLetter { get; set; } = string.Empty;
    public int SpotNumber { get; set; }
    public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;
    
}