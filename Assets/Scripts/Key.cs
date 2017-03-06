using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private Animator animator;
    private KeyManager manager;
    public string inputKey;
    public string outputKey;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Input Device").GetComponent<KeyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(inputKey)){
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
        Output target = manager.GetTarget();
        Debug.Log(manager);
        Debug.Log(target);
        if (target != null) target.ReceiveInput(output);
    }

}
