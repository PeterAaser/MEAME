using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.Protobuf;

namespace MeaExampleNet{

    using Mcs.Usb;
    using ZeroMQ;
    using Newtonsoft.Json;

    public class MeaZMQ {

        private readonly ZContext context;
        private readonly ZSocket meaPublisher;
        private readonly ZSocket commandSubscriber;

        // We initally want to use hardcoded addresses to send data
        // My address: 129.241.201.109
        private const String baseAddress = "tcp://*:1234";
        private const String myAdress = "tcp://*:1234";
        private const String clientAddress = "tcp://129.241.111.251:1234";

        public MeaZMQ(){
            Console.WriteLine("mea interface made");
            context = new ZContext();
            meaPublisher = new ZSocket(context, ZSocketType.PUB);
            meaPublisher.Connect(clientAddress);
        }

        ~MeaZMQ(){
            // TODO close connections etc
        }


        public void sendData(int[] data, int channel){

            MeaJSON jason = new MeaJSON();
            jason.channelNumber = channel;
            jason.channelData = data;
            string json = JsonConvert.SerializeObject(jason);

            using (var updateFrame = new ZFrame("1 " + json)){
                meaPublisher.Send(updateFrame);
                Console.WriteLine("message sent");
            }
        }
    }

    public class MeaJSON {
        public int channelNumber;
        public int[] channelData;
    }
}
