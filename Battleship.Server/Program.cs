using System.Net;
using System.Net.Sockets;

namespace Battleship.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        const int port = 50000;

        TcpListener listener = new TcpListener(IPAddress.Any, port);

        listener.Start();

        Console.WriteLine($"Battleship server is running on port {port} and IP address {IPAddress.Any}...");
        Console.WriteLine("Waiting for client...");

        using TcpClient client = await listener.AcceptTcpClientAsync();

        Console.WriteLine("Client connected!");

        using NetworkStream stream = client.GetStream();
        using StreamReader reader = new StreamReader(stream);
        using StreamWriter writer = new(stream) { AutoFlush = true };

        await writer.WriteLineAsync("Welcome to the Battleship server!");

        while (await reader.ReadLineAsync() is { } message)
        {
            Console.WriteLine($"Client: {message}");

            await writer.WriteLineAsync($"Server received: {message}");
        }

        Console.WriteLine("Client disconnected.");

        listener.Stop();
    }
}