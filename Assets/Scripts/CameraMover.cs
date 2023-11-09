using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private float _maxX;
    private float _maxZ;
    private float _minX;
    private float _minZ;
    private Dictionary<KeyCode, Vector3> _cameraControls;
    private Vector3 _currentPosition;    

    private void Awake()
    {
        Terrain terrain = FindObjectOfType<Terrain>();
        _minX = terrain.GetPosition().x;
        _minZ = terrain.GetPosition().z;
        _maxX = terrain.terrainData.size.x + _minX;
        _maxZ = terrain.terrainData.size.z + _minZ;
        _cameraControls = GetControlsDictionary();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _currentPosition = transform.position;

        foreach (var key in _cameraControls.Keys)
        {
            if (Input.GetKey(key))
                transform.Translate(_cameraControls[key]);
        }

        if (transform.position.x > _maxX || transform.position.z > _maxZ || transform.position.x < _minX || transform.position.z < _minZ)
            transform.position = _currentPosition;
    }

    private Dictionary<KeyCode, Vector3> GetControlsDictionary()
    {
        Dictionary<KeyCode, Vector3> dictionary = new Dictionary<KeyCode, Vector3>
        {
            { KeyCode.A, new Vector3(-1, 0, 0) },
            { KeyCode.D, new Vector3(1, 0, 0) },
            { KeyCode.W, new Vector3(0, 1, 0) },
            { KeyCode.S, new Vector3(0, -1, 0) }
        };

        return dictionary;
    }
}
