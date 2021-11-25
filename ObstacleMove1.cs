using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove1 : MonoBehaviour
{
    Vector3 currentPosition;
    float delta = 5.0f;
    float speed = 4.0f;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        Vector3 nextPosition = currentPosition;
        nextPosition.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = nextPosition;
    }
}
