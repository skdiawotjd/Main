using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public UserInfoManager UserInfoManager;
    
    
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


}
