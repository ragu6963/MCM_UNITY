public class eniac : Exhibit
{
    void Start()
    {
        exhibitId = 0;
        exhibitName = "Eniac";
        year = "1946(1세대)";
        producer = "에커트와 모클리";
        content = "원래의 목적은 군용으로, 포탄의 탄도학 계산을 위해 개발되었다." +
            " 제2차 세계 대전이 한창이던 1943년에 병기국 소속 미육군 소장 글래디온 반즈의 의뢰로 개발이 시작되었지만, " +
            "완성될 때 즈음에는 종전인 1945년 9월 2일 이후이었기에 전쟁에서는 활용되지 못했다." +
            "(무게가 무려 30톤..)";

        m_MinRotate = -90;
        m_MaxRotate = 90;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 50;
    }
}
