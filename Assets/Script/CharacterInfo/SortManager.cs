using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    public GameObject CharacterScrollView;
    public GameObject EquipmentScrollView;
    
    // 1 - 軒什朕軒什闘, 2 - 舌搾軒什闘
    private int ListType = 1;

    void Start()
    {
        
    }

    // SortButtonPanel税 獄動戚 硲窒
    public void SortList(int Type)
    {
        Debug.Log("けいしけしけいし " + EquipmentScrollView.activeSelf);
        if (EquipmentScrollView.activeSelf == false)
        {
            // 1-1. 軒什朕 軒什闘幻 左析 凶
            ListType = 1;
            Debug.Log("軒什朕 軒什闘研");
        }
        else
        {
            // 1-2. 舌搾 軒什闘亜 左析 凶
            ListType = 2;
            Debug.Log("舌搾 軒什闘研");
        }

        // 2. 背雁 軒什闘研
        switch (Type)
        {
            case 0:
                // 去厭 紺稽 舛慶
                Debug.Log("去厭 紺稽 舛慶");
                Sort();
                break;
            case 1:
                // 適掘什 紺稽 舛慶
                Debug.Log("適掘什 紺稽 舛慶");
                Sort();
                break;
            case 2:
                // 傾婚 紺稽 舛慶
                Debug.Log("傾婚 紺稽 舛慶");
                Sort();
                break;
        }
    }

    // 袷 績獣稽 幻窮 敗呪
    private void Sort()
    {
        
    }
}
/*
 * 奄沙旋生稽澗 RiskerNumber/EquipnemtNumber 授生稽 舛慶
 */