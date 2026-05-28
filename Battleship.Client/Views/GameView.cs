using Battleship.Shared.Models;

namespace Battleship.Client.Views;

public static class GameView
{
    public static void WelcomeMessage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Battleship");
        Console.WriteLine();
    }

    public static void DisplayShotBoard(Player player)
    {
        Console.Clear();
        Console.WriteLine($"{player.UsersName}'s Shot Grid");
        Console.WriteLine();
        Render(player.ShotBoard.ShotGrid.Values);
        Console.WriteLine();
    }

    public static void Render(IEnumerable<GridSpot> spots)
    {
        if (spots.Any() == false) 
        { 
            Console.WriteLine("No grid to display."); return; 
        }

        List<string> letters = spots.Select(spot => spot.Letter).Distinct().OrderBy(l => l).ToList();
        List<int> numbers = spots.Select(spot => spot.Number).Distinct().OrderBy(n => n).ToList();

        foreach (string letter in letters)
        {
            Console.Write(letter + "  ");
            foreach (int number in numbers)
            {
                GridSpot spot = spots.First(spot => spot.Matches(letter, number));
                Console.Write(SymbolForGridSpot(spot) + "  ");
            }
            Console.WriteLine();
        }
        Console.Write("  ");
        foreach (int number in numbers)
        {
            Console.Write($" {number} ");
        }
        Console.WriteLine();
    }

    private static string SymbolForGridSpot(GridSpot spot)
    {
        if (GridSpot.GridSpotStatus.Hit == spot.Status)
        {
            return "X";
        }
        else if (GridSpot.GridSpotStatus.Miss == spot.Status)
        {
            return "O";
        }
        else
        {
            return "~";
        }

    }

    public static void IdentifyWinner(Player winner)
    {
        Console.Clear();
        Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
        Console.WriteLine($"{winner.UsersName} took {winner.ShotBoard.ShotCount} shots.");
    }

    public static void IdentifyLoser(Player loser)
    {
        Console.Clear();
        Console.WriteLine($"Better luck next time, {loser.UsersName}.");
        Console.WriteLine($"{loser.UsersName} took {loser.ShotBoard.ShotCount} shots.");
    }
    public static string AskForShot()
    {
        Console.Write("Please enter your shot selection: ");
        return Console.ReadLine() ?? "";
    }

    public static string AskForUsersName()
    {
        Console.Write("What is your name: ");
        return Console.ReadLine() ?? "";
    }

    public static string AskForShipLocation(int shipNumber)
    {
        Console.Write($"Where do you want to place ship number {shipNumber}: ");
        return Console.ReadLine() ?? "";
    }

    public static void ShowPlayerInfoHeader(string title) => Console.WriteLine($"Player information for {title}");
    public static void ShowInvalidShotMessage()     => Console.WriteLine("Invalid shot. Please try again.");
    public static void ShowInvalidLocationMessage() => Console.WriteLine("Invalid location. Please try again.");
}