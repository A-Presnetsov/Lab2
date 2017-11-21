using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);   //Создаём UDP сокет
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.103"), 1918); //"Пункт назначения" пакета данных

            while (true) {
                Console.Write("Введите строку: ");
                string str = Console.ReadLine();
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                socket.SendTo(BitConverter.GetBytes(bytes.Length), serverEndPoint);
                socket.SendTo(bytes, serverEndPoint);


                byte[] buff = new byte[1024];
                socket.Receive(buff);
                int length = BitConverter.ToInt32(buff, 0); 
                byte[] responseBuffer = new byte[length]; 
                for (int i = 0; i < length;)
                {
                    int recvLength = socket.Receive(buff); 
                    Buffer.BlockCopy(buff, 0, responseBuffer, i, recvLength);
                    i += recvLength;
                }
                Console.WriteLine(Encoding.UTF8.GetString(buff, 0, length) + "\n");
            }
            socket.Close();
        }
    }
}
