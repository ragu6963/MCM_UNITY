using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject[] blocks;
    private GameObject blockGroupRoot;
    
    public void Init()
    {
        blockGroupRoot = transform.Find("BlockGroup").gameObject;
        spawn();
    }
    public void spawn()
    {
        if (!TGameManager.Instance.isGameOver)
        {
            int i = Random.Range(0, blocks.Length);
            GameObject go = Instantiate(blocks[i], transform.position, Quaternion.identity);
            go.transform.SetParent(blockGroupRoot.transform);
        }
    }

    public void Reset()
    {
        int cnt = blockGroupRoot.transform.childCount;
        if (cnt == 0) return;
        for(int i = cnt-1; i >=0; --i)
        {
            Destroy(blockGroupRoot.transform.GetChild(i).gameObject);
        }
    }
}
