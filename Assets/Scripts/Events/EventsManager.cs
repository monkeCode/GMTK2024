using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Events
{
    public class EventsManager : MonoBehaviour
    {
        [SerializeField] private List<EventBase> events;

        private void Start()
        {
            events = new List<EventBase> { gameObject.AddComponent<WarEvent>() };
            events[0].StartEvent(20);
        }
    }

}