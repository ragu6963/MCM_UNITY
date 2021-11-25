public class nintendods : Exhibit
{
    void Start()
    {
        exhibitId = 20;
        exhibitName = "Nintendo DS";
        year = "2004";
        producer = "Nintendo";
        content = "듀얼 스크린과 감압식 터치스크린이 탑재된 휴대용 게임기이다. 총 1억 5402만 대가 팔렸으며 2013년도에 단종되었다.";


        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 20f;
        m_MidRigHeight = 10f;
        m_minFOV = 5;
        m_maxFOV = 25;
    }
}
