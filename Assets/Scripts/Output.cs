using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MonoBehaviour {

    private Bar bar;
    string fullText;
    TextMesh textmesh;
    private Paper paper;

	// Use this for initialization
	void Start () {
        textmesh = gameObject.GetComponent<TextMesh>();
        paper = transform.parent.GetComponent<Paper>();
        bar = GameObject.Find("Typewriter-Bar").GetComponent<Bar>();
	}
	
    public void ReceiveInput(string input){
        if (paper.inFrontOfInkPoint)
        {
            fullText += input;
            textmesh.text += input;
        }
        //This should definitely not be handled here...
        bar.MoveBar(input);
    }

    public void SendRecord()
    {
        GameObject.Find("GameManager").GetComponent<RecordChecker>().ReceiveRecord(fullText);
    }
}
