
namespace Battleship.Shared.Models
{
    public abstract class Player
    {
        public string UsersName { get; set; } = string.Empty;
        public Board  FleetBoard { get; } = new();  
        public Board  ShotBoard  { get; } = new(); 

        public bool IsAlive => FleetBoard.ShipLocations.Any(s => s.IsShip);

        public abstract string GetNextShot();
        public abstract string GetShipPlacement(int shipNumber);
    }
}
