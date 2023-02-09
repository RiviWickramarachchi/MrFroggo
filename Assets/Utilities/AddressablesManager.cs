using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

public class AddressablesManager : MonoBehaviour
{
    //level loading
    private bool clearPreviousScene= false;
    private SceneInstance previousLoadedScene;

    private int setSceneId;

    private string defaultSceneAddressable = "Assets/Scenes/Level_HSM.unity";
    private string pirateRetirementPlanAddressable = "Assets/Scenes/Level_PRP.unity";

    void Awake() {
        this.setSceneId = StoreDataLoader.Instance.setSceneId;
    }
    void Start()
    {
        LoadAddressableScene(setSceneId);
    }

    void OnEnable() {
        Store.OnChooseBtnClick += LoadAddressableScene;
    }

    // Update is called once per frame
    // void Update()
    // {
    // }

    public void LoadAddressableScene(int setSceneId) {

        if(clearPreviousScene) {
            Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) => {
                clearPreviousScene = false;
                previousLoadedScene = new SceneInstance();
                Debug.Log("Unloaded Previous Scene");
            };
        }

        switch(setSceneId) {
            case 1:
                LoadTheme(defaultSceneAddressable);
                Debug.Log($"Loading {defaultSceneAddressable} ");
                return;
            case 2:
                LoadTheme(pirateRetirementPlanAddressable);
                Debug.Log($"Loading {pirateRetirementPlanAddressable} ");
                return;
            default:
                return;

        }

        //Addressables.LoadSceneAsync
    }

    private void LoadTheme(string addressableKey) {
        Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Additive).Completed += (asyncHandle) => {
            clearPreviousScene = true;
            previousLoadedScene = asyncHandle.Result;
            Debug.Log($"Addressable scene {addressableKey} loaded successfully");
        };
    }

    void OnDisable() {
        Store.OnChooseBtnClick -= LoadAddressableScene;
    }
}
