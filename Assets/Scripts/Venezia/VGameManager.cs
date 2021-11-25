using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VGameManager : Singleton<VGameManager>
{
    // 오브젝트
    public GameObject Venezia; // Root 오브젝트
    public GameObject BeforeGame; // 게임 시작 전 메뉴 오브젝트
    public GameObject InGame; // 게임 시작 오브젝트

    public Text ScoreText;
    public GameObject[] Lifes;
    //public InputField InputField;
    public TMP_InputField InputField;
    // 플레이어 관련
    int Life;
    [HideInInspector]
    public int Score;
    [HideInInspector]
    public float fallingSpeed = 1f;
    public bool isStart = false;
    private bool isOver = false;

    private string API_URL = "/api/v1/venezia";
    private VeneziaVO[] venezias;


    public void ActiveVenezia(bool flag)
    {
        Venezia.SetActive(flag);
    }
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Life == 0) GameOver();
        
        if (InputField.text == "") return;

        if (Input.GetKeyUp(KeyCode.Return))
        {
            VWordManager.Instance.CheckWord(InputField.text);
            InputField.text = "";
            InputField.ActivateInputField();
        }
    }

    private void Init()
    {
        Life = 3;
        for (int i = 0; i < Life; ++i) Lifes[i].SetActive(true);
        Score = 0;
        ScoreText.text = "0";
        fallingSpeed = 1f;
        InputField.text = "";
    }

    public void ScoreUp(int score)
    {
        Score += score;
        ScoreText.text = Score.ToString();
    }

    public void MinusLife()
    {
        if (Life == 0) GameOver();
        Lifes[3 - (Life--)].SetActive(false);
    }
    // 게임 시작 버튼
    public void GameStart()
    {
        Init();
        VWordManager.Instance.Init();
        BeforeGame.SetActive(false);
        InGame.SetActive(true);
        isStart = true;
    }
    // 게임 오버
    private void GameOver()
    {

        isStart = false;
        BeforeGame.SetActive(true);
        InGame.SetActive(false);
        InputField.text = "";
        CreateScore(Score);
        Init();
    }

    // 게임 나가기 버튼
    public void GameExit()
    {
        ActiveVenezia(false);
        PlayerManager.Instance.EndGame();
    }

    // 점수 기록
    public void CreateScore(int score)
    {
        string url = API_URL;
        string userName = PhotonNetwork.NickName;
        VeneziaDto VeneziaDto = new VeneziaDto
        {
            userName = userName,
            score = score,
        };
        string json = JsonUtility.ToJson(VeneziaDto);
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
            string json = "{\"venezias\":" + response + "}";
            venezias = JsonUtility.FromJson<VeneziasVO>(json).venezias;
            string gamerank = "";
            for (int i = 0; i < venezias.Length; i++)
            {
                VeneziaVO venezia = venezias[i];
                gamerank += $"{i + 1}. {venezia.userName}({venezia.score}점)\n";
            }
            CanvasManager.Instance.SetGameRankText("Venezia", gamerank);
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
public class VeneziaDto
{
    public string userName;
    public int score;
}

[Serializable]
public class VeneziasVO
{
    public VeneziaVO[] venezias;
}

[Serializable]
public class VeneziaVO
{
    public int veneziaId;
    public string userName;
    public int score;
}
