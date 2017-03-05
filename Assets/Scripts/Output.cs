using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MonoBehaviour {

    public Bar bar;
    string fullText;
    TextMesh textmesh;
    private Paper paper;

	// Use this for initialization
	void Start () {
        textmesh = gameObject.GetComponent<TextMesh>();
        paper = GameObject.Find("Paper").GetComponent<Paper>();
	}
	
    public void ReceiveInput(string input){
        if (paper.inFrontOfInkPoint)
        {
            fullText += input;
            textmesh.text += input;
        }
        bar.MoveBar(input);
    }
}
