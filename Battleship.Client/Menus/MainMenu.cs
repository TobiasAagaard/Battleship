using Battleship.Client.Games;
using Battleship.Client.Views;

namespace Battleship.Client.Menus;

public class MainMenu
{
    public void Display()
    {
        var mainMenu = new Dictionary<string, (string Label, Action Action)>
        {
            {"0", ("Exit", () => Environment.Exit(0))},
            { "1", ("Play Game Offline", () => new GameRunner().Run())}
        };

        while (true)
        {
            Console.Clear();
            
            GameView.WelcomeMessage();

            foreach (var menuItem in mainMenu)
            {
                Console.WriteLine($"{ menuItem.Key }: { menuItem.Value.Label }");
            }

            Console.Write("\nSelect an option: ");
            string? input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input) && mainMenu.TryGetValue(input, out var menu))
            {
                menu.Action();
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
                Console.ReadLine();
            }
        }
    }
}