using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovements : MonoBehaviour
{
    [SerializeField] private float moveLimit = 5.0f;
    [SerializeField] private float speed;
    [SerializeField] private bool movingLeft;
    private float moveDistance;
    private Vector3 lastPosition;
   
    // Start is called before the first frame update
    void Start()
    {
        
        moveDistance = 0;
        lastPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveFly(moveLimit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "TongueCol")
        {
            print("Hit = TOngue collision");
            Destroy(this.gameObject);
        }
    }

    private void moveFly(float moveLimit)
    {
        if(movingLeft)
        {
            moveLeft();
            if(moveDistance >moveLimit)
            {
                movingLeft = false;
                moveDistance = 0;
            }
            
        }
        else
        {
            moveRight();
            if(moveDistance > moveLimit)
            {
                movingLeft = true;
                moveDistance = 0;
            }
        }
    }

    private void moveLeft()
    {
        transform.Translate(1 * Time.deltaTime * speed, 0, 0);
        transform.localScale = new Vector2(-1, 1);
        moveDistance += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

    }

    private void moveRight()
    {
        transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
        transform.localScale = new Vector2(1, 1);
        moveDistance += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }
}
