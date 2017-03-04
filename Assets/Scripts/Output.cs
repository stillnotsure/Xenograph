using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MonoBehaviour {

    string fullText;
    TextMesh textmesh;

	// Use this for initialization
	void Start () {
        textmesh = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveInput(string input){
        fullText += input;
        textmesh.text += input;
    }
}
