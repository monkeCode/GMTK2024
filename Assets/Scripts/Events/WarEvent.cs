using UnityEngine;

namespace Events
{
    public class WarEvent : EventBase
    {
        protected override int baseEventDurationInSeconds => 60;
        protected override string Headline => "WAR NEVER ENDS";
        protected override string Description => "Our neighbors have declared war. All military production will be spent on the needs of the army";
        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_57"); // Класть в папку Assets/Resources  

        public override void StartEvent()
        {
            Constants.Buildings.TowerIncome = 0;
            base.StartEvent();
        }

        protected override void EndEvent()
        {
            Constants.Buildings.TowerIncome = Constants.Buildings.DefaultIncome;
        }
    }
}