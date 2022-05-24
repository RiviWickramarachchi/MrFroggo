using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : FlyMovements
{
    
    private float maxXDistance = 5.0f;
    private Vector3 startingPos;
    private Vector3 endingPos;
    //private Vector3 midPos;
    protected float startTime;
    protected float totDistance;
    private bool deadButterfly;
    




    void Start()
    {
        deadButterfly = false;
        startTime = Time.time;
        //startingPos = new Vector3(7.76f, -5.55f, 0f);
        startingPos = transform.position;
        endingPos = new Vector3(generateXPos(), generateYPos(), 0f);
        //midPos = new Vector3((startingPos.x + endingPos.x) * 0.5f, 0.66f, 0f);
        
    }

    
    void Update()
    {
        moveFly();
        
        
    }

    private float generateXPos()
    {
        float randomXPos = Random.Range(startingPos.x, startingPos.x + maxXDistance);
        return randomXPos;
    }

    private float generateYPos()
    {
        float randomYPos = Random.Range(minY, maxY);
        return randomYPos;
    }

    protected override void moveFly()
    {
        float currentDuration;
        float journeyFraction;

        //Butterfly movement
        if (deadButterfly == false)
        {
            if (transform.position == endingPos)
            {
                if (transform.position.x > maxX)
                {
                    deadButterfly = true;
                    Death();
                }
                startingPos = transform.position;
                endingPos = new Vector3(generateXPos(), generateYPos(), 0f);
                journeyFraction = 0;
                startTime = Time.time;
                print(startTime);
                totDistance = Vector3.Distance(startingPos, endingPos);
                currentDuration = (Time.time - startTime) * speed;
                journeyFraction = currentDuration / totDistance;

            }

            totDistance = Vector3.Distance(startingPos, endingPos);
            currentDuration = (Time.time - startTime) * speed;
            journeyFraction = currentDuration / totDistance;
            transform.position = Vector3.Lerp(startingPos, endingPos, journeyFraction);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "TongueCol")
        {
            print("Hit = TOngue collisionButterFly");
            
            anim.SetTrigger("Death");

        }
    }
}
