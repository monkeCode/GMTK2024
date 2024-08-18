using System;
using System.Collections;
using System.Collections.Generic;
using GameResources;
using UnityEngine;
using Random = UnityEngine.Random;

public class HamsterKombatCryptoInvestment : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Vector3 target;
    [SerializeField] private float walkingRadius = 1;
    [SerializeField] private AudioSource audioSource;
    private ResourceManager resourceManager;

    private void OnMouseDown()
    {
        resourceManager.AddArmy(RandLoot());
        resourceManager.AddFood(RandLoot());
        resourceManager.AddMoney(RandLoot());
        audioSource.Play();

        int RandLoot() => Random.Range(0, 3);
    }

    void Start()
    {
        GetRandomDirection();
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void GetRandomDirection()
    {
        direction = Random.insideUnitCircle.normalized;
        target = transform.position + direction * walkingRadius;

    }

    private void Move() // Хочется научить не выбегать за экран
    {
        transform.position += direction * (Time.deltaTime * speed);
        var distance = Vector3.Distance(transform.position, target);
        if (distance < 1)
        {
            GetRandomDirection();
        }
    }
}
