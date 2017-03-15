using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grabs the next moment in the scene, sets a timer for moment.startDelay, then executes it
//Only works for one timer at a time
public class ScenePlayer : MonoBehaviour {

    public Scene scene;
    public int moment;
    private float timer;
    private Dialogue curDialogue;
    public List<Actor> actors;

    void Start()
    {
        GetNextMoment();
    }

    void Set(Dialogue dialogue)
    {
        timer = dialogue.timeStart;
        curDialogue = dialogue;
    }

    void Update()
    {
        if (timer <= 0)
        {
            RunMoment(curDialogue);
            if (moment < scene.moments.Count) {
                GetNextMoment();
            }
            else
            {
                moment = -1;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void GetNextMoment()
    {
        if (moment >= 0)
        {
            Set(scene.moments[moment]);
            moment++;
        }
    }


    void RunMoment(Dialogue dialogue)
    {
        if (moment >= 0)
        {
            Debug.Log(dialogue.dialogue);
            actors[dialogue.actor].SayLine(dialogue.dialogue);
            gameObject.GetComponent<RecordChecker>().ReceiveDialogue(dialogue);
        }
    }
}
