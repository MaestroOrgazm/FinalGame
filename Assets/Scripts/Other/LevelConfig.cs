using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public int Money { get; private set; }
    public bool IsDekster { get; private set; }

    public void SetValue(int money, bool isDekster)
    {
        Money = money;
        IsDekster = isDekster;
    }
}
