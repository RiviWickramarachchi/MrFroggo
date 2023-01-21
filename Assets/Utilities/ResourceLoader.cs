using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ResourceLoader : MonoBehaviour
{
    public static ResourceLoader Instance;
    private Scene currentScene;
    private int buildIndex;
    void Awake()
    {
        if(Instance != null){
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void DisplayHighScore(int playerScore,TMP_Text highscoreText) {
        //check which scene you are in
        //if youre in the start scene just display highscor,e, else compare game scores for highscore update
        currentScene = SceneManager.GetActiveScene();
        buildIndex = currentScene.buildIndex;
        if(buildIndex == 0) {
            int highscoreValue = PlayerPrefs.GetInt("HighScore",0);
            Debug.Log(highscoreValue);
            highscoreText.text = "HIGHESTSCORE:"+highscoreValue.ToString("00");
        }
        else if (buildIndex == 1) {
            if(playerScore > PlayerPrefs.GetInt("HighScore",0)) {
                PlayerPrefs.SetInt("HighScore", playerScore);
            }
            highscoreText.text = PlayerPrefs.GetInt("HighScore").ToString("00");
        }
    }

    public int GetBugCoinValue() {
        int bugCoinValue = PlayerPrefs.GetInt("BugCoins",0);
        return bugCoinValue;
    }

    public void UpdateBugCoinAmount(int totalBugCoins, int bugScore) {
        totalBugCoins += bugScore;
        PlayerPrefs.SetInt("BugCoins", totalBugCoins);
    }

    public void DisplayTotalBugCoins(TMP_Text bugCoinAmount) {
        bugCoinAmount.text = PlayerPrefs.GetInt("BugCoins",0).ToString("00");
    }
}
