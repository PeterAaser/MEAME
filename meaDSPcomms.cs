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

        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMcsUsbFactoryNet dspDevice;
        public bool connected = false;

        public DSPComms() {
            dspDevice = new CMcsUsbFactoryNet();
            usblist.Initialize(DeviceEnumNet.MCS_MEAUSB_DEVICE); // Get list of MEA devices connect by USB

            CMcsUsbListEntryNet dspPort = null;
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

            Console.WriteLine("Writing new stim frequency");
            Console.WriteLine(stimFreq);
            Console.WriteLine(dspDevice);
            dspDevice.WriteRegister(0x1008, (uint)stimFreq);

        }
    }
}
