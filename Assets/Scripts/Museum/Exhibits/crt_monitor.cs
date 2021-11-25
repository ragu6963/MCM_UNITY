public class crt_monitor : Exhibit
{
    void Start()
    {
        exhibitId = 16;
        exhibitName = "CRT모니터";
        year = "1987";
        producer = "칼 브라운 교수";
        content = "1987년 독일 스트라스부르크 대학의 칼 브라운 교수가 발명한 이래 디스플레이 장치의 대명사로 불려왔다. "
                + "가장 오래된 디스플레이 장치로, 브라운관이라고도 불린다. 잔고장이 적고 응답시간이 빠르다는 장점이 있지만, "
                + "디스플레이가 두껍고 전력 소비량이 많다는 단점이 있다. (Cathode-Ray Tube)";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 25f;
        m_MidRigHeight = 5f;

        m_minFOV = 10;
        m_maxFOV = 25;
    }
}
