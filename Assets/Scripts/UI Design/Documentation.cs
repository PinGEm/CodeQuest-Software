using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documentation : MonoBehaviour
{
    [SerializeField] private GameObject DocumentationUI;
    [SerializeField] private GameObject GettingStartedUI, SelectUI, SwapUI, MoveUI, ExampleUI;
    

    public void ShowDocumentation()
    {
        DocumentationUI.SetActive(true);
    }

    public void ExitDocumentation()
    {
        DocumentationUI.SetActive(false);
    }

    public void GoTo(GameObject go)
    {
        go.SetActive(true);
    }

    public void GoBack(GameObject UIMenu)
    {
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
                ExampleUI.SetActive(true);
                break;
        }
    }
}
