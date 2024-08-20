using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class TreeDecoration : MonoBehaviour
{
    [Serializable]
    class SeasonalSprite
    {
        public Sprite Spring;
        public Sprite Winter;
    }

    [SerializeField] private List<SeasonalSprite> _sprites;

    private SpriteRenderer _spriteRenderer;

    private int _spriteIndex = 0;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteIndex = Random.Range(0, _sprites.Count);
        _spriteRenderer.sprite = _sprites[_spriteIndex].Spring;
    }


    public void MakeInSpring()
    {
        _spriteRenderer.sprite = _sprites[_spriteIndex].Spring;
    }

    public void MakeItWinter()
    {
        _spriteRenderer.sprite = _sprites[_spriteIndex].Winter;
    }
}
