using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EventDescriptionController : MonoBehaviour
{
    [SerializeField] private GameObject eventWindow;
    [SerializeField] private TextMeshProUGUI headline;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image image;

    void Update()
    {
        HandleInput();
    }
    
    public void Close()
    {
        eventWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartEvent(string headline, string description, Sprite image)
    {
        this.headline.text = headline;
        this.description.text = description;
        this.image.sprite = image;
        eventWindow.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Close();
        }
    }
}
