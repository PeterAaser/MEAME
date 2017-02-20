namespace MeaExampleNet{

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Collections.Generic;


    public class MeaTcpServer {

        public static List<Socket> listeners = new List<Socket>();

        // public static ManualResetEvent allDone = new ManualResetEvent(false);
        public MeaTcpServer()
        {
            Thread serverThread = new Thread(listen);
            serverThread.Start();
        }

        public static void listen()
        {

            byte[] bytes = new Byte[1024];
            Socket listener = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);

            string ep = "129.241.201.110";
            IPAddress localip;
            IPAddress.TryParse(ep, out localip);

            listener.Bind(new IPEndPoint(localip, 8899));
            listener.Listen(10);

            while(true)
            {
                Console.WriteLine("server is listening");

                Socket connection = listener.Accept();
                listeners.Add(connection);
                Console.WriteLine("Connection accepted");
            }
        }

        public void sendData(Byte[] sweep)
        {
            foreach(Socket s in listeners)
            {
                Console.WriteLine("sending sweep to s");
                s.Send(sweep);
            }
        }
    }
}
