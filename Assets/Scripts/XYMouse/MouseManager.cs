using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] private GameObject XYMouse;
    [SerializeField] private GameObject GameBoard;
    [SerializeField] private GameObject Menu;


    public XYPlayer XYPlayer;
    public GameObject player;
    public Button xMouse;
    public Button yMouse;

    private GameObject[] Stages;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;

    public GameObject EtcObject;
    public GameObject ClearObject;
    public Text ClearTime;
    public Text Timer;

    public int stageIndex;
    public float time = 0;
    private bool isActiveXYMouse;

    private bool isXMouseClick;
    private bool isYMouseClick;


    private XYMouseVO[] xymouses;
    private string API_URL = "/api/v1/xyMouse";

    public void Start()
    {
        Stages = new GameObject[3] { stage1, stage2, stage3 };
    }
    public void ActivateXY()
    {
        XYMouse.SetActive(true);
        Menu.SetActive(true);
        GameBoard.SetActive(false);
    }
    public void DeactivateXY()
    {
        Menu.SetActive(true);
        GameBoard.SetActive(false);
        XYMouse.SetActive(false);
        isActiveXYMouse = false;
        PlayerManager.Instance.EndGame();
    }
    public void StartGame()
    {
        Menu.SetActive(false);
        GameBoard.SetActive(true);
        isActiveXYMouse = true;
    }
    public void OnClickStart()
    {
        StartGame();
        InitGame();
    }
    public void OnClickRetry()
    {
        InitGame();
    }
    public void OnClickExit()
    {
        DeactivateXY();
        InitGame();
    }
    public void OnDownXMouse()
    {
        isXMouseClick = true;
    }
    public void OnUpXMouse()
    {
        isXMouseClick = false;
    }

    public void OnDownYMouse()
    {
        isYMouseClick = true;
    }
    public void OnUpYMouse()
    {
        isYMouseClick = false;
    }
    void OnMouseXDrag()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float dist = (stageIndex == 0) ? 1.5f * Time.deltaTime * 100 : 2.0f * Time.deltaTime * 100;

        if (mousePos.x > xMouse.transform.position.x + 50)
        {
            Vector2 xPos = new Vector2(player.transform.position.x + dist, player.transform.position.y);
            player.transform.position = xPos;
        }
        else if (mousePos.x < xMouse.transform.position.x - 50)
        {
            Vector2 xPos = new Vector2(player.transform.position.x - dist, player.transform.position.y);
            player.transform.position = xPos;
        }
    }
    void OnMouseYDrag()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float dist = (stageIndex == 0) ? 1.5f * Time.deltaTime * 100 : 2.0f * Time.deltaTime * 100;

        if (mousePos.y > yMouse.transform.position.y + 50)
        {
            Vector2 yPos = new Vector2(player.transform.position.x, player.transform.position.y + dist);
            player.transform.position = yPos;
        }
        else if (mousePos.y < yMouse.transform.position.y - 50)
        {
            Vector2 yPos = new Vector2(player.transform.position.x, player.transform.position.y - dist);
            player.transform.position = yPos;
        }
    }
    void Update()
    {
        if (!isActiveXYMouse) return;
        if (isXMouseClick)
        {
            OnMouseXDrag();
        }
        if (isYMouseClick)
        {
            OnMouseYDrag();
        }
        time += Time.deltaTime;
        Timer.text = string.Format("{0:N2}s", time);

    }
    public void InitGame()
    {
        time = 0;
        stageIndex = 0;
        Stages[stageIndex].SetActive(true);
        ClearObject.SetActive(false);
        XYPlayer.InitPosition();
    }
    public void ActiveNextStage()
    {
        stageIndex += 1;
        // 게임 클리어
        if (stageIndex == 3)
        {
            ClearGame();
        }
        // 다음 스테이지
        else
        {
            Stages[stageIndex].SetActive(true);
        }
    }
    public void ClearGame()
    {
        CreateScore(time);
        ClearObject.SetActive(true);
        XYPlayer.InitPosition();
        ClearTime.text = string.Format("Clear Time : {0:N2}s", time);
        foreach (GameObject stage in Stages)
        {
            stage.SetActive(false);
        }
    }

    // 점수 기록
    public void CreateScore(float time)
    {
        string url = API_URL;
        string userName = PhotonNetwork.NickName;
        XYMouseDto XYMouseDto = new XYMouseDto
        {
            userName = userName,
            time = time,
        };
        string json = JsonUtility.ToJson(XYMouseDto);
        StartCoroutine(PostScore(url, json, response =>
        {
            if (response)
            {
                RequestScore();
            }
        }));
    }

    IEnumerator PostScore(string url, string json, System.Action<bool> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            byte[] Byte = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(Byte);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                callback(true);
            }
        }
    }

    // 점수 요청
    public void RequestScore()
    {
        string url = API_URL;
        StartCoroutine(GetScores(url, response =>
        {
            string json = "{\"xymouses\":" + response + "}";
            xymouses = JsonUtility.FromJson<XYMousesVO>(json).xymouses;
            string gamerank = "";
            for (int i = 0; i < xymouses.Length; i++)
            {
                XYMouseVO xymouse = xymouses[i];
                gamerank += $"{i + 1}. {xymouse.userName}({xymouse.time} s)\n";
            }
            CanvasManager.Instance.SetGameRankText("XY-Mouse", gamerank);
        }));
    }

    IEnumerator GetScores(string url, System.Action<string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            long status = request.responseCode;
            if (status == 200)
            {
                string jsonText = request.downloadHandler.text;
                callback(jsonText);
            }
        }
    }
}

[Serializable]
public class XYMouseDto
{
    public string userName;
    public float time;
}

[Serializable]
public class XYMousesVO
{
    public XYMouseVO[] xymouses;
}

[Serializable]
public class XYMouseVO
{
    public int xymouseId;
    public string userName;
    public float time;
}

