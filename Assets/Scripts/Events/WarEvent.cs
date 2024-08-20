using UnityEngine;

namespace Events
{
    public class WarEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 60;
        protected override string Headline => "WAR NEVER CHANGES";
        protected override string Description => "Our neighbors have declared war. All military production will be spent on the needs of the army";
        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_57"); // Класть в папку Assets/Resources  

        public override void StartEvent(int eventDurationInSeconds)
        {
            Constants.Buildings.TowerIncome = 0;
            base.StartEvent(eventDurationInSeconds);
        }

        public override void EndEvent()
        {
            Constants.Buildings.TowerIncome = Constants.Buildings.DefaultIncome;
        }
    }
}