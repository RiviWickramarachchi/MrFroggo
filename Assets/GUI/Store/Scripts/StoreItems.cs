using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class StoreItems : ScriptableObject
{
   public int itemID;
   public int coinsToUnlock;
   public string itemName;
   //public Sprite selectedSprite;
   //public Sprite defaultSprite;
    public enum PurchaseType { OneTimePurchase, RecurringPurchase }
    public PurchaseType purchaseType;
    public enum PurchaseState { Purchased, Not_Purchased, NONE }
    public PurchaseState purchaseState;

    public enum ChooseState { Chosen, Not_Chosen, NONE}
    public ChooseState chooseState;
   public enum ItemType { THEME, ADFREE, BUYCOINS }
   public ItemType itemType;

   public abstract void ItemPurchase();
   public abstract void ItemChoose();
   public abstract void ItemDeselect();

}
