using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int highScore;
    public int bugCoins;
    public int setSceneId;

    public List<StoreItems> storeItems;

    public GameData() {
        this.highScore = 0;
        this.bugCoins = 0;
        this.setSceneId =  1;
        storeItems = new List<StoreItems>();
   }
}
