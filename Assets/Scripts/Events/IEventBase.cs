using System;
using System.Collections;
using UnityEngine;

namespace Events
{
    public abstract class EventBase : MonoBehaviour
    {
        private EventDescriptionController eventDescriptionController;
        protected abstract string Headline { get; }
        protected abstract string Description { get; }
        protected abstract Sprite Image { get; }

        private void Awake()
        {
            eventDescriptionController = FindObjectOfType<EventDescriptionController>();
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
        
        protected abstract void EndEvent();
    }
}