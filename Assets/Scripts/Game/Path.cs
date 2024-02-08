using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private WayPoint[] _waypoints;

    public WayPoint GetPathStart()
    {
        return _waypoints[0];
    }
    public WayPoint GetPathEnd()
    {
        return _waypoints[^1];
    }
    public WayPoint GetNextWaypoint(WayPoint currentWaypoint)
    {
        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (_waypoints[i] == currentWaypoint)
            {
                return _waypoints[i + 1];
            }
        }
        return null;
    }
}
