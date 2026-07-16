using System.Text;
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

    public static void DisplayGameBoards(Player player)
    {
        Console.Clear();
        Console.WriteLine($"{player.UsersName}'s Turn");
        Console.WriteLine();

        RenderBoardsSideBySide(
            "Opponent's Board", player.ShotBoard.ShotGrid.Values, leftShowShips: false,
            "Your Fleet", player.FleetBoard.ShotGrid.Values, rightShowShips: true
        );
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

    public static List<String> BuildLines(IEnumerable<GridSpot> spots, bool showShips = false)
    {
        List<GridSpot> cachedSpots = spots.ToList();

        if (cachedSpots.Any() == false) 
        { 
            return new List<string>{"No grid to display."};
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

        List<string> lines = new();

        foreach (string letter in letters)
        {
            StringBuilder row = new();
            row.Append(letter + " ");
            foreach (int number in numbers)
            {
                GridSpot? spot = cachedSpots.FirstOrDefault(spot => spot.Matches(letter, number)) ?? throw new InvalidOperationException($"No grid spot found for {letter}{number}");
                row.Append(SymbolForGridSpot(spot, showShips) + " ");
            }
            lines.Add(row.ToString());
        }
        
        StringBuilder footer = new();
        footer.Append("  ");
        foreach (int number in numbers)
        {
            footer.Append($"{number} ");
        }
        lines.Add(footer.ToString());
        return lines;
    }

    public static void Render(IEnumerable<GridSpot> spots, bool showShips = false)
    {
        foreach (string line in BuildLines(spots, showShips))
        {
            Console.WriteLine(line);
        }
    }

    private static void RenderBoardsSideBySide(
        string leftTitle, IEnumerable<GridSpot> leftSpots, bool leftShowShips,
        string rightTitle, IEnumerable<GridSpot> rightSpots, bool rightShowShips,
        int gap = 6
    )
    {
        List<string> left = BuildLines(leftSpots, leftShowShips);
        List<string> right = BuildLines(rightSpots, rightShowShips);

        int leftWidth = left.Max(line => line.Length);
        leftWidth = Math.Max(leftWidth, leftTitle.Length);

        string gapSpacer = new string(' ', gap);

        Console.WriteLine(leftTitle.PadRight(leftWidth) + gapSpacer + rightTitle);
        
        int rows = Math.Max(left.Count, right.Count);
        for (int i = 0; i < rows; i++)
        {
            string leftLine = i < left.Count ? left[i] : "";
            string rightLine = i < right.Count ? right[i] : "";

            Console.WriteLine(leftLine.PadRight(leftWidth) + gapSpacer + rightLine);
        }


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