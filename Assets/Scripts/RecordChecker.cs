using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public string text;
}

public class RecordChecker : MonoBehaviour {

    private List<string> dialogueList;
    private string record = "";
    public float score;

    public int pointsPerLetter = 2;
    public int pointsDeductedPerExtraLetter = 1;
    public float maxPoints;

    public void ReceiveRecord(string record)
    {
        this.record += record + " ";
    }

    void Start()
    {
        dialogueList = new List<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            CheckRecords();
        }
    }
    //Two approaches:
    //  Every time dialogue is received, take the neccesary parts of it and add them to a string list
    //  Or add the dialogue itself to a list, and use the extra information for parsing later
    public void ReceiveDialogue(ActingDirection dialogue)
    {
        if (dialogue.marked)
        {
            /*
            if (dialogue.representation != "")
            {
                dialogueList.Add(dialogue.representation);
            } else
            */
            {
                dialogueList.Add(dialogue.dialogue);
            }
        }
    }

    public void CheckRecords()
    {
        foreach (string s in dialogueList)
        {
            maxPoints += s.Length * pointsPerLetter;
            int test = record.IndexOf(s.ToLower());
            if (test != -1) {
                score += (pointsPerLetter * s.Length);
                record.Remove(test, s.Length);
            }
        }
        Debug.Log(score / maxPoints);
    }

}
