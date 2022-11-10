using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyMovements : MonoBehaviour
{

    //Controls the movement of the fly and collisions with the frog tongue

    //[SerializeField] private float moveLimit = 5.0f;
    protected float minX = -2f;
    protected float maxX =50f;
    protected float minY = 0f;
    protected float maxY = 2.3f;

    protected float initialX;
    protected float initialY;
    [SerializeField] protected float speed;
    [SerializeField] protected Animator anim;
    protected List<Vector3> positions = new List<Vector3>();
    protected bool reachedStartingPosition;
    protected Vector3 bugStartingPosition;
    private int index;
    private bool isGoingback;
    public Collider2D lastCollided;
    void Start()
    {
        reachedStartingPosition = false;
        transform.localScale = new Vector2(-0.7f, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(reachedStartingPosition == false) {
            if(transform.position == bugStartingPosition) {
                reachedStartingPosition = true;
                addPositions();
                transform.localScale = new Vector2(0.7f, transform.localScale.y);
            }
            else {
                transform.position = Vector2.MoveTowards(transform.position, bugStartingPosition, Time.deltaTime * speed);
            }
        }
        else {
            moveFly();
        }

    }

    protected virtual void OnEnable() {
        FroggoPlayer.DestroyOnEndState += Death;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        lastCollided = collision;
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
                transform.localScale = new Vector2(-0.7f, transform.localScale.y);
            }
            else if(index == 0)
            {
                //index++; //goingback is false
                isGoingback = false;
                transform.localScale = new Vector2(0.7f, transform.localScale.y);
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

    public void GetInitialCoordinates(float x, float y) {
        initialX = x;
        initialY = y;
        bugStartingPosition = new Vector3(initialX,initialY,0);
    }

    public Collider2D GetLastCollidedObject() {
        return lastCollided;
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
