using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerMain : MonoBehaviour
{
    // Start is called before the first frame update
    private int highscoreValue;
    private int bugCoins;
    [SerializeField] private TMP_Text totalBugCoinsText;
    [SerializeField] private TMP_Text highscoreText;


    void Start() {
        SetMainMenuScoreUI();
    }

    public void SetMainMenuScoreUI() {
        highscoreValue = StoreDataLoader.Instance.highScore;
        bugCoins = StoreDataLoader.Instance.bugCoinAmount;
        highscoreText.text = "HIGHESTSCORE:"+highscoreValue.ToString("00");
        totalBugCoinsText.text = this.bugCoins.ToString("00");
    }

}
