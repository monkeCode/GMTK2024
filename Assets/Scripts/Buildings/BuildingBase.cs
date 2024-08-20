using System;
using System.Collections;
using DefaultNamespace;
using GameResources;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Buildings
{
    public abstract class BuildingBase : MonoBehaviour
    {
        protected ResourceManager ResourceManager;
        protected virtual int Income { get; set; }
        
        [SerializeField] private GameObject bubble;
        private GameObject bubbleImage;
        public virtual float MinResetTime => Constants.Buildings.MinDelayBubble;
        public virtual float MaxResetTime => Constants.Buildings.MaxDelayBubble;

        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            ResourceManager = FindObjectOfType<ResourceManager>();
            bubbleImage = bubble.GetComponentInChildren<Image>().gameObject;
            StartCoroutine(BubbleCoroutine(GetBubbleResetTimeInSeconds()));
            var buildController = FindObjectOfType<BuildController>();
            buildController.onBubbleSwitch.AddListener(SwitchBubble);
        }
        
        protected virtual void OnMouseDown()
        {
                PlaySound();
                HideBubble();
        }

        protected void OnMouseEnter()
        {
            if(Input.GetAxis("Fire1")>0)
                OnMouseDown();
        }


        private void ShowBubble()
        {
            bubble.SetActive(true);
            if (Flags.TaxEvasionEventFlag && Random.value > 0.6 || Flags.BubbleOffFlag)
                bubbleImage.SetActive(false);
            else
                bubbleImage.SetActive(true);
        }

        private void SwitchBubble()
        {
            if (bubble.activeSelf)
                bubbleImage.SetActive(!bubbleImage.activeSelf);
        }

        private void HideBubble()
        {
            bubble.SetActive(false);
            StartCoroutine(BubbleCoroutine(GetBubbleResetTimeInSeconds()));
        }

        private IEnumerator BubbleCoroutine(float resetTimeInSeconds)
        {
            yield return new WaitForSeconds(resetTimeInSeconds);
            ShowBubble();
        }

        private float GetBubbleResetTimeInSeconds()
        {
            return Random.Range(MinResetTime, MaxResetTime);
        }

        private void PlaySound()
        {
            audioSource.Play();
        }

        protected virtual void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        }
    }
}
