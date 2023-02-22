using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class ResourceLoader : MonoBehaviour
{
    public static ResourceLoader Instance {get ; private set;}
    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;
    [SerializeField] private string fileName;
    void Awake()
    {
        if(Instance != null){
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        //load the data here because UI manager wont be able to capture the data when you load it on start
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        LoadDataToNewScene();
    }

    public void DisplayHighScore(int playerScore,TMP_Text highscoreText) {
        //if youre in the start scene just display highscor,e, else compare game scores for highscore updat
        if(playerScore > this.gameData.highScore) {
            //update Gamedata object
            this.gameData.highScore = playerScore;
            //save HS on storeDataLoader instance
            StoreDataLoader.Instance.highScore = playerScore;
        }
        highscoreText.text = this.gameData.highScore.ToString("00");
    }

    public int GetBugCoinValue() {
        return this.gameData.bugCoins;
    }

    public void UpdateBugCoinAmount(int totalBugCoins, int bugScore) {
        totalBugCoins += bugScore;
        this.gameData.bugCoins = totalBugCoins;
        //save BugCoin amount on storeDataLoader instance
        StoreDataLoader.Instance.bugCoinAmount = totalBugCoins;
        //save game to save the data
    }

    public void DisplayTotalBugCoins(TMP_Text bugCoinAmount) {
        bugCoinAmount.text = this.gameData.bugCoins.ToString("00");
    }

    public void NewGame() {
        this.gameData = new GameData();
    }

    public void LoadGame() {
        //Load any saved data from the file handler
        this.gameData = dataHandler.Load();
        //if no data was found initialize a new game.
        Debug.Log("loading game data : "+this.gameData); //test
        if(this.gameData == null) {
            Debug.Log("No Game Data found. Initializing new Game data obj");
            NewGame();
        }
        //Push the loaded data to all the scripts that need it.
        foreach(IDataPersistance dataPersistanceObject in dataPersistanceObjects) {
            dataPersistanceObject.LoadData(gameData);
        }
     }

    public void SaveGame() {
        //pass data to other scripts so they can handle it
        foreach(IDataPersistance dataPersistanceObject in dataPersistanceObjects) {
            dataPersistanceObject.SaveData(ref gameData);
        }
        //save data to a file using the data handler
        dataHandler.Save(gameData);
    }

    public void LoadDataToNewScene() {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects() {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
