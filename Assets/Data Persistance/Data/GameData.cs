using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int highScore;
    public int bugCoins;

    public List<StoreItems> storeItems;

    public GameData() {
        this.highScore = 0;
        this.bugCoins = 0;
        storeItems = new List<StoreItems>();
   }
}
