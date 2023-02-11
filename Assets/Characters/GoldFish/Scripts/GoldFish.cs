using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFish : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 endingPos;
    private Vector3 midPos;
    private float minX = -2f;
    private float maxX =45f;
    private float maxXDistance = 5.0f;
    private float startTime;
    private float totDistance;
    private bool deadFish;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator fishAnim;

    //Animation has been adjusted to work properly with the fish movement only when the speed of the fish is set to 4
    void Start()
    {
        deadFish = false;
        startingPos = transform.position;
        endingPos = new Vector3(generateXPos(), startingPos.y, startingPos.z);
        startTime = Time.time;
    }
    void Update()
    {
        moveFish();
    }
    private void moveFish()
    {
        float currentDuration;
        float journeyFraction;
        //Fish Movement
        if(!deadFish) {
            if (transform.position == endingPos)
            {
                Debug.Log("reached end position");
                fishAnim.Play("goldfish_idle");
                if (transform.position.x < minX)
                {
                    deadFish = true;
                    Death();
                }
                startingPos = transform.position;
                endingPos = new Vector3(generateXPos(), startingPos.y, startingPos.z);
                journeyFraction = 0;
                startTime = Time.time;
            }

            totDistance = Vector3.Distance(startingPos, endingPos);
            currentDuration = (Time.time - startTime) * speed;
            journeyFraction = currentDuration / totDistance;
            //Vector3 startMid = Vector3.Lerp(startingPos, midPos, journeyFraction);
            //Vector3 midEnd = Vector3.Lerp(midPos, endingPos, journeyFraction);
            transform.position = Vector3.Lerp(startingPos,endingPos, journeyFraction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TongueCol")
        {
           callDeathAnim();
        }
    }

    private void callDeathAnim()
    {
        fishAnim.SetTrigger("Death");
    }

    private float generateXPos()
    {
        float randomXPos = Random.Range(startingPos.x, startingPos.x - maxXDistance);
        return randomXPos;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
