using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CYoureSharpPackage;

public class DataManager : MonoBehaviour
{
    public int select_X { get; private set; }
    public int select_Y { get; private set; }

    public int swap_X { get; private set; }
    public int swap_Y { get; private set; }

    public Dictionary<(int, int), GameObject> blocks = new Dictionary<(int, int), GameObject>();

    private GameObject selectedGameObject;
    private GameObject swapGameObject;

    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void InputSelectData(string var1, string var2)
    {
        select_X = int.Parse(var1) - 1;
        select_Y = int.Parse(var2) - 1;
    }

    public void MoveFunction(string direction)
    {
        Debug.Log("Moving function playing");
        switch (direction)
        {
            case "up":
                SwapFunction(select_X, select_Y - 1);
                break;
            case "down":
                SwapFunction(select_X, select_Y + 1);
                break;
            case "left":
                SwapFunction(select_X - 1, select_Y);
                break;
            case "right":
                SwapFunction(select_X + 1, select_Y);
                break;
        }
    }

    public void SwapFunction(int var1, int var2, bool manualSelect = false)
    {
        if (manualSelect)
        {
            swap_X = var1 - 1;
            swap_Y = var2 - 1;
        }
        else
        {
            swap_X = var1;
            swap_Y = var2;
        }

        if (!ApplyBlocks())
        {
            return;
        }

        // SWAPPING VALUES NOW
        int temp = swap_X;
        swap_X = select_X;
        select_X = temp;

        temp = swap_Y;
        swap_Y = select_Y;
        select_Y = temp;

        // APPLY BLOCK CHANGES
        blocks[(swap_X, swap_Y)] = swapGameObject;
        blocks[(select_X, select_Y)] = selectedGameObject;

        Vector2 temp_coords = swapGameObject.transform.position;
        swapGameObject.transform.position = selectedGameObject.transform.position;
        selectedGameObject.transform.position = temp_coords;

        ResetData();
    }

    public void ResetData()
    {
        select_X = -1;
        select_Y = -1;

        swap_X = -1;
        swap_Y = -1;
    }

    private bool ApplyBlocks()
    {
        int[] selectedBlock = { select_X, select_Y };
        Debug.Log(selectedBlock[0] + "," + selectedBlock[1]);

        if (blocks.TryGetValue((select_X, select_Y), out selectedGameObject))
        {
            selectedGameObject = blocks[(select_X, select_Y)];
        }
        else
        {
            Debug.LogError("Selected Object not found!");
            ResetData();
            return false;
        }


        int[] swapBlock = { swap_X, swap_Y };

        if (blocks.TryGetValue((swap_X, swap_Y), out swapGameObject))
        {
            swapGameObject = blocks[(swap_X, swap_Y)];
        }
        else
        {
            Debug.LogError("Swap Block not found!");
            ResetData();
            return false;
        }

        return true;
    }

    public void Shuffle()
    {
        int shuffleCount = Random.Range(1000, 3000);
        Debug.Log("trying to shuffle");
        for (int i = 0; i < shuffleCount; i++)
        {
            // Select Block 
            select_X = Random.Range(0, 2);
            select_Y = Random.Range(0, 2);

            if (Random.Range(0, 1) == 0)
            {
                if (Random.Range(0, 1) == 0 && select_X < 2)
                {
                    select_X++;
                }
                else if(select_X != 2 && select_X != 0)
                {
                    select_X--;
                }
            }

            if (Random.Range(0, 1) == 0)
            {
                if (Random.Range(0, 1) == 0 && select_Y < 2)
                {
                    select_Y++;
                }
                else if(select_X != 2 && select_X != 0)
                {
                    select_Y--;
                }
            }

            // Swap Block
            int swapItem_X = Random.Range(0, 2);
            int swapItem_Y = Random.Range(0, 2);

            if (Random.Range(0, 1) == 1)
            {
                if (Random.Range(0, 1) == 0 && swapItem_X < 2)
                {
                    swapItem_X++;
                }

                if (Random.Range(0, 1) == 0 && swapItem_X != 0)
                {
                    swapItem_X--;
                }
            }

            if (Random.Range(0, 1) == 0)
            {
                if (Random.Range(0, 1) == 0 && swapItem_Y < 2)
                {
                    swapItem_Y++;
                }

                if (Random.Range(0, 1) == 0 && swapItem_X != 0)
                {
                    swapItem_Y--;
                }
            }

            SwapFunction(swapItem_X, swapItem_Y);
        }
    }
}
