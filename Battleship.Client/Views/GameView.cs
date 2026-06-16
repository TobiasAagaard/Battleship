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
        Console.WriteLine($"Number of shots taken: {player.ShotBoard.ShotCount}");
        Console.WriteLine();
        Render(player.ShotBoard.ShotGrid.Values);
        Console.WriteLine();
    }

    public static void DisplayFleetBoard(Player player)
    {
        Console.Clear();
        Console.WriteLine($"{player.UsersName}'s Fleet");
        Console.WriteLine($"Ships placed: {player.FleetBoard.ShipLocations.Count} / 5");
        Console.WriteLine();
        Render(player.FleetBoard.ShotGrid.Values, showShips: true);
        Console.WriteLine();
    }

    public static void Render(IEnumerable<GridSpot> spots, bool showShips = false)
    {
        List<GridSpot> cachedSpots = spots.ToList();

        if (cachedSpots.Any() == false) 
        { 
            Console.WriteLine("No grid to display."); return; 
        }

        HashSet<string> uniqueLetters = new();
        HashSet<int> uniqueNumbers = new();

        foreach (GridSpot spot in cachedSpots)
        {
            uniqueLetters.Add(spot.Letter);
            uniqueNumbers.Add(spot.Number);
        }

        List<string> letters = uniqueLetters.OrderBy(l => l).ToList();
        List<int> numbers = uniqueNumbers.OrderBy(n => n).ToList();

        foreach (string letter in letters)
        {
            Console.Write(letter + "  ");
            foreach (int number in numbers)
            {
                GridSpot spot = cachedSpots.First(spot => spot.Matches(letter, number));

                Console.Write(SymbolForGridSpot(spot, showShips) + "  ");
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

    private static string SymbolForGridSpot(GridSpot spot, bool showShips)
    {
        return spot.Status switch
        {
            GridSpot.GridSpotStatus.Hit  => "X",
            GridSpot.GridSpotStatus.Miss => " ",
            GridSpot.GridSpotStatus.Sunk => "#",
            GridSpot.GridSpotStatus.Ship => showShips ? "S" : "~",
            _                            => "~"
        };
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

    public static string AskForShipLocation(ShipType type)
    {
        int size = Ships.SizeOf(type);
        Console.Write($"Where do you want to place your {type} (size {size})? Starting cell (e.g. A5): ");
        return Console.ReadLine() ?? "";
    }

    public static Orientation AskForShipOrientation(ShipType type)
    {
        while (true)
        {
            Console.Write($"Place {type} (H)orizontal or (V)ertical? ");
            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "H" || input == "HORIZONTAL")
            {
                return Orientation.Horizontal;
            }
            if (input == "V" || input == "VERTICAL")
            {
                return Orientation.Vertical;
            }
            Console.WriteLine("Invalid orientation. Please enter H or V.");
        }
    }

    public static void ShowPlayerInfoHeader(string title) => Console.WriteLine($"Type the player information for: {title}");
    public static void ShowInvalidShotMessage() => Console.WriteLine("Invalid shot. Please try again.");
    public static void ShowInvalidLocationMessage()
    {
        Console.WriteLine("Invalid location. The ship doesn't fit there or overlaps another ship. Press Enter to retry.");
        Console.ReadLine();
    }
}