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
    using ZeroMQ;

    public partial class MeaForm : Form{

        public MeaInterface meaInterface;
        public DSPComms dspInterface;
        private bool running = false;

        public MeaForm(){
            InitializeComponent();
            meaInterface = new MeaInterface();

            for(int i = 0; i < 100; i++){
                cbChannel.Items.Add(i); comboBox1.Items.Add(i);
                comboBox2.Items.Add(i); comboBox3.Items.Add(i);
            }
        }

        // Device present button
        private void btMeaDevice_present_Click(object sender, EventArgs e){

            cbDevices.Items.Clear();
            String[] deviceList = meaInterface.getDeviceListDescriptors();

            foreach(string device in deviceList){
                cbDevices.Items.Add(device);
            }

            if (cbDevices.Items.Count > 0){
                cbDevices.SelectedIndex = 0;
                btStart.Enabled = true;
                button1.Enabled = true;
            }
        }

        // Selected device combobox changed
        private void CbDevicesSelectedIndexChanged(object sender, EventArgs e){
            uint selectedDeviceIndex = (uint)cbDevices.SelectedIndex;

            // a uint can never be less than zero, but maybe it's like this for a reason?
            if (selectedDeviceIndex >= 0){
                btStart.Enabled = true;
                Console.WriteLine("no device");
            } else {
                btStart.Enabled = false;
                Console.WriteLine("device..?");
            }

        }

        // Start button clicked
        private void btStart_Click(object sender, EventArgs e){
            if(meaInterface
               .dataAcquisitionDevice
               .connectDataAcquisitionDevice((uint)cbDevices.SelectedIndex)
               // &&
               // meaInterface
               // .stgDevice
               // .connectSTGDevice((uint)cbDevices.SelectedIndex)
               ){

                meaInterface.startDevice();
                btStart.Enabled = false;
                button1.Enabled = true;
                btStop.Enabled = true;
            }
        }

        // Stop button clicked
        private void btStop_Click(object sender, EventArgs e){
            if(meaInterface.dataAcquisitionDevice.stopDevice()){
                btStop.Enabled = false;
                btStart.Enabled = true;
            }
        }


        // really...
        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e){
            // meaInterface.setOutputChannel((uint)cbChannel.SelectedIndex, 0);
            Console.WriteLine("panel 0 reading channel {0}", cbChannel.SelectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){
            // meaInterface.setOutputChannel((uint)comboBox1.SelectedIndex, 2);
            Console.WriteLine("panel 1 reading channel {0}", comboBox1.SelectedIndex);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e){
            // meaInterface.setOutputChannel((uint)comboBox2.SelectedIndex, 3);
            Console.WriteLine("panel 2 reading channel {0}", comboBox2.SelectedIndex);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e){
            // meaInterface.setOutputChannel((uint)comboBox3.SelectedIndex, 1);
            Console.WriteLine("panel 3 reading channel {0}", comboBox3.SelectedIndex);
        }

        private void MeaForm_Load(object sender, EventArgs e)
        {
        }

        // connect stims
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Attempting to connect to DSP");
            dspInterface = new DSPComms();
            if(dspInterface.connected){
                textBox1.Enabled = true;
                button2.Enabled = true;
            }
        }

        // new stim freq
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // set new stim
        private void button2_Click(object sender, EventArgs e)
        {
            int stimFreq = Convert.ToInt32(textBox1.Text);
            dspInterface.triggerStimReg((uint)stimFreq);
        }
    }
}
