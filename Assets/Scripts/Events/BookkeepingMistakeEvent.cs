using DefaultNamespace;
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

        protected override Sprite Image => Resources.Load<Sprite>("#1 - Transparent Icons_174");

        public override void StartEvent(int eventDurationInSeconds)
        {
            base.StartEvent(eventDurationInSeconds);
            Flags.BookkeepingMistakeEventFlag = true;
        }
        
        public override void EndEvent()
        {
            base.EndEvent();
            Flags.BookkeepingMistakeEventFlag = false;
        }
    }
}