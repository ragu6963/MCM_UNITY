public class PongMachine : Exhibit
{
    void Start()
    {
        content = "최초의 아케이드 비디오 게임이자 최초의 스포츠 아케이드 비디오 게임 중 하나이다.\n" +
            "단순한 2차원 그래픽을 차용한 탁구 스포츠 게임이다";
        helpmessage = "위 아래 방향키를 이용해  Paddle 을\n" +
                    "조종합니다.\n" +
                    "가능한 높은 라운드까지 도달해보세요.";
    }
    public override void StartGame()
    {
        PongManager.Instance.ActivatePong();
    }
    public override void RequestGameRank()
    {
        PongManager.Instance.RequestScore();
    }
    public override void OpenRankBoard()
    {
        CanvasManager.Instance.CloseHelpText();
        CanvasManager.Instance.OpenGameRankBoard();
    }
    public override void CloseRankBoard()
    {
        CanvasManager.Instance.OpenHelpText();
        CanvasManager.Instance.CloseGameRankBoard();
    }
}
