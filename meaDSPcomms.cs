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
        public uint b = 10000;
        private uint lockMask = 64;

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
            Console.WriteLine("DSP found");

            if(dspPortFound && (dspDevice.Connect(dspPort, lockMask) == 0)){
                Console.WriteLine("DSP is connected, we are ready to go");
                connected = true;
            }
            else {
                Console.WriteLine("DSP connection failed");
            }
            dspDevice.Disconnect();
        }

        public void disconnect()
        {
            dspDevice.Disconnect();
        }

        public void resetDevices()
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                Console.WriteLine("resetting MCU1");
                dspDevice.Coldstart(CFirmwareDestinationNet.MCU1);
            }
            else{ Console.WriteLine("Connection Error"); return; }

            dspDevice.Disconnect();
        }

        public void triggerStimReg(uint dac_id,
                                   uint elec1,
                                   uint elec2,
                                   uint period,
                                   uint sample)
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                uint req_id = ++a;
                uint req_ack = a;
                dspDevice.WriteRegister(DAC_ID, dac_id);
                dspDevice.WriteRegister(ELECTRODES1, elec1);
                dspDevice.WriteRegister(ELECTRODES2, elec2);
                dspDevice.WriteRegister(PERIOD, period);
                dspDevice.WriteRegister(SAMPLE, sample);
                dspDevice.WriteRegister(REQUEST_ID, req_id);

                for (int ii = 0; ii < 1; ii++)
                {
                    if (req_ack == dspDevice.ReadRegister(REQUEST_ACK)){
                        Console.WriteLine("Got em");
                        break;
                    }
                    Console.WriteLine("Request failure");
                }
            }
            else{ Console.WriteLine("Connection Error"); return; }

            dspDevice.Disconnect();
        }
        public void triggerStimRegTest( uint group, uint period )
        {
            triggerStimReg( group, 0x0303, 0x0, period, 0 );
            // triggerStimReg(1, 0x0000, 0x0, 210000, 1);
            // triggerStimReg(2, 0x0000, 0x0, 330000, 2);

        }


        public void triggerOldStimReq()
        {
            if(dspDevice.Connect(dspPort, lockMask) == 0)
            {
                b *= 2;
                if(b > 1000000){ b = 10000; }

                dspDevice.WriteRegister(DAC_ID, b);
            }
            else{ Console.WriteLine("Connection Error"); return; }
            dspDevice.Disconnect();
        }

        private void uploadBinary(String path)
        {

            if(!System.IO.File.Exists(path)){
                throw new System.IO.FileNotFoundException("Binary file not found");
            }

            Console.WriteLine("Uploading new binary...");
            dspDevice.LoadUserFirmware(path, dspPort);           // Code for uploading compiled binary

            Console.WriteLine("Binary uploaded, reconnecting device...");
        }

        public void uploadMeameBinary()
        {
            string FirmwareFile;
            FirmwareFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FirmwareFile += @"\..\..\..\FB_Example.bin";

            Console.WriteLine("Uploading MEAME binary");
            uploadBinary(FirmwareFile);
        }


        public void uploadOldBinary()
        {
            string FirmwareFile;
            FirmwareFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FirmwareFile += @"\..\..\..\control_group.bin";

            Console.WriteLine("Uploading control binary");
            uploadBinary(FirmwareFile);
        }
    }
}
