using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documentation : MonoBehaviour
{
    [SerializeField] private GameObject DocumentationUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDocumentation()
    {
        DocumentationUI.SetActive(true);
    }

    public void ExitDocumentation()
    {
        DocumentationUI.SetActive(false);
    }
}
