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

    public GameObject questPanel; // ����Ʈ�� �ε�Ǵ� �г�
    public GameObject btnPanel; // ��ư ����� Ȱ��ȭ�Ǵ� �г�
    

    QuestData test;
    //public GameObject[] questObject;
    TextMeshProUGUI questText; // �������� �� ����Ʈ �г��� �ؽ�Ʈ
    TextMeshProUGUI btnText; // ��ư�� �̸��� �� �ؽ�Ʈ
    Dictionary<int, QuestData> questList; // ����Ʈ ����Ʈ�� �� ��ųʸ�
    public GameObject btnPrefab; // ��ư ������ ���� ������
    List<Dictionary<string, object>> QCell; // ���� ���� csv�� ����� �ҷ��� ��

    // Start is called before the first frame update
    void Start()
    {
        questText = gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // Q�г��� ����Ʈ �ؽ�Ʈ �ҷ�����
        QCell = CSVReader.Read("Quest"); // csv ���� �ҷ�����
        questList = new Dictionary<int, QuestData>(); // ����Ʈ ����Ʈ ��ųʸ��� �����ϱ�
        GenerateData(); // ����Ʈ ����Ʈ�� �������� �ҷ��� ������ ���� �ֱ�

        btnPrefab = Resources.Load<GameObject>("Prefabs/QuestBtn"); // ��ư ������ �ҷ�����
        if (btnPrefab == null)
        {
            Debug.Log("btnPrefab==null");
        }
        CreateBtns(); // ��ư ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateData() // ����Ʈ ����Ʈ�� ����
    {
        // questList.Add(10, new QuestData(questName, questContents, questRewards, questProcessivity, questCount));
        for (int i = 0; i < QCell.Count; i++)
        {
            // �� �κ��� �������� �ҷ����� �κ�
            questName = QCell[i]["questName"].ToString();
            questContents = QCell[i]["questContents"].ToString();
            questRewards = int.Parse(QCell[i]["questRewards"].ToString()); 
            questProcessivity = int.Parse(QCell[i]["questProcessivity"].ToString()); 
            questCount = int.Parse(QCell[i]["questCount"].ToString());
            
            // ��ųʸ��� �߰�
            questList.Add(10*(i+1), new QuestData(questName, questContents, questRewards, questProcessivity, questCount));

        }
    }

    public void QuestRoad() // ����Ʈ ����� Ŭ�� �� 
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject; // Ŭ�� ������Ʈ�� ������ �����´�
        switch (clickObject.name) { // Ŭ�� ������Ʈ�� �̸���
            case "10": // 10�� ��
                questList.TryGetValue(10, out test); // ����Ʈ ����Ʈ���� Ű�� 10�� ����Ʈ�� ������ test�� ��´�
                questContents = test.questContents; // test���� ����Ʈ �������� �����´�
                questText.text = questContents; // questText�� �ؽ�Ʈ, �� Ŭ���� ��� ��ư�� ������ �� ������ ȭ���� ������ �ٲ۴�.
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
        questPanel.SetActive(true); // Q�г��� Ȱ��ȭ
        btnPanel.SetActive(false); // ��ư ��� �г��� ��Ȱ��ȭ
    }
    public void CreateBtns() // ��ư ����
    {
        for (int nn = 0; nn < QCell.Count; nn++)
        {
            GameObject button = Instantiate(btnPrefab); // ��ư�� ����
            button.name = ((nn + 1) * 10).ToString(); // ��ư�� �̸��� ��ųʸ��� �ε����� �����ϰ� ����
            button.GetComponent<Button>().onClick.AddListener(() => { QuestRoad(); }); // ��ư�� ����Ʈ�� �ε��ϴ� ��Ŭ�� ������ ����
            btnText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // ��ư�� �ؽ�Ʈ�� �����´�
            btnText.text = "����Ʈ " + (nn + 1) + "��"; // ��ư�� �ؽ�Ʈ�� ����
            RectTransform btnpos = button.GetComponent<RectTransform>(); // ��ư�� Transform�� �����´�
            btnpos.SetParent(gameObject.transform.GetChild(0).transform, false);
            // �θ� ������Ʈ ����. �θ��� transform�� �ޱ� ����
        }
    }
    public void QuestReturn() // ����Ʈ ���� Ȯ�� �� �ڷΰ��� ��ư Ŭ�� ��
    {
        questPanel.SetActive(false); // Q�г��� ��Ȱ��ȭ
        btnPanel.SetActive(true); // ��ư ��� �г��� Ȱ��ȭ
    }
    public void RoadMainquest() // ��������Ʈ�� �ҷ����� �ڵ�
    {
        SceneManager.LoadScene("Quest");
    }

    public void QuestClear() // ����Ʈ Ŭ���� ��
    {
        if(/*Ŭ���� ������ ���� ���� ��*/true)
        {
            // ������ ������ �ִ� �ڵ�
            // ��ư�� ���ش�
            // ����Ʈ ����Ʈ���� ���� Ȥ�� ����
        }
        else
        {
            // Ŭ���� ������ �̴޼� ���� ��
            // �Ϸ� ��ư�� ��� �ۿ��ϰ� ����, �ƴϸ� ����Ʈ ���� �޼��� �Ϸ� ��ư�� ���̰� ���� �����
            // ���ڶ�� �� �κп� ����Ʈ�� �� �ƴٴ� �˸��� ������� �ϰ�, ���ڶ�� �̺κ��� �ʿ����. if�� �ʿ����
        }


    }

}
