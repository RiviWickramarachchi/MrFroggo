using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FroggoPlayer : MonoBehaviour
{
    //Manages the UI actions such as  timebar actions timer actions and win/loss GUI transitions and level actions
    //Player scores etc
    [SerializeField] private int maxTime = 60;
    [SerializeField] private float startingTimeVal;
    [SerializeField] private TimerBar tb;
    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    private float butterflyEffectTime = 10f;
    private float timeOfEffect;
    private float currentTimeVal;
    private int playerScore;
    private int flyScore = 5;
    private int fireBugScore = 5;
    private int fireBugPenalty = -2;
    private int butterflyScore = 8;
    private int fishScore = 10;


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
    public static event Action<int> UpdateBugGeneratorPlayer;
    public static event Action<int> UpdateBugGeneratorTime;
    public static event Action<int> UpdateBugGeneratorScoreState;
    public static event Action DestroyOnEndState;
    public static event Action TimeIsOver;

    void OnEnable() {
        FrogActions.FrogCollision += CollidedWithFroggo;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeValues();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameState == GameStates.Game) {
            TimerBarActions();
            FroggoEffects();
            PlayerScoreChecker();
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
            GameOverState();
        }

        if(currentTimeVal < 10)
        {
            tb.setAnimations("warning");
            UpdateBugGeneratorTime?.Invoke(3);
        }
    }

    public void PlayerScoreChecker() {
        if((playerScore > 25) && (playerScore < 50)) {
            UpdateBugGeneratorScoreState?.Invoke(7);
        }
        else if(playerScore > 50){
            UpdateBugGeneratorScoreState?.Invoke(8);
        }
    }

    private void UpdateScore(int score) {
        playerScore += score;
        if(playerScore < 0) {
            playerScore = 0;
        }
        scoreText.text = playerScore.ToString("00");
    }

    public void InitializeValues() {
        gameOverPanel.SetActive(false);
        frogEffects = FrogEffects.Normal;
        currentTimeVal = startingTimeVal;
        playerScore = 0;
        tb.setTime(currentTimeVal);
        gameState = GameStates.Game;
    }

    private void CollidedWithFroggo(Collider2D collision)
    {
        if (collision.tag == "Fly") {
            print("Hit = Fly");
            UpdateScore(flyScore);
            AdjustTime(10.0f);
            tb.setAnimations("boost");
        }

        if(collision.tag == "FireBug") {
            print("FireBugTongueTriggered");
            if(collision.gameObject.GetComponent<FireBug>().Timer < collision.gameObject.GetComponent<FireBug>().ColorTime)
            {
                UpdateScore(fireBugPenalty);
                UpdateBugGeneratorPlayer?.Invoke(4);
                anim.Play("froggo_stun");
            }
            else {
                UpdateScore(fireBugScore);
                AdjustTime(15.0f);
            }
        }

        if (collision.tag == "Fairy") {
            print("FairyTongueTriggered");
            anim.Play("froggo_fairyDust");
            frogEffects = FrogEffects.Normal;
            UpdateBugGeneratorPlayer?.Invoke(6);
        }

        if(collision.tag == "GoldFish"){
            UpdateScore(fishScore);
            AdjustTime(25.0f);
            tb.setAnimations("boost");
        }

        if(collision.tag == "Butterfly") {
            UpdateScore(butterflyScore);
            AdjustTime(20.0f);
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

    private void GameOverState() {
        //change game state and go to game over UI
        gameState = GameStates.End;
        TimeIsOver?.Invoke();
        DestroyOnEndState?.Invoke();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void OnDisable() {
         FrogActions.FrogCollision -= CollidedWithFroggo;
    }
}
