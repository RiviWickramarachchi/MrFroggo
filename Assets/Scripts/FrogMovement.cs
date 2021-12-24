using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    Rigidbody2D rb;
    float xDir;
    [SerializeField] float moveSpeed = 5f;

   

    // Start is called before the first frame update
    void Start()
    {
       
        
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        xDir = Input.acceleration.x * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,-32.8f,35.7f), transform.position.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir, 0f);
    }
}
