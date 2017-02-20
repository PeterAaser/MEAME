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


        // Selected device combobox changed
        private void Devices_combobox_SelectedIndexChanged(object sender, EventArgs e){
            uint selectedDeviceIndex = (uint)Devices_combobox.SelectedIndex;

            // a uint can never be less than zero, but maybe it's like this for a reason?
            // TODO: relic, should be removed
            if (selectedDeviceIndex >= 0){
                MEA_start_button.Enabled = true;
            } else {
                MEA_start_button.Enabled = false;
            }
        }



        private void Refresh_MEA_Devices(object sender, EventArgs e)
        {
            Devices_combobox.Items.Clear();
            String[] deviceList = meaInterface.getDeviceListDescriptors();

            foreach(string device in deviceList){
                Devices_combobox.Items.Add(device);
            }

            MEA_connect_button.Enabled =
                (deviceList.Any(p => p[p.Length - 1] == 'A'));

            connect_DSP_button.Enabled =
                (deviceList.Any(p => p[p.Length - 1] == 'B'));

            if (Devices_combobox.Items.Count > 0){
                Devices_combobox.SelectedIndex = 0;
                Console.WriteLine("we on");
            }
        }


        private void Connect_MEA_click(object sender, EventArgs e)
        {
            Console.WriteLine("Not implemented atm");
        }


        private void MEA_start_click(object sender, EventArgs e)
        {
            Console.WriteLine((uint)Devices_combobox.SelectedIndex);
            if(meaInterface
               .dataAcquisitionDevice
               .connectDataAcquisitionDevice((uint)Devices_combobox.SelectedIndex))
            {
                meaInterface.startDevice();
                MEA_start_button.Enabled = false;
                MEA_connect_button.Enabled = true;
                MEA_stop_button.Enabled = true;
            }
        }


        private void MEA_stop_click(object sender, EventArgs e)
        {
            if(meaInterface.dataAcquisitionDevice.stopDevice()){
                MEA_stop_button.Enabled = false;
                MEA_start_button.Enabled = true;
            }
        }


        private void connect_DSP_click(object sender, EventArgs e)
        {
            Console.WriteLine("Attempting to connect to DSP");
            dspInterface = new DSPComms();
            if(dspInterface.connected){
                Console.WriteLine("Connection successful");
                Frequency_textbox.Enabled = true;
                DSP_start_button.Enabled = true;
                DSP_stop_button.Enabled = true;
                request_stim_button.Enabled = true;
                DSP_load_binary_button.Enabled = true;
            }
            else{
                Console.WriteLine("Connection failed");
            }
        }


        private void DSP_load_binary_clicked(object sender, EventArgs e)
        {
            dspInterface.uploadBinary();
            DSP_get_debug.Enabled = true;
            sg3.Enabled = true;
            sg2.Enabled = true;
            sg1.Enabled = true;
            sg_clear.Enabled = true;
            Console.WriteLine("all debug buttons should be ready");
        }


        private void DSP_start_click(object sender, EventArgs e)
        {
            dspInterface.readDevicePrint();
        }


        private void DSP_stop_click(object sender, EventArgs e)
        {
            dspInterface.disconnect();
        }


        private void DSP_debug_clicked(object sender, EventArgs e)
        {
            dspInterface.barfDebug();
        }


        private void request_stim_button_Click(object sender, EventArgs e)
        {
            dspInterface.triggerStimReg(100);
        }


        private void Frequency_textbox_changed(object sender, EventArgs e)
        {

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

        private void tbDeviceInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void sg3_Click(object sender, EventArgs e)
        {
            dspInterface.sg_debug(3);
        }

        private void sg2_Click(object sender, EventArgs e)
        {
            dspInterface.sg_debug(2);
        }

        private void sg1_Click(object sender, EventArgs e)
        {
            dspInterface.sg_debug(1);
        }

        private void sg_clear_Click(object sender, EventArgs e)
        {
            dspInterface.sg_clear();
        }
    }
}
