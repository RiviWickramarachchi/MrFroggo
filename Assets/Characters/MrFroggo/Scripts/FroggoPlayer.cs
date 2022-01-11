using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggoPlayer : MonoBehaviour
{
    //Controls the timebar actions timer actions and win/loss GUI transitions and level actions 
    [SerializeField] private int maxTime = 60;
    [SerializeField] private float currentTimeVal;
    [SerializeField] private TimerBar tb;
    

    // Start is called before the first frame update
    void Start()
    {
        
        tb.setTime(currentTimeVal);
    }

    // Update is called once per frame
    void Update()
    {
        timerBarActions();
    }

    public void timerBarActions()
    {
         
        currentTimeVal -= 1 * Time.deltaTime;
        tb.setTime(currentTimeVal);

        if(currentTimeVal > 60)
        {
            //Win screen and continue to next level
        }
        if(tb.getTimeVal() == 0)
        {
            //Transition to game over screen
        }
        if(currentTimeVal < 10)
        {
            tb.setAnimations("warning");
        }
        
        



        /* Game Over Scene or Next level Scene transitions
        if (current_time <= 0)
        {
            status = EnemySceneStatus.Ended;

            if (PlayerManager.Instance.CurrentPlayer.GetComponent<GpsManager>().Distance > distanceFromEnemy)
            {
                print("success");
                status = EnemySceneStatus.SuccessfulEscape;
            }
            else
            {
                //print("fail");
                status = EnemySceneStatus.FailedEscape;
            }
            //print("ended");
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Fly")
        {
            print("Hit = Fly");
            currentTimeVal += 15.0f;
            tb.setAnimations("boost");
            //tb.boostTime("Fly",currentTimeVal);
            //relevant frog/ timebar and fly animations 
        }
    }
}
