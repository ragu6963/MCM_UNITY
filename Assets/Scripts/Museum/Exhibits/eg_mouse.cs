public class eg_mouse : Exhibit
{
    void Start()
    {
        exhibitId = 6;
        exhibitName = "EG Mouse";
        year = "1968";
        producer = "엥겔바트";
        content = "미국의 과학자인 더글라스 엥겔바트가 개발한 마우스로 최초의 컴퓨터 마우스이다. "
                + "X축 Y축 2개의 휠이 돌면서 모니터 위치에 맞게 커서를 가져다 놓는다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 15f;
        m_MidRigHeight = 0f;

        m_minFOV = 5;
        m_maxFOV = 15;
    }
}