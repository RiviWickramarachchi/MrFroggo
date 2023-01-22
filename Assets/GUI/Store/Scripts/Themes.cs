using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Theme", menuName ="ScriptableObjects/Theme")]
public class Themes : StoreItems
{

    private enum PurchaseState { Purchased, Not_Purchased }
    private PurchaseState purchaseState;

    private enum ChooseState { Chosen, Not_Chosen}
    private ChooseState chooseState;

    public override void ItemPurchase()
    {
        purchaseState = PurchaseState.Purchased;
    }
    public override void SetItemValues(int bugCoin)
    {
        itemType = ItemType.THEME;

    }
}
