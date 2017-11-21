using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab2_server
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient socket = new UdpClient(1918); //Создаём сокет
            IPEndPoint senderEndPoint = null; //Здесь будет в результате находиться адрес отправителя

            Console.WriteLine("Сервер запущен");

            while (true) {
                int length = BitConverter.ToInt32(socket.Receive(ref senderEndPoint), 0); 
                byte[] buff = new byte[length]; 
                byte[] buff2; 
                for (int i = 0;  i < length; i += buff2.Length)
                {
                    buff2 = socket.Receive(ref senderEndPoint); //Приём сообщения
                    Buffer.BlockCopy(buff2, 0, buff, i, buff2.Length); 
                }
                string clientStr = Encoding.UTF8.GetString(buff);
                Console.WriteLine("Клиент прислал строку: " + clientStr);
                
                byte[] bytes = Encoding.UTF8.GetBytes(clientStr.Length + " символов");
                socket.Send(BitConverter.GetBytes(bytes.Length), 4, senderEndPoint); 
                socket.Send(bytes, bytes.Length, senderEndPoint); 
            }
            Console.ReadKey();
        }
    }
}
