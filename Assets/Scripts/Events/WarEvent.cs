using System.Collections;
using GameResources;
using UnityEngine;

namespace Events
{
    public class WarEvent : EventBase
    {
        public override void StartEvent(int eventDurationInSeconds)
        {
            Debug.Log("Event Started");
            Constants.Buildings.TowerIncome = 0;
            base.StartEvent(eventDurationInSeconds);
        }

        protected override void EndEvent()
        {
            Debug.Log("Event ended");
            Constants.Buildings.TowerIncome = Constants.Buildings.DefaultIncome;
        }
    }
}