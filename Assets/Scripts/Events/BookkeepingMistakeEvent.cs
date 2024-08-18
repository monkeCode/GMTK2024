using GameResources;
using UnityEngine;

namespace Events
{
    public class BookkeepingMistakeEvent : EventBase
    {
        protected override int BaseEventDurationInSeconds => 180;
        protected override string Headline => "BOOKKEEPING MISTAKE";

        protected override string Description =>
            "Our bookkeeper got drunk last night and made a few mistakes. Now instead of getting taxes from people, we might have to pay some of them";

        protected override Sprite Image => null; //TODO: пикчу найти

        private ResourceManager resourceManager;

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);
            resourceManager = FindObjectOfType<ResourceManager>();
            resourceManager.BookkeepingMistakeEventFlag = true;
        }
        
        protected override void EndEvent()
        {
            resourceManager.BookkeepingMistakeEventFlag = false;
        }
    }
}