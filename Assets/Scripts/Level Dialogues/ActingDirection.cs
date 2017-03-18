using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActingDirection {
    public int actor;   //Who is performing the action
    public float timeStart;   //The time after the previous action this one should come
   
    public enum DirectionType{
        StandUp,
        SitDown
    };
    public DirectionType direction;
}
