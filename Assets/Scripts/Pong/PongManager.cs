using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PongManager : Singleton<PongManager>
{
    private int PlayerScore;
    private int MaxRound;
    private string BaseText = "최대 라운드 - Round ";

    public Text MaxRoundText;
    public GameObject Pong;

    public GameObject HelpText;
    public GameObject GameBoard;

    public GameObject ball;

    public GameObject PlayerPaddle;
    public GameObject PlayerWall;

    public GameObject ComputerPaddle;
    public GameObject ComputerWall;

    public GameObject PlayerScoreText;

    public Button StartButton;
    public Button ExitButton;

    private PongVO[] pongs;

    private string API_URL = "/api/v1/pong";

    private void Awake()
    {
        InitGame();
        PlayerScore = 1;
        MaxRound = 1;
        SetMaxRoundText(MaxRound);
    }
    private void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        ExitButton.onClick.AddListener(ExitPong);
    }
    private void SetMaxRoundText(int MaxRound)
    {
        MaxRoundText.text = BaseText + $"{MaxRound}";
    }
    public void ExitPong()
    {
        PlayerScore = 0;
        PlayerScoreText.GetComponent<TextMeshProUGUI>().text = PlayerScore.ToString();
        MuseumManager.Instance.ActiveMuseum();
        CanvasManager.Instance.OpenHelpText();
        PlayerManager.Instance.ActivatePlayer();
        DeactivatePong();
    }
    public void ActivatePong()
    {
        Pong.SetActive(true);
    }
    public void DeactivatePong()
    {
        Pong.SetActive(false);
        PlayerManager.Instance.EndGame();
    }
    public void StartGame()
    {
        HelpText.SetActive(false);
        GameBoard.SetActive(true);
    }
    public void InitGame()
    {
        HelpText.SetActive(true);
        GameBoard.SetActive(false);
    }
    public void PlayerScored()
    {
        PlayerScore += 1;
        PlayerScoreText.GetComponent<TextMeshProUGUI>().text = "ROUND " + PlayerScore.ToString();

        ball.GetComponent<Ball>().Reset();
        PlayerPaddle.GetComponent<Paddle>().Reset();
        ComputerPaddle.GetComponent<Computer>().Reset();
        ComputerPaddle.GetComponent<Computer>().IncreaseSpeed(PlayerScore);
        ball.GetComponent<Ball>().IncreaseSpeed(PlayerScore);

    }
    public void ComputerScored()
    {
        ResetPosition();
        SetMaxRoundText(PlayerScore);
        CreateScore(PlayerScore);
        PlayerScore = 1;
        PlayerScoreText.GetComponent<TextMeshProUGUI>().text = "ROUND " + PlayerScore.ToString();
    }
    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        ball.GetComponent<Ball>().ResetSpeed();
        PlayerPaddle.GetComponent<Paddle>().Reset();
        ComputerPaddle.GetComponent<Computer>().Reset();
        ComputerPaddle.GetComponent<Computer>().ResetSpeed();
        InitGame();
    }

    // 점수 기록
    public void CreateScore(int round)
    {
        string url = API_URL;
        string userName = PhotonNetwork.NickName;
        PongDto PongDto = new PongDto
        {
            userName = userName,
            round = round,
        };
        string json = JsonUtility.ToJson(PongDto);
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
            string json = "{\"pongs\":" + response + "}";
            pongs = JsonUtility.FromJson<PongsVO>(json).pongs;
            string gamerank = "";
            for (int i = 0; i < pongs.Length; i++)
            {
                PongVO pong = pongs[i];
                gamerank += $"{i + 1}. {pong.userName}(Round {pong.round})\n";
            }
            CanvasManager.Instance.SetGameRankText("Pong", gamerank);
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
public class PongDto
{
    public string userName;
    public int round;
}

[Serializable]
public class PongsVO
{
    public PongVO[] pongs;
}

[Serializable]
public class PongVO
{
    public int pongId;
    public string userName;
    public int round;
}
