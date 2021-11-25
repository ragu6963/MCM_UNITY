using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private float speed = 1000;
    public Rigidbody2D RigidBody;
    public Vector2 startPosition;
    public Vector2 ballDirection;

    public AudioClip bleep;
    public AudioSource ballAudioSound;

    private float countDownTime = 3f;
    public GameObject CountTimer;
    private bool isLaunch;
    private bool isStart;

    private void Start()
    {
        startPosition = transform.position;
    }
    void OnEnable()
    {
        Launch();
    }
    private void Update()
    {
        if (isLaunch)
        {
            isLaunch = false;
            isStart = true;
            float x = -1;
            float y = Random.Range(0.2f, 0.4f);
            float multi = Random.Range(1, 2);
            if (multi == 2) multi = -1;
            y = y * multi;

            RigidBody.velocity = new Vector2(speed * x, speed * y);
            if (RigidBody.velocity.x > 0)
                ballDirection = Vector2.right;
            else
                ballDirection = Vector2.left;
        }
        if (isLaunch == false && isStart == false)
        {
            if (countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
                CountTimer.GetComponent<Text>().text = Mathf.Ceil(countDownTime).ToString();
            }
            else
            {
                CountTimer.SetActive(false);
                isLaunch = true;
            }
        }

    }
    public void Launch()
    {
        CountTimer.SetActive(true);
        countDownTime = 3f;
        isLaunch = false;
        isStart = false;
    }
    public void Reset()
    {
        RigidBody.velocity = Vector2.zero;
        transform.position = startPosition;
        Launch();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballAudioSound.PlayOneShot(bleep);
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (RigidBody.velocity.x > 0)
                ballDirection = Vector2.right;
            else
                ballDirection = Vector2.left;
        }
    }
    public void IncreaseSpeed(int playerscore)
    {
        this.speed += playerscore * 50;
    }
    public void ResetSpeed()
    {
        countDownTime = 3;
        this.speed = 1000;
    }
}
