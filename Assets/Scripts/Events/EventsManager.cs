using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventsManager : MonoBehaviour
    {
        [SerializeField] private List<EventBase> events;
        [SerializeField] private int MinEventDelayInSeconds = 30;
        [SerializeField] private int MaxEventDelayInSeconds = 120;

        private void Start()
        {
            events = new List<EventBase>
            {
                gameObject.AddComponent<WarEvent>(),
                gameObject.AddComponent<FestivalEvent>(),
                gameObject.AddComponent<BookkeepingMistakeEvent>()
            };
            StartCoroutine(EventsCoroutine());
        }

        private IEnumerator EventsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetNextEventDelay());
                events[Random.Range(0, events.Count)].StartEvent();
            }
        }

        private float GetNextEventDelay()
        {
            return Random.Range(MinEventDelayInSeconds, MaxEventDelayInSeconds);
        }
    }

}