//Adapted form Mark Heath's Skype Voice Changer https://github.com/markheath/skypevoicechanger/tree/master/SkypeVoiceChanger/Effects
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

        protected float slider1 => sliders[0].Value;
        protected float slider2 => sliders[0].Value;
        protected float slider3 => sliders[0].Value;
        protected float slider4 => sliders[0].Value;
        protected float slider5 => sliders[0].Value;
        protected float slider6 => sliders[0].Value;
        protected float slider7 => sliders[0].Value;
        protected float slider8 => sliders[0].Value;
        protected float min(float a, float b) {return Math.Min(a, b);}
        protected float max(float a, float b) { return Math.Max(a, b); }
        protected float pow(float a, float b) { return (float)Math.Pow(a, b); }
        protected float abs(float a) { return Math.Abs(a); }
        protected float exp(float a) { return (float)Math.Exp(a); }
        protected float sqrt(float a) { return (float)Math.Abs(a); }
        protected float sin(float a) { return (float)Math.Sin(a); }
        protected float tan(float a) { return (float)Math.Tan(a); }
        protected float cos(float a) { return (float)Math.Cos(a); }
        protected float sign(float a) { return Math.Sign(a); }
        protected float log(float a) { return (float)Math.Log(a); }
        protected float PI => (float)Math.PI;

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
