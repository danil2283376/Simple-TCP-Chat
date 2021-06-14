using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static int port = 228;
        static Socket _socket;
        static byte[] _buffer;
        static Socket client;
        static void Main(string[] args)
        {
            ChatAsync();
            Console.ReadLine();
        }
        static async void ChatAsync() 
        {
            await Task.Run(() => Chat());
        }
        static void Chat() 
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, port);

                _socket.Bind(ip);
                _socket.Listen(1);

                while (true)
                {
                    Console.WriteLine(StringBuild());

                    Console.Write("Введите сообщение: ");
                    string message = Console.ReadLine();

                    client.Send(Encoding.UTF8.GetBytes(message + " ответил сервер!"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }
        static string StringBuild() 
        {
            StringBuilder stringBuilder = new StringBuilder();
            client = _socket.Accept();
            _buffer = new byte[256];
            client.Receive(_buffer);

            stringBuilder.Append(Encoding.UTF8.GetString(_buffer));
            return stringBuilder.ToString();
        }
    }
}
