using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tgame.decode;
using Tgame.socket;
//using Tgame.common;
//using System.Windows.Forms;

namespace Tgame 
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //decode.Decode.decode("");

            //initially join to the server
            client client1 = new client();
            client1.sendData(common.parameters.JOIN);

            //client1.getConnect();

            //init a socket for call back from the server to fetch messages
            server serverCon = new server();
            Thread serverThread = new Thread(new ThreadStart(() => serverCon.waitForConnection()));
            serverThread.Start();

           



            //Console.WriteLine("thread start");

            //server server1 = new server();
            //server1.waitForConnection();


            //client1.getConnect();
            //client1.sendData("test");
            //client1.sendData("ok");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //TankGUI.Game1 game = new TankGUI.Game1();

            
             
             
             
             
        }
    }
}
