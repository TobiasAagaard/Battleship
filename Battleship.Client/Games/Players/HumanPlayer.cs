using Battleship.Client.Views;
using Battleship.Shared.Models;

namespace Battleship.Client.Games;

public class HumanPlayer : Player
{
    public override string GetNextShot() => GameView.AskForShot();
    public override string GetShipPlacement(ShipType type) => GameView.AskForShipLocation(type);
    public override Orientation GetShipOrientation(ShipType type) => GameView.AskForShipOrientation(type);
}