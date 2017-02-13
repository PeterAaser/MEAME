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

    public class DSPComms {

        private CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMcsUsbListEntryNet dspPort;
        private CMcsUsbFactoryNet dspDevice;
        private uint requestID = 0;
        public bool connected = false;

        static uint MAIL_BASE         =  0x1000;
        static uint REQUEST_ID        =  MAIL_BASE;
        static uint DAC_ID            =  MAIL_BASE + 0x8;
        static uint ELECTRODES        =  MAIL_BASE + 0xc;
        static uint ELECTRODES1       =  MAIL_BASE + 0xc;
        static uint ELECTRODES2       =  MAIL_BASE + 0x10;
        static uint PERIOD            =  MAIL_BASE + 0x14;
        static uint SAMPLE            =  MAIL_BASE + 0x18;
        static uint REQUEST_ACK       =  MAIL_BASE + 0x1c;

        public DSPComms() {
            dspDevice = new CMcsUsbFactoryNet();
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

        public void triggerStimReg(uint stimFreq){

            dspDevice.WriteRegister(REQUEST_ID, requestID);
            requestID++;

            dspDevice.WriteRegister(DAC_ID, (uint)0x0);
            dspDevice.WriteRegister(ELECTRODES, (uint)0x200201);
            dspDevice.WriteRegister(PERIOD, (uint)100000);
            dspDevice.WriteRegister(SAMPLE, (uint)0x0);

            uint response = dspDevice.ReadRegister(REQUEST_ACK);
            while(response != requestID){
                Console.WriteLine("Blocking on DSP transfer");
                response = dspDevice.ReadRegister(REQUEST_ACK);
            }
        }



        public void uploadBinary(){

            string FirmwareFile;
            FirmwareFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FirmwareFile += @"\..\..\..\FB_Example.bin";

            dspDevice.LoadUserFirmware(FirmwareFile, dspPort);           // Code for uploading compiled binary
            Console.WriteLine("New binary uploaded!");

        }
    }

}
