using Battleship_Client.Views;
using Battleship_Shared;
using Battleship_Shared.Models;

namespace Battleship_Client.Game;

public class GameRunner
{
    public void Run()
    {
        GameView.WelcomeMessage();

        Player activePlayer = CreatePlayer("Player 1");
        Player opponent = CreatePlayer("Player 2");
        Player? winner = null;

        do
        {
            GameView.DisplayShotGrid(activePlayer);

            RecordPlayerShot(activePlayer, opponent);

            bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

            if (doesGameContinue == true)
            {
                (activePlayer, opponent) = (opponent, activePlayer);
            }
            else
            {
                winner = activePlayer;
            }

        } while (winner == null);

        GameView.IdentifyWinner(winner);

        Console.ReadLine();
    }

    private static Player CreatePlayer(string playerTitle)
    {
        Player output = new Player();

        GameView.ShowPlayerInfoHeader(playerTitle);

        output.UsersName = GameView.AskForUsersName();

        GameLogic.InitializeGrid(output);

        PlaceShips(output);

        Console.Clear();

        return output;
    }

    private static void PlaceShips(Player model)
    {
        do
        {
            string location = GameView.AskForShipLocation(model.ShipLocations.Count + 1);

            bool isValidLocation = GameLogic.PlaceShip(model, location);

            if (isValidLocation == false)
            {
                GameView.ShowInvalidLocationMessage();
            }
        } while (model.ShipLocations.Count < 5);
    }

    private static void RecordPlayerShot(Player activePlayer, Player opponent)
    {
        bool isValidShot = false;
        string row = "";
        int column = 0;

        do
        {
            string shot = GameView.AskForShot();
            (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
            isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

            if (isValidShot == false)
            {
                GameView.ShowInvalidShotMessage();
            }
        } while (isValidShot == false);

        bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

        GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
    }
}
