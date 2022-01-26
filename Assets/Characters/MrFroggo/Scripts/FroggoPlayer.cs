using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggoPlayer : MonoBehaviour
{
    //Controls the timebar actions timer actions and win/loss GUI transitions and level actions 
    [SerializeField] private int maxTime = 60;
    [SerializeField] private float currentTimeVal;
    [SerializeField] private TimerBar tb;
    private Animator anim;
    private int flyCount;
    

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        tb.setTime(currentTimeVal);
        flyCount = BugGenerator.Instance.FlyCount;
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
            flyCount--;
            if(flyCount == 0)
            {
                //Level Complete Message !!!
            }
        }

        if(collision.tag == "FireBug")
        {
            print("FireBugTongueTriggered");
            print(collision.gameObject.GetComponent<FireBug>().Timer);
            if(collision.gameObject.GetComponent<FireBug>().Timer < collision.gameObject.GetComponent<FireBug>().ColorTime)
            {
                anim.Play("froggo_stun");      
            }      
        }
    }
}
