using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VWordManager : Singleton<VWordManager>
{
    // Word 저장공간
    List<string> WordList = new List<string>();
    // 오브젝트 풀 관련
    List<GameObject> WordObjectPool = new List<GameObject>();
    public GameObject WordPrefab;
    public GameObject WordSpawnRoot;
    const float Min = 200f, Max = 1100f;
    const float PosY = 845f;
    private int PoolIdx;
    // Word Match 관련
    public Dictionary<string, bool> WordMatch = new Dictionary<string, bool>();

    private void Awake()
    {
        InitWordList();
        InitWordPool();
    }
   

    // 입력된 문자열 정답 체크
   public void CheckWord(string input)
    {
        // 키로 등록되어있는 문자열이고 현재 활성화 되어있다면
        if(WordMatch.ContainsKey(input) && WordMatch[input])
        {
            for(int i = 0; i < PoolIdx; ++i)
            {
                if (WordObjectPool[i].GetComponent<Text>().text == input)
                {
                    WordObjectPool[i].SetActive(false); // 비활성화
                    WordMatch[input] = false; // 비활성화 상태 저장
                    VGameManager.Instance.ScoreUp(100);
                }
            }
        }
    }

    // 게임 정보 초기화
    public void Init()
    {
        PoolIdx = 0;
        ResetPool();
        WordObjectPool.Clear();
        WordMatch.Clear();
        InitWordPool();
        ShuffleWord();
    }

    private void ResetPool()
    {
        int cnt = WordSpawnRoot.transform.childCount;
        if (cnt == 0) return;
        for (int i = cnt - 1; i >= 0; --i)
        {
            Destroy(WordSpawnRoot.transform.GetChild(i).gameObject);
        }
    }

    public void ShuffleWord()
    {
        int a, b;
        //0-49, 1-48,
        for(int i = 0; i < WordObjectPool.Count; ++i)
        {
            a = Random.Range(0, WordObjectPool.Count);
            b = Random.Range(0, WordObjectPool.Count);
            GameObject tmp = WordObjectPool[a];
            WordObjectPool[a] = WordObjectPool[b];
            WordObjectPool[b] = tmp;
        }
    }

    // Word 오브젝트 반환
    public GameObject GetWordObject()
    {
        VGameManager.Instance.fallingSpeed += 0.1f;
        WordMatch[WordObjectPool[PoolIdx].GetComponent<Text>().text] = true;
        if (PoolIdx < 100) return WordObjectPool[PoolIdx++];
        else return null;
    }


    // Word Pool 생성
    private void InitWordPool()
    {
        Vector3 vec;
        for(int i = 0; i < WordList.Count; ++i)
        {
            float PosX = Random.Range(Min, Max);
            vec = new Vector3(PosX, PosY, 0f);

            GameObject go = Instantiate(WordPrefab, vec, Quaternion.identity);
            go.transform.SetParent(WordSpawnRoot.transform);
            go.GetComponent<Text>().text = WordList[i];
            go.SetActive(false);

            WordMatch.Add(WordList[i], false);
            WordObjectPool.Add(go);
        }
    }

    // Word List 생성
    private void InitWordList()
    {
        //10
        WordList.Add("공기");
        WordList.Add("어린이집");
        WordList.Add("고기");
        WordList.Add("고슴도치");
        WordList.Add("서양");
        WordList.Add("바른생활");
        WordList.Add("지나치다");
        WordList.Add("가져오다");
        WordList.Add("냄새");
        WordList.Add("오토바이");
        //20
        WordList.Add("여기다");
        WordList.Add("파인애플");
        WordList.Add("공연");
        WordList.Add("코딩");
        WordList.Add("내놓다");
        WordList.Add("떼다");
        WordList.Add("만들어지다");
        WordList.Add("심각하다");
        WordList.Add("신묘장구대다라니");
        WordList.Add("준비");
        //30
        WordList.Add("고운점박이푸른부전나비");
        WordList.Add("모시금자라남생이잎벌레 ");
        WordList.Add("사슴뿔마른가지싸리버섯");
        WordList.Add("자바");
        WordList.Add("계속되다");
        WordList.Add("구월");
        WordList.Add("맑다");
        WordList.Add("소년");
        WordList.Add("소식");
        WordList.Add("바이올린");
        //40
        WordList.Add("싸피");
        WordList.Add("하늘의 별 따기");
        WordList.Add("누워서 떡 먹기");
        //WordList.Add("가는 말이 고와야 오는 말도 곱다");
        WordList.Add("박물관");
        WordList.Add("이미지");
        WordList.Add("움직임");
        WordList.Add("썰다");
        WordList.Add("반면");
        WordList.Add("나름");
        WordList.Add("대답하다");
        //50
        WordList.Add("기록");
        WordList.Add("경향");
        WordList.Add("결정");
        WordList.Add("영어");
        WordList.Add("살리다");
        WordList.Add("게슴츠레");
        WordList.Add("노인");
        WordList.Add("스웨터");
        WordList.Add("키보드");
        WordList.Add("게르마늄");
        // 60
        WordList.Add("각촉");
        WordList.Add("베르나르 베르베르");
        WordList.Add("국수주의");
        WordList.Add("오버라이딩");
        WordList.Add("오버로딩");
        WordList.Add("목가적");
        WordList.Add("변증법");
        WordList.Add("박탈");
        WordList.Add("외람되다");
        WordList.Add("탄소");
        //70
        WordList.Add("에니악");
        WordList.Add("도자기");
        WordList.Add("삼성");
        WordList.Add("유니콘");
        WordList.Add("예속적");
        WordList.Add("거풀거리다");
        WordList.Add("우수리");
        WordList.Add("마우스");
        WordList.Add("오딧세이");
        WordList.Add("닌텐도");
        //80
        WordList.Add("산");
        WordList.Add("토끼");
        WordList.Add("괴짜");
        WordList.Add("프로젝트");
        WordList.Add("메타버스");
        WordList.Add("가상현실");
        WordList.Add("자바스크립트");
        WordList.Add("데이터베이스");
        WordList.Add("운영체제");
        WordList.Add("네트워크");
        //90
        WordList.Add("건담");
        WordList.Add("영화");
        WordList.Add("차은우");
        WordList.Add("호루라기");
        WordList.Add("칼륨");
        WordList.Add("캡사이신");
        WordList.Add("마그네슘");
        WordList.Add("마늘");
        WordList.Add("숭례문");
        WordList.Add("금동비로자나불좌상");
        //100
        WordList.Add("택하다");
        WordList.Add("졸라대다");
        WordList.Add("갤럭시폴드");
        WordList.Add("같은 값이면 다홍치마");
        WordList.Add("아이스크림");
        WordList.Add("흡수체");
        WordList.Add("원자가각");
        WordList.Add("비스코스");
        WordList.Add("시각회로");
        WordList.Add("전압 개폐 통로");
    }
}
