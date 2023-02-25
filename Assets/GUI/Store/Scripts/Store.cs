using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Store : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button prevBtn;
    [SerializeField] private Button chooseBtn;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Image bugCoinImg;
    [SerializeField] private TMP_Text requiredCoinsText;
    [SerializeField] private TMP_Text infoText2;
    GameObject panel;
    public List<StoreItems> storeItems;
    public static event Action<int> OnChooseBtnClick;
    private int bugCoinAmount;
    private int setSceneId;

    public int itemCount;

    void Start() {
        this.storeItems = StoreDataLoader.Instance.storeItems;
        this.bugCoinAmount = StoreDataLoader.Instance.bugCoinAmount;
        this.setSceneId = StoreDataLoader.Instance.setSceneId;
        foreach(StoreItems item in storeItems) {
            Debug.Log(item);
        }
    }
    public void DisplayStore() {
        panel = GameObject.FindGameObjectWithTag("Panel");
        //check item type and run the necessary methods depending on it
        foreach(Transform child in panel.transform) {
            if(child.CompareTag("StoreUI")) {
                child.gameObject.SetActive(true);
                ShowFirstItem();
            }
        }
        Time.timeScale = 0;
    }

    public void HideStore() {
        Time.timeScale = 1;
    }

    public void ShowFirstItem() {
        itemCount = 0;
        StoreItems firstItem = storeItems[itemCount];
        GetItemDetails(firstItem);
        prevBtn.interactable = false;
    }

    public void ShowNextItem() {
        itemCount++;
        if(itemCount <= storeItems.Count-1) {
            if(itemCount == storeItems.Count-1) {
                nextBtn.interactable = false;
            }
            StoreItems nextItem = storeItems[itemCount];
            GetItemDetails(nextItem);
            prevBtn.interactable = true;
        }
        Debug.Log(itemCount);
    }
    public void ShowPreviousItem() {
        itemCount--;
        if(itemCount >= 0) {
            if(itemCount == 0) {
                prevBtn.interactable = false;
            }
            StoreItems prevItem = storeItems[itemCount];
            GetItemDetails(prevItem);
            nextBtn.interactable = true;
        }
        Debug.Log(itemCount);
    }

    public void ChooseBtnClicked() {
        //get ID of the item and make all the choose items not choose
        Debug.Log("ITEM COUNT VALUE : "+itemCount);
        StoreItems item = storeItems[itemCount];
        int itemId = item.itemID;
        foreach(StoreItems storeItem in storeItems) {
            if(storeItem.chooseState != StoreItems.ChooseState.NONE) {
                if(storeItem.itemID == itemId) {
                    storeItem.ItemChoose();
                    //set scene ID to chosen scene ID and invoke addressable method to load new scene
                    this.setSceneId = itemId;
                    StoreDataLoader.Instance.setSceneId = this.setSceneId;
                    Debug.Log("Current Scene ID : "+ setSceneId);
                    if(SceneManager.GetActiveScene().buildIndex == 1) {
                        OnChooseBtnClick?.Invoke(this.setSceneId);
                    }
                }
                else {
                    storeItem.ItemDeselect();
                }
            }
        }
        GetItemDetails(item);
    }

    public void BuyBtnClicked() {
        //use itemCount to do necessary changes
        StoreItems item = storeItems[itemCount];
        bugCoinAmount -= item.coinsToUnlock;
        StoreDataLoader.Instance.bugCoinAmount = this.bugCoinAmount;
        item.ItemPurchase();
        GetItemDetails(item);

    }
    private void GetItemDetails(StoreItems item) {
        if(item.purchaseType == StoreItems.PurchaseType.RecurringPurchase) {
            //show coin
            //show buy button
            HideRequiredCoinsText();
            itemImage.sprite = StoreItemAssets.Instance.buyCoins;
            chooseBtn.gameObject.SetActive(false);
            buyBtn.gameObject.SetActive(true);
            //buyBtn.interactable = true;
        }
        else if(item.purchaseType == StoreItems.PurchaseType.OneTimePurchase) {
            //check if purchased or not
            //show purchase button if not purchased, choose btn if purchased
            if(item.purchaseState == StoreItems.PurchaseState.Purchased) {
                //hide buy btn
                HideRequiredCoinsText();
                buyBtn.gameObject.SetActive(false);
                if(item.chooseState == StoreItems.ChooseState.Chosen) {
                    itemImage.sprite = ReturnChosenSprite(item);
                    //hide chose btn
                    chooseBtn.gameObject.SetActive(false);
                }
                else if (item.chooseState == StoreItems.ChooseState.Not_Chosen) {
                    itemImage.sprite = ReturnNonChosenSprite(item);
                    //show chose btn
                    chooseBtn.gameObject.SetActive(true);
                }
                else {
                    //hide choose and buy btns
                    itemImage.sprite = StoreItemAssets.Instance.adFree;
                    chooseBtn.gameObject.SetActive(false);
                }

            }
            else if(item.purchaseState == StoreItems.PurchaseState.Not_Purchased) {
                //show buy btn //make it interactable depending on bugCoins
                //hide chose btn
                chooseBtn.gameObject.SetActive(false);
                buyBtn.gameObject.SetActive(true);
                if(item.chooseState == StoreItems.ChooseState.NONE) {
                    itemImage.sprite = StoreItemAssets.Instance.adFree;
                }
                else {
                    itemImage.sprite = ReturnNonChosenSprite(item);
                }

                if(bugCoinAmount < item.coinsToUnlock) {
                    DisplayRequiredCoinsText();
                    requiredCoinsText.text = "YOU NEED "+item.coinsToUnlock.ToString()+" ";
                    buyBtn.interactable = false;
                }
                else {
                    HideRequiredCoinsText();
                    buyBtn.interactable = true;
                }
            }
        }


    }

    private void DisplayRequiredCoinsText() {
        requiredCoinsText.gameObject.SetActive(true);
        bugCoinImg.gameObject.SetActive(true);
        infoText2.gameObject.SetActive(true);
    }
    private void HideRequiredCoinsText() {
        requiredCoinsText.gameObject.SetActive(false);
        bugCoinImg.gameObject.SetActive(false);
        infoText2.gameObject.SetActive(false);
    }

    private Sprite ReturnChosenSprite(StoreItems item) {
        switch(item.itemName) {
            default:
            case "HOME SWEET HOME":
                return StoreItemAssets.Instance.hshSelected;
            case "PIRATE RETIREMENT PLAN":
                return StoreItemAssets.Instance.prpSelected;
        }
    }

    private Sprite ReturnNonChosenSprite(StoreItems item) {
        switch(item.itemName) {
            default:
            case "HOME SWEET HOME":
                return StoreItemAssets.Instance.hshDefault;
            case "PIRATE RETIREMENT PLAN":
                return StoreItemAssets.Instance.prpDefault;
        }
    }
}
