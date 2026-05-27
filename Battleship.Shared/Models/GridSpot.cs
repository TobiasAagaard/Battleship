namespace Battleship_Shared;

public class GridSpot
{
    public string SpotLetter { get; set; } = string.Empty;
    public int SpotNumber { get; set; }
    public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;
    
}