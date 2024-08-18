using System;
using DefaultNamespace;
using UI.BuildInfo;
using UI.ResourcesInfo;
using UnityEngine;

namespace Events
{
    public class DigitalRomanReformEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 60;
        protected override string Headline => "DIGITAL REFORM";
        protected override string Description => "Have you thought about Roman Empire today? Our bookkeepers thought and released roman digital reform";
        protected override Sprite Image => null; //TODO: пикчу добавить

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);

            Flags.DigitalRomanReformFlag = true;
        }

        protected override void EndEvent()
        {
            base.EndEvent();
            Flags.DigitalRomanReformFlag = false;
        }

        public static string ToRoman(int number)
        {
            if (number > 3999) return "No clue";
            if (number < 1) return string.Empty;            
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); 
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);            
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentException("Impossible state reached");
        }
    }
}