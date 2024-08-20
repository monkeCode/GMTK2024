using System.Linq;
using GameResources;
using UnityEngine;

namespace Events
{
    public class DayOfTheDayEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 0;
        protected override string Headline => "CELEBRATING DAY OF THE DAY";

        protected override string Description => "Well, that is definitely one of the days. Guess we will randomly trade resources then?";

        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_277");

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);
            Debug.Log("Shuffled");
            var resourceManager = FindObjectOfType<ResourceManager>();
            var resources = new[] { resourceManager.MoneyCount, resourceManager.ArmyCount, resourceManager.FoodCount };
            var rearrangedResources = resources.OrderBy(x => Random.value).ToArray();

            resourceManager.TrySpendMoney(resources[0]);
            resourceManager.AddMoney(rearrangedResources[0]);

            resourceManager.TrySpendArmy(resources[1]);
            resourceManager.AddArmy(rearrangedResources[1]);

            resourceManager.TrySpendFood(resources[2]);
            resourceManager.AddFood(rearrangedResources[2]);
        }
    }
}