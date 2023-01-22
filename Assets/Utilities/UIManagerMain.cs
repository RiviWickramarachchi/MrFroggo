using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerMain : MonoBehaviour,IDataPersistance
{
    // Start is called before the first frame update
    private int highscoreValue;
    private int bugCoins;
    [SerializeField] private TMP_Text totalBugCoinsText;
    [SerializeField] private TMP_Text highscoreText;

    public void LoadData(GameData data) {
        this.highscoreValue = data.highScore;
        this.bugCoins = data.bugCoins;
        highscoreText.text = "HIGHESTSCORE:"+highscoreValue.ToString("00");
        totalBugCoinsText.text = this.bugCoins.ToString("00");
    }

    public void SaveData(ref GameData data) {
        //no need to save data here...
    }

}
