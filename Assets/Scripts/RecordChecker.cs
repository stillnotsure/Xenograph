using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        if (record != null || record != "")

        {
            var sb = new StringBuilder();
            char lastChar = ' ';
            if (record != "")
            {
                foreach (char c in record)
                {
                    if (!(char.IsWhiteSpace(c) && char.IsWhiteSpace(lastChar)))
                    {
                        if (!char.IsPunctuation(c))
                        {
                            sb.Append(c);
                            lastChar = c;
                        }
                    }
                }
            }

            this.record += sb.ToString() + " ";

        }

    }

    void Start()
    {
        dialogueList = new List<string>();
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
                string text = dialogue.dialogue;
                var sb = new StringBuilder();

                foreach (char c in text)
                {
                    if (!char.IsPunctuation(c))
                        sb.Append(c);
                }
                dialogueList.Add(sb.ToString());

            }
        }
    }

    public float CheckRecords()
    {
        foreach (string s in dialogueList)
        {
            maxPoints += 1;
            int test = record.IndexOf(s.ToLower());

            if (test != -1) {
                score += 1;
                record.Remove(test, s.Length);
            }

        }
        if (score == 0 || maxPoints == 0)
        {
            return 0;
        } else
        {
            return (score / maxPoints) * 100;
        }

    }

}
