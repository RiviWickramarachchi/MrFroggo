using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CoinPurchase", menuName ="ScriptableObjects/CoinPurchase")]
public class CoinPurchase : StoreItems
{
//    public CoinPurchase() {
//         purchaseType = PurchaseType.RecurringPurchase;
//         purchaseState = PurchaseState.NONE;
//         itemType = ItemType.BUYCOINS;
//         chooseState = ChooseState.NONE;
//    }

    public override void ItemPurchase()
    {
        //purchaseState = PurchaseState.Purchased;
        //handle coin purchase using a payment gateway
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
