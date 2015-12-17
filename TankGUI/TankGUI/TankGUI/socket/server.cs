using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TankGUI.decode;
//using TankGUI;

namespace TankGUI.socket
{
    class server
    {

        bool errorOcurred = false;
        Socket connection = null; //The socket that is listened to     
        TcpListener listener = null;
        NetworkStream serverStream;

        public server()
        {

        }



        public void waitForConnection()
        {
            try
            {
                //Creating listening Socket
                this.listener = new TcpListener(IPAddress.Any, 7000);

                Console.WriteLine("Waiting for game engine response");

                //Starts listening
                this.listener.Start();

                //Establish connection upon server request
                while (true)
                {

                    connection = listener.AcceptSocket();   //connection is connected socket
                    if (connection.Connected)
                    {
                        //Console.WriteLine("Connetion established");

                        //Fetch the messages from the server
                        int asw = 0;

                        serverStream = new NetworkStream(connection);     //create a network stream using connection
                        List<Byte> inputMsg = new List<byte>();

                        //fetch messages from  server
                        while (asw != -1)
                        {
                            asw = this.serverStream.ReadByte();
                            inputMsg.Add((Byte)asw);
                        }
                        


                        String messageFromServer = Encoding.UTF8.GetString(inputMsg.ToArray());
                        messageFromServer = messageFromServer.Substring(0, messageFromServer.Length -2);
                        Console.WriteLine("Recieved message- " + messageFromServer);
                        //throw new FormatException();
                        //Decode dec1 = new Decode();

                        try {
                            /* if (messageFromServer.StartsWith("I") && messageFromServer.EndsWith("#"))
                             {
                                 Console.WriteLine("initiation");
                                 Console.WriteLine(messageFromServer);
                                 dec1.initiation(messageFromServer);

                             }
                             else if (messageFromServer.StartsWith("G") && messageFromServer.EndsWith("#"))
                             {
                                 Console.WriteLine("moving");
                                 Console.WriteLine(messageFromServer);
                                 dec1.moving(messageFromServer);

                             }*/


                            Decode.decode(messageFromServer);


                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Erorr in decoder - "+ e.Message);
                        }






                        this.serverStream.Close();       //close the netork stream
                        //connection.Close();       //close the connection
                    }
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
