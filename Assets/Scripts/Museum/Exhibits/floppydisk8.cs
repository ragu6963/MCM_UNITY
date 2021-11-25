public class floppydisk8 : Exhibit
{
    void Start()
    {
        exhibitId = 7;
        exhibitName = "FloppyDisk 8";
        year = "1971";
        producer = "IBM";
        content = "초창기 플로피 디스크로 용량은 50KB였다. 5.25인치 플로피디스크가 나오면서 얼마 지나지 않아 단종되었다.";

        m_MinRotate = -180;
        m_MaxRotate = 180;

        m_TopRigHeight = 15f;
        m_MidRigHeight = 5f;

        m_minFOV = 5;
        m_maxFOV = 20;
    }
}