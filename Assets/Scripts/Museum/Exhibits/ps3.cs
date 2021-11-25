public class ps3 : Exhibit
{
    void Start()
    {
        exhibitId = 21;
        exhibitName = "PlayStation 3";
        year = "2006";
        producer = "Sony";
        content = "PS 시리즈의 3번째 게임기로 초고성능 CPU를 탑재한 가정용 게임기이다. "
                + "하지만 PS2와 큰 차별점을 두지 못해 많은 인기를 이끌지는 못했다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;


        m_TopRigHeight = 25f;
        m_MidRigHeight = 10f;

        m_minFOV = 10;
        m_maxFOV = 30;
    }
}
