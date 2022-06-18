using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickDetect : MonoBehaviour, IPointerClickHandler
{
    public GameObject SettingMenu;
    public Toggle SettingToggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SettingToggle.isOn = false;
        SettingMenu.SetActive(false);
    }
}
