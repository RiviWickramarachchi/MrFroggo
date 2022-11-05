using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spider : FlyMovements
{
    [SerializeField] private float waitTime = 2f;
    private float time = 0f;
    private int index;
    private bool goingback;
    public static event Action<Collider2D> FroggoCollided;

    void Start() {
        addPositions();
    }
    void Update()
    {
        moveFly();
    }

    protected override void moveFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
        if(transform.position == positions[index])
        {
            if(index == positions.Count - 1)
            {
                time = time + 1f * Time.deltaTime;
                if(time > waitTime) {
                    time = 0f;
                    goingback = true;
                    index --;
                }
                //wait for some time

            }
            else if(index == 0)
            {
                //check if isGoingback is true and destroy object after a few secs
                if(goingback) {
                    Invoke("Death", 1);
                }
                else {
                    index++;
                }
            }
        }
    }
    protected override void addPositions() {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = new Vector3(transform.position.x, -1.0f, transform.position.z);
        positions.Add(pos1);
        positions.Add(pos2);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TongueCol")
        {
            print("Hit = TOngue collision");
            anim.SetTrigger("Death");
            FroggoCollided?.Invoke(collision);
        }
        else if(collision.tag == "FrogBody")
        {
            FroggoCollided?.Invoke(collision);
        }
    }

}
