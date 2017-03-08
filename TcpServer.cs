namespace MeaExampleNet{

    using System;
    using System.Net;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class MeaTcpServer {

        public static List<Socket> listeners = new List<Socket>();
        public static List<Double> stuff = new List<Double>();
        public Action<StimReq> onTcpStimRequest { get; set; }

        // public static ManualResetEvent allDone = new ManualResetEvent(false);
        public MeaTcpServer()
        {
            Thread serverThread = new Thread( () => listen(this.onTcpStimRequest) );
            serverThread.Start();
        }

        public static void listen(Action<StimReq> onRequest)
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

                Thread stimThread = new Thread( () => attachReader(connection, onRequest) );
                stimThread.Start();

                Console.WriteLine("Connection accepted");
            }
        }

        public void sendData(Byte[] sweep)
        {
            foreach(Socket s in listeners)
            {
                s.Send(sweep);
            }
        }

        public static void attachReader(Socket socket, Action<StimReq> onRequest)
        {
            NetworkStream ns = new NetworkStream(socket);
            StreamReader streamreader = new StreamReader(ns);
            int throttle = 0;

            while (true)
            {
                // Console.WriteLine("We have received data");
                string memeString = streamreader.ReadLine();
                StringReader memeReader = new StringReader(memeString);
                JsonTextReader memer = new JsonTextReader(memeReader);
                JsonSerializer serializer = new JsonSerializer();
                StimReq s = serializer.Deserialize<StimReq>(memer);
                if(throttle == 40)
                {
                    onRequest(s);
                    throttle = 0;
                }
                else
                {
                    throttle++;
                }
            }
        }

    }

    public class StimReq{
        public int[] electrodes { get; set; }
        public double[] stimFreqs { get; set; }
        public void printMe() {
            Console.WriteLine("----");
            foreach (int el in electrodes) { Console.Write("[" + el + "]"); }
            foreach (double sf in stimFreqs) { Console.Write("[" + sf + "]"); }
        }
    }

}
