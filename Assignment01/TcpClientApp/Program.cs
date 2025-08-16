using System.Net.Sockets;
using System. Text;

Console.WriteLine("Connecting to server ... ");

TcpClient client = new TcpClient();
await client.ConnectAsync("127.0.0.1", 5000);

NetworkStream stream = client.GetStream();

string message = "Hello from client!";
byte[] data = Encoding.UTF8.GetBytes(message);
await stream.WriteAsync(data, 0, data.Length);

byte[] buffer = new byte[1024];
int bytesRead = await stream. ReadAsync(buffer, 0, buffer. Length);
string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

Console.WriteLine("Server replied: " + response);

Console.WriteLine("Press ENTER to close connection.");
Console.ReadLine();

client.Close();