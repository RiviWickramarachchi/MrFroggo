using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemAssets : MonoBehaviour
{
    public static StoreItemAssets Instance {get; private set;}
    public Sprite hshDefault;
    public Sprite hshSelected;
    public Sprite prpDefault;
    public Sprite prpSelected;
    public Sprite adFree;
    public Sprite buyCoins;

    void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);

        }
        else {
            Instance = this;
        }
    }
}
