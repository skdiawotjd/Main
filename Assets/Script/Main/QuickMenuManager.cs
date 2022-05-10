using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickMenuManager : MonoBehaviour
{
    public void MoveScene()
    {
        GameObject ClickedObject = EventSystem.current.currentSelectedGameObject;
        print("클릭 버튼 : " + ClickedObject.name);
        switch (ClickedObject.name)
        {
            case "BattleButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Incremental Battle");
                break;
            case "CharacterInfoButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterInfo");
                break;
            case "DeckButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Deck");
                break;
            case "BarButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Bar");
                break;
            case "ShopButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
                break;
            case "SummonButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Summon");
                break;
            case "SettingButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Setting");
                break;
            case "CollectionButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Collection");
                break;
            case "AgitButton":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Agit");
                break;
        }
    }
}
