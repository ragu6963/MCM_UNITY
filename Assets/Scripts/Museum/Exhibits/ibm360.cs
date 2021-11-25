public class ibm360 : Exhibit
{
    void Start()
    {
        exhibitId = 5;
        exhibitName = "IBM360";
        year = "1964(3세대)";
        producer = "IBM";
        content = "메인프레임 컴퓨터 시스템 계열이다. 상업용, 과학용 목적을 포함한 완전한 범위의 초기 다목적 컴퓨터이다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;

        m_TopRigHeight = 25f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 50;
    }
}