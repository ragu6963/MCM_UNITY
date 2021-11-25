public class pc5150 : Exhibit
{
    void Start()
    {
        exhibitId = 12;
        exhibitName = "PC5150";
        year = "1981(4세대)";
        producer = "IBM";
        content = "개인용 컴퓨터(PC)라는 단어를 처음 사용한 컴퓨터이다. 흑백 모니터였지만 "
                + "저렴한 가격으로 당시 많은 사람들이 사용했던 IBM사의 퍼스널 컴퓨터이다.";
        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 25;
    }
}