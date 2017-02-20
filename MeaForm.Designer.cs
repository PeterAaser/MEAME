using System;

namespace MeaExampleNet
{
    partial class MeaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MEA_stop_button = new System.Windows.Forms.Button();
            this.MEA_start_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Devices_combobox = new System.Windows.Forms.ComboBox();
            this.Refresh_Devices_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.MEA_connect_button = new System.Windows.Forms.Button();
            this.connect_DSP_button = new System.Windows.Forms.Button();
            this.Frequency_textbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.DSP_start_button = new System.Windows.Forms.Button();
            this.DSP_load_binary_button = new System.Windows.Forms.Button();
            this.DSP_stop_button = new System.Windows.Forms.Button();
            this.mail_text = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.req_id_text = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.electrodes1_text = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dac_id_text = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.request_ACK_text = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.sample_text = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.period_text = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.electrodes2_text = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.request_stim_button = new System.Windows.Forms.Button();
            this.DSP_get_debug = new System.Windows.Forms.Button();
            this.sg1 = new System.Windows.Forms.Button();
            this.sg2 = new System.Windows.Forms.Button();
            this.sg3 = new System.Windows.Forms.Button();
            this.sg_clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(543, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 241);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(543, 269);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(525, 241);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(543, 516);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(525, 241);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Info;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(12, 516);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(525, 241);
            this.panel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(403, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Channel 1";
            // 
            // cbChannel
            // 
            this.cbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannel.FormattingEnabled = true;
            this.cbChannel.Location = new System.Drawing.Point(464, 97);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(70, 21);
            this.cbChannel.TabIndex = 15;
            this.cbChannel.SelectedIndexChanged += new System.EventHandler(this.cbChannel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Sampling";
            // 
            // MEA_stop_button
            // 
            this.MEA_stop_button.Enabled = false;
            this.MEA_stop_button.Location = new System.Drawing.Point(93, 80);
            this.MEA_stop_button.Name = "MEA_stop_button";
            this.MEA_stop_button.Size = new System.Drawing.Size(75, 23);
            this.MEA_stop_button.TabIndex = 13;
            this.MEA_stop_button.Text = "Stop";
            this.MEA_stop_button.UseVisualStyleBackColor = true;
            this.MEA_stop_button.Click += new System.EventHandler(this.MEA_stop_click);
            // 
            // MEA_start_button
            // 
            this.MEA_start_button.Enabled = false;
            this.MEA_start_button.Location = new System.Drawing.Point(12, 80);
            this.MEA_start_button.Name = "MEA_start_button";
            this.MEA_start_button.Size = new System.Drawing.Size(75, 23);
            this.MEA_start_button.TabIndex = 12;
            this.MEA_start_button.Text = "Start";
            this.MEA_start_button.UseVisualStyleBackColor = true;
            this.MEA_start_button.Click += new System.EventHandler(this.MEA_start_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Devices";
            // 
            // Devices_combobox
            // 
            this.Devices_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Devices_combobox.FormattingEnabled = true;
            this.Devices_combobox.Location = new System.Drawing.Point(207, 22);
            this.Devices_combobox.Name = "Devices_combobox";
            this.Devices_combobox.Size = new System.Drawing.Size(227, 21);
            this.Devices_combobox.TabIndex = 10;
            this.Devices_combobox.SelectedIndexChanged += new System.EventHandler(this.Devices_combobox_SelectedIndexChanged);
            // 
            // Refresh_Devices_button
            // 
            this.Refresh_Devices_button.Location = new System.Drawing.Point(12, 22);
            this.Refresh_Devices_button.Name = "Refresh_Devices_button";
            this.Refresh_Devices_button.Size = new System.Drawing.Size(138, 23);
            this.Refresh_Devices_button.TabIndex = 9;
            this.Refresh_Devices_button.Text = "Refresh MEA devices";
            this.Refresh_Devices_button.UseVisualStyleBackColor = true;
            this.Refresh_Devices_button.Click += new System.EventHandler(this.Refresh_MEA_Devices);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Channel 2";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(464, 123);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(70, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(403, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Channel 4";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(464, 177);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(70, 21);
            this.comboBox2.TabIndex = 22;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(403, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Channel 3";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(464, 150);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(70, 21);
            this.comboBox3.TabIndex = 20;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // MEA_connect_button
            // 
            this.MEA_connect_button.Enabled = false;
            this.MEA_connect_button.Location = new System.Drawing.Point(13, 51);
            this.MEA_connect_button.Name = "MEA_connect_button";
            this.MEA_connect_button.Size = new System.Drawing.Size(155, 23);
            this.MEA_connect_button.TabIndex = 24;
            this.MEA_connect_button.Text = "Connect MEA";
            this.MEA_connect_button.UseVisualStyleBackColor = true;
            this.MEA_connect_button.Click += new System.EventHandler(this.Connect_MEA_click);
            // 
            // connect_DSP_button
            // 
            this.connect_DSP_button.Enabled = false;
            this.connect_DSP_button.Location = new System.Drawing.Point(12, 175);
            this.connect_DSP_button.Name = "connect_DSP_button";
            this.connect_DSP_button.Size = new System.Drawing.Size(156, 23);
            this.connect_DSP_button.TabIndex = 25;
            this.connect_DSP_button.Text = "Connect DSP";
            this.connect_DSP_button.UseVisualStyleBackColor = true;
            this.connect_DSP_button.Click += new System.EventHandler(this.connect_DSP_click);
            // 
            // Frequency_textbox
            // 
            this.Frequency_textbox.Location = new System.Drawing.Point(187, 236);
            this.Frequency_textbox.Margin = new System.Windows.Forms.Padding(1);
            this.Frequency_textbox.Name = "Frequency_textbox";
            this.Frequency_textbox.Size = new System.Drawing.Size(95, 20);
            this.Frequency_textbox.TabIndex = 26;
            this.Frequency_textbox.Text = "Frequency";
            this.Frequency_textbox.TextChanged += new System.EventHandler(this.Frequency_textbox_changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Stimpack";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(187, 205);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "Set frequency";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // DSP_start_button
            // 
            this.DSP_start_button.Enabled = false;
            this.DSP_start_button.Location = new System.Drawing.Point(13, 233);
            this.DSP_start_button.Name = "DSP_start_button";
            this.DSP_start_button.Size = new System.Drawing.Size(75, 23);
            this.DSP_start_button.TabIndex = 30;
            this.DSP_start_button.Text = "Start";
            this.DSP_start_button.UseVisualStyleBackColor = true;
            this.DSP_start_button.Click += new System.EventHandler(this.DSP_start_click);
            // 
            // DSP_load_binary_button
            // 
            this.DSP_load_binary_button.Enabled = false;
            this.DSP_load_binary_button.Location = new System.Drawing.Point(13, 205);
            this.DSP_load_binary_button.Name = "DSP_load_binary_button";
            this.DSP_load_binary_button.Size = new System.Drawing.Size(155, 23);
            this.DSP_load_binary_button.TabIndex = 32;
            this.DSP_load_binary_button.Text = "Load binary";
            this.DSP_load_binary_button.UseVisualStyleBackColor = true;
            this.DSP_load_binary_button.Click += new System.EventHandler(this.DSP_load_binary_clicked);
            // 
            // DSP_stop_button
            // 
            this.DSP_stop_button.Enabled = false;
            this.DSP_stop_button.Location = new System.Drawing.Point(94, 233);
            this.DSP_stop_button.Name = "DSP_stop_button";
            this.DSP_stop_button.Size = new System.Drawing.Size(75, 23);
            this.DSP_stop_button.TabIndex = 31;
            this.DSP_stop_button.Text = "Stop";
            this.DSP_stop_button.UseVisualStyleBackColor = true;
            this.DSP_stop_button.Click += new System.EventHandler(this.DSP_stop_click);
            // 
            // mail_text
            // 
            this.mail_text.Location = new System.Drawing.Point(420, 315);
            this.mail_text.Margin = new System.Windows.Forms.Padding(1);
            this.mail_text.Name = "mail_text";
            this.mail_text.Size = new System.Drawing.Size(95, 20);
            this.mail_text.TabIndex = 45;
            this.mail_text.Text = "Frequency";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(346, 341);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "req id";
            // 
            // req_id_text
            // 
            this.req_id_text.Location = new System.Drawing.Point(420, 338);
            this.req_id_text.Margin = new System.Windows.Forms.Padding(1);
            this.req_id_text.Name = "req_id_text";
            this.req_id_text.Size = new System.Drawing.Size(95, 20);
            this.req_id_text.TabIndex = 47;
            this.req_id_text.Text = "Frequency";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(346, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "electrodes 1";
            // 
            // electrodes1_text
            // 
            this.electrodes1_text.Location = new System.Drawing.Point(420, 385);
            this.electrodes1_text.Margin = new System.Windows.Forms.Padding(1);
            this.electrodes1_text.Name = "electrodes1_text";
            this.electrodes1_text.Size = new System.Drawing.Size(95, 20);
            this.electrodes1_text.TabIndex = 51;
            this.electrodes1_text.Text = "Frequency";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(346, 365);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "dac id";
            // 
            // dac_id_text
            // 
            this.dac_id_text.Location = new System.Drawing.Point(420, 362);
            this.dac_id_text.Margin = new System.Windows.Forms.Padding(1);
            this.dac_id_text.Name = "dac_id_text";
            this.dac_id_text.Size = new System.Drawing.Size(95, 20);
            this.dac_id_text.TabIndex = 49;
            this.dac_id_text.Text = "Frequency";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(346, 482);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "request ACK";
            // 
            // request_ACK_text
            // 
            this.request_ACK_text.Location = new System.Drawing.Point(420, 479);
            this.request_ACK_text.Margin = new System.Windows.Forms.Padding(1);
            this.request_ACK_text.Name = "request_ACK_text";
            this.request_ACK_text.Size = new System.Drawing.Size(95, 20);
            this.request_ACK_text.TabIndex = 59;
            this.request_ACK_text.Text = "Frequency";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(346, 459);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 58;
            this.label13.Text = "sample";
            // 
            // sample_text
            // 
            this.sample_text.Location = new System.Drawing.Point(420, 456);
            this.sample_text.Margin = new System.Windows.Forms.Padding(1);
            this.sample_text.Name = "sample_text";
            this.sample_text.Size = new System.Drawing.Size(95, 20);
            this.sample_text.TabIndex = 57;
            this.sample_text.Text = "Frequency";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(346, 435);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "period";
            // 
            // period_text
            // 
            this.period_text.Location = new System.Drawing.Point(420, 432);
            this.period_text.Margin = new System.Windows.Forms.Padding(1);
            this.period_text.Name = "period_text";
            this.period_text.Size = new System.Drawing.Size(95, 20);
            this.period_text.TabIndex = 55;
            this.period_text.Text = "Frequency";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(346, 412);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 54;
            this.label15.Text = "electrodes 2";
            // 
            // electrodes2_text
            // 
            this.electrodes2_text.Location = new System.Drawing.Point(420, 409);
            this.electrodes2_text.Margin = new System.Windows.Forms.Padding(1);
            this.electrodes2_text.Name = "electrodes2_text";
            this.electrodes2_text.Size = new System.Drawing.Size(95, 20);
            this.electrodes2_text.TabIndex = 53;
            this.electrodes2_text.Text = "Frequency";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(346, 318);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "mail base";
            // 
            // request_stim_button
            // 
            this.request_stim_button.Enabled = false;
            this.request_stim_button.Location = new System.Drawing.Point(13, 262);
            this.request_stim_button.Name = "request_stim_button";
            this.request_stim_button.Size = new System.Drawing.Size(156, 23);
            this.request_stim_button.TabIndex = 78;
            this.request_stim_button.Text = "Request stim";
            this.request_stim_button.UseVisualStyleBackColor = true;
            this.request_stim_button.Click += new System.EventHandler(this.request_stim_button_Click);
            // 
            // DSP_get_debug
            // 
            this.DSP_get_debug.Enabled = false;
            this.DSP_get_debug.Location = new System.Drawing.Point(349, 278);
            this.DSP_get_debug.Name = "DSP_get_debug";
            this.DSP_get_debug.Size = new System.Drawing.Size(95, 23);
            this.DSP_get_debug.TabIndex = 79;
            this.DSP_get_debug.Text = "Get debug data";
            this.DSP_get_debug.UseVisualStyleBackColor = true;
            this.DSP_get_debug.Click += new System.EventHandler(this.DSP_debug_clicked);
            // 
            // sg1
            // 
            this.sg1.Enabled = false;
            this.sg1.Location = new System.Drawing.Point(12, 341);
            this.sg1.Name = "sg1";
            this.sg1.Size = new System.Drawing.Size(75, 23);
            this.sg1.TabIndex = 80;
            this.sg1.Text = "sg1 debug";
            this.sg1.UseVisualStyleBackColor = true;
            this.sg1.Click += new System.EventHandler(this.sg1_Click);
            // 
            // sg2
            // 
            this.sg2.Enabled = false;
            this.sg2.Location = new System.Drawing.Point(13, 370);
            this.sg2.Name = "sg2";
            this.sg2.Size = new System.Drawing.Size(75, 23);
            this.sg2.TabIndex = 81;
            this.sg2.Text = "sg2 debug";
            this.sg2.UseVisualStyleBackColor = true;
            this.sg2.Click += new System.EventHandler(this.sg2_Click);
            // 
            // sg3
            // 
            this.sg3.Enabled = false;
            this.sg3.Location = new System.Drawing.Point(13, 399);
            this.sg3.Name = "sg3";
            this.sg3.Size = new System.Drawing.Size(75, 23);
            this.sg3.TabIndex = 82;
            this.sg3.Text = "sg3 debug";
            this.sg3.UseVisualStyleBackColor = true;
            this.sg3.Click += new System.EventHandler(this.sg3_Click);
            // 
            // sg_clear
            // 
            this.sg_clear.Enabled = false;
            this.sg_clear.Location = new System.Drawing.Point(13, 428);
            this.sg_clear.Name = "sg_clear";
            this.sg_clear.Size = new System.Drawing.Size(75, 23);
            this.sg_clear.TabIndex = 83;
            this.sg_clear.Text = "debug clear";
            this.sg_clear.UseVisualStyleBackColor = true;
            this.sg_clear.Click += new System.EventHandler(this.sg_clear_Click);
            // 
            // MeaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 674);
            this.Controls.Add(this.sg_clear);
            this.Controls.Add(this.sg3);
            this.Controls.Add(this.sg2);
            this.Controls.Add(this.sg1);
            this.Controls.Add(this.DSP_get_debug);
            this.Controls.Add(this.request_stim_button);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.request_ACK_text);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.sample_text);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.period_text);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.electrodes2_text);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.electrodes1_text);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dac_id_text);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.req_id_text);
            this.Controls.Add(this.mail_text);
            this.Controls.Add(this.DSP_load_binary_button);
            this.Controls.Add(this.DSP_stop_button);
            this.Controls.Add(this.DSP_start_button);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Frequency_textbox);
            this.Controls.Add(this.connect_DSP_button);
            this.Controls.Add(this.MEA_connect_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MEA_stop_button);
            this.Controls.Add(this.MEA_start_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Devices_combobox);
            this.Controls.Add(this.Refresh_Devices_button);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MeaForm";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.MeaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DSP_load_binary_click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button MEA_stop_button;
        private System.Windows.Forms.Button MEA_start_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Devices_combobox;
        private System.Windows.Forms.Button Refresh_Devices_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button MEA_connect_button;
        private System.Windows.Forms.Button connect_DSP_button;
        private System.Windows.Forms.TextBox Frequency_textbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button DSP_start_button;
        private System.Windows.Forms.Button DSP_load_binary_button;
        private System.Windows.Forms.Button DSP_stop_button;
        private System.Windows.Forms.TextBox mail_text;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox req_id_text;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox electrodes1_text;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox dac_id_text;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox request_ACK_text;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox sample_text;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox period_text;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox electrodes2_text;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button request_stim_button;
        private System.Windows.Forms.Button DSP_get_debug;
        private System.Windows.Forms.Button sg1;
        private System.Windows.Forms.Button sg2;
        private System.Windows.Forms.Button sg3;
        private System.Windows.Forms.Button sg_clear;
    }
}
