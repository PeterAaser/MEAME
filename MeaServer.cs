namespace MeaExampleNet{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Mcs.Usb;

    public class MeaInterface {

        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMeaDeviceNet dataAcquisitionDevice;
        private MeaZMQ zmq = new MeaZMQ();

        int channelblocksize = 0;
        int mChannelHandles = 0;
        int selectedDevice = 0;
        int hwchannels = 0;
        int gain = 0;
        int block = 0;
        uint[] outputChannels;

        int datawidth = 32;
        bool datasign = true;

        const int samplingRate = 1000;

        delegate void onChannelDataDelegate(int[] data, int offset);

        public MeaInterface(){
            outputChannels = new uint[4];
        }


        public void setOutputChannel(uint channel, int index){
            outputChannels[index] = channel;
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


        // TODO move hardcoded data to arg list
        public bool connectDataAcquisitionDevice(uint index, SampleSizeNet dataFormat = SampleSizeNet.SampleSize32Signed){

            Console.WriteLine("Connecting data acquisition object to device");

            if(dataAcquisitionDevice != null){
                dataAcquisitionDevice.StopDacq();
                dataAcquisitionDevice.Disconnect();
                dataAcquisitionDevice.Dispose();
                dataAcquisitionDevice = null;
            }

            dataAcquisitionDevice = new CMeaDeviceNet(usblist.GetUsbListEntry(index).DeviceId.BusType,
                                       onChannelData,
                                       onError);

            // The second arg refers to lock mask, allowing multiple device objects to be connected
            // to the same physical device. Yes, I know, what the fuck...
            dataAcquisitionDevice.Connect(usblist.GetUsbListEntry(index), 1);
            dataAcquisitionDevice.SendStop();

            dataAcquisitionDevice.HWInfo().GetNumberOfHWADCChannels(out hwchannels);
            if (hwchannels == 0){ hwchannels = 64;}

            dataAcquisitionDevice.SetNumberOfChannels(hwchannels);

            int ana, digi, che, tim, block;
            dataAcquisitionDevice.GetChannelLayout(out ana, out digi, out che, out tim, out block, 0);
            block = dataAcquisitionDevice.GetChannelsInBlock(); // I guess

            dataAcquisitionDevice.SetSampleRate(samplingRate, 1, 0);

            int gain = dataAcquisitionDevice.GetGain();

            List<CMcsUsbDacqNet.CHWInfo.CVoltageRangeInfoNet> voltageranges;
            dataAcquisitionDevice.HWInfo().GetAvailableVoltageRangesInMicroVoltAndStringsInMilliVolt(out voltageranges);
            dataAcquisitionDevice.EnableDigitalIn(true, 0);
            dataAcquisitionDevice.EnableChecksum(true, 0);

            bool[] selectedChannels = new bool[block];
            for (int i = 0; i < block; i++){ selectedChannels[i] = true; } // hurr

            channelblocksize = samplingRate / 10;

            dataAcquisitionDevice.SetSelectedData(selectedChannels,
                                  10 * channelblocksize,
                                  channelblocksize,
                                  dataFormat,
                                  block);

            mChannelHandles = block;

            dataAcquisitionDevice.ChannelBlock_SetCheckChecksum((uint)che, (uint)tim); // ???

            {
            Console.WriteLine(block);
            Console.WriteLine(samplingRate);
            Console.WriteLine(hwchannels);
            Console.WriteLine(ana);
            Console.WriteLine(digi);
            Console.WriteLine(che);
            Console.WriteLine(tim);
            Console.WriteLine(gain);
            Console.WriteLine("" + dataAcquisitionDevice.GetVoltageRangeInMicroVolt() + "µV");
            int validDataBits = -1;
            int deviceDataFormat = -1;
            dataAcquisitionDevice.GetNumberOfDataBits(0, DacqGroupChannelEnumNet.HeadstageElectrodeGroup, out validDataBits);
            dataAcquisitionDevice.GetDataFormat(0, DacqGroupChannelEnumNet.HeadstageElectrodeGroup, out deviceDataFormat);
            Console.WriteLine(validDataBits);
            Console.WriteLine(deviceDataFormat);

            // for(int i = 0; i < 10; i++){ int crash = 1/i;}
            }

            return true;

        }


        void onChannelData(CMcsUsbDacqNet d, int cbHandle, int numSamples){

            int returnedFrames, totalChannels, offset, channels;
            dataAcquisitionDevice.ChannelBlock_GetChannel(0, 0, out totalChannels, out offset, out channels);
            int[] data = dataAcquisitionDevice.ChannelBlock_ReadFramesI32(0, channelblocksize, out returnedFrames);
            for (int ii = 0; ii < totalChannels; ii++){

                int[] channelData = new int[returnedFrames];

                for (int jj = 0; jj < returnedFrames; jj++){
                    channelData[jj] = data[jj * mChannelHandles + ii];
                }

                if(ii == 26 || ii == 27 || ii == 28 || ii == 29){
                    zmq.sendData(channelData, ii);
                }
            }
        }

        void onError(String msg, int info){
            Console.WriteLine(msg);
            Console.WriteLine(info);
            Console.WriteLine("Error!");
            dataAcquisitionDevice.StopDacq();
        }

        void channelDataHandler(int[] data, int offset){
            Console.WriteLine("On data handler invoked");
            if (offset == 33){
                sendData(data);
            }
        }

        void sendData(int[] data){
            Console.WriteLine("hheh");
        }

        public bool startDevice(){
            Console.WriteLine("Starting device");
            dataAcquisitionDevice.StartDacq();
            Console.WriteLine("Device started");
            return true;
        }

        public bool stopDevice(){
            dataAcquisitionDevice.StopDacq();
            return true;
        }
    }

    // TODO should be used instead of just changing object state with activateDevice
    public class DeviceInfo {
    }
}
