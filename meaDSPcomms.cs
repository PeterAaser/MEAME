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

    public partial class DSPComms {

        private CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMcsUsbListEntryNet dspPort;
        private CMcsUsbFactoryNet dspDevice;
        private uint requestID = 0;
        public bool connected = false;
        public uint a = 0;


        public DSPComms()
        {
            dspDevice = new CMcsUsbFactoryNet();
            dspDevice.EnableExceptions(true);
            usblist.Initialize(DeviceEnumNet.MCS_MEAUSB_DEVICE); // Get list of MEA devices connect by USB

            bool dspPortFound = false;
            uint lockMask = 64;

            for (uint ii = 0; ii < usblist.Count; ii++){

                if (usblist.GetUsbListEntry(ii).SerialNumber.EndsWith("B")){

                    dspPort = usblist.GetUsbListEntry(ii);
                    dspPortFound = true;
                    break;
                }
            }

            if(dspPortFound && (dspDevice.Connect(dspPort, lockMask) == 0)){
                Console.WriteLine("DSP is connected, we are ready to go");
                connected = true;
            }
            else {
                Console.WriteLine("DSP connection failed");
            }
        }

        public void disconnect()
        {
            dspDevice.Disconnect();
        }

        public void triggerStimReg(uint dac_id,
                                   uint elec1,
                                   uint elec2,
                                   uint period,
                                   uint sample)
        {
            uint req_id = ++a;
            uint req_ack = a;
            dspDevice.WriteRegister(DAC_ID, dac_id);
            dspDevice.WriteRegister(ELECTRODES1, elec1);
            dspDevice.WriteRegister(ELECTRODES2, elec2);
            dspDevice.WriteRegister(PERIOD, period);
            dspDevice.WriteRegister(SAMPLE, sample);
            dspDevice.WriteRegister(REQUEST_ID, req_id);

            for (int ii = 0; ii < 5; ii++)
            {
                if (req_ack == dspDevice.ReadRegister(REQUEST_ACK)){
                    Console.WriteLine("Got em");
                    break;
                }
                Console.WriteLine("That didn't go so well, trying again in 1 sec");
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void triggerStimRegTest()
        {


            triggerStimReg(0, 0x0103, 0x0, 100, 0);
            triggerStimReg(1, 0xFF, 0x0, 200, 1);
            triggerStimReg(2, 0x0, 0xFF, 300, 2);

        }

        public void uploadBinary()
        {
            string FirmwareFile;
            FirmwareFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FirmwareFile += @"\..\..\..\FB_Example.bin";

            if(!System.IO.File.Exists(FirmwareFile)){
                throw new System.IO.FileNotFoundException("Binary file not found");
            }
            pp.l("Disconnecting DSP...");
            dspDevice.Disconnect();
            pp.l("Uploading new binary...");
            dspDevice.LoadUserFirmware(FirmwareFile, dspPort);           // Code for uploading compiled binary
            uint lockMask = 64;
            pp.l("Binary uploaded, reconnecting device...");
            dspDevice.Connect(dspPort, lockMask);
            pp.l("Device reconnected. We are ready to go...");
        }
    }
}
