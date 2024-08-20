using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
    public class SusEvent : EventBase
    {
        private readonly string[] words = { "president", "religion", "year", "reform", "festival" };
        private string word;
        protected override int BaseEventDurationInSeconds => 0;
        protected override string Headline => $"NEW {word.ToUpper()}";
        protected override string Description => $"New {word} has come. Nothing changes for you, {Environment.UserName}";
        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_205");
        public override void StartEvent(int eventDurationInSeconds)
        {
            word = words[Random.Range(0, words.Length - 1)];
            base.StartEvent(eventDurationInSeconds);
        }
    }
}