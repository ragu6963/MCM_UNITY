public class optical_mouse : Exhibit
{
    void Start()
    {
        exhibitId = 11;
        exhibitName = "Optical Mouse";
        year = "1980";
        producer = "스티브 커시&마우스 시스템즈";
        content = "광센서를 사용한 마우스로 마우스 바닥에 움직임을 감지하는 광센서가 달려있다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 15f;
        m_MidRigHeight = 0;

        m_minFOV = 5;
        m_maxFOV = 15;
    }
}