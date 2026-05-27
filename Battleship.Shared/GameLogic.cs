
namespace Battleship_Shared;

public static class GameLogic
{

    // Method to initialize the player's shot grid
    public static void InitializeGrid(Player playerModel)
    {
        List<string> Letters = new()
        {
            "A",
            "B",
            "C",
            "D",
            "E",
        };

        List<int> Numbers = new()
        {
            1,
            2,
            3,
            4,
            5,
        };

        for (int x = 0; x < Letters.Count; x++)
        {
            for (int y = 0; y < Numbers.Count; y++)
            {
                AddGridSpot(playerModel, Letters[x], Numbers[y]);
            }
        }
    }

    private static void AddGridSpot(Player playerModel, string letter, int number)
    {
        GridSpot spot = new();

        spot.SpotLetter = letter;
        spot.SpotNumber = number;
        spot.Status = GridSpotStatus.Empty;

        playerModel.ShotGrid.Add(spot);
    }
}