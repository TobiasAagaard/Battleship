using Battleship.Client.Views;
using Battleship.Shared.Models;

namespace Battleship.Client.Games;

public class HumanPlayer : Player
{
    public override string GetNextShot() => GameView.AskForShot();
    public override string GetShipPlacement(int shipNumber) => GameView.AskForShipLocation(shipNumber);
}