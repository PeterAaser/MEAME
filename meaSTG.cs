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

    public class STGInterface {


        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CStg200xDownloadNet stgDevice;

        public bool connectSTGDevice(uint index){

            Console.WriteLine("Initializing and connecting STG device object to physical device");
            if(stgDevice != null){
                stgDevice.Disconnect();
                stgDevice.Dispose();
                stgDevice = null;
            }

            uint electrode = 3;

            stgDevice = new CStg200xDownloadNet();

            // As with the data acquisition, the second argument is a lock mask allowing
            // multiple device objects to share a single physical device
            stgDevice.Connect(usblist.GetUsbListEntry(index), 2);

            // Set electrode 3 to manual. in MC_Rack this corresponds to
            // pushing buttons to stimulate which is what we want.
            stgDevice.SetElectrodeMode(electrode, ElectrodeModeEnumNet.emManual);

            // The last arg defines which DAC to use for an electrode.
            // DACs are 1 through 3, with 0 being ground. The second arg is used
            // with list mode.
            stgDevice.SetElectrodeDacMux(electrode, 0, 1);

            stgDevice.SetElectrodeEnable(electrode, 0, true);
            stgDevice.SetBlankingEnable(electrode, false);

            // Toggle the Amplifier Protection Switch while stimulation is in progress.
            // In the MCS example it is turned off.
            stgDevice.SetEnableAmplifierProtectionSwitch(electrode, true);

            // Toggle voltage stimulation as opposed to current stimulation.
            // Voltage stimulation seems to be agreed upon to be a better choice for neurons
            // In one example this value is apparently meaningless for stg200x (MeaDownloadNet)
            // However, in that same example current is set during setup and memory load so might
            // be contextual..
            stgDevice.SetVoltageMode();

            return true;
        }

        public void setupDeviceMemory(){

            // two datapoints specifying 10000µV, positive and negative...
            int[] amplitude = new int[2] {10000, -10000};

            // ...Two timespans, each corresponding to previous datapoints...
            ulong[] duration = new ulong[2] {100000, 100000};

            // ...resulting in a square wave for channel 0
            stgDevice.PrepareAndSendData(0,
                                         amplitude,
                                         duration,
                                         STG_DestinationEnumNet.channeldata_voltage);

            // connect all stimulation channels to the first trigger
            stgDevice.SetupTrigger(0, new uint[] { 255 }, new uint[] { 255 }, new uint[] { 1 });

        }
    }
}
