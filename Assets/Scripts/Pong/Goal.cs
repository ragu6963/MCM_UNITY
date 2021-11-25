using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayerWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayerWall)
            {
                PongManager.Instance.ComputerScored();
            }
            else
            {
                PongManager.Instance.PlayerScored();
            }
        }
    }
}
