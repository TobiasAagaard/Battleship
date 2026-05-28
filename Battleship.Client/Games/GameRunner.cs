using Battleship.Client.Views;
using Battleship.Shared.Models;
using Battleship.Shared.Logic;

namespace Battleship.Client.Games;

public class GameRunner
{   
    public void Run()
    {
        Player active = CreatePlayer("Player 1");
        Player opponent = CreatePlayer("Player 2");
        Player? winner = null;

        while (winner == null)
        {
            GameView.DisplayShotBoard(active);
            TakeTurn(active, opponent);

            if (!opponent.IsAlive)
            {
                winner = active;
            }
            else
                (active, opponent) = (opponent, active);
        }
        
        GameView.IdentifyWinner(winner);
        Console.ReadLine();
    }

    private static Player CreatePlayer(string title)
    {
        Console.Clear();
        
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
            BoardLogic logic = new();
            bool placed  = logic.TryPlaceShip(player.FleetBoard, input);

            if (placed == false) 
            {
                GameView.ShowInvalidLocationMessage();
            }
        }
    }

    private static void TakeTurn(Player active, Player opponent)
    {
        string letter = "";
        int    number = 0;
        BoardLogic logic = new();

        bool validShot = false;
        while (!validShot)
        {
            string shot = active.GetNextShot();
           

            if (!Board.TryParseLocation(shot, out letter, out number))
            {
                GameView.ShowInvalidShotMessage();
                continue;
            }

            if (!logic.IsShotValid(active.ShotBoard, letter, number))
            {
                GameView.ShowInvalidShotMessage();
                continue;
            }

            validShot = true;
        }

        bool isHit = logic.IsHitOnFleet(opponent.FleetBoard, letter, number);

        logic.RecordShot(active.ShotBoard, letter, number, isHit);
    }
}