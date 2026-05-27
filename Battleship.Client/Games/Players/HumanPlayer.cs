using Battleship_Client.Views;
using Battleship_Shared.Models;

namespace Battleship_Client.Games;

public class HumanPlayer : Player
{
    public override string GetNextShot() => GameView.AskForShot();
    public override string GetShipPlacement(int shipNumber) => GameView.AskForShipLocation(shipNumber);
}