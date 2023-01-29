using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Theme", menuName ="ScriptableObjects/Theme")]
public class Themes : StoreItems
{
    // public Themes() {
    //     purchaseType = PurchaseType.OneTimePurchase;
    //     purchaseState = PurchaseState.Not_Purchased;
    //     itemType = ItemType.THEME;
    //     chooseState = ChooseState.Not_Chosen;
    // }

    public override void ItemPurchase()
    {
        purchaseState = PurchaseState.Purchased;
    }

    public override void ItemChoose()
    {
        chooseState = ChooseState.Chosen;
    }
    public override void ItemDeselect()
    {
        chooseState = ChooseState.Not_Chosen;
    }

}
