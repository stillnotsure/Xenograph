using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private Animator animator;
    private KeyManager manager;
    private AudioManager am;
    public AudioClip keypressSound;
    public string inputKey;
    private KeyCode keyCode;
    public string outputKey;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Input Device").GetComponent<KeyManager>();
        keypressSound = (AudioClip)Resources.Load("Typewriter-Key");
        am = AudioManager.GetInstance();
        if (inputKey == "'")
        {
            keyCode = KeyCode.Quote;
        }
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(inputKey) || Input.GetKeyDown(keyCode)){
            animator.SetBool("Pressed", true);
            SendMessage("KeyDown", true);
            SendOutput(outputKey);
            am.PlayOnce(am.localAudioSource, keypressSound);
        }
        else if (Input.GetKeyUp(inputKey))
        {
            animator.SetBool("Pressed", false);
            SendMessage("KeyDown", false);
        }
	}

    void SendOutput(string output){
        Output target = manager.GetTarget();
        if (target != null) target.ReceiveInput(output);
    }

}
