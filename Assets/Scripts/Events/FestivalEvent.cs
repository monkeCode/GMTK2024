using GameResources;
using UnityEngine;

namespace Events
{
    public class FestivalEvent: EventBase
    {
        protected override int baseEventDurationInSeconds => 0;
        protected override string Headline => "FESTIVAL!";

        protected override string Description =>
            "The annual festival begins. Half of the money will be spent on food.";
        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_5");

        public override void StartEvent()
        {
            base.StartEvent();
            var resourceManager = FindObjectOfType<ResourceManager>();
            resourceManager.TrySpendMoney(resourceManager.MoneyCount / 2);
            resourceManager.AddFood(resourceManager.MoneyCount);
            
        }

        protected override void EndEvent(){}
    }
}