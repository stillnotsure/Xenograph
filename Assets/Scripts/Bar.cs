﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    public int lettersPerRow = 26;
    public float distancePerLetter = -0.1f;
    public float distancePerSpace = -0.07f;
    //public float distanceWhenFull = 26;

	
	public void MoveBar(string key)
    {
        if (key == " ")
        {
            transform.Translate(distancePerSpace, 0, 0);
        }
        else
        {
            transform.Translate(distancePerLetter, 0, 0);
        }
        
    }
}