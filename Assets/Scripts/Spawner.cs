using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _creation;
    [SerializeField] private int _amount;
    [SerializeField] private Terrain _terrain;

    private Vector2 _minXZ;
    private Vector2 _maxXZ;
    private float _spawnFrequency = 5;

    private void Start()
    {
        SpawnResources(_amount);
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        bool isSpawning = true;
        WaitForSeconds spawnDelay;
        int resourcesAmount = 1;

        while(isSpawning)
        {
            spawnDelay = new WaitForSeconds(_spawnFrequency);
            SpawnResources(resourcesAmount);

            yield return spawnDelay;
        }
    }

    private void SpawnResources(int amount)
    {
        float minX = _terrain.GetPosition().x;
        float minZ = _terrain.GetPosition().z;
        float maxX = _terrain.terrainData.size.x + minX;
        float maxZ = _terrain.terrainData.size.z + minZ;
        _minXZ = new Vector2(minX, minZ);
        _maxXZ = new Vector2(maxX, maxZ);

        for (int i = 0; i < amount; i++)
            Instantiate(_creation, GetRandomPointOnTerrain(_minXZ, _maxXZ), Quaternion.identity, _terrain.transform);
    }

    private Vector3 GetRandomPointOnTerrain(Vector2 minXZ, Vector2 maxXZ)
    {
        float resourceHeight = 1;
        Vector3 point = new Vector3(Random.Range(minXZ.x, maxXZ.x),
                                    0,
                                    Random.Range(minXZ.y, maxXZ.y));

        point.y = _terrain.SampleHeight(point) + _terrain.GetPosition().y + resourceHeight;

        return point;
    }
}
