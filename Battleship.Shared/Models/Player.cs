
namespace Battleship.Shared.Models
{
    public abstract class Player
    {
        public string UsersName { get; set; } = string.Empty;
        public Board  FleetBoard { get; } = new();  
        public Board  ShotBoard  { get; } = new(); 

        public bool IsAlive => FleetBoard.ShipLocations.Any(s => !s.IsSunk);

        public abstract string GetNextShot();
        public abstract string GetShipPlacement(ShipType type);
        public abstract Orientation GetShipOrientation(ShipType type);
    }
}
