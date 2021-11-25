public class adbmouse : Exhibit
{
    void Start()
    {
        exhibitId = 8;
        exhibitName = "ADB Mouse";
        year = "1972";
        producer = "로지텍";
        content = "최초의 볼마우스로 로지텍이 1972년에 출시하였다. 볼 하나를 놓고 그 볼로 가로세로의 위치를 감지하는 방식이다. " 
                + "(볼 분실 문제가 심각했다고..)";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 15f;
        m_MidRigHeight = 0f;

        m_minFOV = 5;
        m_maxFOV = 15;
    }
}