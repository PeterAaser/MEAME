namespace MeaExampleNet{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Mcs.Usb;

    public class MeaInterface {

        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        public DAQ dataAcquisitionDevice;
        public MeaTcpServer server;

        public MeaInterface(){
            server = new MeaTcpServer();
            dataAcquisitionDevice = new DAQ{samplerate = 40000,
                                            channelBlockSize = 128,
                                            onChannelData = this.OnChannelData };
        }

        public void OnChannelData(int mChannelHandles, int[] data, int totalChannels, int returnedFrames)
        {
            // Console.WriteLine("Pretendin we sendin'");

            byte[] sendBuffer = new byte[returnedFrames * 4 * 4];

            for (int ii = 0; ii < totalChannels; ii++){

                int[] channelData = new int[returnedFrames];

                for (int jj = 0; jj < returnedFrames; jj++){
                    channelData[jj] = data[jj * mChannelHandles + ii];
                }

                if(ii == 32 || ii == 33 || ii == 34 || ii == 35){
                    int offset = (ii - 32)*returnedFrames*4;
                    Buffer.BlockCopy(channelData, 0, sendBuffer, offset, returnedFrames);
                }
            }
            server.sendData(sendBuffer);
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
