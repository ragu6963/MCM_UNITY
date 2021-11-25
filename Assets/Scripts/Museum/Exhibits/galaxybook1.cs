public class galaxybook1 : Exhibit
{
    void Start()
    {
        exhibitId = 24;
        exhibitName = "Galaxy Book 1";
        year = "2017";
        producer = "Samsung";
        content = "Samsung의 Galaxy Book 1 이다. 윈도우 OS와 분리형 키보드를 탑재한 프리미엄 태블릿이다. "
                + "사용 목적에 따라 키보드를 탈부착하며 태블릿과 노트북으로 자유자재로 변형할 수 있다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 25;
    }
}
