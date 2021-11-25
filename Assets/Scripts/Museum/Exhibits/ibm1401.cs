public class ibm1401 : Exhibit
{
    void Start()
    {
        exhibitId = 3;
        exhibitName = "IBM1401";
        year = "1959(2세대)";
        producer = "IBM";
        content = "대한민국에서 IBM 1401은 최초로 공식적으로 도입한 컴퓨터라는 의미가 있다. "
                + "가변 워드 길이를 채택하였으며 이전의 컴퓨터에 비해 진공관 수를 줄이고 트랜지스터 비율을 높여 전체 부피를 줄였다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;

        m_TopRigHeight = 25f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 50;
    }
}