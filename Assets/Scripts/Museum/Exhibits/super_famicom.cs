public class super_famicom : Exhibit
{
    void Start()
    {
        exhibitId = 17;
        exhibitName = "수퍼패미콤";
        year = "1990";
        producer = "Nintendo";
        content = "패미콤의 후속작으로 보다 향상된 성능을 가진 16비트 거치형 게임기이다. "
                + "총 판매량은 4910만 대로 2003년에 단종되었다.";


        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 20f;
        m_MidRigHeight = 10f;
        m_minFOV = 5;
        m_maxFOV = 25;
    }
}
