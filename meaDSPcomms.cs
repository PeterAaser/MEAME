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
        public int a = 0;


        public DSPComms()
        {
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


        public void triggerStimReg(uint stimFreq)
        {

            this.a++;
            dspDevice.WriteRegister(MAIL_BASE, (uint)a);
            uint fuck = dspDevice.ReadRegister(0x1004);
            uint fuck2 = dspDevice.ReadRegister(ELECTRODE_ENABLE);

            Console.WriteLine(a);
            Console.WriteLine(fuck);
            Console.WriteLine(fuck2);

            barfDebug();
        }


        public String readDeviceBytes(uint start, uint nBytes)
        {
            List<byte> byteBuffer = new List<byte>();
            uint bytesRead = 0;
            while (bytesRead < nBytes)
            {
                uint meme = dspDevice.ReadRegister(start + bytesRead);
                byteBuffer.AddRange(BitConverter.GetBytes(meme).ToList());
                bytesRead += 4;
            }
            uint overshoot = (nBytes - bytesRead);
            var trimmed = byteBuffer.Take((int)(nBytes - overshoot));
            ASCIIEncoding encoding = new ASCIIEncoding();
            string hello  = encoding.GetString(trimmed.ToArray());
            return hello;
        }


        public void barfDebug()
        {
            String debug1 = Convert.ToString(dspDevice.ReadRegister(DEBUG1), 2);
            String debug2 = Convert.ToString(dspDevice.ReadRegister(DEBUG2), 2);
            String debug3 = Convert.ToString(dspDevice.ReadRegister(DEBUG3), 2);

            Console.WriteLine("DEBUG INFO");
            Console.WriteLine(debug1);
            Console.WriteLine(debug2);
            Console.WriteLine(debug3);
        }

        public void barf(){
            String mailbox     = Convert.ToString(dspDevice.ReadRegister(MAIL_BASE), 2);
            String req_id      = Convert.ToString(dspDevice.ReadRegister(REQUEST_ID), 2);
            String dac_id      = Convert.ToString(dspDevice.ReadRegister(DAC_ID), 2);
            String electrodes1 = Convert.ToString(dspDevice.ReadRegister(ELECTRODES1), 2);
            String electrodes2 = Convert.ToString(dspDevice.ReadRegister(ELECTRODES2), 2);
            String period      = Convert.ToString(dspDevice.ReadRegister(PERIOD), 2);
            String sample      = Convert.ToString(dspDevice.ReadRegister(SAMPLE), 2);
            String req_ACK     = Convert.ToString(dspDevice.ReadRegister(REQUEST_ACK), 2);

            String barfString = "Device send registers are: \n" +
                $"mail base: ${mailbox} \n" +
                $"req id: ${req_id} \n" +
                $"dac_id: ${dac_id} \n" +
                $"elec1: ${electrodes1} \n" +
                $"elec2: ${electrodes2} \n" +
                $"period: ${period} \n" +
                $"sample: ${sample} \n" +
                $"req_ACK: ${req_ACK} \n";

            Console.WriteLine(barfString);
        }


        public void uploadBinary()
        {
            string FirmwareFile;
            FirmwareFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FirmwareFile += @"\..\..\..\FB_Example.bin";

            if(!System.IO.File.Exists(FirmwareFile)){
                throw new System.IO.FileNotFoundException("Binary file not found");
            }
            else {
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

        public void readDevicePrint()
        {

            const uint segment_start = 0x110C;
            const uint segment_end = 0x1FFC;
            const uint segment_length = segment_end - segment_start;

            pp.l("reading device log");

            uint writeHead = dspDevice.ReadRegister(0x1100);
            uint readHead = dspDevice.ReadRegister(0x1104);

            Console.WriteLine(writeHead);
            Console.WriteLine(readHead);

            // check if we need to wrap
            if (readHead > writeHead)
            {
                string first = readDeviceBytes(readHead, (segment_length - readHead));
                string second = readDeviceBytes(segment_start, (writeHead - segment_start));

                dspDevice.WriteRegister(0x1104, writeHead);

                Console.Write(first);
                Console.WriteLine(second);
            }
            else
            {
                string meme = readDeviceBytes(readHead, (writeHead - readHead));
                Console.WriteLine(meme);

                dspDevice.WriteRegister(0x1104, writeHead);
            }
        }
    }
}
