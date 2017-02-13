namespace MeaExampleNet{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Mcs.Usb;

    public class MeaInterface {

        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        public DAQ dataAcquisitionDevice;
        public MeaSTG stgDevice;
        private MeaZMQ zmq = new MeaZMQ();

        public MeaInterface(){
            dataAcquisitionDevice = new DAQ{samplerate = 40000,
                                            channelBlockSize = 64,
                                            onChannelData = DAQ.simpleChannelData };
        }

        public String[] getDeviceListDescriptors(){
            Console.WriteLine("getting descriptors");
            usblist.Initialize(DeviceEnumNet.MCS_MEA_DEVICE);
            String[] devices = new String[usblist.Count];

            for(uint ii = 0; ii < usblist.Count; ii++){
                devices[ii] =
                    usblist.GetUsbListEntry(ii).DeviceName +" / "
                    + usblist.GetUsbListEntry(ii).SerialNumber;
            }

            Console.WriteLine("returning descriptors");
            return devices;
        }

        public bool startDevice(){
            dataAcquisitionDevice.startDevice();
            return true;
        }

        public bool stopDevice(){
            dataAcquisitionDevice.stopDevice();
            return true;
        }
    }
}
