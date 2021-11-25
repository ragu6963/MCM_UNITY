using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TGameManager : Singleton<TGameManager>
{
    // 게임 시작 버튼
    public GameObject tetrisMenu; // 테트리스 메뉴 root오브젝트
    public GameObject boundary; // 테트리스 시작할 때만 보이는 경계선
     

    public GameObject inGame; // 테트리스 시작 시 Info

    // Activate 시 활성화 
    public GameObject tetrisObject;
    // 가로, 세로
    private const int w = 10;
    public int getW() { return w; }
    private const int h = 20;
    public int getH() { return h; }
    // 테트리스 관리 배열
    public Transform[,] blockMap = new Transform[w, h];
    // 게임 오버 플래그
    [HideInInspector]
    public bool isGameOver = false;
    public GameObject gameOverObj;
    public Text countDownTxt; // 게임 오버 시 카운트 다운
    private float countDownTime = 5f;
    // 점수 
    public Text scoreTxt;
    private int score = 0;
    private float time = 0f;
    private int scorePoint = 1000;
    [HideInInspector]
    public float fallingSpeed = 1f;
    [HideInInspector]
    public bool isTetrisStart = false;

    private TetrisVO[] tetriss;

    private string API_URL = "/api/v1/tetris";

    void Update()
    {
        if (!isTetrisStart) return;
        if (!isGameOver)
        {
            time += Time.deltaTime;
            if (time >= 1f) // 1초마다 포인트 업
            {
                score += 10;
                scoreTxt.text = score.ToString();
                time = 0f;
            }
            fallingSpeed = 1f - (float)((score / 5000) * 0.1);
            if (fallingSpeed <= 0.05f) fallingSpeed = 0.05f;
        }
        else // isGameOver
        {

            // 5초 뒤에 이동.
            if (countDownTime > 0) countDownTime -= Time.deltaTime;
            else // 인 게임에서 게임 오버시.
            {
                InGameToggle(false); // 인게임 UI를 꺼준다.
                TetrisMenuToggle(true); // 타이틀을 켜준다.
                Reset();
            }
            countDownTxt.text = Mathf.Ceil(countDownTime).ToString();
        }

    }

    public Vector2 roundVec2(Vector2 vec)
    {
        return new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y));
    }

    public bool isInMap(Vector2 vec)
    {
        return 0 <= vec.x && vec.x < w && 0 <= vec.y;
    }

    public void checkAndDeleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                fallRow(y + 1);
                --y;
                AddScore();
            }
        }
    }
    private bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (blockMap[x, y] == null) return false;
        }
        return true;
    }

    private void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(blockMap[x, y].gameObject);
            blockMap[x, y] = null;
        }
    }

    private void fallRow(int y)
    {
        for (int i = y; i < h; ++i)
            fall(i);
    }

    private void fall(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (blockMap[x, y] != null)
            {
                blockMap[x, y - 1] = blockMap[x, y];
                blockMap[x, y] = null;
                blockMap[x, y - 1].position += new Vector3(0f, -1f, 0f);
            }
        }
    }

    public void gameOver()
    {
        isGameOver = true;
        gameOverObj.SetActive(true);
    }

    private void AddScore()
    {
        if (!isGameOver)
        {
            score += scorePoint;
            scoreTxt.text = score.ToString();
        }
    }

    public void ActivateTetris(bool flag) // 끄고 키는 메소드
    {
        tetrisObject.SetActive(flag);
        CameraManager.Instance.mainChine.enabled = !flag;
        CameraManager.Instance.TetrisCamera.enabled = flag;
        if(!flag)
            MuseumManager.Instance.ActiveMuseum();
        else
        {
            // 게임 타이틀 온
            tetrisMenu.SetActive(true);
        }
    }
    public void GameStart() // 타이틀에서 start 눌리면 실행.
    {
        InGameToggle(true);
        TetrisMenuToggle(false);
        isTetrisStart = true;
        SpawnManager.Instance.Init(); // spawn 시작
    }
    private void InGameToggle(bool flag)
    {
        boundary.SetActive(flag);
        inGame.SetActive(flag);
    }
    private void TetrisMenuToggle(bool flag) => tetrisMenu.SetActive(flag);
    public void GameExit()
    {
        if (isGameOver == true) Reset();
        ActivateTetris(false); 
        PlayerManager.Instance.EndGame();
    }

    private void Reset()
    {
        for (int i = 0; i < w; ++i)
        {
            for (int j = 0; j < h; ++j)
            {
                blockMap[i, j] = null;
            }
        }
        CreateScore(score);
        isGameOver = false;
        countDownTime = 5f;
        score = 0;
        scoreTxt.text = "0";
        time = 0f;
        isTetrisStart = false;
        SpawnManager.Instance.Reset();
        gameOverObj.SetActive(false);
    }


    // 점수 기록
    public void CreateScore(int score)
    {
        string url = API_URL;
        string userName = PhotonNetwork.NickName;
        TetrisDto TetrisDto = new TetrisDto
        {
            userName = userName,
            score = score,
        };
        string json = JsonUtility.ToJson(TetrisDto);
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
            string json = "{\"tetriss\":" + response + "}";
            tetriss = JsonUtility.FromJson<TetrisVOs>(json).tetriss;
            string gamerank = "";
            for (int i = 0; i < tetriss.Length; i++)
            {
                TetrisVO tetris = tetriss[i];
                gamerank += $"{i + 1}. {tetris.userName}({tetris.score}점)\n";
            }
            CanvasManager.Instance.SetGameRankText("Tetris", gamerank);
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
public class TetrisDto
{
    public string userName;
    public int score;
}

[Serializable]
public class TetrisVOs
{
    public TetrisVO[] tetriss;
}

[Serializable]
public class TetrisVO
{
    public int tetrisId;
    public string userName;
    public int score;
}
