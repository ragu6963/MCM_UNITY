using Photon.Pun;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : Singleton<ButtonManager>
{
    public Button CloseGuestButton;
    public Button OpenCreateBoardButton;

    public Button PrevCommentButton;
    public Button NextCommentButton;

    public Button CloseCreateBoardButton;
    public Button CreateCommentButton;

    public Button CloseExhibitContentButton;

    public Button CloseGameRankBoardButton;
    public Button CloseGameInfoButton;


    private void Start()
    {
        CloseGuestButton.onClick.AddListener(CloseGuestBook);

        PrevCommentButton.onClick.AddListener(PrevComment);
        NextCommentButton.onClick.AddListener(NextComment);

        CloseCreateBoardButton.onClick.AddListener(CloseCreateBoard);
        CreateCommentButton.onClick.AddListener(CreateComment);

        CloseExhibitContentButton.onClick.AddListener(CloseExhibitInfo);

        CloseGameRankBoardButton.onClick.AddListener(CloseGameRankBoard);
        CloseGameInfoButton.onClick.AddListener(CloseGameInfo);

    }
    // 방명록 보드 닫기 
    public void CloseGuestBook()
    {
        PlayerManager.Instance.CloseGuestBook();
    }
    // 방명록 의견 작성 보드 닫기
    public void CloseCreateBoard()
    {
        PlayerManager.Instance.CloseCreateGuestBook();
    }
    // 방명록 작성 완료
    public void CreateComment()
    {
        ApiManager.Instance.CreateComment();
        CloseCreateBoard();
    }
    // 방명록 다음 의견 불러오기
    public void NextComment()
    {
        bool isLastPage = ApiManager.Instance.NextComment();
        if (isLastPage)
            CanvasManager.Instance.SetControllCommentButtons(true, false);
        else
            CanvasManager.Instance.SetControllCommentButtons(true, true);
    }
    // 방명록 이전 의견 불러오기
    public void PrevComment()
    {
        bool isFirstPage = ApiManager.Instance.PrevComment();
        if (isFirstPage)
            CanvasManager.Instance.SetControllCommentButtons(false,true);
        else
            CanvasManager.Instance.SetControllCommentButtons(true, true);
    } 
    // 전시물 정보 닫기
    public void CloseExhibitInfo()
    { 
        PlayerManager.Instance.CloseExhibitInfo();
    }
    // 게임 순위 보드 닫기
    public void CloseGameRankBoard()
    {
        PlayerManager.Instance.CloseGameRank();
    }
    public void CloseGameInfo()
    {
        PlayerManager.Instance.CloseGameInfo();
    }
}
