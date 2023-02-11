using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBug : FlyMovements
{
    private Transform frogTransform;
    private int index;
    private bool reachedEndPosition;
    Vector3 direction = new Vector3(0,0,0);
    //test
    private Vector3 target;

    void Start()
    {
        frogTransform = GameObject.Find("Froggo").transform;
        addPositions();
        index = 0;
        reachedEndPosition = false;
        target = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        direction = target- transform.position;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg +120;
        Quaternion angleAxis = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,angleAxis, 50 * Time.deltaTime);
        moveFly();
    }

    protected override void moveFly()
    {
        if(index < positions.Count) {
            transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
            if(transform.position == positions[index]) {
                index++;
                if(index < positions.Count) {
                   target = positions[index];
                }
            }
        }
        else {
            Death();
        }
    }

    protected override void addPositions()
    {
        Vector3 pos1 = new Vector3(frogTransform.position.x + 8f, 6f, 0f);
        Vector3 pos2 = new Vector3(frogTransform.position.x + 3f, frogTransform.position.y + 1f, 0f);
        Vector3 pos3 = new Vector3(frogTransform.position.x - 3f, frogTransform.position.y + 3f, 0f);
        Vector3 pos4 = new Vector3(frogTransform.position.x - 8f, -6f, 0f);
        positions.Add(pos1);
        positions.Add(pos2);
        positions.Add(pos3);
        positions.Add(pos4);
    }
}
