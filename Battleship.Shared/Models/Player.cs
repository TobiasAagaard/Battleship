
namespace Battleship.Shared.Models
{
    public class Player
    {
        public string UsersName { get; set; } = string.Empty;
        public Board  FleetBoard { get; } = new();  
        public Board  ShotBoard  { get; } = new(); 

        public bool IsAlive => FleetBoard.ShipLocations.Any(s => !s.IsSunk);
    }
}
