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
        Render(player.ShotBoard.ShotGrid);
        Console.WriteLine();
    }

    public static void Render(IReadOnlyList<GridSpot> spots)
    {
        if (!spots.Any()) { Console.WriteLine("No grid to display."); return; }

        var letters = spots.Select(s => s.Letter).Distinct().OrderBy(l => l).ToList();
        var numbers = spots.Select(s => s.Number).Distinct().OrderBy(n => n).ToList();

        foreach (var letter in letters)
        {
            Console.Write(letter + "  ");
            foreach (var number in numbers)
            {
                var spot = spots.First(s => s.Matches(letter, number));
                Console.Write(SymbolFor(spot) + "  ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.Write("   ");
        foreach (var number in numbers)
        {
            Console.Write($" {number}   ");
        }
        Console.WriteLine();
    }

    private static string SymbolFor(GridSpot spot) => spot.Status switch
    {
        GridSpot.GridSpotStatus.Hit  => " X ",
        GridSpot.GridSpotStatus.Miss => " o ",
        GridSpot.GridSpotStatus.Sunk => " # ",
        _                            => "---",
    };

    public static void IdentifyWinner(Player winner)
    {
        Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
        Console.WriteLine($"{winner.UsersName} took {winner.ShotBoard.ShotCount} shots.");
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