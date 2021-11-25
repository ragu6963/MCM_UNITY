public class ps5 : Exhibit
{
    void Start()
    {
        exhibitId = 27;
        exhibitName = "PlayStation 5";
        year = "2020";
        producer = "Sony";
        content = "PS 시리즈의 5번째 게임기로 가장 최근에 출시된 모델이다. 21년 7월 기준 1000만대 판매량을 돌파하였으며, "
                + "PS 기기 통틀어 가장 빠르게 1000만대 판매량을 돌파한 기기가 되었다.";

        m_MinRotate = -30;
        m_MaxRotate = 90;

        m_TopRigHeight = 25f;
        m_MidRigHeight = 10f;

        m_minFOV = 10;
        m_maxFOV = 30;
    }
}
