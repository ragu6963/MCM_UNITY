using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VWord : MonoBehaviour
{
    void Update()
    {
        if (VGameManager.Instance.isStart)
        {
            transform.position += new Vector3(0f, -1 * VGameManager.Instance.fallingSpeed, 0f);
            if (transform.position.y <=40f)
            {
                gameObject.SetActive(false);
                VWordManager.Instance.WordMatch[gameObject.GetComponent<Text>().text] = false;
                VGameManager.Instance.MinusLife(); // 라이프 감소
            }
        }
    }
}
