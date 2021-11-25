using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D RigidBody;
    private float movement;
    public Vector2 startPosition;
    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        movement = Input.GetAxisRaw("Vertical");
        RigidBody.velocity = new Vector2(RigidBody.velocity.x, movement * speed);
    }

    public void Reset()
    {
        RigidBody.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
