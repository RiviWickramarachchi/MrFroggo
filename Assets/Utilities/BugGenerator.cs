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
    [SerializeField] private int maxNoOfFliesAllowed = 5;
    [SerializeField] private int maxNoOfButterfliesAllowed = 2;
    [SerializeField] private int maxNoOfLadyBugsAllowed = 1;
    [SerializeField] private int maxNoOfFirebugsAllowed = 6;
    [SerializeField] private int maxNoOfBeesAllowed = 10;
    [SerializeField] private int maxNoOfSpidersAllowed = 4;
    [SerializeField] private GameObject flies;
    [SerializeField] private GameObject fireBugs;
    [SerializeField] private GameObject butterflies;
    [SerializeField] private GameObject goldFish;
    [SerializeField] private GameObject fairy;
    [SerializeField] private GameObject spiders;
    [SerializeField] private GameObject bees;
    [SerializeField] private GameObject ladyBug;
    private float minX = 12f;
    private float maxX = 40f;
    private float minY = 0f;
    private float maxY = 2.3f;
    private float startingXPos = 1f;
    private float startingYPos = 10f;
    private IEnumerator routine;

    public enum TimeStates{
        TimeQuiteFull, //time > 20 <40
        TimeAlmostFull, //time > 40 :Increase the no of firebugs and other enemy types if the player is doing well
        TimeAlmostFinished, //time < 10: Help the player by sending normal flies and goldfish to the game
    }

    public enum BugGenStates {
        GenerateBugs,
        GeneratingBugs,
        StopGenerating,
        Stopped
    }
    public enum PlayerStates {
        Normal,
        PlayerNerfed, //Add a fairybug for the player to debuff
        PlayerStunned //Help the player by adding more flies
    }

    public enum PlayerScoreStates {
        ScoreBelow25,
        ScoreOver25,
        ScoreOver50,
        ScoreOver75,
        ScoreOver100
    }

    public PlayerStates playerStates;
    public TimeStates timeStates;
    public PlayerScoreStates playerScoreStates;
    public BugGenStates bugGenStates;
    public int FlyCount { get => flyCount; }
    public static event Action<GameObject> SendFrogObjectToFairy;

    void OnEnable(){
        FroggoPlayer.UpdateBugGeneratorScoreState += ChangePlayerScoreStates;
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

    private float GenerateXPos(float minx, float maxX)
    {
        float randomXPos = UnityEngine.Random.Range(minX, maxX);
        return randomXPos;
    }

    private float GenerateYPos(float minY, float maxY)
    {
        float randomYPos = UnityEngine.Random.Range(minY, maxY);
        return randomYPos;
    }

    private void InitialSpawn() {
        float x = GenerateXPos(minX,maxX);
        float y = GenerateYPos(minY, maxY);
        //Instantiate(availableGems[index], new Vector3(x, y, z), Quaternion.identity);
        GameObject newFly = Instantiate(flies, new Vector3(x, y, 0),Quaternion.identity);
        newFly.GetComponent<FlyMovements>().GetInitialCoordinates(x,y);
    }

    //Use coroutines to instantiate normal flies depending on the player score as the game progresses
    private void InstantiateFlies()
    {
        float x = GenerateXPos(minX,maxX);
        float y = GenerateYPos(minY, maxY);
        //Instantiate(availableGems[index], new Vector3(x, y, z), Quaternion.identity);
        GameObject newFly = Instantiate(flies, new Vector3(startingXPos, startingYPos, 0),Quaternion.identity);
        newFly.GetComponent<FlyMovements>().GetInitialCoordinates(x,y);
        print("fly instantiated");
    }

    //Use coroutines to instantiate firebugs depending on the player score as the game progresses
    private void InstantiateFireBugs()
    {
        float x = GenerateXPos(minX, maxX);
        float y = GenerateYPos(minY,maxY);
        GameObject newFirebug = Instantiate(fireBugs, new Vector3(startingXPos, startingYPos, 0), Quaternion.identity);
        newFirebug.GetComponent<FireBug>().GetInitialCoordinates(x,y);
        print("fireBug instantiated");
    }
    private void InstantiateButterfly() {
        float x = GenerateXPos(minX, maxX);
        float y = GenerateYPos(minY,maxY);
        Instantiate(butterflies, new Vector3(startingXPos, y, 0), Quaternion.identity);
        print("butterfly instantiated");
    }

    private void InstantiateLadyBug() {
        float x = transform.position.x + 8f;
        float y = 6f;
        Instantiate(ladyBug, new Vector3(x,y,0), Quaternion.identity);
    }

    private void InstantiateBee() {
        float y = GenerateYPos(minY,maxY);
        Instantiate(bees, new Vector3(50f,y, 0), Quaternion.identity);
    }

    private void InstantiateFairy() {
        float y = GenerateYPos(minY,maxY);
        Instantiate(fairy, new Vector3(70f,y, 0), Quaternion.identity); //better not to hardcode position values
        print("Fairy instantiated");
    }

    private void InstantiateGoldFish() {
        float x = GenerateXPos(minX, maxX);
        Instantiate(goldFish, new Vector3(x, -6f, 0), Quaternion.identity);
        print("Goldfish instantiated");
    }

    private void InstantiateSpider() {
        float x = GenerateXPos(minX,maxX);
        Instantiate(spiders, new Vector3(x, 5.9f,0), Quaternion.identity);
    }

    private int GetCurrentFlyCount() {
        return GameObject.FindGameObjectsWithTag("Fly").Length;
    }
    private int GetCurrentLadyBugCount() {
        return GameObject.FindGameObjectsWithTag("LadyBug").Length;
    }
    private int GetCurrentButterflyCount() {
        return GameObject.FindGameObjectsWithTag("Butterfly").Length;
    }
    private int GetCurrentFirebugCount() {
        return GameObject.FindGameObjectsWithTag("FireBug").Length;
    }

    private int GetCurrentBeeCount() {
         return GameObject.FindGameObjectsWithTag("Bee").Length;
    }

    //A method to instantiate a fairy when the player gets a nerf
    //Methods to spawn other bugs as the game progresses
    IEnumerator InGameBugGenerator() {
        while(true) {
            //might not need all of the states here but keep it since it might be useful in the future
            switch(timeStates) {
                case TimeStates.TimeAlmostFinished:
                    yield return new WaitForSeconds(2f);
                    if(GetCurrentBeeCount() < maxNoOfBeesAllowed) {
                        InstantiateBee();
                    }
                    break;
                default:
                    yield return new WaitForSeconds(0.1f);
                    break;
            }

            switch(playerScoreStates) {
                case PlayerScoreStates.ScoreBelow25:
                    yield return new WaitForSeconds(2f);
                    if(GetCurrentBeeCount() < maxNoOfBeesAllowed) {
                        InstantiateBee();
                    }
                    yield return new WaitForSeconds(1f);
                    if(GetCurrentLadyBugCount() < maxNoOfLadyBugsAllowed) {
                        InstantiateLadyBug();
                    }
                    break;
                case PlayerScoreStates.ScoreOver25:
                    yield return new WaitForSeconds(2f);
                    if(GetCurrentFlyCount() < maxNoOfFliesAllowed) {
                        InstantiateFlies();
                    }
                    yield return new WaitForSeconds(2f);
                    if(GetCurrentBeeCount() < maxNoOfBeesAllowed) {
                        InstantiateBee();
                    }
                    yield return new WaitForSeconds(1f);
                    if(GetCurrentLadyBugCount() < maxNoOfLadyBugsAllowed) {
                        InstantiateLadyBug();
                    }
                    break;
                case PlayerScoreStates.ScoreOver50:
                    yield return new WaitForSeconds(2f);
                    InstantiateGoldFish();
                    if(GetCurrentFlyCount() < maxNoOfFliesAllowed) {
                        InstantiateFlies();
                    }
                    yield return new WaitForSeconds(1f);
                    if(GetCurrentBeeCount() < maxNoOfBeesAllowed) {
                        InstantiateBee();
                    }
                    yield return new WaitForSeconds(1f);
                    if(GetCurrentButterflyCount() < maxNoOfButterfliesAllowed) {
                        InstantiateButterfly();
                    }
                    break;
                case PlayerScoreStates.ScoreOver75:
                    yield return new WaitForSeconds(2f);
                    if(GetCurrentFirebugCount() < maxNoOfFirebugsAllowed) {
                        InstantiateFireBugs();
                    }
                    yield return new WaitForSeconds(1f);
                    if(GetCurrentButterflyCount() < maxNoOfButterfliesAllowed) {
                        InstantiateButterfly();
                    }
                    if(GetCurrentFlyCount() < maxNoOfFliesAllowed) {
                        InstantiateFlies();
                    }
                    break;
                case PlayerScoreStates.ScoreOver100:
                    yield return new WaitForSeconds(2f);
                    if(GetCurrentFirebugCount() < maxNoOfFirebugsAllowed) {
                        InstantiateFireBugs();
                    }
                    yield return new WaitForSeconds(1f);
                    if(GetCurrentFlyCount() < maxNoOfFliesAllowed) {
                        InstantiateFlies();
                    }
                    yield return new WaitForSeconds(2f);
                    InstantiateSpider();
                    break;

            }

            switch(playerStates) {
                case PlayerStates.PlayerStunned:
                    yield return new WaitForSeconds(5f);
                    InstantiateGoldFish();
                    yield return new WaitForSeconds(8f);
                    InstantiateFairy();
                    break;
                case PlayerStates.PlayerNerfed:
                    InstantiateFairy();
                    yield return new WaitForSeconds(13f);
                    break;
            }
        }
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

    //might not use all of its states but keep this regardless
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

    public void ChangePlayerScoreStates(int z) {
        switch(z) {
            case 7:
                playerScoreStates = PlayerScoreStates.ScoreOver25;
                maxNoOfFliesAllowed = 6;
                break;
            case 8:
                playerScoreStates = PlayerScoreStates.ScoreOver50;
                maxNoOfFliesAllowed = 5;
                break;
            case 9:
                playerScoreStates = PlayerScoreStates.ScoreOver75;
                maxNoOfFliesAllowed = 4;
                maxNoOfFirebugsAllowed = 5;
                break;
            case 10:
                playerScoreStates = PlayerScoreStates.ScoreOver100;
                maxNoOfFirebugsAllowed = 4;
                maxNoOfBeesAllowed = 4;
                break;
            default:
                break;
        }
    }

    public void GameOverState(){
        //change BugGenState
        bugGenStates = BugGenStates.StopGenerating;
    }
    void Update() {
        if(bugGenStates == BugGenStates.GenerateBugs) {
            StartCoroutine(routine);
            bugGenStates = BugGenStates.GeneratingBugs;
        }
        if(bugGenStates == BugGenStates.StopGenerating) {
            StopCoroutine(routine);
            bugGenStates = BugGenStates.Stopped;
        }
    }
    void Start()
    {
        routine = InGameBugGenerator();
        playerStates = PlayerStates.Normal;
        playerScoreStates = PlayerScoreStates.ScoreBelow25;
        bugGenStates = BugGenStates.GenerateBugs;
    }

    //This method is invoked when a fairy is instantiated
    public void SendFroggoToFairy() {
        //This Event triggers the GetFrogObject method attached on fairy objects and sends the game object attached to this script
        SendFrogObjectToFairy?.Invoke(this.gameObject);
    }
    void OnDisable(){
        FroggoPlayer.UpdateBugGeneratorScoreState -= ChangePlayerScoreStates;
        FroggoPlayer.UpdateBugGeneratorPlayer -= ChangePlayerStates;
        FroggoPlayer.UpdateBugGeneratorTime -= ChangeTimeStates;
        FroggoPlayer.TimeIsOver -= GameOverState;
        Fairy.GetFroggoObj -= SendFroggoToFairy;
    }
}
