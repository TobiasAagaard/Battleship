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
            GameView.DisplayGameBoards(active);
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
        BoardLogic logic = new();
        ShipType[] fleet =
        {
            ShipType.Carrier,
            ShipType.Battleship,
            ShipType.Cruiser,
            ShipType.Submarine,
            ShipType.Destroyer
        };

        foreach (ShipType type in fleet)
        {
            bool placed = false;
            while (!placed)
            {
                GameView.DisplayFleetBoard(player);
                string location = player.GetShipPlacement(type);
                Orientation orientation = player.GetShipOrientation(type);
                placed = logic.TryPlaceShip(player.FleetBoard, type, location, orientation);

                if (!placed)
                {
                    GameView.ShowInvalidLocationMessage();
                }
            }
        }

        GameView.DisplayFleetBoard(player);
        Console.WriteLine("All ships placed. Press Enter to continue.");
        Console.ReadLine();
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