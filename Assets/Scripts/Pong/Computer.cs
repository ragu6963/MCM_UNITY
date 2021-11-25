using UnityEngine;

public class Computer : MonoBehaviour
{
    private int topBound = 298;
    private int bottomBound = -298;

    public GameObject ball;
    private Vector2 ballPostion;
    public Vector2 startPosition;
    private float speed = 250;
    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        if (ball.GetComponent<Ball>().ballDirection == Vector2.right)
        {
            ballPostion = ball.transform.localPosition;

            if (transform.localPosition.y > bottomBound && ballPostion.y < transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
            }
            else if (transform.localPosition.y < topBound && ballPostion.y > transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
            }
        }
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
    public void IncreaseSpeed(int playerscore) {
        speed += playerscore * 25;
    }
    public void ResetSpeed()
    {
        speed = 250;
    }
}
