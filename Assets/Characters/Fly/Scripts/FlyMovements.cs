using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyMovements : MonoBehaviour
{

    //Controls the movement of the fly and collisions with the frog tongue

    //[SerializeField] private float moveLimit = 5.0f;
    protected float minX = -25f;
    protected float maxX =75f;
    protected float minY = 0f;
    protected float maxY = 2.3f;
    [SerializeField] protected float speed;
    [SerializeField] protected Animator anim;
    protected List<Vector3> positions = new List<Vector3>();
    private int index;
    private bool isGoingback;
    void Start()
    {
        addPositions();
    }

    // Update is called once per frame
    void Update()
    {
        moveFly();
    }

    protected virtual void OnEnable() {
        FroggoPlayer.DestroyOnEndState += Death;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TongueCol")
        {
            print("Hit = TOngue collision");
            anim.SetTrigger("Death");
        }
    }

    protected virtual void moveFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
        if(transform.position == positions[index])
        {
            if(index == positions.Count - 1)
            {
                //index = 0;
                isGoingback = true;
                transform.localScale = new Vector2(-0.5f, transform.localScale.y);
            }
            else if(index == 0)
            {
                //index++; //goingback is false
                isGoingback = false;
                transform.localScale = new Vector2(0.5f, transform.localScale.y);
            }

            if(isGoingback)
            {
                index--;
            }
            else
            {
                index++;
            }
        }
    }

    protected virtual void addPositions()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = new Vector3(transform.position.x - 3.0f, transform.position.y, transform.position.z);
        Vector3 pos3 = new Vector3(transform.position.x - 3.0f, transform.position.y - 0.65f, transform.position.z);
        Vector3 pos4 = new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z);
        Vector3 pos5 = new Vector3(transform.position.x - 7.0f, transform.position.y, transform.position.z);
        positions.Add(pos1);
        positions.Add(pos2);
        positions.Add(pos3);
        positions.Add(pos4);
        positions.Add(pos5);
    }

    protected virtual void Death()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnDisable() {
        FroggoPlayer.DestroyOnEndState -= Death;
    }
}
