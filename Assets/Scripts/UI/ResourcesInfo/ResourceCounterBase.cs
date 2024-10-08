﻿using DefaultNamespace;
using Events;
using GameResources;
using TMPro;
using UnityEngine;

namespace UI.ResourcesInfo
{
    public class ResourceCounterBase: MonoBehaviour
    {
        [SerializeField] protected ResourceManager resourceManager;
        [SerializeField] private TextMeshProUGUI text;

        protected virtual void Start()
        {
            resourceManager = FindObjectOfType<ResourceManager>();
            SetCount(0);
        }

        protected void SetCount(int value)
        {
            text.text = Flags.DigitalRomanReformFlag
                ? DigitalRomanReformEvent.ToRoman(value)
                : value.ToString();
        }
    }
}