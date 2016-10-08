using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeaExampleNet{

    using Mcs.Usb;
    using ZeroMQ;

    public class MeaInterface {

        private readonly ZmqContext ZContext;
        private readonly ZmqSocket meaPublisher;
        private readonly ZmqSocket commandSubscriber;

        // We initally want to use hardcoded addresses to send data
        private const String clientAddress = "tcp://129.241.111.251:1234";

        public MeaInterface(){

            Console.WriteLine("mea interface made");
            ZContext = ZmqContext.Create();
            meaPublisher = ZContext.CreateSocket(SocketType.PUB);
            commandSubscriber = ZContext.CreateSocket(SocketType.SUB)

        }

        public ~MeaInterface(){
            // TODO close connections etc
        }
    }
}
