﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	void OnMouseDown()
    {
         SendMessageUpwards("PullLever");
    }
}
