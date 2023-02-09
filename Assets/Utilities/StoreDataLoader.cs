using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDataLoader : MonoBehaviour,IDataPersistance
{
    public static StoreDataLoader Instance {get; private set;}
    public List<StoreItems> storeItems;
    public int bugCoinAmount;
    public int highScore;

    public int setSceneId;

    void Awake()
    {
        if(Instance != null){
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    void IDataPersistance.LoadData(GameData data)
    {
        this.highScore = data.highScore;
        this.bugCoinAmount = data.bugCoins;
        this.setSceneId = data.setSceneId;
        //load store information from the game data save file to storeItems List
        if(data.storeItems.Count != 0) {
            Debug.Log("Loading data.storeItems to this.storeItems");
            this.storeItems = data.storeItems;
        }
        else {
            Debug.Log(data.storeItems);
        }
    }

    void IDataPersistance.SaveData(ref GameData data)
    {
        data.storeItems = this.storeItems;
        data.highScore = this.highScore;
        data.bugCoins = this.bugCoinAmount;
        data.setSceneId = this.setSceneId;

    }
}
