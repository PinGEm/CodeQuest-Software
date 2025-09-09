using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documentation : MonoBehaviour
{
    [SerializeField] private GameObject DocumentationUI;
    [SerializeField] private GameObject OutlineUI, GettingStartedUI, SelectUI, SwapUI, MoveUI, Example_1_UI, Example_2_UI, Example_3_UI;
    

    public void ShowDocumentation()
    {
        OutlineUI.SetActive(true);
        DocumentationUI.SetActive(true);
    }

    public void ExitDocumentation()
    {
        OutlineUI.SetActive(false);
        DocumentationUI.SetActive(false);
    }

    public void GoTo(GameObject go)
    {
        OutlineUI.SetActive(false);
        go.SetActive(true);
    }

    public void GoBack(GameObject UIMenu)
    {
        OutlineUI.SetActive(true);
        UIMenu.SetActive(false);
    }

    public void GoForward(int currentMenu)
    {
        switch (currentMenu)
        {
            case 1:
                GettingStartedUI.SetActive(false);
                SelectUI.SetActive(true);
                break;
            case 2:
                SelectUI.SetActive(false);
                SwapUI.SetActive(true);
                break;
            case 3:
                SwapUI.SetActive(false);
                MoveUI.SetActive(true);
                break;
            case 4:
                MoveUI.SetActive(false);
                Example_1_UI.SetActive(true);
                break;
            case 5:
                Example_1_UI.SetActive(false);
                Example_2_UI.SetActive(true);
                break;
            case 6:
                Example_2_UI.SetActive(false);
                Example_3_UI.SetActive(true);
                break;
        }
    }
}
