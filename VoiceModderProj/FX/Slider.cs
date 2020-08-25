//Adapted form Mark Heath's Skype Voice Changer https://github.com/markheath/skypevoicechanger/tree/master/SkypeVoiceChanger/Effects
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModderProj.FX
{
    public class Slider
    {
        List<string> discreteValueText;

        public Slider(float defaultValue, float minimum, float maximum, float increment, string description) 
        {
            this.Default = defaultValue;
            this.Value = defaultValue;
            this.Min = minimum;
            this.Max = maximum;
            this.Increment = increment;
            this.Description = description;
            this.discreteValueText = new List<string>();
        }

        public float Default { get; private set; }
        public float Value { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public float Increment { get; set; }
        public string Description { get; set; }

        public IList<string> DiscreteValueText => discreteValueText;
    }
}