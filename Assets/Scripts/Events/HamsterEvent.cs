using System;
using UnityEngine;

namespace Events
{
    public class HamsterEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 20;
        protected override string Headline => "A HAMSTER?";

        protected override string Description =>
            "Villagers say they tap some hamster to get rich. Why don't you try yourself?";
        protected override Sprite Image => Resources.Load<Sprite>("Хомяк");
        private GameObject hamsterPrefab;
        private GameObject hamster;

        private void Start()
        {
            hamsterPrefab = Resources.Load<GameObject>("ХомякPrefab");
        }

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);
            hamster = Instantiate(hamsterPrefab);
        }

        protected override void EndEvent()
        {
            base.EndEvent();
            Destroy(hamster);
        }
    }
}