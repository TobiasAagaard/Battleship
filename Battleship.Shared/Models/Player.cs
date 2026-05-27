
namespace Battleship_Shared.Models
{
    public abstract class Player
    {
        public string UsersName { get; set; } = string.Empty;
        public List<GridSpotModel> ShipLocations { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> ShotGrid { get; set; } = new List<GridSpotModel>();

        public abstract string GetNextShot();
        public abstract string GetShipPlacement(int shipNumber);
    }
}
