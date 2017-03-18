using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grabs the next moment in the scene, sets a timer for moment.startDelay, then executes it
//Only works for one timer at a time
public class ScenePlayer : MonoBehaviour {

    public Scene scene;
    public ActingDirections directions;

    public int moment;
    public int direction;

    private float timer;
    private float directionTimer;
    private Dialogue curDialogue;
    private ActingDirection curDirection;
    public List<Actor> actors;

    void Start()
    {
        GetNextMoment();
        GetNextDirection();
    }

    void Set(Dialogue dialogue)
    {
        timer = dialogue.timeStart;
        curDialogue = dialogue;
    }

    void SetDirection(ActingDirection direction)
    {
        directionTimer = direction.timeStart;
        curDirection = direction;
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

        if (directionTimer <= 0)
        {
            RunDirection(curDirection);
            if (direction < directions.directions.Count)
            {
                GetNextDirection();
            }
            else
            {
                direction = -1;
            }
        }
        else
        {
            directionTimer -= Time.deltaTime;
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

    void GetNextDirection()
    {
        if (direction >= 0)
        {
            SetDirection(directions.directions[direction]); //TODO: Fuck this is awful
            direction++;
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

    //TODO - Combine this and moments
    void RunDirection(ActingDirection act)
    {
        if (direction >= 0)
        {
            actors[act.actor].PerformAction(act.direction);
        }
    }
}
