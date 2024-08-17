using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Mapdrawer : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] private TileBase _ground;
    [SerializeField] private TileBase _river;
    [SerializeField] private TileBase _road;
    [SerializeField] private Tilemap _tilemap;

    [Header("Map decorations")]
    [SerializeField] private List<Sprite> _spriteDecorations;
    [SerializeField] private GameObject _prefabDecoration;
    [SerializeField] [Range(0,1)] private float _decorationProbability = 0.1f;
    [SerializeField] private LayerMask _decorationLayer;

    [Header("House")]
    [SerializeField] private float _houseAnimationDuration = 0.3f;
    [SerializeField] private AudioClip _houseAnimSound;


    private AudioSource _audioSource;

    public void Draw(CellType[,] logicMap)
    {
        for(int i = 0; i < logicMap.GetLength(0); i++)
        for (int j = 0; j < logicMap.GetLength(1); j++)
        {
            var tile = logicMap[i, j] switch
            {
                CellType.River => _river,
                CellType.Road => _road,
                _ => _ground
            };

            if (logicMap[i, j] == CellType.Empty)
                SpawnDecorations(i,j);

            _tilemap.SetTile(new Vector3Int(i,j, 0), tile);

            Debug.Log($"new tile at ({i},{j})");

        }
    }

    public static (float,float) ConvertCoord(float x, float y)
    {
        var angle = Mathf.PI / 4;

        var xTransformed = (x * Mathf.Cos(angle) - y * Mathf.Sin(angle))* 0.707f;
        var yTransformed = (x * Mathf.Sin(angle) + y * Mathf.Cos(angle))*0.353f + 0.5f;
        return (xTransformed, yTransformed);
    }

    private void SpawnDecorations(int x, int y)
    {
        if(Random.value > _decorationProbability)
            return;

        var xn = x + (Random.value-0.5f)/10f;
        var yn = y +(Random.value-0.5f)/10f;

        var (xTransformed, yTransformed) = ConvertCoord(xn, yn);

        var go =Instantiate(_prefabDecoration, new Vector3(xTransformed,yTransformed,0), Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = _spriteDecorations[Random.Range(0, _spriteDecorations.Count)];
    }

    private void DeleteDecorationAt(int x, int y)
    {
        var (xn, yn) = ConvertCoord(x, y);
        foreach (var dec in Physics2D.OverlapCircleAll(new Vector2(xn, yn), 1, _decorationLayer))
        {
            Destroy(dec.gameObject);
        }
    }

    private IEnumerator SpawnHouseAnimation(Transform house, float endY)
    {
        float startTime = Time.time;
        float curTime = startTime;
        float startPos = house.transform.position.y;
        do
        {
            curTime = Time.time;
            var pos = house.position;
            pos.y = Mathf.Max((curTime - startTime) / _houseAnimationDuration * (endY - startPos) + startPos, endY);
            house.position = pos;
            yield return null;
        } while (curTime - startTime <= _houseAnimationDuration);
        _audioSource.Stop();
        _audioSource.clip = _houseAnimSound;
        _audioSource.Play();
    }

    public void DrawHouse(GameObject housePrefab, int x, int y)
    {
            var (xn, yn) = ConvertCoord(x, y);
            DeleteDecorationAt(x,y);
            var house = Instantiate(housePrefab, new Vector2(xn, yn+15), Quaternion.identity);
            StartCoroutine(SpawnHouseAnimation(house.transform, yn));
    }

    public void DrawRoad(ICollection<Vector2Int> roadPoints)
    {
        foreach (var point in roadPoints)
        {
            _tilemap.SetTile((Vector3Int)point, _road);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}



