using System.Collections;
using UnityEngine;

namespace Events
{
    public abstract class EventBase : MonoBehaviour
    {
        public virtual void StartEvent(int eventDurationInSeconds)
        {
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