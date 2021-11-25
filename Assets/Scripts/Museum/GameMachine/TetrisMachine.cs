public class TetrisMachine : Exhibit
{
    void Start()
    {
        content = "1984년 러시아의 천재 프로그래머 알렉세이 파지노프가 만든 퍼즐 게임. " +
            "기네스북에 가장 많이 이식된 게임 과 공식 / 비공식적으로 가장 많은 아류작이 나온 게임으로 등재돼있다.";
        helpmessage = "방향키와 스페이스바를 이용해 \n테트로미노(블럭)을 조종합니다.\n" +
            "한 줄을 채우면 블럭이 사라지고, 블럭이 꼭대기에 닿으면 게임이 끝납니다.\n" +
            "최대한 많은 점수를 얻어보세요.";
    }
    public override void StartGame()
    {
        TGameManager.Instance.ActivateTetris(true);
    }
    public override void RequestGameRank()
    {
        TGameManager.Instance.RequestScore();
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
