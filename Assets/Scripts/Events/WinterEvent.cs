using UnityEngine;

namespace Events
{
    public class WinterEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 60;
        protected override string Headline => "WINTER IS COMING";
        protected override string Description => "Winter is coming, milord. People will stock their food";
        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_281");

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);
            Constants.Buildings.FarmIncome = 0;
            GameManager.Instance.StartWinter();
        }

        public override void EndEvent()
        {
            base.EndEvent();
            Constants.Buildings.FarmIncome = Constants.Buildings.DefaultIncome;
            GameManager.Instance.EndWinter();
        }
    }
}