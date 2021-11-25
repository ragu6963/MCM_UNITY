public class floppydisk3_5 : Exhibit
{
    void Start()
    {
        exhibitId = 13;
        exhibitName = "FloppyDisk 3.5";
        year = "1982";
        producer = "SONY";
        content = "기존의 디스켓과 달리 딱딱한 플라스틱 틀과 금속 덮개를 사용하기 때문에 견고하고 갖고 다니기 쉽다. "
                + "가장 보편화된 플로피 디스크였다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 15f;
        m_MidRigHeight = 5f;

        m_minFOV = 5;
        m_maxFOV = 20;
    }
}