using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                gameObject.AddComponent<WarEvent>(), // Можно на другие ресурсы дополнить/скопипастить
                gameObject.AddComponent<FestivalEvent>(), // Можно на другие ресурсы дополнить/скопипастить
                gameObject.AddComponent<BookkeepingMistakeEvent>(), // Можно на другие ресурсы дополнить/скопипастить
                gameObject.AddComponent<DayOfTheDayEvent>(),
                gameObject.AddComponent<DigitalRomanReformEvent>(),
                gameObject.AddComponent<TaxEvasionEvent>(), // Можно чутарика поменять BuildingBase и приватность баблов и сделать на разные ресурсы отдельно
                gameObject.AddComponent<SusEvent>(), // Хз как оно в браузере будет работать, мб придется убрать
                gameObject.AddComponent<HamsterEvent>(),
                gameObject.AddComponent<WinterEvent>()
            };
            StartCoroutine(EventsCoroutine());
        }

        private IEnumerator EventsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetNextEventDelay());
                Debug.Log("Start event");
                events[Random.Range(0, events.Count)].StartEvent();
            }
        }

        private float GetNextEventDelay()
        {
            return Random.Range(MinEventDelayInSeconds, MaxEventDelayInSeconds);
        }
    }

}