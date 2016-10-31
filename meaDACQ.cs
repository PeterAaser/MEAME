namespace MeaExampleNet{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Mcs.Usb;

    public class MeaDACQ {

        private readonly CMcsUsbListNet usblist = new CMcsUsbListNet();
        private CMeaDeviceNet dataAcquisitionDevice;
        private MeaZMQ zmq;

        private int channelblocksize = 0;
        private int mChannelHandles = 0;
        private int selectedDevice = 0;
        private int hwchannels = 0;
        private int gain = 0;
        private int block = 0;
        private uint[] outputChannels;

        private SampleSizeNet dataFormat;
        private int samplerate = 1000;

        private string deviceInfo = "Uninitialized DACQ device";

        // TODO is there a idiomatic way to initialize fields?
        public MeaDACQ(MeaZMQ zmq,
                       SampleSizeNet dataFormat = SampleSizeNet.SampleSize32Signed,
                       bool datasign = true,
                       int samplerate = 1000){

            this.dataFormat = dataFormat;
            this.samplerate = samplerate;
            this.zmq = zmq;
        }


        public override String ToString(){
            return deviceInfo;
        }


        public bool connectDataAcquisitionDevice(uint index){

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

            // Returns 64. Does this mean only 64 electrodes can be read simultaneously?? Dunno..
            dataAcquisitionDevice.HWInfo().GetNumberOfHWADCChannels(out hwchannels);

            dataAcquisitionDevice.SetNumberOfChannels(hwchannels);

            int ana, digi, che, tim, block;
            dataAcquisitionDevice.GetChannelLayout(out ana, out digi, out che, out tim, out block, 0);

            dataAcquisitionDevice.SetSampleRate(samplerate, 1, 0);

            int gain = dataAcquisitionDevice.GetGain();

            List<CMcsUsbDacqNet.CHWInfo.CVoltageRangeInfoNet> voltageranges;
            dataAcquisitionDevice.HWInfo().
                GetAvailableVoltageRangesInMicroVoltAndStringsInMilliVolt(out voltageranges);

            dataAcquisitionDevice.EnableDigitalIn(true, 0);
            dataAcquisitionDevice.EnableChecksum(true, 0);

            bool[] selectedChannels = new bool[block];
            for (int i = 0; i < block; i++){ selectedChannels[i] = true; } // hurr

            channelblocksize = 64;


            dataAcquisitionDevice.SetSelectedData(selectedChannels,
                                  10 * channelblocksize,
                                  channelblocksize,
                                  dataFormat,
                                  block);

            mChannelHandles = block;

            dataAcquisitionDevice.ChannelBlock_SetCheckChecksum((uint)che, (uint)tim); // ???

            // int voltrange = voltageranges.ToArray()[0];

            int validDataBits = -1;
            int deviceDataFormat = -1;
            dataAcquisitionDevice.GetNumberOfDataBits(0,
                                                      DacqGroupChannelEnumNet.HeadstageElectrodeGroup,
                                                      out validDataBits);

            dataAcquisitionDevice.GetDataFormat(0,
                                                DacqGroupChannelEnumNet.HeadstageElectrodeGroup,
                                                out deviceDataFormat);

            DataModeEnumNet dataMode = dataAcquisitionDevice.GetDataMode(0);

            int meme = dataAcquisitionDevice.GetChannelsInBlock();
            Console.WriteLine($"#####################\n{meme}\n############");

            deviceInfo =
                "Data acquisition device connected to physical device with parameters: \n" +
                $"number of blocks: {block}\n" +
                $"sample rate: {samplerate}\n" +
                $"Voltage range: {voltageranges[0].VoltageRangeDisplayStringMilliVolt}\n" +
                $"Corresponding to {voltageranges[0].VoltageRangeInMicroVolt} µV\n" +
                "--- channel layout ---\n" +
                $"hardware channels: {hwchannels}\n" +
                $"analog channels: {ana}\n" +
                $"digital channels: {digi}\n" +
                $"che(??) channels: {che}\n" +
                $"tim(??) channels: {tim}\n" +
                "---\n" +
                $"valid data bits: {validDataBits}\n" +          // 24
                $"device data format: {deviceDataFormat}\n" +    // 32
                $"device data mode: {dataMode}\n";               // dmSigned24bit


            Console.WriteLine(this);

            return true;

        }


        // TODO don't hardcode... Should ideally be passed as a parameter or something?
        void onChannelData(CMcsUsbDacqNet d, int cbHandle, int numSamples){

            int returnedFrames, totalChannels, offset, channels;
            dataAcquisitionDevice.ChannelBlock_GetChannel(0, 0, out totalChannels, out offset, out channels);
            int[] data = dataAcquisitionDevice.ChannelBlock_ReadFramesI32(0, channelblocksize, out returnedFrames);

            Console.WriteLine($" {returnedFrames}, {totalChannels}, {offset}, {channels}");

            for (int ii = 0; ii < totalChannels; ii++){

                int[] channelData = new int[returnedFrames];

                for (int jj = 0; jj < returnedFrames; jj++){
                    channelData[jj] = data[jj * mChannelHandles + ii];
                }

                if(ii == 32 || ii == 33 || ii == 34 || ii == 35){
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
}
