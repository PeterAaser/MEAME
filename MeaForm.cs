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
        private bool running = false;

        public MeaForm(){
            InitializeComponent();
            meaInterface = new MeaInterface();
        }

        private void btMeaDevice_present_Click(object sender, EventArgs e){

            String[] deviceList = meaInterface.getDeviceListDescriptors();

            foreach(string device in deviceList){
                cbDevices.Items.Add(device);
            }

            if (cbDevices.Items.Count > 0)
                cbDevices.SelectedIndex = 0;
        }

        private void CbDevicesSelectedIndexChanged(object sender, EventArgs e){
            uint selectedDeviceIndex = (uint)cbDevices.SelectedIndex;

            // a uint can never be less than zero, but maybe it's like this for a reason?
            if (sel >= 0){
                btStart.Enabled = true;
            } else {
                btStart.Enabled = false;
            }

        }

        private void btStart_Click(object sender, EventArgs e){

        }

        private void btStop_Click(object sender, EventArgs e){

        }
    }
}
