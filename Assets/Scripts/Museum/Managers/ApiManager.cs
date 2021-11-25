using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : Singleton<ApiManager>
{
    private CommentVO[] comments;
    private int m_length;
    private int page;
    private int last;

    private string API_URL;

    private void Awake()
    {
       API_URL = "/api/v1/"; 
    }

    public void InitExhibitCommentBoard()
    {
        m_length = comments.Length;
        page = 0;
        last = m_length - 1;
        DrawComment();
    }
    public void RequestComment()
    {
        string url = API_URL + "comments/" + ExhibitVO.exhibitId;
        StartCoroutine(GetComments(url, response =>
        {
            string json = "{\"comments\":" + response + "}";
            comments = JsonUtility.FromJson<CommentsVO>(json).comments;
            InitExhibitCommentBoard();
        }));
    }
    IEnumerator GetComments(string url, System.Action<string> callback)
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
    public void CreateComment()
    {
        string url = API_URL + "comments";
        int exhibitId = ExhibitVO.exhibitId;
        string content = CanvasManager.Instance.GetCommentInputText();
        string userName = PhotonNetwork.NickName;
        string password = "1234";
        CommentDto commentDto = new CommentDto
        {
            exhibitId = exhibitId,
            content = content,
            userName = userName,
            password = password,
        };
        Debug.Log(commentDto);

        string json = JsonUtility.ToJson(commentDto);
        StartCoroutine(PostComment(url, json, response =>
        {
            if (response)
            {
                RequestComment();
                CanvasManager.Instance.SetCommentInputText("");
            }
        }));
    }

    IEnumerator PostComment(string url, string json, System.Action<bool> callback)
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
    public void DrawComment()
    {
        string content = "";
        string username = "";
        bool prevButtonStatus = false;
        bool nextButtonStatus = true;

        if (m_length >= 1)
        {
            CommentVO comment = comments[page];
            content = comment.content;
            username = comment.userName; 
            CanvasManager.Instance.SetComment(content, username);
        }
        if (m_length == 1)
        {
            prevButtonStatus = false;
            nextButtonStatus = false;
        }
        if(m_length == 0)
        {
            prevButtonStatus = false;
            nextButtonStatus = false;
        }
        CanvasManager.Instance.SetControllCommentButtons(prevButtonStatus, nextButtonStatus);
    }

    public bool NextComment()
    {
        page += 1;
        DrawComment();
        if (page == last) return true;

        return false;
    }
    public bool PrevComment()
    {
        page -= 1;
        DrawComment();
        if (page == 0) return true;

        return false;
    }
}


[Serializable]
public class CommentsVO
{
    public CommentVO[] comments;
}

[Serializable]
public class CommentVO
{
    public int commentId;
    public DateTime commentTime;
    public int exhibitId;
    public string userName;
    public string content;
    public int password;
}  
[Serializable]
public class CommentDto
{
    public int exhibitId;
    public string userName;
    public string content;
    public string password;
}

public static class ExhibitVO
{
    public static int exhibitId;
    public static string exhibitName;
}