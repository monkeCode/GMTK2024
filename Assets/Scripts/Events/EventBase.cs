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

        private void Awake()
        {
            eventDescriptionController = FindObjectOfType<EventDescriptionController>();
        }

        public void StartEvent()
        {
            StartEvent(BaseEventDurationInSeconds);
        }

        public virtual void StartEvent(int eventDurationInSeconds)
        {
            eventDescriptionController.StartEvent(Headline, Description, Image);
            StartCoroutine(DelayTillTheEnd(eventDurationInSeconds));
        }

        protected virtual IEnumerator DelayTillTheEnd(int eventDurationInSeconds)
        {
            yield return new WaitForSeconds(eventDurationInSeconds);
            EndEvent();
        }

        protected virtual void EndEvent()
        {
            
        }
    }
}