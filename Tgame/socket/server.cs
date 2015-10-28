using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tgame.socket
{
    class server
    {

        bool errorOcurred = false;
        Socket connection = null; //The socket that is listened to     
        TcpListener listener = null;

        public void waitForConnection()
        {
            try
            {
                //Creating listening Socket
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);

                Console.WriteLine("Waiting for game engine response");

                //Starts listening
                this.listener.Start();

                //Establish connection upon server request
                while (true)
                {

                    connection = listener.AcceptSocket();   //connection is connected socket

                    Console.WriteLine("Connetion established");

                    //Fetch the messages from the server
                    int asw = 0;
  
                    NetworkStream serverStream = new NetworkStream(connection);     //create a network stream using connection
                    List<Byte> inputMsg = new List<byte>();

                    //fetch messages from  server
                    while (asw != -1)
                    {
                        asw = serverStream.ReadByte();
                        inputMsg.Add((Byte)asw);
                    }

                    String messageFromServer = Encoding.UTF8.GetString(inputMsg.ToArray());
                    Console.WriteLine("Recieved message- "+messageFromServer);


                    /*
                    Main torkenizer = new Main();
                    //Console.Write("Response from server to join "+torkenizer.serverJoinReply(messageFromServer));

                    

                    try
                    {
                        if (messageFromServer.StartsWith("I") && messageFromServer.EndsWith("#"))
                        {
                            Console.WriteLine("initialize");
                            torkenizer.initiation(messageFromServer);
                        }
                        else if (messageFromServer.StartsWith("S") && messageFromServer.EndsWith("#"))
                        {
                            Console.WriteLine("accept");
                            torkenizer.acceptance(messageFromServer);
                        }
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                    }


                    */



                    serverStream.Close();       //close the netork stream
                    //connection.Close();       //close the connection

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! Due to " + e.Message);
                errorOcurred = true;
            }
            finally
            {
                if (connection != null)
                    if (connection.Connected)
                        connection.Close();
                if (errorOcurred)
                    this.waitForConnection();
            }
        }


    }
}
