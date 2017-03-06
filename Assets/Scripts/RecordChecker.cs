using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public string text;
}

public class RecordChecker : MonoBehaviour {

    private List<string> dialogueList;
    private string record;
    public int score;

    public int pointsPerLetter = 2;
    public int pointsDeductedPerExtraLetter = 1;
    public int maxPoints;

    public void ReceiveRecord(string record)
    {
        this.record += record;
        Debug.Log(this.record);
    }

    void Start()
    {
        dialogueList = new List<string>();
        Debug.Log("test");
        Debug.Log(dialogueList.Count);
    }

    //Two approaches:
    //  Every time dialogue is received, take the neccesary parts of it and add them to a string list
    //  Or add the dialogue itself to a list, and use the extra information for parsing later
    public void ReceiveDialogue(Dialogue dialogue)
    {
        if (dialogue.marked)
        {
            if (dialogue.representation != "")
            {
                dialogueList.Add(dialogue.representation);
            } else
            {
                dialogueList.Add(dialogue.dialogue);
            }

            Debug.Log(dialogueList[dialogueList.Count-1]);
        }
    }

    public void CheckRecords()
    {
        foreach (string s in dialogueList)
        {
            maxPoints += s.Length * pointsPerLetter;
            int test = record.IndexOf(s);
            Debug.Log(test);
            if (test != -1) {
                score += (pointsPerLetter * s.Length);
                record.Remove(test, s.Length);
            }
        }
    }

}
