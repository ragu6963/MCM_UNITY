public class nintendowill : Exhibit
{
    void Start()
    {
        exhibitId = 22;
        exhibitName = "Nintendo Wii";
        year = "2006";
        producer = "Nintendo";
        content = "모션 센서가 적용된 체감형 컨트롤러를 탑재한 게임기로 대박을 터뜨리며 당대 콘솔 시장의 승자로 자리매김했다.";


        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 20f;
        m_MidRigHeight = 10f;
        m_minFOV = 5;
        m_maxFOV = 25;
    }
}
