using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    private bool moving = false;
    public int lettersPerRow = 26;
    public float distancePerLetter = -0.1f;
    public float distancePerSpace = -0.07f;
    public float distancePerLine = -0.2f;
    //public float distanceWhenFull = 26;

	
	public void MoveBar(string key)
    {
        if (key == " ")
        {
            transform.Translate(distancePerSpace, 0, 0);
        }
        else
        {
            transform.Translate(distancePerLetter, 0, 0);
        }
        
    }

    public void CarriageReturn()
    {
        /*
        GameObject.Find("Paper").transform.Translate(0, distancePerLine, 0);
        */
        GameObject paper = GameObject.FindGameObjectWithTag("Active Paper");
        if (paper != null)
        {
            paper.transform.FindChild("TextInput").GetComponent<Output>().NewLine();
            paper.GetComponent<Paper>().MoveUp(distancePerLine);
        }

    }

    IEnumerator MoveRight()
    {
        if (!moving)
        { // do nothing if already moving
            moving = true; // signals "I'm moving, don't bother me!"
            float time = 1f / (1.42f - transform.position.x );
            Vector3 targetPos = new Vector3(1.42f, transform.position.y, transform.position.z);
            while (transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 8);
                yield return 0; // leave the routine and return here in the next frame
            }
            moving = false; // finished moving
            CarriageReturn();
        }
    }

    public void PullLever()
    {
        StartCoroutine(MoveRight());
    }
}
