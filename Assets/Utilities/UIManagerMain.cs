using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerMain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text totalBugCoinsText;
    [SerializeField] private TMP_Text highscoreText;
    void Start()
    {
        ResourceLoader.Instance.DisplayHighScore(0,highscoreText);
        ResourceLoader.Instance.DisplayTotalBugCoins(totalBugCoinsText);
    }
}
