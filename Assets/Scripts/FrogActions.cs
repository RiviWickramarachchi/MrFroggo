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
                print(tongueZRot);
                tongueObj.transform.Rotate(0f, 0f,tongueZRot);
                anim.SetBool("TongueOut", true);

            }
            if (touch.phase == TouchPhase.Ended)
            {
                anim.SetBool("TongueOut", false);
                tongueObj.transform.Rotate(0f, 0f, 0f);


            }


        }
    }
}
