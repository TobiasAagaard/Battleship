using Battleship.Client.Views;
using Battleship.Shared.Models;

namespace Battleship.Client.Games;

public class GameRunner
{
    public void Run()
    {
        GameView.WelcomeMessage();

        Player active = CreatePlayer("Player 1");
        Player opponent = CreatePlayer("Player 2");
        Player? winner = null;

        do
        {
            GameView.DisplayShotBoard(active);

            TakeTurn(active, opponent);

            if (!opponent.IsAlive)
                winner = active;
            else
                (active, opponent) = (opponent, active);

        } while (winner is null);

        GameView.IdentifyWinner(winner);
        Console.ReadLine();
    }

    private static Player CreatePlayer(string title)
    {
        Player player = new HumanPlayer();
        GameView.ShowPlayerInfoHeader(title);
        player.UsersName = GameView.AskForUsersName();
        PlaceShips(player);
        Console.Clear();
        return player;
    }

    private static void PlaceShips(Player player)
    {
        while (player.FleetBoard.ShipLocations.Count < 5)
        {
            string input = player.GetShipPlacement(player.FleetBoard.ShipLocations.Count + 1);
            bool placed  = player.FleetBoard.TryPlaceShip(input);

            if (!placed)
                GameView.ShowInvalidLocationMessage();
        }
    }

    private static void TakeTurn(Player active, Player opponent)
    {
        string letter = "";
        int    number = 0;

        bool validShot = false;
        while (!validShot)
        {
            string shot = active.GetNextShot();

            if (!Board.TryParseLocation(shot, out letter, out number))
            {
                GameView.ShowInvalidShotMessage();
                continue;
            }

            if (!active.ShotBoard.IsShotValid(letter, number))
            {
                GameView.ShowInvalidShotMessage();
                continue;
            }

            validShot = true;
        }

        bool isHit = opponent.FleetBoard.IsHitOnFleet(letter, number);

        active.ShotBoard.RecordShot(letter, number, isHit);
    }
}