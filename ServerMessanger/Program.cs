using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMessanger
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            TaskMessageReader();
            // В следующем обновление мы доделаем чат

            Console.ReadLine();
        }

        private static async void MessageReader()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var localIp = IPAddress.Parse("192.168.8.109");
                var port = 8080;
                var endPoint = new IPEndPoint(localIp, port);

                socket.Bind(endPoint);
                socket.Listen(10);

                // "айпи порт имя сообщение"
                do
                {
                    var incomingSocket = await socket.AcceptAsync(); // поток блокируется, пока не будет получено сообщение                    

                    while (incomingSocket.Available > 0)
                    {
                        var buffer = new byte[incomingSocket.Available]; // массив байтов, куда мы будем записывать 
                        incomingSocket.Receive(buffer);
                        string[] messageData = (System.Text.Encoding.UTF8.GetString(buffer)).Split('|');
                        Message message = new Message();
                        message.Name = messageData[0];
                        message.Text = messageData[1];
                        Console.WriteLine($"{message.Name}: {message.Text}");
                        //Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
                        //using (var context = new ServerMessangerDbContext())
                        //{
                        //    context.Messages.Add(message);
                        //    await context.SaveChangesAsync();
                        //}

                        //incomingSocket.Send(); // можно в ответку отправить что-либо                                               
                    }
                    incomingSocket.Close();
                } while (true);
            }
        }

        private static Task TaskMessageReader()
        {
            return Task.Run(MessageReader);
        }
    }
}
