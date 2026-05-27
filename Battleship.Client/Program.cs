using Battleship_Client.Views;
using Battleship_Shared;

namespace Battleship_Client;

public class Program
{
    public static void Main()
    {
        MainMenu mainMenu = new();
        mainMenu.Display();
    }

    private static Player CreatePlayer(string Name)
    {
        Player player = new();

        player.Name = AskForPlayerName();

        player.ShotGrid = new List<GridSpot>();

        return player;
    }

    private static string AskForPlayerName()
    {
        Console.WriteLine("Enter player name:");
        string output = Console.ReadLine() ?? string.Empty;
        return output;
    }


}