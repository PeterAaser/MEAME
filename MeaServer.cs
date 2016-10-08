namespace MeaExampleNet{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Mcs.Usb;

    public class MeaInterface {

        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMeaDeviceNet device;

        int channelblocksize = 0;
        int mChannelHandles = 0;
        int selectedDevice = 0;
        int hwchannels = 0;
        int gain = 0;
        int block = 0;

        const int samplingRate = 1000;

        delegate void onChannelDataDelegate(ushort[] data, int offset);

        public MeaInterface(){}

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


        public bool activateDevice(uint index){
            Console.WriteLine("activating device");

            if(device != null){
                device.StopDacq();
                device.Disconnect();
                device.Dispose();
                device = null;
            }

            device = new CMeaDeviceNet(usblist.GetUsbListEntry(index).DeviceId.BusType,
                                       onChannelData,
                                       onError);

            device.Connect(usblist.GetUsbListEntry(index));
            device.SendStop();

            device.HWInfo().GetNumberOfHWADCChannels(out hwchannels);
            if (hwchannels == 0){ hwchannels = 64;}

            device.SetNumberOfChannels(hwchannels);

            int ana, digi, che, tim, block;
            device.GetChannelLayout(out ana, out digi, out che, out tim, out block, 0);
            block = device.GetChannelsInBlock(); // I guess

            device.SetSampleRate(samplingRate, 1, 0);

            int gain = device.GetGain();

            List<CMcsUsbDacqNet.CHWInfo.CVoltageRangeInfoNet> voltageranges;
            device.HWInfo().GetAvailableVoltageRangesInMicroVoltAndStringsInMilliVolt(out voltageranges);
            device.EnableDigitalIn(true, 0);
            device.EnableChecksum(true, 0);

            bool[] selectedChannels = new bool[block];
            for (int i = 0; i < block; i++){ selectedChannels[i] = true; } // hurr


            channelblocksize = samplingRate / 10;

            device.SetSelectedData(selectedChannels,
                                  10 * channelblocksize,
                                  channelblocksize,
                                  SampleSizeNet.SampleSize16Unsigned,
                                  block);

            mChannelHandles = block;

            device.ChannelBlock_SetCheckChecksum((uint)che, (uint)tim); // ???

            Console.WriteLine(block);
            Console.WriteLine(samplingRate);
            Console.WriteLine(hwchannels);
            Console.WriteLine(ana);
            Console.WriteLine(digi);
            Console.WriteLine(che);
            Console.WriteLine(tim);
            Console.WriteLine(gain);

            return true;

        }

        void onChannelData(CMcsUsbDacqNet d, int cbHandle, int numSamples){

            Console.WriteLine("getting the data, lads");
            int returnedFrames, totalChannels, offset, channels;
            device.ChannelBlock_GetChannel(0, 0, out totalChannels, out offset, out channels);
            ushort[] data = device.ChannelBlock_ReadFramesUI16(0, channelblocksize, out returnedFrames);
            for (int ii = 0; ii < totalChannels; ii++){

                ushort[] channelData = new ushort[returnedFrames];

                for (int jj = 0; jj < returnedFrames; jj++){
                    channelData[jj] = data[jj * mChannelHandles + ii];
                }
            }
            Console.WriteLine("returned frames: {0}", returnedFrames);
            Console.WriteLine("Total amount of datapoints: {0}", returnedFrames*totalChannels);
        }

        void onError(String msg, int info){
            Console.WriteLine(msg);
            Console.WriteLine(info);
            Console.WriteLine("Error!");
            device.StopDacq();
        }

        void channelDataHandler(ushort[] data, int offset){
            Console.WriteLine("On data handler invoked");
            if (offset == 33){
                sendData(data);
            }
        }

        void sendData(ushort[] data){
            Console.WriteLine("hheh");
        }

        public bool startDevice(){
            Console.WriteLine("Starting device");
            device.StartDacq();
            Console.WriteLine("Device started");
            return true;
        }

        public bool stopDevice(){
            device.StopDacq();
            return true;
        }
    }
}
