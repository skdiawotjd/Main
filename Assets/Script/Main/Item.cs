using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Item : MonoBehaviour, IPointerClickHandler
{
    ItemManager ItemManager;

    void Start()
    {
        ItemManager = GetComponentInParent<ItemManager>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ItemManager.MoveScene(gameObject.name);
    }
}
