using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FrogActions : MonoBehaviour
{
    [SerializeField] private GameObject frogPupilObj;
    [SerializeField] private GameObject tongueObj;

    private Animator anim;

   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                float tongueZRot = UnityEditor.TransformUtils.GetInspectorRotation(frogPupilObj.transform).z;
                //print("Set tongue value =" +tongueZRot);
                tongueObj.transform.Rotate(0f, 0f,tongueZRot);
                anim.SetFloat("EyeSpeed", 0f);
                anim.SetBool("TongueOut", true);

            }
            if (touch.phase == TouchPhase.Ended)
            {
                
                StartCoroutine(waitForAnimFinish(0.8f)); //have to get the proper animation length
                anim.SetBool("TongueOut", false);
                



            }


        }
    }

    IEnumerator waitForAnimFinish(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); 
        tongueObj.transform.rotation = Quaternion.Euler(Vector3.forward * 0);
        //print("Reset tongue rot value =" + UnityEditor.TransformUtils.GetInspectorRotation(tongueObj.transform).z);
        anim.SetFloat("EyeSpeed", 1f);




    }

    
}
