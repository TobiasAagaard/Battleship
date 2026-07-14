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

        Console.WriteLine($"Server started on port {port}, and IP address {IPAddress.Any}.");

        while (true)
        {
            Console.WriteLine("Waiting for a client to connect...");
            TcpClient client = await listener.AcceptTcpClientAsync();

            _ = HandleClientAsync(client);
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            string clientId = Guid.NewGuid().ToString();

            string remoteEndPoint = client.Client.RemoteEndPoint?.ToString() ?? "Unknown";

            Console.WriteLine($"Client connected: {clientId} from {remoteEndPoint}");

            try 
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                {
                    await writer.WriteLineAsync($"Your client ID is: {clientId}");

                    while (await reader.ReadLineAsync() is { } message)
                    {
                        Console.WriteLine($"Received from {clientId}: {message}");

                        await writer.WriteLineAsync($"Echo: {message}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Connection error with client {clientId}: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Client disconnected: {clientId}");
            }
        }
    }
}