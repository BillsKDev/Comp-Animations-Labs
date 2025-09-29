using UnityEngine;

[System.Serializable]
public class Waypoint 
{
    [SerializeField] public Vector3 _position;

    public void SetPosition(Vector3 newPosition) => _position = newPosition;

    public Vector3 GetPosition() => _position;

    public Waypoint() => _position = Vector3.zero;
}
