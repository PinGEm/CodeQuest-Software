using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int select_X { get; private set; }
    public int select_Y { get; private set; }

    public int swap_X { get; private set; }
    public int swap_Y { get; private set; }

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
        select_X = int.Parse(var1);
        select_Y = int.Parse(var2);
    }

    public void MoveFunction(string direction)
    {
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

    public void SwapFunction(int var1, int var2)
    {
        swap_X = var1;
        swap_Y = var2;

        int temp = swap_X;
        swap_X = select_X;
        select_X = temp;

        temp = swap_Y;
        swap_Y = select_Y;
        select_Y = temp;
    }

    public void ResetData()
    {
        select_X = -1;
        select_Y = -1;

        swap_X = -1;
        swap_Y = -1;
    }
}
