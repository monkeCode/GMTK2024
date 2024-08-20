using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
    public class EventsManager : MonoBehaviour
    {
        [SerializeField] private List<EventBase> events;
        [SerializeField] private int MinEventDelayInSeconds = 30;
        [SerializeField] private int MaxEventDelayInSeconds = 120;

        private AudioSource _audioSource;

        private List<EventBase> _events = new List<EventBase>();

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

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
                _audioSource.Play();
                var ev = events[Random.Range(0, events.Count)];
                //var ev = events[7];
                ev.StartEvent();
                _events.Add(ev);
            }
        }

        private void OnDestroy()
        {
            foreach(var e in events)
                if(e.EventActive)
                    e.EndEvent();
        }

        private float GetNextEventDelay()
        {
            return Random.Range(MinEventDelayInSeconds, MaxEventDelayInSeconds);
        }
    }

}