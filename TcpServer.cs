namespace MeaExampleNet{

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Collections.Generic;


    // hacked together based on some microsoft example.
    public class TcpServer {

        public static List<Socket> listeners = new List<Socket>();

        // public static ManualResetEvent allDone = new ManualResetEvent(false);
        public TcpServer()
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

            while(true)
            {
                listener.BeginAccept(new AsyncCallback(acceptCallback),
                                     listener);

            }
        }

        public static void acceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            listeners.Add(handler);
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
