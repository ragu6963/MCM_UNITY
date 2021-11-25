public class ps2 : Exhibit
{
    void Start()
    {
        exhibitId = 19;
        exhibitName = "PlayStation 2";
        year = "2000";
        producer = "Sony";
        content = "PS 시리즈의 2번째 게임기로 발매된 거치형 게임기이다. 한국은 2002년에 발매되었다. "
                + "상업적으로 가장 성공한 기기라고 할 수 있다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;


        m_TopRigHeight = 25f;
        m_MidRigHeight = 10f;

        m_minFOV = 10;
        m_maxFOV = 30;
    }
}
