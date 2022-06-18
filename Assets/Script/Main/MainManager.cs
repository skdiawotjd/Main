using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject ClosePopUp;

    //public SPUM_Prefabs spum;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("abc");
    }

    // Update is called once per frame
    void Update()
    {
        //spum.PlayAnimation("Attack_Normal");
        if (Input.GetKey(KeyCode.Escape))
        {
            ClosePopUp.SetActive(true);
        }
    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("asd");
    }

    /*IEnumerator abc()
    {
        while(true)
        {
            // 반복실행하는 법
            spum._anim.Play("2_Attack_Normal", -1, 0f);

            yield return new WaitForSeconds(1.0f);
        }
    }*/
}
