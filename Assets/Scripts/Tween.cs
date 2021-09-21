using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target { get; }
    public Vector3 StartPos { get; }
    public Vector3 EndPos { get; }
    public float StartTime { get; }
    public float Duration { get; }

    public Tween(Transform target, Vector3 startpos, Vector3 endpos, float starttime, float duration)
    {
        Target = target;
        StartPos = startpos;
        EndPos = endpos;
        StartTime = starttime;
        Duration = duration;

    }
}
