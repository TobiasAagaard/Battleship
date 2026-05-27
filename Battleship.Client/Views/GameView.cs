using Battleship_Shared;
using Battleship_Shared.Models;

namespace Battleship_Client.Views;

public class GameView
{
    public static void WelcomeMessage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Battleship");
        Console.WriteLine();
    }

    public static void DisplayShotGrid(Player activePlayer)
    {
        Console.Clear();
        Console.WriteLine($"{ activePlayer.UsersName }'s Shot Grid");
        Console.WriteLine();

        if (activePlayer.ShotGrid == null || activePlayer.ShotGrid.Count == 0)
        {
            Console.WriteLine("No grid to display.");
            return;
        }

        string currentRow = activePlayer.ShotGrid[0].SpotLetter;

        foreach (var gridSpot in activePlayer.ShotGrid)
        {
            if (gridSpot.SpotLetter != currentRow)
            {
                Console.WriteLine();
                currentRow = gridSpot.SpotLetter;
            }

            if (gridSpot.Status == GridSpotStatus.Empty)
            {
                Console.Write($" { gridSpot.SpotLetter }{ gridSpot.SpotNumber } ");
            }
            else if (gridSpot.Status == GridSpotStatus.Hit)
            {
                Console.Write(" X ");
            }
            else if (gridSpot.Status == GridSpotStatus.Miss)
            {
                Console.Write(" O ");
            }
            else
            {
                Console.Write(" ? ");
            }
        }
        Console.WriteLine();
    }

    public static void IdentifyWinner(Player winner)
    {
        Console.WriteLine($"Congratulations to { winner.UsersName } for winning!");
        Console.WriteLine($"{ winner.UsersName } took { GameLogic.GetShotCount(winner) } shots.");
    }

    public static string AskForShot()
    {
        Console.Write("Please enter your shot selection: ");
        string? output = Console.ReadLine();

        return output ?? "";
    }

    public static string AskForUsersName()
    {
        Console.Write("What is your name: ");
        string? output = Console.ReadLine();

        return output ?? "";
    }

    public static string AskForShipLocation(int shipNumber)
    {
        Console.Write($"Where do you want to place ship number { shipNumber }: ");
        string? location = Console.ReadLine();

        return location ?? "";
    }

    public static void ShowPlayerInfoHeader(string playerTitle)
    {
        Console.WriteLine($"Player information for { playerTitle }");
    }

    public static void ShowInvalidShotMessage()
    {
        Console.WriteLine("Invalid Shot Location. Please try again.");
    }

    public static void ShowInvalidLocationMessage()
    {
        Console.WriteLine("That was not a valid location. Please try again.");
    }
}
