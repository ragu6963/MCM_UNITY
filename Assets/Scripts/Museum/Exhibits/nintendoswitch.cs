public class nintendoswitch : Exhibit
{
    void Start()
    {
        exhibitId = 25;
        exhibitName = "Nintendo Swtich";
        year = "2017";
        producer = "Nintendo";
        content = "휴대용 게임기와 유사한 형태이나 버튼이 위치한 양 옆은 본체에서 분리가 가능한 Joy-Con으로 이루어져있다. "
                + "본체는 전용 독에 장착하여 TV와 연결하여 게임을 즐길 수 있다.";


        m_MinRotate = 0;
        m_MaxRotate = 180;

        m_TopRigHeight = 20f;
        m_MidRigHeight = 10f;
        m_minFOV = 5;
        m_maxFOV = 25;
    }
}
