using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AdFree", menuName ="ScriptableObjects/AdFree")]
public class AdFree : StoreItems
{
//    public AdFree() {
//         purchaseType = PurchaseType.OneTimePurchase;
//         purchaseState = PurchaseState.Not_Purchased;
//         itemType = ItemType.ADFREE;
//         chooseState = ChooseState.NONE;
//     }
    public override void ItemPurchase()
    {
        //purchaseState = PurchaseState.Purchased;
        //handle coin purchase using a payment gateway
        purchaseState = PurchaseState.Purchased;
    }

    public override void ItemChoose()
    {
        //no use for this class
    }

    public override void ItemDeselect()
    {
        //no use for this class
    }
}
