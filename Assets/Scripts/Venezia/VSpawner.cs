using UnityEngine;

public class VSpawner : MonoBehaviour
{
    float time = 0;
    bool isPlay = true;

    private void Update()
    {
        if (!VGameManager.Instance.isStart) return;
        if(Time.time - time >= 1.5f && isPlay)
        {
            GameObject go = VWordManager.Instance.GetWordObject();
            if (go == null) isPlay = false;
            else go.SetActive(true);
            time = Time.time;
        }
    }
}
