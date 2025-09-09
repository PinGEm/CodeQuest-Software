using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
    GameObject parentTextContainer;
    TextMeshPro text;
    (int, int) blockCoords;

    private void Start()
    {
        // Text Container
        parentTextContainer = new GameObject("Text Container");
        parentTextContainer.transform.parent = this.transform;
        parentTextContainer.transform.localPosition = Vector3.zero;

        SpriteRenderer sr = parentTextContainer.AddComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0.5f);
        Sprite sprite = Resources.Load<Sprite>("Sprite/Square");

        sr.sprite = sprite;

        // Add a TextMesh component
        GameObject textObj = new GameObject("Text");
        textObj.transform.parent = parentTextContainer.transform;
        textObj.transform.localPosition = Vector3.zero;

        text = textObj.AddComponent<TextMeshPro>();

        // Customize the TextMesh
        text.text = "1,1";
        text.fontSize = 6;
        text.color = Color.black;
        text.alignment = TextAlignmentOptions.Center;

        text.GetComponent<MeshRenderer>().sortingOrder = 2;


        parentTextContainer.SetActive(false);
    }

    private void OnMouseEnter()
    {
        FindObjectInDictionary();
        text.text = $"{blockCoords.Item1},{blockCoords.Item2}";
        parentTextContainer.SetActive(true);
        Debug.Log("Mouse cursor on game object!");
    }

    private void OnMouseExit()
    {
        parentTextContainer.SetActive(false);
    }

    void FindObjectInDictionary()
    {
        foreach (var thisObject in DataManager.Instance.blocks)
        {
            if (thisObject.Value == this.gameObject)
            {
                blockCoords = thisObject.Key;
                blockCoords.Item1 += 1;
                blockCoords.Item2 += 1;
            }
        }
    }
}
