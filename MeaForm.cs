﻿using System;
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
            if (selectedDeviceIndex >= 0){
                MEA_start_button.Enabled = true;
                Console.WriteLine("no device");
            } else {
                MEA_start_button.Enabled = false;
                Console.WriteLine("device..?");
            }
        }



        private void Refresh_MEA_Devices(object sender, EventArgs e)
        {
            Devices_combobox.Items.Clear();
            String[] deviceList = meaInterface.getDeviceListDescriptors();

            foreach(string device in deviceList){
                Devices_combobox.Items.Add(device);
            }

            if (Devices_combobox.Items.Count > 0){
                Devices_combobox.SelectedIndex = 0;
                MEA_start_button.Enabled = true;
                MEA_connect_button.Enabled = true;
                connect_DSP_button.Enabled = true;
                Console.WriteLine("we on");
            }
        }


        private void Connect_MEA_click(object sender, EventArgs e)
        {
        }


        private void MEA_start_click(object sender, EventArgs e)
        {
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
            }
            else{
                Console.WriteLine("Connection failed");
            }
        }

        private void DSP_load_binary_clicked(object sender, EventArgs e)
        {

        }


        private void DSP_start_click(object sender, EventArgs e)
        {

        }


        private void DSP_stop_click(object sender, EventArgs e)
        {

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

    }
}
