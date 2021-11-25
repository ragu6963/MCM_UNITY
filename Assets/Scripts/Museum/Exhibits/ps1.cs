public class ps1 : Exhibit
{
    void Start()
    {
        exhibitId = 18;
        exhibitName = "PlayStation 1";
        year = "1994";
        producer = "Sony";
        content = "Sony의 첫 번째 콘솔 비디오 게임이다. 닌텐도에 대항하기 위해 발매되었다. "
                + "철권, 파이널판타지 등의 게임이 성공하고 그와 더불어 1억 249만 대의 판매량을 기록했다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;


        m_TopRigHeight = 25f;
        m_MidRigHeight = 10f;

        m_minFOV = 10;
        m_maxFOV = 30;
    }
}
