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
using NAudio.Gui;

namespace VoiceModderProj
{
    public partial class Form1 : Form
    {
        private int RATE = 44100; //sound card sample rate, may need tweaking
        //private int BUFFSIZE = (int)(Math.Pow(2, 11)); //Must be a multiple of 2

        private BufferedWaveProvider bwp;
        private WaveIn recorder;
        private WaveOut player;
        bool recordToggle = false;

        public Form1()
        {
            InitializeComponent();
            populatecombobox();
        }

        //Make dropdown menu
        public void populatecombobox() {
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                comboBox1.Items.Add(deviceInfo.ProductName);
            }
        }

        //When the selected audio device has audio ready,
        // add this audio in samples to the buffer.
        void RecorderDataAvailable(object sender, WaveInEventArgs args) 
        {
            bwp.AddSamples(args.Buffer, 0, args.BytesRecorded);
        }

        public void StartListeningToMicrophone()
        {
            //throw new NotImplementedException();
            int audioDeviceNumber = GetInputDevice();
            recorder = new WaveIn()
            {
                DeviceNumber = audioDeviceNumber,
                WaveFormat = new WaveFormat(RATE, 1)
            };
            recorder.DataAvailable += new EventHandler<WaveInEventArgs>(RecorderDataAvailable);

            bwp = new BufferedWaveProvider(recorder.WaveFormat);

            //recorder = new WaveIn()
            //{
            //    DeviceNumber = audioDeviceNumber,
            //    WaveFormat = new NAudio.Wave.WaveFormat(RATE, 1),
            //    BufferMilliseconds = (int)((double)BUFFSIZE / (double)RATE * 1000.0)
            //};
            //recorder.DataAvailable += new EventHandler<WaveInEventArgs>(RecorderDataAvailable);

            //bwp = new BufferedWaveProvider(recorder.WaveFormat)
            //{
            //    BufferLength = BUFFSIZE * 2,
            //    DiscardOnBufferOverflow = true
            //};

            player = new WaveOut();
            player.Init(bwp);
            player.Play();
            recorder.StartRecording();
        }

        public void StopListeningToMicrophone() 
        {
            recorder.StopRecording();
            player.Stop();
        }

        private int GetInputDevice()
        {
            //get current dropdown value
            return comboBox1.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //recorder.DeviceNumber = GetInputDevice();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            if (!recordToggle)
            {
                StartListeningToMicrophone();
                recordToggle = true;
                ctrl.BackColor = Color.Red;
            }
            else if (recordToggle)
            {
                StopListeningToMicrophone();
                recordToggle = false;
                ctrl.BackColor = Color.Gray;
            }
        }
    }
}
