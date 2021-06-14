using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static int port = 228;
        static string IP = "127.0.0.1";
        static Socket _socket;
        static byte[] _buffer;
        static void Main(string[] args)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(IP), port);
                _socket.Connect(ip);
                while (true)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    Console.Write("Введите сообщение: ");
                    string message = Console.ReadLine();

                    _buffer = Encoding.UTF8.GetBytes(message);
                    _socket.Send(_buffer);

                    _buffer = new byte[256];
                    _socket.Receive(_buffer);

                    stringBuilder.Append(Encoding.UTF8.GetString(_buffer));
                    Console.WriteLine(stringBuilder);
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
    }
}
