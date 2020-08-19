using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VoiceModderProj.FX
{
    public abstract class Effect
    {
        private List<Slider> sliders;
        public float SampleRate { get; set; }
        public float Tempo { get; set; }
        public bool Enabled { get; set; }

        public Effect()
        {
            sliders = new List<Slider>();
            Enabled = true;
            Tempo = 120;
            SampleRate = 44100;
        }

        public IList<Slider> Sliders => sliders;

        public Slider AddSlider(float defaultValue, float minimum, float maximum, float increment, string description)
        {
            Slider slider = new Slider(defaultValue, minimum, maximum, increment, description);
            sliders.Add(slider);
            return slider;
        }

        public abstract string Name { get; }

        public virtual void Init() { }

        private volatile bool sliderChanged;

        public void SliderChanged() 
        {
            sliderChanged = true;
        }

        protected abstract void Slider();

        public virtual void Block(int samplesblock)
        {
        }

        public void OnSample(ref float left, ref float right)
        {
            if (sliderChanged) {
                Slider();
                sliderChanged = false;
            }
            Sample(ref left, ref right);
        }

        protected abstract void Sample(ref float spl0, ref float spl1);

        public override string ToString()
        {
            return Name;
        }
    }
}
