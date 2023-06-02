using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class FrogActions : MonoBehaviour
{
    //Controls the frog tongue animations

    //[SerializeField] private GameObject frogPupilObj;

    //Frog Anim Sounds
    [SerializeField] private AudioSource tongueOutSound;
    [SerializeField] private AudioSource dizzySound;
    [SerializeField] private GameObject tongueObj;
    [SerializeField] private FroggoPlayer froggoPlayer;
    [SerializeField] private SpriteRenderer fogSr;
    private Animator anim;
    private SpriteRenderer sr;
    public static event Action<Collider2D> FrogCollision;
    private float fogAlphaVal;
    private float fogAlphaProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        fogAlphaVal = 0;
    }

    void OnEnable() {
        FroggoPlayer.OnFlyCatch += FogValueIncreased;
        FroggoPlayer.OnFairyCatch += ResetFogValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //float tongueZRot = UnityEditor.TransformUtils.GetInspectorRotation(frogPupilObj.transform).z;
                //print("Set tongue value =" +tongueZRot);
                //tongueObj.transform.Rotate(0f, 0f,tongueZRot);
                anim.SetFloat("idleSpeed", 0f);
                Sprite sprite = sr.sprite;
                //print(sprite.name);
                string animSprite = sprite.name;
                if(froggoPlayer.GetFrogEffect() == 1) {
                    tongueOutSound.Play();
                    NormalTongueActions (animSprite);
                }
                else if(froggoPlayer.GetFrogEffect() == 2) {
                    tongueOutSound.Play();
                    BeeEffectTongueActions(animSprite);
                }
                else if(froggoPlayer.GetFrogEffect() == 3) {
                    tongueOutSound.Play();
                    LadyBugEffectTongueActions(animSprite);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(waitForAnimFinish(0.8f)); //have to get the proper animation length
                //anim.SetBool("tongueOut", false);
            }

        }
    }

    private void BeeEffectTongueActions(string animSprite) {

        switch(animSprite)
        {
            case "idle_1":
                anim.Play("froggo_bee_tongue1");
                break;
            case "idle_2":
                anim.Play("froggo_bee_tongue2");
                break;
            case "idle_3":
                anim.Play("froggo_bee_tongue3");
                 break;
            case "idle_4":
                anim.Play("froggo_bee_tongue4");
                break;
            case "idle_5":
                anim.Play("froggo_bee_tongue5");
                break;
            case "idle_6":
                anim.Play("froggo_bee_tongue6");
                break;
            case "idle_7":
                anim.Play("froggo_bee_tongue7");
                break;
            case "idle_8":
                anim.Play("froggo_bee_tongue8");
                break;
            case "idle_9":
                anim.Play("froggo_bee_tongue9");
                break;
            case "idle_10":
                anim.Play("froggo_bee_tongue10");
                break;
            case "idle_11":
                anim.Play("froggo_bee_tongue11");
                break;
            case "idle_12":
                anim.Play("froggo_bee_tongue12");
                break;
            default:
                break;
        }
        switch(animSprite)
        {
            case "idle_1":
                anim.Play("froggo_tongue1");
                break;
            case "idle_2":
                anim.Play("froggo_tongue2");
                break;
            case "idle_3":
                anim.Play("froggo_tongue3");
                break;
            case "idle_4":
                anim.Play("froggo_tongue4");
                break;
            case "idle_5":
                anim.Play("froggo_tongue5");
                break;
            case "idle_6":
                anim.Play("froggo_tongue6");
                break;
            case "idle_7":
                anim.Play("froggo_tongue7");
                break;
            case "idle_8":
                anim.Play("froggo_tongue8");
                break;
            case "idle_9":
                anim.Play("froggo_tongue9");
                break;
            case "idle_10":
                anim.Play("froggo_tongue10");
                break;
            case "idle_11":
                anim.Play("froggo_tongue11");
                break;
            case "idle_12":
                anim.Play("froggo_tongue12");
                break;
            default:
                break;
        }
    }

    private void LadyBugEffectTongueActions(string animSprite) {
        switch(animSprite)
        {
            case "idle_1":
                anim.Play("froggo_long_tongue1");
                break;
            case "idle_2":
                anim.Play("froggo_long_tongue2");
                break;
            case "idle_3":
                anim.Play("froggo_long_tongue3");
                break;
            case "idle_4":
                anim.Play("froggo_long_tongue4");
                break;
            case "idle_5":
                anim.Play("froggo_long_tongue5");
                break;
            case "idle_6":
                anim.Play("froggo_long_tongue6");
                break;
            case "idle_7":
                anim.Play("froggo_long_tongue7");
                break;
            case "idle_8":
                anim.Play("froggo_long_tongue8");
                break;
            case "idle_9":
                anim.Play("froggo_long_tongue9");
                break;
            case "idle_10":
                anim.Play("froggo_long_tongue10");
                break;
            case "idle_11":
                anim.Play("froggo_long_tongue11");
                break;
            case "idle_12":
                anim.Play("froggo_long_tongue12");
                break;
            default:
                break;
        }
    }

    private void NormalTongueActions (string animSprite) {
         switch(animSprite)
        {
            case "idle_1":
                anim.Play("froggo_tongue1");
                break;
            case "idle_2":
                anim.Play("froggo_tongue2");
                break;
            case "idle_3":
                anim.Play("froggo_tongue3");
                break;
            case "idle_4":
                anim.Play("froggo_tongue4");
                break;
            case "idle_5":
                anim.Play("froggo_tongue5");
                break;
            case "idle_6":
                anim.Play("froggo_tongue6");
                break;
            case "idle_7":
                anim.Play("froggo_tongue7");
                break;
            case "idle_8":
                anim.Play("froggo_tongue8");
                break;
            case "idle_9":
                anim.Play("froggo_tongue9");
                break;
            case "idle_10":
                anim.Play("froggo_tongue10");
                break;
            case "idle_11":
                anim.Play("froggo_tongue11");
                break;
            case "idle_12":
                anim.Play("froggo_tongue12");
                break;
            default:
                break;
        }
    }

    IEnumerator FogIncrease() {
        while(fogAlphaVal > fogAlphaProgress) {
            Debug.Log(fogAlphaProgress);
            fogAlphaProgress += Time.deltaTime;
            fogSr.color = new Color(fogSr.color.r,fogSr.color.g,fogSr.color.b,fogAlphaProgress);
            yield return null;
        }
    }

    IEnumerator FogDecrease() {
        while(fogAlphaVal < fogAlphaProgress) {
            Debug.Log(fogSr.color.a);
            fogAlphaProgress -= Time.deltaTime;
            fogSr.color = new Color(fogSr.color.r,fogSr.color.g,fogSr.color.b,fogAlphaProgress);
            yield return null;
        }
    }



    public void FogValueIncreased(float newVal) {
        fogAlphaVal += newVal;
        StartCoroutine(FogIncrease());
    }

    public void ResetFogValue() {
        fogAlphaVal = 0;
        StartCoroutine(FogDecrease());
    }
    public void PlayDizzySound() {
        dizzySound.Play();
    }

    IEnumerator waitForAnimFinish(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tongueObj.transform.rotation = Quaternion.Euler(Vector3.forward * 0);
        //print("Reset tongue rot value =" + UnityEditor.TransformUtils.GetInspectorRotation(tongueObj.transform).z);
        //anim.SetInteger("tongueVal", 0);
        anim.SetFloat("idleSpeed", 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        FrogCollision?.Invoke(collision);
    }

    void OnDisable() {
        FroggoPlayer.OnFlyCatch -= FogValueIncreased;
        FroggoPlayer.OnFairyCatch -= ResetFogValue;
    }
}
