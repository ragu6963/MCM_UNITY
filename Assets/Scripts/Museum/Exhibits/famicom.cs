public class famicom : Exhibit
{
    void Start()
    {
        exhibitId = 14;
        exhibitName = "패미콤";
        year = "1983";
        producer = "Nintendo";
        content = "닌텐도에서 처음으로 출시한 카트리지 교환식 8비트 가정용 거치형 게임기이다. "
                + "슈퍼 마리오브라더스의 발매로 폭발적인 인기를 끌었다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 20f;
        m_MidRigHeight = 10f;
        m_minFOV = 5;
        m_maxFOV = 25;
    }
}
