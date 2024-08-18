using UnityEngine;

namespace Events
{
    public class WinterEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 60;
        protected override string Headline => "WINTER IS COMING";
        protected override string Description => "Winter is coming, milord. People will stock their food";
        protected override Sprite Image => null; //TODO: добавить пикчу

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);
            Constants.Buildings.FarmIncome = 0;
            GameManager.Instance.StartWinter();
        }

        protected override void EndEvent()
        {
            base.EndEvent();
            Constants.Buildings.FarmIncome = Constants.Buildings.DefaultIncome;
            GameManager.Instance.EndWinter();
        }
    }
}