public class apple2 : Exhibit
{
    void Start()
    {
        exhibitId = 10;
        exhibitName = "Apple 2";
        year = "1977(4세대)";
        producer = "애플";
        content = "1977년에 만든 최초의 일체형 개인용 컴퓨터이다. 가장 초창기의 개인용 컴퓨터 중 하나이자 "
                + "그 중에서도 가장 성공한 제품으로 1970년대 말부터 80년대 전반에 걸쳐 개인용 컴퓨터 붐을 이끈 주역이었다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 25;
    }
}