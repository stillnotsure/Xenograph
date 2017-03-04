using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonTrigger : MonoBehaviour {

    private Animator animator;

    void Start()
    {
        animator = GameObject.Find("RibbonGraphic").GetComponent<Animator>();
    }

    public void KeyDown(bool keyIsDown)
    {
        animator.SetBool("Pressed", keyIsDown);
    }
}
