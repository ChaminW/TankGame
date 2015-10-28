using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tgame.common;

namespace Tgame.socket
{
    class client
    {
        static System.Net.Sockets.TcpClient clientSocket = new TcpClient(); // for connet with server create a tcpclient socket
        static NetworkStream stream = null;
        private static BinaryWriter writer;

        public void getConnect()
        {
            clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);
            stream = clientSocket.GetStream();

            byte[] messsage = Encoding.ASCII.GetBytes(parameters.JOIN);

            for (int i = 0; i < messsage.Length; i++)
            {
                Console.WriteLine(messsage[i]);
            }

            stream.Write(messsage, 0, messsage.Length);

            
            stream.Flush();     //flush the stream
            stream.Close();     //close stream

        }

        public void sendData(String message)
        {
            try {


                clientSocket = new TcpClient();

                clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);
                if (clientSocket.Connected)
                {

                    stream = clientSocket.GetStream();


                    writer = new BinaryWriter(stream);
                    Byte[] tempMsg = Encoding.ASCII.GetBytes(message);


                    writer.Write(tempMsg);
                    Console.WriteLine("Data Sended");
                    writer.Close();
                    stream.Close();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (WRITING) Failed! due to "+ e.Message);
                sendData(message);
            }
            finally
            {
                clientSocket.Close();
                
            }

        }


        


    }
}
