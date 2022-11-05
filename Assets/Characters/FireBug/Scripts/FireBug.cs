using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FireBug : FlyMovements
{

    private float colorTime = 4.0f;
    private float transitionTime = 3.0f;
    private float offColorTime = 3.0f;
    private float timer = 0f;
    private bool ascending;

    public float Timer { get => timer;  }
    public float ColorTime { get => colorTime; }

    private void Awake()
    {
        Assert.IsNotNull(anim);
    }

    protected override void addPositions()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = new Vector3(transform.position.x - 3.0f, transform.position.y -0.65f, transform.position.z);
        Vector3 pos3 = new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z);
        Vector3 pos4 = new Vector3(transform.position.x - 7.0f, transform.position.y - 0.65f, transform.position.z);
        Vector3 pos5 = new Vector3(transform.position.x - 9.0f, transform.position.y, transform.position.z);
        positions.Add(pos1);
        positions.Add(pos2);
        positions.Add(pos3);
        positions.Add(pos4);
        positions.Add(pos5);
    }

    private void setFireBugAnimations()
    {

        if (ascending)
        {
            timer += 1 * Time.deltaTime;
        }
        else
        {
            timer -= 1 * Time.deltaTime;
        }

        if (timer < colorTime)
        {

            if (timer <= 0)
            {
                ascending = true;
            }
            anim.SetBool("fireOn", true);
            anim.SetBool("transition", false);
            anim.SetBool("fireOff", false);
        }
        else
        {
            if(timer < colorTime + transitionTime)
            {
                anim.SetBool("fireOn", false);
                anim.SetBool("transition", true);
                anim.SetBool("fireOff", false);
            }
            else
            {
                if(timer < colorTime + transitionTime + offColorTime)
                {
                    anim.SetBool("fireOn", false);
                    anim.SetBool("transition", false);
                    anim.SetBool("fireOff", true);
                }
                else
                {
                    ascending = false;
                }

            }
        }
    }
    private void Update()
    {
        moveFly();
        setFireBugAnimations();
    }
}
