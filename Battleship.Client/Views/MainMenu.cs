namespace Battleship_Client.Views;

public class MainMenu
{
    public void Display()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Play Local");
            Console.WriteLine("2. Multiplayer");
            Console.WriteLine("3. Exit");

            string input = Console.ReadLine() ?? string.Empty;

            switch (input)
            {
                case "1":
                    Console.WriteLine("Starting game vs Computer...");
                    // Code to start game vs Computer
                    break;
                case "2":
                    Console.WriteLine("Starting Multiplayer...");
                    // Code to start Multiplayer
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}