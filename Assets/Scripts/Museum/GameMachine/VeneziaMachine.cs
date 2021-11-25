
public class VeneziaMachine : Exhibit
{
    void Start()
    {
        content = "한메 타자교사 1.0 에 있던 게임으로, 한컴타자의 산성비 게임의 원조 게임입니다.";
        helpmessage = "떨어지는 단어가 물에 빠지기 전에\n" +
            "단어를 입력해서 최대한 많은 점수를 쌓아보세요.";
    }
    public override void StartGame()
    {
        VGameManager.Instance.ActiveVenezia(true);
    }
    public override void RequestGameRank()
    {
        VGameManager.Instance.RequestScore();
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
