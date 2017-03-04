using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private Animator animator;
    private Output target;
    public string inputKey;
    public string outputKey;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Active Paper").GetComponent<Output>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(inputKey)){
            Debug.Log(outputKey);
            animator.SetBool("Pressed", true);
            SendMessage("KeyDown", true);
            SendOutput(outputKey);
        }
        else if (Input.GetKeyUp(inputKey))
        {
            animator.SetBool("Pressed", false);
            SendMessage("KeyDown", false);
        }
	}

    void SendOutput(string output){
        target.ReceiveInput(output);
    }
}
