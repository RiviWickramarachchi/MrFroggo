using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : FlyMovements
{
    [SerializeField] private GameObject frogPlayer;
    [SerializeField] private float waitTime = 3f;
    private int positionNumber = 0;
    private float time = 0f;

    void Update()
    {
        moveFly();
    }

    protected override void moveFly()
    {
        Debug.Log(positionNumber);
        switch(positionNumber)
        {
            case 0:
                Vector3 positionOne = new Vector3((frogPlayer.transform.position.x - 3f), (frogPlayer.transform.position.y + 1.05f), 0f);
                transform.position = Vector2.MoveTowards(transform.position, positionOne, Time.deltaTime * speed);
                if(transform.position == positionOne)
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
                Vector3 positionTwo = new Vector3((frogPlayer.transform.position.x + 3f), (frogPlayer.transform.position.y - 1.45f), 0f);
                transform.position = Vector2.MoveTowards(transform.position, positionTwo, Time.deltaTime * speed);
                if (transform.position == positionTwo)
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
                Vector3 positionThree = new Vector3((frogPlayer.transform.position.x), (frogPlayer.transform.position.y +1.2f), 0f);
                transform.position = Vector2.MoveTowards(transform.position, positionThree, Time.deltaTime * speed);
                if (transform.position == positionThree)
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
                Vector3 positionFour = new Vector3((frogPlayer.transform.position.x - 3f), (frogPlayer.transform.position.y - 1.45f), 0f);
                transform.position = Vector2.MoveTowards(transform.position, positionFour, Time.deltaTime * speed);
                if (transform.position == positionFour)
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
                Vector3 positionFive = new Vector3((frogPlayer.transform.position.x + 3f), (frogPlayer.transform.position.y + 1.05f), 0f);
                transform.position = Vector2.MoveTowards(transform.position, positionFive, Time.deltaTime * speed);
                if (transform.position == positionFive)
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
                Vector3 endPos = new Vector3(80f, 1.05f, 0f);
                transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
                if (transform.position == endPos)
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

    private void lookAtFroggo()
    {
        float fairyX = transform.position.x;
        float froggoX = frogPlayer.transform.position.x;

        if((fairyX - froggoX)< 0)
        {
            transform.localScale = new Vector2(-0.5f, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(0.5f, transform.localScale.y);
        }
    }

}
