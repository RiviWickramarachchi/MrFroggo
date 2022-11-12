using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : FlyMovements
{
    private Vector3 secondPos;
    private Vector3 thirdPos;
    private int index = 0;
    private bool deadBee;

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
        //Bee movements
        if(deadBee == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
            if(transform.position.x < minX)
            {
                deadBee = true;
                Death();
            }
            if (positions[index].x > transform.position.x)
            {
                transform.localScale = new Vector2(-0.7f, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(0.7f, transform.localScale.y);
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
}


