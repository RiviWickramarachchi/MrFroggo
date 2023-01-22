using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class StoreItems : ScriptableObject
{
   public int itemID;
   public int coinsToUnlock;
   public string itemName;
   public Sprite selectedSprite;
   public Sprite defaultSprite;
   public enum ItemType { THEME, ADFREE, BUYCOINS }
   public ItemType itemType;

   public abstract void ItemPurchase();
   public abstract void SetItemValues(int bugCoin);

}
