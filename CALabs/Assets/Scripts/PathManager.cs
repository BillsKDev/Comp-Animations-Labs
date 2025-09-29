using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector] [SerializeField] public List<Waypoint> _path;

    public List<GameObject> _prefabPoints;
    public GameObject _prefab;
    int _currentPointIndex;

    public void Start()
    {
        _prefabPoints = new List<GameObject>();
        
        // create prefab colliders for the path locations
        foreach (Waypoint p in _path)
        {
            GameObject go = Instantiate(_prefab);
            go.transform.position = p._position;
            _prefabPoints.Add(go);
        }
    } 
    public void Update()  
    {
        // update all the prefabs to the waypoint locations
        for (int i = 0; i < _path.Count; i++)
        {
            Waypoint p = _path[i];
            GameObject g = _prefabPoints[i];
            g.transform.position = p._position; 
        }
    }
    public Waypoint GetNextTarget()
    {
        int nextPointIndex = (_currentPointIndex + 1) % (_path.Count);
        _currentPointIndex = nextPointIndex;
        return _path[nextPointIndex];
    }


    public List<Waypoint> GetPath()
    {
        if (_path == null) _path = new List<Waypoint>();
        return _path;
    }

    public void CreateAddPoint()
    {
        Waypoint go = new Waypoint();
        _path.Add(go);  
    }
}