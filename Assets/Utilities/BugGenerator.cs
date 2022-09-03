using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

//This class is used to generate various bugs depending on how the player plays the game
public class BugGenerator : Singleton<BugGenerator>
{
    [SerializeField] private int flyCount;
    [SerializeField] private int fireBugCount;
    [SerializeField] private GameObject flies;
    [SerializeField] private GameObject fireBugs;
    [SerializeField] private GameObject butterflies;
    [SerializeField] private GameObject goldFish;
    [SerializeField] private GameObject fairy;
    private float minX = 0f;
    private float maxX = 50f;
    private float minY = 0f;
    private float maxY = 2.3f;
    private bool gameIsNotOverYet;

    public enum TimeStates{
        TimeQuiteFull, //time > 20 <40
        TimeAlmostFull, //time > 40 :Increase the no of firebugs and other enemy types if the player is doing well
        TimeAlmostFinished, //time < 10: Help the player by sending normal flies and goldfish to the game
    }
    public enum PlayerStates {
        Normal,
        PlayerNerfed, //Add a fairybug for the player to debuff
        PlayerStunned //Help the player by adding more flies
    }

    public PlayerStates playerStates;
    public TimeStates timeStates;
    public int FlyCount { get => flyCount; }
    public static event Action<GameObject> SendFrogObjectToFairy;

    void OnEnable(){
        FroggoPlayer.UpdateBugGeneratorPlayer += ChangePlayerStates;
        FroggoPlayer.UpdateBugGeneratorTime += ChangeTimeStates;
        FroggoPlayer.TimeIsOver += GameOverState;
        Fairy.GetFroggoObj += SendFroggoToFairy;
    }
    private void Awake()
    {
        Assert.IsNotNull(flies);
        Assert.IsNotNull(fireBugs);
    }

    private float GenerateXPos()
    {
        float randomXPos = UnityEngine.Random.Range(minX, maxX);
        return randomXPos;
    }

    private float GenerateYPos()
    {
        float randomYPos = UnityEngine.Random.Range(minY, maxY);
        return randomYPos;
    }

    //Use coroutines to instantiate normal flies depending on the player score as the game progresses
    private void InstantiateFlies()
    {
        float x = GenerateXPos();
        float y = GenerateYPos();
        //Instantiate(availableGems[index], new Vector3(x, y, z), Quaternion.identity);
        Instantiate(flies, new Vector3(x, y, 0),Quaternion.identity);
        print("fly instantiated");
    }

    //Use coroutines to instantiate firebugs depending on the player score as the game progresses
    private void InstantiateFireBugs()
    {
        float x = GenerateXPos();
        float y = GenerateYPos();
        Instantiate(fireBugs, new Vector3(x, y, 0), Quaternion.identity);
        print("fireBug instantiated");
    }
    private void InstantiateButterfly() {
        float x = GenerateXPos();
        float y = GenerateYPos();
        Instantiate(butterflies, new Vector3(x, y, 0), Quaternion.identity);
        print("butterfly instantiated");
    }

    private void InstantiateFairy() {
        float y = GenerateYPos();
        Instantiate(fairy, new Vector3(70f,y, 0), Quaternion.identity); //better not to hardcode position values
        print("Fairy instantiated");
    }

    private void InstantiateGoldFish() {
        float x = GenerateXPos();
        Instantiate(goldFish, new Vector3(x, -5.36f, 0), Quaternion.identity);
        print("Goldfish instantiated");
    }

    //A method to instantiate a fairy when the player gets a nerf
    //Methods to spawn other bugs as the game progresses
    IEnumerator InGameBugGenerator(bool isGameState) {
        while(true) {
            switch(timeStates) {
            case TimeStates.TimeAlmostFull:
                yield return new WaitForSeconds(2f);
                InstantiateFireBugs();
                break;
            case TimeStates.TimeQuiteFull:
                yield return new WaitForSeconds(3f);
                InstantiateButterfly();
                break;
            case TimeStates.TimeAlmostFinished:
                yield return new WaitForSeconds(2f);
                InstantiateFlies();
                break;
            default:
                yield return new WaitForSeconds(0.1f);
                break;
            }

            switch(playerStates) {
            case PlayerStates.PlayerStunned:
                yield return new WaitForSeconds(5f);
                InstantiateGoldFish();
                playerStates = PlayerStates.Normal;
                break;
            case PlayerStates.PlayerNerfed:
                InstantiateFairy();
                yield return new WaitForSeconds(13f);
                break;
            }
        }
        //Debug.Log("game over");
    }

    public void ChangePlayerStates(int x){
        switch(x) {
            case 4:
                playerStates = PlayerStates.PlayerStunned;
                break;
            case 5:
                playerStates = PlayerStates.PlayerNerfed;
                break;
            case 6:
                playerStates = PlayerStates.Normal;
                break;
        }
    }

    public void ChangeTimeStates(int y) {
        switch(y) {
            case 1:
                timeStates = TimeStates.TimeAlmostFull;
                break;
            case 2:
                timeStates = TimeStates.TimeQuiteFull;
                break;
            case 3:
                timeStates = TimeStates.TimeAlmostFinished;
                break;
        }
    }

    public void GameOverState(bool isGameOver){
        gameIsNotOverYet = isGameOver;
    }
    void Update() {
        Debug.Log(playerStates);
    }
    void Start()
    {
        gameIsNotOverYet = true;
        playerStates = PlayerStates.Normal;
        for (int i = 0; i < flyCount; i++)
        {
            InstantiateFlies();
        }
        for(int i=0; i< fireBugCount; i++)
        {
            InstantiateFireBugs();
        }
        StartCoroutine(InGameBugGenerator(gameIsNotOverYet));
    }

    //This method is invoked when a fairy is instantiated
    public void SendFroggoToFairy() {
        //This Event triggers the GetFrogObject method attached on fairy objects and sends the game object attached to this script
        SendFrogObjectToFairy?.Invoke(this.gameObject);
    }
    void OnDisable(){
        FroggoPlayer.UpdateBugGeneratorPlayer -= ChangePlayerStates;
        FroggoPlayer.UpdateBugGeneratorTime -= ChangeTimeStates;
        FroggoPlayer.TimeIsOver -= GameOverState;
        Fairy.GetFroggoObj -= SendFroggoToFairy;
    }
}
