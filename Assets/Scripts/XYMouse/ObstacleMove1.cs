using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove1 : MonoBehaviour
{
    Vector2 currentPosition;
    float delta = 20.0f;
    float speed = 5.0f;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        Vector2 nextPosition = currentPosition;
        nextPosition.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = nextPosition;
    }
}
