public class cdc6600 : Exhibit
{
    void Start()
    {
        exhibitId = 4;
        exhibitName = "CDC6600";
        year = "1964(3세대)";
        producer = "Control Data Corporation";
        content = "CDC 6000시리즈의 플래그십 메인프레임 컴퓨터로, 최초의 슈퍼컴퓨터라 일컬어진다. " 
                + "최대 3메가플롭스의 속도를 내며 1964년부터 1969년까지 세계에서 가장 빠른 컴퓨터였다.";

        m_MinRotate = -90;
        m_MaxRotate = 90;

        m_TopRigHeight = 25f;
        m_MidRigHeight = 0f;

        m_minFOV = 10;
        m_maxFOV = 50;
    }
}