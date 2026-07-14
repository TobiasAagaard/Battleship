using System.Net.Sockets;

namespace Battleship.Client.Networking;

public class TcpGameClient
{
    private const string ServerAddress = "127.0.0.1";
    private const int ServerPort = 50000;

    public async Task RunAsync()
    {
        using TcpClient client = new();

        Console.WriteLine("Connecting to the Battleship server...");

        await client.ConnectAsync(ServerAddress, ServerPort);

        Console.WriteLine("Connected!");

        using NetworkStream stream = client.GetStream();
        using StreamReader reader = new(stream);
        using StreamWriter writer = new(stream)
        {
            AutoFlush = true
        };

        string? welcomeMessage = await reader.ReadLineAsync();

        Console.WriteLine($"Server: {welcomeMessage}");

        while (true)
        {
            Console.Write("Message (or 'exit'): ");

            string message = Console.ReadLine() ?? "";

            if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            await writer.WriteLineAsync(message);

            string? response = await reader.ReadLineAsync();

            if (response is null)
            {
                Console.WriteLine("The server disconnected.");
                break;
            }

            Console.WriteLine($"Server: {response}");
        }
    }
}