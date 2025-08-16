using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServerApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var ip = IPAddress.Any;
            var listener = new TcpListener(ip, 5000);
            listener.Start();
            Console.WriteLine("Server started. Waiting for connection ... " + ip);

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected!");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer);
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from client: {received}");

                string response = $"Hello, client! You said: {received}";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseBytes);

                client.Close();
            }
        }
    }
}