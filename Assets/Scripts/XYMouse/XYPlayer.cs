using UnityEngine;

public class XYPlayer : MonoBehaviour
{
    public MouseManager MouseManager;

    private Vector2 initPos;

    private void Awake()
    {
        initPos = transform.position;
        InitPosition();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 목표지점에 도착
        if (other.gameObject.name == "Goal")
        {
            InitPosition();
            MouseManager.ActiveNextStage(); 
        }
        // 장애물에 부딪히면 출발점으로
        else
        {
            InitPosition();
        }
    }

    public void InitPosition()
    {
        this.transform.position = initPos;
    }
}
