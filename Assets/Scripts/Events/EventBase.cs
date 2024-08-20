using System.Collections;
using UnityEngine;

namespace Events
{
    public abstract class EventBase : MonoBehaviour
    {
        private EventDescriptionController eventDescriptionController;
        protected abstract int BaseEventDurationInSeconds { get; }
        protected abstract string Headline { get; }
        protected abstract string Description { get; }
        protected abstract Sprite Image { get; }

        public bool EventActive { get; private set; } = false;

        private void Awake()
        {
            eventDescriptionController = FindObjectOfType<EventDescriptionController>();
        }

        public void StartEvent()
        {
            StartEvent(BaseEventDurationInSeconds);
            EventActive = true;
        }

        public virtual void StartEvent(int eventDurationInSeconds)
        {
            eventDescriptionController.StartEvent(Headline, Description, Image);
            StartCoroutine(DelayTillTheEnd(eventDurationInSeconds));
        }

        protected virtual IEnumerator DelayTillTheEnd(int eventDurationInSeconds)
        {
            yield return new WaitForSeconds(eventDurationInSeconds);
            if(EventActive)
                EndEvent();
        }

        public virtual void EndEvent()
        {
            EventActive = false;
        }
    }
}