using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class FrogActions : MonoBehaviour
{
    //Controls the frog tongue animations

    //[SerializeField] private GameObject frogPupilObj;
    [SerializeField] private GameObject tongueObj;

    private Animator anim;
    private SpriteRenderer sr;

    public static event Action<Collider2D> FrogCollision;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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
                //anim.SetBool("tongueOut", true);

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
            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(waitForAnimFinish(0.8f)); //have to get the proper animation length
                //anim.SetBool("tongueOut", false);
            }

        }
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
}
