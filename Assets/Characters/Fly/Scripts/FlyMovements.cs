using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovements : MonoBehaviour
{

    //Controls the movement of the fly and collisions with the frog tongue

    //[SerializeField] private float moveLimit = 5.0f;
    [SerializeField] private float speed;
    private List<Vector3> positions = new List<Vector3>();
    private int index;
   
    void Start()
    {

        addPositions();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveFly();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "TongueCol")
        {
            print("Hit = TOngue collision");
            Destroy(this.gameObject);
        }
    }

    private void moveFly()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if(transform.position == positions[index])
        {
            if(index == positions.Count - 1)
            {
                index = 0;
                //index--;
            }
           
            else
            {
                index++;
            }
        }
    }

    private void addPositions()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos2 = new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z);
        Vector3 pos3 = new Vector3(transform.position.x - 5.0f, transform.position.y - 0.65f, transform.position.z);
        Vector3 pos4 = new Vector3(transform.position.x, transform.position.y - 0.65f, transform.position.z);
        positions.Add(pos1);
        positions.Add(pos2);
        positions.Add(pos3);
        positions.Add(pos4);
    }



    
}
