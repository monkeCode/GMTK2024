using System.Collections;
using DefaultNamespace;
using GameResources;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public abstract class BuildingBase : MonoBehaviour
    {
        protected ResourceManager ResourceManager;
        protected virtual int Income { get; set; }
        
        [SerializeField] private GameObject bubble;
        private GameObject bubbleImage;
        [SerializeField] private int minResetTime = 4;
        [SerializeField] private int maxResetTime = 10;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            ResourceManager = FindObjectOfType<ResourceManager>();
            bubbleImage = bubble.GetComponentInChildren<Image>().gameObject;
            StartCoroutine(BubbleCoroutine(GetBubbleResetTimeInSeconds()));
        }
        
        protected virtual void OnMouseDown()
        {
            Debug.Log("click");
            PlaySound();
            HideBubble();
        }

        private void ShowBubble()
        {
            bubble.SetActive(true);
            if (Flags.TaxEvasionEventFlag && Random.value > 0.6)
                bubbleImage.SetActive(false);
            else
                bubbleImage.SetActive(true);
        }

        private void HideBubble()
        {
            bubble.SetActive(false);
            StartCoroutine(BubbleCoroutine(GetBubbleResetTimeInSeconds()));
        }

        private IEnumerator BubbleCoroutine(int resetTimeInSeconds)
        {
            yield return new WaitForSeconds(resetTimeInSeconds);
            ShowBubble();
        }

        private int GetBubbleResetTimeInSeconds()
        {
            return Random.Range(minResetTime, maxResetTime);
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
