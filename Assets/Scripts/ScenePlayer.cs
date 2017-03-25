using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Grabs the next moment in the scene, sets a timer for moment.startDelay, then executes it
//Only works for one timer at a time
public class ScenePlayer : MonoBehaviour {

    private bool playing;
    public Scene scene;
    public int directionIndex;
    private ActingDirection curDirection;
    private float timer;
    public List<Actor> actors;

    public void Begin()
    {
        playing = true;
        GetNextDirection();
    }

    void SetDirection(ActingDirection direction)
    {
        timer = direction.timeStart;
        curDirection = direction;
    }

    void Update()
    {
        if (playing)
        {
            if (timer <= 0)
            {
                RunDirection(curDirection);
                if (directionIndex < scene.directions.Count)
                {
                    GetNextDirection();
                }
                else
                {
                    directionIndex = -1;
                    GameManager.GetInstance().SetState(GameManager.States.postTrial);
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

    }

    void GetNextDirection()
    {
        if (directionIndex >= 0)
        {
            SetDirection(scene.directions[directionIndex]);
            directionIndex++;
        }
    }

    void RunDirection(ActingDirection direction)
    {
        if (directionIndex >= 0)
        {
            if  (direction.directionType == ActingDirection.DirectionType.Dialogue)
            {
                actors[direction.actor].SayLine(direction.dialogue);
                gameObject.GetComponent<RecordChecker>().ReceiveDialogue(direction);
            }
            else if (direction.directionType == ActingDirection.DirectionType.Animation)
            {
                actors[direction.actor].PerformAnimation(direction.animation);
            }
            else if (direction.directionType == ActingDirection.DirectionType.Position)
            {
                StartCoroutine(actors[direction.actor].Teleport(direction.targetPosition, direction.flipX));
            }
        }
    }

}
