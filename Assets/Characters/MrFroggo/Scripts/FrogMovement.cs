using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogMovement : MonoBehaviour
{
    //Controls the movements of the frog
    Rigidbody2D rb;
    private float xDir;
    // [SerializeField] private float minXPos = 12.09f;
    // [SerializeField] private float maxXPos = 36.67f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private ParticleSystemRenderer psr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xDir = Input.acceleration.x * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,11.41f,37.03f), transform.position.y);
        if((xDir >= -3f) && (xDir <= 3f))
        {
            psr.pivot = new Vector3(0f, 0f, 0f);
        }
        if(xDir < -3f)
        {
            psr.pivot = new Vector3(0.3f, 0f, 0f);
        }
        if (xDir > 3f)
        {
            psr.pivot = new Vector3(-0.3f, 0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir, 0f);
    }
}
