using System;
using System.Threading;
//using System.Windows.Forms;
using TankGUI.common;
using TankGUI.GameEngine;
using TankGUI.GUI;
using TankGUI.socket;

namespace TankGUI
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {


            Game1 game = new Game1();
            game.Run();


            /*/initially join to the server
            client client1 = new client();
            client1.sendData(common.parameters.JOIN);

            //init a socket for call back from the server to fetch messages
            server serverCon = new server();
            Thread serverThread = new Thread(new ThreadStart(() => serverCon.waitForConnection()));
            serverThread.Start();

           


            /*/
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            

            
            
        }
    }
#endif
}

