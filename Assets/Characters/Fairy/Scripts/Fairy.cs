using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fairy : FlyMovements
{

    public static event Action GetFroggoObj;
    [SerializeField] private float waitTime = 3f;
    private GameObject frogPlayer;
    private Transform frogTransform;
    private int positionNumber = 0;
    private float time = 0f;
    public bool send = true;

    protected override void OnEnable(){
        BugGenerator.SendFrogObjectToFairy += GetFrogObject;
        //The fairy object requests for the frogObject from the BugGenerator which is attached onto froggo on Enable
        GetFroggoObj?.Invoke();
        frogTransform = frogPlayer.transform;
    }

    void Start() {
         addPositions();
    }
    void Update()
    {
        moveFly();
    }

    protected override void moveFly()
    {

        switch(positionNumber)
        {
            case 0:
                transform.position = Vector2.MoveTowards(transform.position, positions[positionNumber], Time.deltaTime * speed);
                if(transform.position == positions[positionNumber])
                {
                    transitionToFlyIdleState();
                    time = time + 1f * Time.deltaTime;
                    if(time > waitTime)
                    {
                        time = 0f;
                        transitionToFlyMoveState();
                        positionNumber++;
                    }

                }
                break;

            case 1:
                transform.position = Vector2.MoveTowards(transform.position, positions[positionNumber], Time.deltaTime * speed);
                if (transform.position == positions[positionNumber])
                {
                    transitionToFlyIdleState();
                    time = time + 1f * Time.deltaTime;
                    if (time > waitTime)
                    {
                        time = 0f;
                        transitionToFlyMoveState();
                        positionNumber++;
                    }
                }
                break;
            case 2:
                transform.position = Vector2.MoveTowards(transform.position, positions[positionNumber], Time.deltaTime * speed);
                if (transform.position == positions[positionNumber])
                {
                    transitionToFlyIdleState();
                    time = time + 1f * Time.deltaTime;
                    if (time > waitTime)
                    {
                        time = 0f;
                        positionNumber++;
                        transitionToFlyMoveState();
                    }
                }
                break;
            case 3:
                transform.position = Vector2.MoveTowards(transform.position, positions[positionNumber], Time.deltaTime * speed);
                if (transform.position == positions[positionNumber])
                {
                    transitionToFlyIdleState();
                    time = time + 1f * Time.deltaTime;
                    if (time > waitTime)
                    {
                        time = 0f;
                        positionNumber++;
                        transitionToFlyMoveState();
                    }
                }
                break;
            case 4:
                transform.position = Vector2.MoveTowards(transform.position, positions[positionNumber], Time.deltaTime * speed);
                if (transform.position == positions[positionNumber])
                {
                    transitionToFlyIdleState();
                    time = time + 1f * Time.deltaTime;
                    if (time > waitTime)
                    {
                        time = 0f;
                        positionNumber++;
                        transitionToFlyMoveState();
                    }
                }
                break;
            case 5:
                transform.position = Vector2.MoveTowards(transform.position,positions[positionNumber], Time.deltaTime * speed);
                if (transform.position == positions[positionNumber])
                {
                    transitionToFlyIdleState();
                    time = time + 1f * Time.deltaTime;
                    if (time > waitTime)
                    {
                        time = 0f;
                        positionNumber++;
                        transitionToFlyMoveState();
                    }
                }
                break;
            default:
                Debug.Log("Destroyed");
                Death();
                break;
        }
    }

    protected override void addPositions()
    {
        Vector3 positionOne = new Vector3((frogPlayer.transform.position.x - 3f), (frogPlayer.transform.position.y + 1.05f), 0f);
        Vector3 positionTwo = new Vector3((frogPlayer.transform.position.x + 3f), (frogPlayer.transform.position.y - 1.45f), 0f);
        Vector3 positionThree = new Vector3((frogPlayer.transform.position.x), (frogPlayer.transform.position.y +1.2f), 0f);
        Vector3 positionFour = new Vector3((frogPlayer.transform.position.x - 3f), (frogPlayer.transform.position.y - 1.45f), 0f);
        Vector3 positionFive = new Vector3((frogPlayer.transform.position.x + 3f), (frogPlayer.transform.transform.position.y + 1.05f), 0f);
        Vector3 endPos = new Vector3(80f, 1.05f, 0f);
        positions.Add(positionOne);
        positions.Add(positionTwo);
        positions.Add(positionThree);
        positions.Add(positionFour);
        positions.Add(positionFive);
        positions.Add(endPos);
    }

    private void lookAtFroggo()
    {
        float fairyX = transform.position.x;
        float froggoX = frogTransform.position.x;

        if((fairyX - froggoX)< 0)
        {
            transform.localScale = new Vector2(-0.7f, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(0.7f, transform.localScale.y);
        }
    }

    private void transitionToFlyIdleState()
    {
        anim.SetBool("FairyIdle", true);
        lookAtFroggo();
        //transform.LookAt(frogPlayer.transform);
    }

    private void transitionToFlyMoveState()
    {
        anim.SetBool("FairyIdle", false);
    }

    private void callFairyDeathAnim()
    {
        anim.SetTrigger("Death");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TongueCol")
        {
            print("Hit = TOngue collisionButterFly");
            callFairyDeathAnim();

        }
    }

    //Frog object is retrieved by this method where the transform.position of it is used for the fairy movements
    public void GetFrogObject(GameObject go) {
        frogPlayer = go;
     }

     protected override void OnDisable() {
        BugGenerator.SendFrogObjectToFairy -= GetFrogObject;
    }

}
