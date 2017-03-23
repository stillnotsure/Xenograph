using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Scene : ScriptableObject
{
    public List<ActingDirection> directions;
    public AudioClip bgMusic;
}
