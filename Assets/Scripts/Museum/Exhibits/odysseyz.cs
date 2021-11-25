public class odysseyz : Exhibit
{
    void Start()
    {
        exhibitId = 26;
        exhibitName = "Odyssey Z";
        year = "2018";
        producer = "Samsung";
        content = "삼성전자의 고성능 게이밍 PC 브랜드인 오딧세이Z이다. 2017년 오딧세이 노트북 출시이후로 현재까지 많은 사랑을 받고 있다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 25;
    }
}
