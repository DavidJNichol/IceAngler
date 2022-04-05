using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player SharedInstance { get { return sharedInstance; } } // singleton
    private static Player sharedInstance;

    public int Score { get { return score; } set { score = value; } }
    private int score;

    [SerializeField] private KeyCode fightFishKey;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject meter;
    private float maxMeterScaleY = 85;

    [HideInInspector] public bool timerIsActive;
    private Timer timer;
    private int timesReeled;
    private int targetTimesReeled = 5;
    private Fish fishOnTheLine;
    private Collider2D hookCollider;
    float meterLossAmount = .1f;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        timer = new Timer();

        hookCollider = transform.GetChild(2).transform.GetChild(1).GetComponent<Collider2D>();
    }

    void Update()
    {
        if(timerIsActive)
        {
            if (timesReeled < targetTimesReeled)
            {
                if (timer.timeRemaining == 0)
                {
                    if (timesReeled < targetTimesReeled)
                        LoseFish();
                    else
                        CatchFish();
                }

                if (Input.GetKeyDown(fightFishKey))
                {
                    float meterRemaining = maxMeterScaleY - meter.transform.localScale.y;
                    meter.transform.localScale = new Vector2(meter.transform.localScale.x, meter.transform.localScale.y + (meterRemaining/(targetTimesReeled-timesReeled+1)));
                    timesReeled++;
                    Debug.Log("Reeled " + timesReeled + " times. " + timer.timeRemaining + " time remaining.");
                }

                timer.RunTimer();
                MoveMeter();
            }
            else
            {
                CatchFish();
            }    
        }
    }

    public void FightFish(MarineObject marineObject)
    {
        if(!timerIsActive)
        {
            Fish fish = marineObject as Fish;

            if (fish)
            {
                timer.SetTimerDuration(5);
                targetTimesReeled = fish.Durability;
                timerIsActive = true;
                fishOnTheLine = fish;
                hookCollider.enabled = false;

                fishOnTheLine.gameObject.AddComponent<Rigidbody2D>();
            }
            else
            {
                Debug.Log("Obstacle");
            }

            Debug.Log("timer started! " + marineObject.name);
        }       
        else
        {
            marineObject.CanMove = true;
        }
    }

    private void CatchFish()
    {
        Debug.Log("Fish caught!: " + fishOnTheLine.name);
        timesReeled = 0; // qte button reset
        timerIsActive = false;
        fishOnTheLine.Deactivate(); // make fish disappear
        score++;
        scoreText.text = score.ToString(); // set UI
        hookCollider.enabled = true;
        fishOnTheLine.IsOnHook = false; // unties fish y pos to hook y pos

        Destroy(fishOnTheLine.GetComponent<Rigidbody2D>());

        meter.transform.localScale = new Vector2(meter.transform.localScale.x, 0); // reset meter    
    }

    public void LoseFish()
    {
        Debug.Log("Fish lost!: " + fishOnTheLine.name);
        timesReeled = 0;
        timerIsActive = false; // deactivate timer

        fishOnTheLine.CanMove = true; // resume fish movement
        fishOnTheLine.IsOnHook = false; // fish movement no longer tied to hook
        fishOnTheLine.CanCollideWithHook = false; // this fish gets immunity
        hookCollider.enabled = true;

        Destroy(fishOnTheLine.GetComponent<Rigidbody2D>());

        meter.transform.localScale = new Vector2(meter.transform.localScale.x, 0); // reset meter
    }

    private void MoveMeter()
    {
        if(meter.transform.localScale.y > 0)
        {
            meter.transform.localScale = new Vector2(meter.transform.localScale.x, meter.transform.localScale.y - meterLossAmount);
        }
    }
}
