using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace VoiceModderProj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //StartListeningToMicrophone();
            populatecombobox();
        }
        public void populatecombobox() {
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                //make dropdown menu
                //Console.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
                comboBox1.Items.Add(deviceInfo.ProductName);
            }
        }

        public void StartListeningToMicrophone()
        {
            //throw new NotImplementedException();
            WaveIn wi = new WaveIn();
            wi.DeviceNumber = GetInputDevice();
            wi.WaveFormat = new NAudio.
        }

        private int GetInputDevice()
        {
            //throw new NotImplementedException();
            //get current dropdown value
            return comboBox1.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //wi.DeviceNumber = GetInputDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                //make dropdown menu
                //Console.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
                comboBox1.Items.Add(deviceInfo.ProductName);
            }
        }
    }
}
