using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tgame.socket;

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
            
            client client1 = new client();
            client1.sendData(common.parameters.JOIN);



            //init a socket for call back from the server to fetch messages
            server serverCon = new server();
            Thread thread = new Thread(new ThreadStart(() => serverCon.waitForConnection()));
            thread.Start();
            //Console.WriteLine("thread start");

            //server server1 = new server();
            //server1.waitForConnection();


            //client1.getConnect();
            //client1.sendData("test");
            //client1.sendData("ok");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }
    }
}
