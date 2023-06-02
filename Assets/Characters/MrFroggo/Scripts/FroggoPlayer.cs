using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FroggoPlayer : MonoBehaviour
{

    //Froggo Sounds
    [SerializeField] private AudioSource croakSound;
    [SerializeField] private AudioSource spiderDmgSound;
    [SerializeField] private AudioSource bugPopSound;
    [SerializeField] private AudioSource fairyDustSound;

    //Device Vibration Settings
    [SerializeField] private int vibrateTime = 100;
    [SerializeField] private int vibrationAmplitude = 40;

    //Manages the UI actions such as  timebar actions timer actions and win/loss GUI transitions and level actions
    //Player scores etc
    [SerializeField] private int maxTime = 60;
    [SerializeField] private float startingTimeVal;
    [SerializeField] private TimerBar tb;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator bugCoinAnim;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bugCoinScoreText;
    [SerializeField] private TMP_Text boostValue;

    //Game over values
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text gameOverHighScoreText;
    [SerializeField] private TMP_Text gameOverBugsCollectedText;
    [SerializeField] private TMP_Text gameOverTotalBugsCollectedText;
    [SerializeField] private GameObject gameOverPanel;

    //Pause Button Sprites
    [SerializeField] private Sprite[] exitButtonSprites;
    [SerializeField] private Sprite[] soundButtonSprites;
    [SerializeField] private Image exitBtnImage;
    [SerializeField] private Image soundBtnImg;

    //Bug Scores
    [SerializeField] private int beeScore = 1;
    [SerializeField] private int ladyBugScore = 2;
    [SerializeField] private int fishScore = 3;
    [SerializeField] private int flyScore = 4;
    [SerializeField] private int butterflyScore = 5;
    [SerializeField] private int fireBugScore = 6;
    [SerializeField] private int fireBugPenalty = -6;
    [SerializeField] private int spiderScore = 7;

    //Private Variables Non-Serialized
    private float butterflyEffectTime = 10f;
    private float timeOfEffect;
    private float currentTimeVal;
    private int playerScore;
    private int bugScore;
    private int totalBugCoins;
    private float fogValue;
    private bool isMuted; //to mute/unmute game sound
    private IEnumerator froggoStunRoutine;

    //Public Variables
    public enum FrogEffects {
        Normal,
        FireBugEffect,
        FireBugEffectStarted,
        ButterflyEffect,
        BeeEffect,
        LadyBugEffect
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
    public static event Action<float> OnFlyCatch;
    public static event Action OnFairyCatch;

    void OnEnable() {
        FrogActions.FrogCollision += CollidedWithFroggo;
        Spider.FroggoCollided += CollisionsWithSpider;
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

    IEnumerator FroggoStunEffect() {
        while(true) {
            croakSound.Play();
            anim.Play("froggo_stun");
            yield return new WaitForSeconds(5f);
        }

    }
    private void FroggoEffects() {
        if (frogEffects == FrogEffects.Normal) {
            anim.SetBool("isDizzy",false);
            anim.speed = 1f;
        }
        if (frogEffects== FrogEffects.ButterflyEffect){
            anim.SetBool("isDizzy",true);
            anim.speed = 0.5f;
        }
        if (frogEffects == FrogEffects.FireBugEffect) {
            StartCoroutine(froggoStunRoutine);
            frogEffects = FrogEffects.FireBugEffectStarted;
        }

        if(currentTimeVal <= timeOfEffect - butterflyEffectTime) {
            frogEffects = FrogEffects.Normal;
            UpdateBugGeneratorPlayer?.Invoke(6);
        }
    }

    public int GetFrogEffect() {
        if(frogEffects == FrogEffects.Normal) {
            return 1;
        }
        else if (frogEffects == FrogEffects.BeeEffect) {
            return 2;
        }
        else if(frogEffects == FrogEffects.LadyBugEffect) {
            return 3;
        }
        else {
            return 4;
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
        else if((playerScore > 50) && (playerScore < 75)){
            UpdateBugGeneratorScoreState?.Invoke(8);
        }
        else if((playerScore > 75) && (playerScore < 100)) {
            UpdateBugGeneratorScoreState?.Invoke(9);
        }
        else if(playerScore > 100) {
            UpdateBugGeneratorScoreState?.Invoke(10);
        }
    }

    private void UpdateScore(int score) {
        //do all the bug coin animations here
        string calcSymbol = "";
        playerScore += score;
        if(playerScore < 0) {
            playerScore = 0;
        }
        //check if the score is + or -
        if(score >= 0) {
            calcSymbol = "+";
        }
        else {
            calcSymbol = "-";
        }
        //update bugscore properties
        bugScore++;
        bugCoinScoreText.text = bugScore.ToString("00");
        bugCoinAnim.Play("BugCoin");
        //update score for bug(s) #boost value
        boostValue.text = calcSymbol+score.ToString();
        DisplayBoostValue();
        //update total player score
        scoreText.text = "SCORE:"+playerScore.ToString("00");
        Invoke("HideBoostValue",2f);
    }

    private void DisplayBoostValue() {
        boostValue.gameObject.SetActive(true);
    }
    private void HideBoostValue() {
        boostValue.gameObject.SetActive(false);
    }

    public void InitializeValues() {
        gameOverPanel.SetActive(false);
        isMuted = false;
        frogEffects = FrogEffects.Normal;
        currentTimeVal = startingTimeVal;
        playerScore = 0;
        bugScore = 0;
        tb.setTime(currentTimeVal);
        gameState = GameStates.Game;
        froggoStunRoutine = FroggoStunEffect();
        totalBugCoins = ResourceLoader.Instance.GetBugCoinValue();
    }

    private void CollidedWithFroggo(Collider2D collision)
    {
        DeviceVibrate.VibrateWithAmp(vibrateTime, vibrationAmplitude); //vibrates device on collision
        if (collision.CompareTag("Fly")) {
            if(collision.gameObject.GetComponent<FlyMovements>().GetLastCollidedObject().tag == "TongueCol")
            {
                bugPopSound.Play();
                UpdateScore(flyScore);
                AdjustTime(12.0f);
                tb.setAnimations("boost");
                OnFlyCatch?.Invoke(0.1f);
            }
        }
        else if (collision.CompareTag("LadyBug")) {
            if(collision.gameObject.GetComponent<FlyMovements>().GetLastCollidedObject().tag == "TongueCol")
            {
                bugPopSound.Play();
                UpdateScore(ladyBugScore);
                AdjustTime(7.0f);
                tb.setAnimations("boost");
                frogEffects = FrogEffects.LadyBugEffect;
                UpdateBugGeneratorPlayer?.Invoke(5);

            }
        }
        else if(collision.CompareTag("FireBug")) {
            if(collision.gameObject.GetComponent<FireBug>().GetLastCollidedObject().tag == "TongueCol")
            {
                bugPopSound.Play();
                if(collision.gameObject.GetComponent<FireBug>().Timer < collision.gameObject.GetComponent<FireBug>().ColorTime)
                {
                    UpdateScore(fireBugPenalty);
                    frogEffects = FrogEffects.FireBugEffect;
                    UpdateBugGeneratorPlayer?.Invoke(4);
                }
                else {
                    UpdateScore(fireBugScore);
                    AdjustTime(18.0f);
                }
            }
        }
        else if (collision.CompareTag("Fairy")) {
            if(collision.transform.parent.gameObject.GetComponent<Fairy>().GetLastCollidedObject().tag == "TongueCol")
            {
                fairyDustSound.Play();
                anim.Play("froggo_fairyDust");
                if(frogEffects == FrogEffects.FireBugEffectStarted) {
                    StopCoroutine(froggoStunRoutine);
                }
                frogEffects = FrogEffects.Normal;
                UpdateBugGeneratorPlayer?.Invoke(6);
                OnFairyCatch?.Invoke();
            }

        }
        else if(collision.CompareTag("GoldFish")){
            bugPopSound.Play();
            UpdateScore(fishScore);
            AdjustTime(8.0f);
            tb.setAnimations("boost");
        }
        else if(collision.CompareTag("Butterfly")) {
            if(collision.gameObject.GetComponent<Butterfly>().GetLastCollidedObject().tag == "TongueCol")
            {
                bugPopSound.Play();
                UpdateScore(butterflyScore);
                AdjustTime(15.0f);
                tb.setAnimations("boost");
                timeOfEffect = currentTimeVal;
                frogEffects = FrogEffects.ButterflyEffect;
                UpdateBugGeneratorPlayer?.Invoke(5);
            }
        }
        else if(collision.CompareTag("Bee")) {
            if(collision.gameObject.GetComponent<Bee>().GetLastCollidedObject().tag == "TongueCol")
            {
                bugPopSound.Play();
                UpdateScore(beeScore);
                AdjustTime(5.0f);
                tb.setAnimations("boost");
                frogEffects = FrogEffects.BeeEffect;
                UpdateBugGeneratorPlayer?.Invoke(5);
            }
        }
    }

    private void CollisionsWithSpider(Collider2D collision) {
        DeviceVibrate.VibrateWithAmp(vibrateTime, vibrationAmplitude); //vibrates device on collision
        Debug.Log("Spider Collision actions are taken here");
        if(collision.CompareTag("TongueCol")) {
            bugPopSound.Play();
            UpdateScore(spiderScore);
            AdjustTime(20.0f);
            tb.setAnimations("boost");
        }

        if(collision.CompareTag("FrogBody")) {
            AdjustTime(-20.0f);
            spiderDmgSound.Play();
            anim.Play("froggo_bleed");
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
        //update bugCoin details here  --> total bug coins available , no of bugCoins Collected
        gameState = GameStates.End;
        TimeIsOver?.Invoke();
        DestroyOnEndState?.Invoke();
        gameOverPanel.SetActive(true);
        gameOverPanel.gameObject.transform.Find("ExitSession").gameObject.SetActive(false);
        gameOverPanel.gameObject.transform.Find("Board").gameObject.SetActive(true);
        gameOverScoreText.text = playerScore.ToString("00");
        gameOverBugsCollectedText.text = bugScore.ToString("00");
        ResourceLoader.Instance.DisplayHighScore(playerScore, gameOverHighScoreText);
        ResourceLoader.Instance.UpdateBugCoinAmount(totalBugCoins,bugScore);
        ResourceLoader.Instance.DisplayTotalBugCoins(gameOverTotalBugsCollectedText);
        Time.timeScale = 0;
    }

    public void PauseGame() {
        ChangeExitBtnSprite();
        gameOverPanel.SetActive(true);
        gameOverPanel.gameObject.transform.Find("Board").gameObject.SetActive(false);
        gameOverPanel.gameObject.transform.Find("ExitSession").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        ChangeExitBtnSprite();
        gameOverPanel.gameObject.transform.Find("Board").gameObject.SetActive(false);
        gameOverPanel.gameObject.transform.Find("ExitSession").gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameSound() {
        ChangeSoundBtnSprite();
        isMuted = !isMuted;
        MuteAndUnmute(isMuted);
    }

    private void MuteAndUnmute(bool val) {
        if(val == true) {
            AudioListener.volume = 0;
        }
        else
            AudioListener.volume = 1;
    }

    private void ChangeSoundBtnSprite() {
        if(soundBtnImg.sprite == soundButtonSprites[0]) {
            soundBtnImg.sprite = soundButtonSprites[1];
            return;
        }
        soundBtnImg.sprite = soundButtonSprites[0];
    }

    private void ChangeExitBtnSprite() {
        if(exitBtnImage.sprite == exitButtonSprites[0]) {
            exitBtnImage.sprite = exitButtonSprites[1];
            return;
        }
        exitBtnImage.sprite = exitButtonSprites[0];
    }

    void OnDisable() {
         FrogActions.FrogCollision -= CollidedWithFroggo;
         Spider.FroggoCollided -= CollisionsWithSpider;
    }
}
