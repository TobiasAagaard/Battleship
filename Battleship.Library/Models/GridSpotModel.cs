namespace Battleship_Libary.Models;

public class GridSpotModel
{
    public string SpotLetter { get; set; } = string.Empty;
    public int SpotNumber { get; set; }
    public Status SpotStatus { get; set; }
    public enum Status
    {
        Empty,
        Ship,
        Miss,
        Hit,
        Sunk
    }
}