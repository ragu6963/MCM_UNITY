public class ibm650 : Exhibit
{
    void Start()
    {
        exhibitId = 2;
        exhibitName = "IBM650";
        year = "1954(2세대)";
        producer = "IBM";
        content = "마그네틱 드럼장치로 데이터를 처리하는 IBM 650은 초기의 디지털 컴퓨터이자 세계 최초의 "
                + "대량 생산 컴퓨터였습니다. 마그네틱 드럼은 금속제의 원통 표면에 자성물질을 입혀서, 그것을 "
                + "회전시킴으로써 정보를 기억시키고 읽어낼 수 있는 장치입니다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;

        m_TopRigHeight = 30f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 50;
    }
}