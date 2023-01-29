using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private List<Themes> themeObj;
    // private List<string> themeObj = new List<string>() {
    //     "Animal", "dAVE", "BATISTA"
    // };
    // public void LoadData(GameData data)
    // {
    //     //throw new System.NotImplementedException();
    //     //this.themeObj = data.themes;
    //     Debug.Log(this.themeObj);
    //     if(data.themes.Count != 0) {
    //         Debug.Log("hello");
    //         this.themeObj = data.themes;
    //     }
    //     foreach(Themes theme in themeObj) {
    //             Debug.Log("ItemType : "+ theme.itemType);
    //             Debug.Log("Chose state : "+ theme.chooseState);
    //         }
    // }

    // public void SaveData(ref GameData data)
    // {
    //     data.themes = this.themeObj;
    //     foreach(Themes theme in data.themes) {
    //             Debug.Log("ItemType : "+ theme.itemType);
    //             Debug.Log("Chose state : "+ theme.chooseState);
    //         }
    //     foreach(Themes theme in data.themes) {
    //             theme.chooseState = Themes.ChooseState.Chosen;
    //         }
        //throw new System.NotImplementedException();
    //}
}
