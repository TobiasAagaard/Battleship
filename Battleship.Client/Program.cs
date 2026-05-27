using Battleship_Client.Views;
namespace Battleship_Client;

public class Program
{
    public static void Main()
    {
        MainMenu mainMenu = new();
        mainMenu.Display();
    }
}