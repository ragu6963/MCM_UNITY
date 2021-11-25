using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [DllImport("__Internal")]
    private static extern void ShowChat();
    [DllImport("__Internal")]
    private static extern void HideChat();
    public GameObject Player { get; set; }
    public bool IsActivePlayer { get; set; }
    public bool IsInExhibitArea { get; set; }
    public Exhibit m_CollisionObject { get; set; }
    public AudioSource BGM;
    public Guid guid;
    private void Update()
    {
        if (MuseumManager.Instance.IsActive == false) return;

        if (IsInExhibitArea)
        {
            if (IsActivePlayer)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    if (m_CollisionObject.CompareTag("Exhibit")) StartViewExhibit();
                    if (m_CollisionObject.CompareTag("Game")) StartGame();
                    if (m_CollisionObject.CompareTag("GuestBook")) OpenGuestBook();
                    if (m_CollisionObject.CompareTag("Video")) StartVideo();
                }
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    if (m_CollisionObject.CompareTag("Exhibit")) OpenExhibitInfo();
                    if (m_CollisionObject.CompareTag("GuestBook")) OpenCreateGuestBook();
                    if (m_CollisionObject.CompareTag("Game")) OpenGameInfo();
                }
                if (Input.GetKeyUp(KeyCode.R))
                {
                    if (m_CollisionObject.CompareTag("Game")) OpenGameRank();
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.X) )
                {
                    if (m_CollisionObject.CompareTag("Exhibit"))
                        StopViewExhibit();
                }
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    if (m_CollisionObject.CompareTag("Video"))
                        VideoManager.Instance.VideoStop();
                }
            }
        }
    }
    // 게임 정보 패널 열기
    private void OpenGameInfo()
    {
        DeactivatePlayer();
        MuseumManager.Instance.DeactiveMuseum();
        CanvasManager.Instance.CloseHelpText();
        CanvasManager.Instance.OpenGameInfo();
        m_CollisionObject.StopBlink();
    }
    // 게임 정보 패널 닫기
    public void CloseGameInfo()
    {
        ActivatePlayer();
        MuseumManager.Instance.ActiveMuseum();
        CanvasManager.Instance.OpenHelpText();
        CanvasManager.Instance.CloseGameInfo();
        m_CollisionObject.StartBlink();
    }

    // 게임 시작하기
    void StartGame()
    {
        BGM.volume = 0f;
        DeactivatePlayer();
        MuseumManager.Instance.DeactiveMuseum();
        CanvasManager.Instance.CloseHelpText();
        m_CollisionObject.StartGame();
        M_HideChat();
    }

    // 게임 끝내기
    public void EndGame()
    {
        BGM.volume = 0.15f;
        ActivatePlayer();
        MuseumManager.Instance.ActiveMuseum();
        CanvasManager.Instance.OpenHelpText();
        M_ShowChat();
    }
    // 게임 순위 보드 열기
    private void OpenGameRank()
    {
        DeactivatePlayer();
        MuseumManager.Instance.DeactiveMuseum();
        CanvasManager.Instance.CloseHelpText();
        m_CollisionObject.OpenRankBoard();
        m_CollisionObject.StopBlink();
    }
    // 게임 순위 보드 닫기
    public void CloseGameRank()
    {
        ActivatePlayer();
        MuseumManager.Instance.ActiveMuseum();
        CanvasManager.Instance.OpenHelpText();
        m_CollisionObject.CloseRankBoard();
        m_CollisionObject.StartBlink();
    }
    // 방명록 작성 보드 열기
    private void OpenCreateGuestBook()
    {
        DeactivatePlayer();
        m_CollisionObject.StopBlink();
        CanvasManager.Instance.OpenCreateGuestBook();
        CanvasManager.Instance.CloseHelpText();
    }
    // 방명록 작성 보드 닫기
    public void CloseCreateGuestBook()
    {
        ActivatePlayer();
        M_ShowChat();
        m_CollisionObject.StartBlink();
        CanvasManager.Instance.CloseCreateGuestBook();
        CanvasManager.Instance.OpenHelpText();
    }
    // 방명록 보드 열기
    private void OpenGuestBook()
    {
        DeactivatePlayer();
        m_CollisionObject.StopBlink();
        CanvasManager.Instance.OpenGuestBook();
        CanvasManager.Instance.CloseHelpText();
    }
    // 방명록 보드 닫기
    public void CloseGuestBook()
    {
        ActivatePlayer();
        m_CollisionObject.StartBlink();
        CanvasManager.Instance.CloseGuestBook();
        CanvasManager.Instance.OpenHelpText();
    }

    // 전시물 정보 패널 열기
    void OpenExhibitInfo()
    {
        DeactivatePlayer();
        m_CollisionObject.StopBlink();
        CanvasManager.Instance.CloseHelpText();
        CanvasManager.Instance.OpenExhibitContentBoard(); 
    }
    // 전시물 정보 패널 닫기
    public void CloseExhibitInfo()
    {
        ActivatePlayer();
        m_CollisionObject.StartBlink();
        CanvasManager.Instance.CloseExhibitInfo();
        CanvasManager.Instance.OpenHelpText(); ;
    }
    private void StartVideo()
    {
        BGM.volume = 0f;
        DeactivatePlayer();
        CanvasManager.Instance.VideoCanvas.SetActive(true);
        VideoManager.Instance.VideoPlay();
        M_HideChat();
    }
    // 전시물로 카메라 시점 변경
    void StartViewExhibit()
    {
        m_CollisionObject.StopBlink();
        DeactivatePlayer();
        HideGlass();
        HidePlayer();
        HideWall();
        string HelpText = "마우스 휠 : 화면 축소/확대\n마우스 드래그 : 화면 회전\nX : 전시물 그만 보기";
        CanvasManager.Instance.SetHelpText(HelpText);
        CameraManager.Instance.ActivateExhibitCam(); // 전시물 시점 카메라 활성화 
        M_HideChat();
    }
    // 플레이어로 카메라 시점 변경
    void StopViewExhibit()
    {
        m_CollisionObject.StartBlink();
        ActivatePlayer();
        ShowGlass();
        ShowPlayer();
        ShowWall();
        string HelpText = "E : 전시물 보기\nQ : 전시물 정보";
        CanvasManager.Instance.SetHelpText(HelpText);
        CameraManager.Instance.DeactivateExhibitCam();
        M_ShowChat();
    }
    // 전시물 영역 안으로 들어왔을 때
    public void EnterInExhibitArea(Exhibit CollisionExhibit)
    {
        m_CollisionObject = CollisionExhibit;
        m_CollisionObject.StartBlink();
        IsInExhibitArea = true;

        CanvasManager.Instance.OpenHelpText();
        ExhibitVO.exhibitName = m_CollisionObject.exhibitName;
        ExhibitVO.exhibitId = m_CollisionObject.exhibitId;
    }
    // 전시물 영역 밖으로 나갈 때
    public void ExitInExhibitArea()
    {
        m_CollisionObject.StopBlink();
        IsInExhibitArea = false;
        CanvasManager.Instance.OpenHelpText();
    }
    public void ShowGlass()
    {
        CameraManager.Instance.ShowGlassLayer();
    }
    public void HideGlass()
    {
        CameraManager.Instance.HideGlassLayer();
    }
    public void ShowPlayer()
    {
        CameraManager.Instance.ShowPlayerLayer();
    }
    public void HidePlayer()
    {
        CameraManager.Instance.HidePlayerLayer();
    }
    public void ShowWall()
    {
        CameraManager.Instance.ShowWallLayer();
    }
    public void HideWall()
    {
        CameraManager.Instance.HideWallLayer();
    }
    public void ActivatePlayer()
    {
        IsActivePlayer = true;
    }
    public void DeactivatePlayer()
    {
        IsActivePlayer = false;
    }

    private void M_HideChat()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        HideChat();
#endif
    }
    public void M_ShowChat()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
 WebGLInput.captureAllKeyboardInput = false;     
ShowChat();
#endif
    }
}
