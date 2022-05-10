using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BarManager2 : MonoBehaviour
{
    string questName = "asdf";
    string questContents = "ads";
    int questRewards = 0;
    int questProcessivity = 0;
    int questCount = 0;

    public GameObject questPanel; // 퀘스트가 로드되는 패널
    public GameObject btnPanel; // 버튼 목록이 활성화되는 패널
    

    QuestData test;
    //public GameObject[] questObject;
    TextMeshProUGUI questText; // 컨텐츠가 들어간 퀘스트 패널의 텍스트
    TextMeshProUGUI btnText; // 버튼의 이름이 들어갈 텍스트
    Dictionary<int, QuestData> questList; // 퀘스트 리스트가 들어갈 딕셔너리
    public GameObject btnPrefab; // 버튼 생성을 위한 프리팹
    List<Dictionary<string, object>> QCell; // 엑셀 파일 csv로 만들어 불러온 것

    // Start is called before the first frame update
    void Start()
    {
        questText = gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // Q패널의 퀘스트 텍스트 불러오기
        QCell = CSVReader.Read("Quest"); // csv 파일 불러오기
        questList = new Dictionary<int, QuestData>(); // 퀘스트 리스트 딕셔너리로 생성하기
        GenerateData(); // 퀘스트 리스트에 엑셀에서 불러온 데이터 집어 넣기

        btnPrefab = Resources.Load<GameObject>("Prefabs/QuestBtn"); // 버튼 프리팹 불러오기
        if (btnPrefab == null)
        {
            Debug.Log("btnPrefab==null");
        }
        CreateBtns(); // 버튼 생성
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateData() // 퀘스트 리스트를 저장
    {
        // questList.Add(10, new QuestData(questName, questContents, questRewards, questProcessivity, questCount));
        for (int i = 0; i < QCell.Count; i++)
        {
            // 이 부분이 서버에서 불러오는 부분
            questName = QCell[i]["questName"].ToString();
            questContents = QCell[i]["questContents"].ToString();
            questRewards = int.Parse(QCell[i]["questRewards"].ToString()); 
            questProcessivity = int.Parse(QCell[i]["questProcessivity"].ToString()); 
            questCount = int.Parse(QCell[i]["questCount"].ToString());
            
            // 딕셔너리에 추가
            questList.Add(10*(i+1), new QuestData(questName, questContents, questRewards, questProcessivity, questCount));

        }
    }

    public void QuestRoad() // 퀘스트 목록을 클릭 시 
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject; // 클릭 오브젝트의 정보를 가져온다
        switch (clickObject.name) { // 클릭 오브젝트의 이름이
            case "10": // 10일 시
                questList.TryGetValue(10, out test); // 퀘스트 리스트에서 키가 10인 퀘스트의 정보를 test에 담는다
                questContents = test.questContents; // test에서 퀘스트 컨텐츠를 가져온다
                questText.text = questContents; // questText의 텍스트, 즉 클릭한 목록 버튼을 눌렀을 때 보여줄 화면의 내용을 바꾼다.
                break;
            case "20": 
                questList.TryGetValue(20, out test);
                questContents = test.questContents; 
                questText.text = questContents; 
                break;
            case "30":
                questList.TryGetValue(30, out test);
                questContents = test.questContents;
                questText.text = questContents;
                break;
            case "40":
                questList.TryGetValue(40, out test);
                questContents = test.questContents;
                questText.text = questContents;
                break;
            case "50":
                questList.TryGetValue(50, out test);
                questContents = test.questContents;
                questText.text = questContents;
                break;
            case "60":
                questList.TryGetValue(60, out test);
                questContents = test.questContents;
                questText.text = questContents;
                break;

            default:
                break;
        }
        questPanel.SetActive(true); // Q패널을 활성화
        btnPanel.SetActive(false); // 버튼 목록 패널을 비활성화
    }
    public void CreateBtns() // 버튼 생성
    {
        for (int nn = 0; nn < QCell.Count; nn++)
        {
            GameObject button = Instantiate(btnPrefab); // 버튼을 생성
            button.name = ((nn + 1) * 10).ToString(); // 버튼의 이름을 딕셔너리의 인덱스와 동일하게 생성
            button.GetComponent<Button>().onClick.AddListener(() => { QuestRoad(); }); // 버튼에 퀘스트를 로드하는 온클릭 리스너 삽입
            btnText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // 버튼의 텍스트를 가져온다
            btnText.text = "퀘스트 " + (nn + 1) + "번"; // 버튼의 텍스트를 수정
            RectTransform btnpos = button.GetComponent<RectTransform>(); // 버튼의 Transform을 가져온다
            btnpos.SetParent(gameObject.transform.GetChild(0).transform, false);
            // 부모 오브젝트 설정. 부모의 transform을 받기 위함
        }
    }
    public void QuestReturn() // 퀘스트 내용 확인 후 뒤로가기 버튼 클릭 시
    {
        questPanel.SetActive(false); // Q패널을 비활성화
        btnPanel.SetActive(true); // 버튼 목록 패널을 활성화
    }
    public void RoadMainquest() // 메인퀘스트를 불러오는 코드
    {
        SceneManager.LoadScene("Quest");
    }

    public void QuestClear() // 퀘스트 클리어 시
    {
        if(/*클리어 조건을 만족 했을 시*/true)
        {
            // 보상을 서버로 넣는 코드
            // 버튼을 없앤다
            // 퀘스트 리스트에서 제거 혹은 갱신
        }
        else
        {
            // 클리어 조건이 미달성 됐을 시
            // 완료 버튼을 상시 작용하게 할지, 아니면 퀘스트 조건 달성시 완료 버튼이 보이게 할지 고민중
            // 전자라면 이 부분에 퀘스트가 덜 됐다는 알림을 보여줘야 하고, 후자라면 이부분이 필요없음. if도 필요없고
        }


    }

}
