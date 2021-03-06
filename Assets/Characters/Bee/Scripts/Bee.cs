using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : FlyMovements
{
    private Vector3 secondPos;
    private Vector3 thirdPos;
    private int index = 0;
    private bool deadBee;

    //testing
    //private float interpolateAmt;
    //[SerializeField] private Transform pointA;
    //[SerializeField] private Transform pointB;
    //[SerializeField] private Transform pointC;
    //[SerializeField] private Transform pointD;
    //[SerializeField] private Transform pointE;
    //[SerializeField] private Transform pointF;
    //[SerializeField] private Transform pointAB;
    //[SerializeField] private Transform pointBC;
    //[SerializeField] private Transform pointCD;
    //[SerializeField] private Transform pointDE;
    //[SerializeField] private Transform pointEF;
    //[SerializeField] private Transform pointABC;
    //[SerializeField] private Transform pointBCD;
    //[SerializeField] private Transform pointCDE;
    //[SerializeField] private Transform pointDEF;
    //[SerializeField] private Transform pointABCD;
    //[SerializeField] private Transform pointBCDE;
    //[SerializeField] private Transform pointCDEF;
    //[SerializeField] private Transform pointABCDE;
    //[SerializeField] private Transform pointBCDEF;
    //[SerializeField] private Transform pointABCDEF;



    void Start()
    {
        deadBee = false;
        addPositions();
        
    }


    void Update()
    {
        moveFly();
     
    }

    protected override void moveFly()
    {

        //float currentDuration;
        //float journeyFraction;

        //Fish Movement
        //if (transform.position == endingPos)
        //{
        //    startingPos = transform.position;
        //    endingPos = new Vector3(startingPos.x + 7.0f, startingPos.y, 0f);
        //    midPos = new Vector3((startingPos.x + endingPos.x) * 0.5f, 0.66f, 0f);
        //    journeyFraction = 0;
        //    startTime = Time.time;
        //    print(startTime);
        //    totDistance = Vector3.Distance(startingPos, endingPos);
        //    currentDuration = (Time.time - startTime) * speed;
        //    journeyFraction = currentDuration / totDistance;

        //}


        //totDistance = Vector3.Distance(startingPos, endingPos);
        //print(Time.time);
        //currentDuration = (Time.time - startTime) * speed;
        //journeyFraction = currentDuration / totDistance;
        //Vector3 startMid = Vector3.Lerp(startingPos, midPos, journeyFraction);
        //Vector3 midEnd = Vector3.Lerp(midPos, endingPos, journeyFraction);
        //transform.position = Vector3.Lerp(startMid, midEnd, journeyFraction);


        //Bee movements 
        if(deadBee == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
            if(transform.position.x < minX)
            {
                anim.SetTrigger("Death");
                deadBee = true;
                return;
            }
            if (positions[index].x > transform.position.x)
            {
                transform.localScale = new Vector2(-0.5f, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(0.5f, transform.localScale.y);
            }

            if (transform.position == positions[index])
            {
                if (index == positions.Count - 1)
                {
                    index = 0;
                    positions.Clear();
                    addPositions();
                }
                else
                {
                    index++;
                }

            }

        }
        
       

    }
    protected override void addPositions()
    {
        Vector3 startingPos = transform.position;
        secondPos = new Vector3(startingPos.x - 6f, startingPos.y + 2f, 0f);
        thirdPos = new Vector3(startingPos.x - 3f, startingPos.y - 2f, 0f);
        Vector3 endingPos = new Vector3(startingPos.x - 10f, startingPos.y, 0f);
        positions.Add(startingPos);
        positions.Add(secondPos);
        positions.Add(thirdPos);
        positions.Add(endingPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TongueCol")
        {
            print("Hit = TOngue collisionButterFly");

            anim.SetTrigger("Death");

        }
    }

    //private void LerpTest()
    //{
    //    interpolateAmt = (interpolateAmt + Time.deltaTime) % 1f;

    //    pointAB.position = Vector3.Lerp(pointA.position, pointB.position, interpolateAmt);
    //    pointBC.position = Vector3.Lerp(pointB.position, pointC.position, interpolateAmt);
    //    pointCD.position = Vector3.Lerp(pointC.position, pointD.position, interpolateAmt);
    //    pointDE.position = Vector3.Lerp(pointD.position, pointE.position, interpolateAmt);
    //    pointEF.position = Vector3.Lerp(pointE.position, pointF.position, interpolateAmt);
    //    pointABC.position = Vector3.Lerp(pointAB.position, pointBC.position, interpolateAmt);
    //    pointBCD.position = Vector3.Lerp(pointBC.position, pointCD.position, interpolateAmt);
    //    pointCDE.position = Vector3.Lerp(pointCD.position, pointDE.position, interpolateAmt);
    //    pointDEF.position = Vector3.Lerp(pointDE.position, pointEF.position, interpolateAmt);
    //    pointABCD.position = Vector3.Lerp(pointABC.position, pointBCD.position, interpolateAmt);
    //    pointBCDE.position = Vector3.Lerp(pointBCD.position, pointCDE.position, interpolateAmt);
    //    pointCDEF.position = Vector3.Lerp(pointCDE.position, pointDEF.position, interpolateAmt);
    //    pointABCDE.position = Vector3.Lerp(pointABCD.position, pointBCDE.position, interpolateAmt);
    //    pointBCDEF.position = Vector3.Lerp(pointBCDE.position, pointCDEF.position, interpolateAmt);
    //    pointABCDEF.position = Vector3.Lerp(pointABCDE.position, pointBCDEF.position, interpolateAmt);
    //} 


}


