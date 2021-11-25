using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float lastFallTime = 0f;
    private float fallingTime = 0.1f;
    void Start()
    {
        if (!isBoundary())
        {
            Destroy(gameObject);
            TGameManager.Instance.gameOver();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1f, 0f, 0f);
            if (isBoundary()) updateBlock();
            else transform.position += new Vector3(1f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0f, 0f);
            if (isBoundary()) updateBlock();
            else transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0f, 0f, -90f);
            if (isBoundary()) updateBlock();
            else transform.Rotate(0f, 0f, 90f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFallTime >= TGameManager.Instance.fallingSpeed)
        {
            transform.position += new Vector3(0f, -1f, 0f);
            if (isBoundary()) updateBlock();
            else
            {
                transform.position += new Vector3(0f, 1f, 0f);
                TGameManager.Instance.checkAndDeleteFullRows();
                SpawnManager.Instance.spawn();
                enabled = false;
            }
            lastFallTime = Time.time;
        }else if (Input.GetKeyDown(KeyCode.Space)) // 하드 드롭
        {
            bool canMove = false;
            while (isBoundary())
            {
                canMove = true;
                transform.position += new Vector3(0f, -1f, 0f);
            }
            if (canMove)
            {
                transform.position += new Vector3(0f, 1f, 0f);
                updateBlock();
                TGameManager.Instance.checkAndDeleteFullRows();
                SpawnManager.Instance.spawn();
                enabled = false;
            }

        }
    }

    private bool isBoundary()
    {
        foreach (var childBlock in transform)
        {
            Vector2 vec = TGameManager.Instance.roundVec2((childBlock as Transform).position);
            if (!TGameManager.Instance.isInMap(vec)) return false;
            int x = (int)vec.x;
            int y = (int)vec.y;
            if (TGameManager.Instance.blockMap[x, y] != null &&
                TGameManager.Instance.blockMap[x, y].parent != transform) return false;
        }
        return true;
    }

    private void updateBlock()
    {
        for (int y = 0; y < TGameManager.Instance.getH(); ++y)
        {
            for (int x = 0; x < TGameManager.Instance.getW(); ++x)
            {
                if (TGameManager.Instance.blockMap[x, y] != null)
                {
                    if (TGameManager.Instance.blockMap[x, y].parent == transform)
                        TGameManager.Instance.blockMap[x, y] = null;
                }
            }
        }
        foreach (Transform childBlock in transform)
        {
            Vector2 vec = TGameManager.Instance.roundVec2(childBlock.position);
            TGameManager.Instance.blockMap[(int)vec.x, (int)vec.y] = childBlock;
        }
    }
}
