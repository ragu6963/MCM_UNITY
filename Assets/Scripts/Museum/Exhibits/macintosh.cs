public class macintosh : Exhibit
{
    void Start()
    {
        exhibitId = 15;
        exhibitName = "매킨토시";
        year = "1984(4세대)";
        producer = "애플";
        content = "매킨토시는 당시 유행하던 명령 줄 인터페이스 대신 그래픽 사용자 인터페이스(GUI)와 "
                + "마우스를 채용해 상업적으로 성공한 최초의 개인용 컴퓨터였다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 25;
    }
}
