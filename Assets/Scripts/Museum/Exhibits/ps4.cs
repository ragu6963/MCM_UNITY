public class ps4 : Exhibit
{
    void Start()
    {
        exhibitId = 23;
        exhibitName = "PlayStation 4";
        year = "2013";
        producer = "Sony";
        content = "PS 시리즈의 4번째 게임기로 전 세계 출하량 1억 1590만 대 달성 및 역대 모든 게임기 중 가장 빠른 기록으로 판매된 게임기다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;


        m_TopRigHeight = 25f;
        m_MidRigHeight = 10f;

        m_minFOV = 10;
        m_maxFOV = 30;
    }
}
