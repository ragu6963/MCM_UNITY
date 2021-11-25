public class XYMouseMachine : Exhibit
{
    void Start()
    {
        content = "X-Y 좌표표시기(마우스)의 원리를 이해해보기 위한 게임입니다.";
        helpmessage = "수평 혹은 수직 마우스를 클릭한 상태로 드래그를 하면 캐릭터를 조종할 수 있습니다.\n" +
            "장애물에 부딪히지 않고, 최대한 빠르게 게임을 끝내보세요.";
    }
    public override void StartGame()
    {
        MouseManager.Instance.ActivateXY();
    }
    public override void RequestGameRank()
    {
        MouseManager.Instance.RequestScore();
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

