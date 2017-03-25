using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private AudioManager am;
    private ScenePlayer scenePlayer;
    private UIManager uiManager;
    private RecordChecker recordChecker;
    private float timer;

    //Timers 
    private float preTrialDuration = 2f;
    private float postTrialDuration = 5f;

    public float score = 0;

    private Dictionary<States, float> stateTimers;

    public enum States
    {
        instructions,
        preTrial,
        trial,
        postTrial,
        recap
    }
    public States state;

    public void SetState(States state)
    {
        this.state = state;
        float tryTimer;
        if (stateTimers.TryGetValue(state, out tryTimer))
        {
            timer = tryTimer;
        }
        Debug.Log(timer);
        Debug.Log("Switched to " + this.state);
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad( this.gameObject );

        scenePlayer = gameObject.GetComponent<ScenePlayer>();

        stateTimers = new Dictionary<States, float>();
        stateTimers.Add(States.preTrial, preTrialDuration);
        stateTimers.Add(States.postTrial, postTrialDuration);
        SetState(States.instructions);
    }

    void Start()
    {
        am = AudioManager.GetInstance();
        uiManager = UIManager.GetInstance();
        recordChecker = gameObject.GetComponent<RecordChecker>();
    }
    void Update()
    {
        switch (state)
        {
            case (States.preTrial): PreTrial();
                break;
            case (States.postTrial): PostTrial();
                break;
            default: 
                break;
        }
    }

    public void CommenceTrial()
    {
        am.PlayBGMusic();
        scenePlayer.Begin();
    }

    public void PreTrial()
    {
        if (timer <= 0)
        {
            CommenceTrial();
            SetState(States.trial);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    //Need to set timer to postTrialDuration before this point
    public void PostTrial()
    {
        uiManager.FadeScreenToBlack(postTrialDuration);
        if (timer <= 0)
        {
            score = recordChecker.CheckRecords();
            Debug.Log(score);
            SceneManager.LoadScene("Recap");
            state = States.recap;
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
