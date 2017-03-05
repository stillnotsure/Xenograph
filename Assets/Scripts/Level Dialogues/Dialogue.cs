using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialogue
{
    public int actor;   //Who is saying the line
    public float timeStart;   //The time after the previous comment this line should come
    public float duration;   //How long the line stays onscreen
    public string dialogue;     //What the actual text is
    public string representation;   //How it should be represented
    public bool marked; //If the player is marked down for not including this
}
