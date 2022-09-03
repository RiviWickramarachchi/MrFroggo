using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFish : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 endingPos;
    private Vector3 midPos;
    private float startTime;
    private float totDistance;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Animator fishAnim;

    //Animation has been adjusted to work properly with the fish movement only when the speed of the fish is set to 4
    void Start()
    {
        startingPos = transform.position;
        endingPos = new Vector3(startingPos.x - 7.0f, startingPos.y, 0f);
        midPos = new Vector3((startingPos.x + endingPos.x) * 0.5f, 0.66f, 0f);
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
        if (transform.position == endingPos)
        {
            //Debug.Log("total time :" + (Time.time - startTime) );
            startingPos = transform.position;
            endingPos = new Vector3(startingPos.x - 7.0f, startingPos.y, 0f);
            midPos = new Vector3((startingPos.x + endingPos.x) * 0.5f, 0.66f, 0f);
            journeyFraction = 0;
            startTime = Time.time;
        }


        totDistance = Vector3.Distance(startingPos, endingPos);
        currentDuration = (Time.time - startTime) * speed;
        journeyFraction = currentDuration / totDistance;
        Vector3 startMid = Vector3.Lerp(startingPos, midPos, journeyFraction);
        Vector3 midEnd = Vector3.Lerp(midPos, endingPos, journeyFraction);
        transform.position = Vector3.Lerp(startMid, midEnd, journeyFraction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TongueCol")
        {
            print("Hit = TOngue collisionButterFly");
            fishAnim.SetTrigger("Death");
        }
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
