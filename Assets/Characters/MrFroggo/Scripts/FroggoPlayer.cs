using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FroggoPlayer : MonoBehaviour
{
    //Manages the UI actions such as  timebar actions timer actions and win/loss GUI transitions and level actions
    //Player scores etc
    [SerializeField] private int maxTime = 60;
    [SerializeField] private float currentTimeVal;
    [SerializeField] private TimerBar tb;
    [SerializeField] private Animator anim;
    private float butterflyEffectTime = 10f;
    private float timeOfEffect;

    public enum FrogEffects {
        Normal,
        ButterflyEffect,
        BeeEffect
    };
    public enum GameStates {
        Begin,
        Game,
        End
    };
    public FrogEffects frogEffects;
    public GameStates gameState = GameStates.Begin;
    public bool isGameState;
    public static event Action<int> UpdateBugGeneratorPlayer;
    public static event Action<int> UpdateBugGeneratorTime;
    public static event Action<bool> TimeIsOver;

    void OnEnable() {
        FrogActions.FrogCollision += CollidedWithFroggo;
    }
    // Start is called before the first frame update
    void Start()
    {
        frogEffects = FrogEffects.Normal;
        tb.setTime(currentTimeVal);
        gameState = GameStates.Game;
        isGameState = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameState == GameStates.Game) {
            TimerBarActions();
            FroggoEffects();
        }

    }

    private void FroggoEffects() {
        if(frogEffects == FrogEffects.Normal) {
            anim.SetBool("isDizzy",false);
            anim.speed = 1f;
        }
        if(frogEffects== FrogEffects.ButterflyEffect){
            anim.SetBool("isDizzy",true);
            anim.speed = 0.5f;
        }

        if(currentTimeVal <= timeOfEffect - butterflyEffectTime){
            frogEffects = FrogEffects.Normal;
            UpdateBugGeneratorPlayer?.Invoke(6);
        }
    }

    public void TimerBarActions()
    {

        currentTimeVal -= 1 * Time.deltaTime;
        tb.setTime(currentTimeVal);

        if(tb.getTimeVal() <= 0)
        {
            //change game state and go to game over UI
            isGameState = false;
            gameState = GameStates.End;
            TimeIsOver?.Invoke(isGameState);
        }

        if(currentTimeVal > 40)
        {
            UpdateBugGeneratorTime?.Invoke(1);
        }
        else if((currentTimeVal < 40) &&(currentTimeVal > 20)){
            UpdateBugGeneratorTime?.Invoke(2);
        }
        else if(currentTimeVal < 10)
        {
            tb.setAnimations("warning");
            UpdateBugGeneratorTime?.Invoke(3);
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

    private void CollidedWithFroggo(Collider2D collision)
    {
        if (collision.tag == "Fly") {
            print("Hit = Fly");
            AdjustTime(15.0f);
            tb.setAnimations("boost");
        }

        if(collision.tag == "FireBug") {
            print("FireBugTongueTriggered");
            if(collision.gameObject.GetComponent<FireBug>().Timer < collision.gameObject.GetComponent<FireBug>().ColorTime)
            {
                UpdateBugGeneratorPlayer?.Invoke(4);
                anim.Play("froggo_stun");
            }
            else {
                AdjustTime(20.0f);
            }
        }

        if (collision.tag == "Fairy") {
            print("FairyTongueTriggered");
            anim.Play("froggo_fairyDust");
            frogEffects = FrogEffects.Normal;
            UpdateBugGeneratorPlayer?.Invoke(6);
        }

        if(collision.tag == "GoldFish"){
            AdjustTime(25.0f);
            tb.setAnimations("boost");
        }

        if(collision.tag == "Butterfly") {
            AdjustTime(15.0f);
            tb.setAnimations("boost");
            timeOfEffect = currentTimeVal;
            frogEffects = FrogEffects.ButterflyEffect;
            UpdateBugGeneratorPlayer?.Invoke(5);
        }
    }

    private void AdjustTime(float time) {
        if(currentTimeVal + time > maxTime){
            currentTimeVal = maxTime;
        }
        else {
            currentTimeVal += time;
        }
    }

    void OnDisable() {
         FrogActions.FrogCollision -= CollidedWithFroggo;
    }
}
