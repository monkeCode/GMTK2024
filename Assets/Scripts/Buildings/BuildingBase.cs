using System.Collections;
using Resources;
using UnityEngine;

namespace Buildings
{
    public abstract class BuildingBase : MonoBehaviour
    {
        protected ResourceManager ResourceManager;
        protected virtual int Income { get; set; }
        
        [SerializeField]private GameObject bubble;

        private void Start()
        {
            ResourceManager = FindObjectOfType<ResourceManager>();
            StartCoroutine(BubbleCoroutine(GetBubbleResetTimeInSeconds()));
        }
        
        protected virtual void OnMouseDown()
        {
            Debug.Log("click");
            HideBubble();
        }

        private void ShowBubble()
        {
            bubble.SetActive(true);
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
            return Random.Range(4, 10);
        }
    }
}
