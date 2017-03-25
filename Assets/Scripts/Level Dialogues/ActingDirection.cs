using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ActingDirection {
    public int actor;   //Who is performing the action
    public float timeStart;   //The time after the previous action this one should come

    public enum DirectionType
    {
        Dialogue,
        Animation,
        Position,
    };
    public enum Animation
    {
        StandUp,
        SitDown
    };
    public DirectionType directionType;
    public Animation animation;
    public string dialogue;
    public Vector3 targetPosition;
    public bool flipX;
    public bool marked;
}
