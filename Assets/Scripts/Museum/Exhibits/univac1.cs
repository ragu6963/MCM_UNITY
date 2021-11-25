public class univac1 : Exhibit
{
    void Start()
    {
        exhibitId = 1;
        exhibitName = "UNIVAC1";
        year = "1951(1세대)";
        producer = "모클리&에커트";
        content = "유니박 I(UNIVersal Automatic Computer I)은 에니악(ENIAC)을 만든 모클리와 "
                + "에커트에 의해 미국에서 처음으로 만들어진 최초의 상업용 컴퓨터";

        m_MinRotate = -90;
        m_MaxRotate = 90;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 50;
    }
}