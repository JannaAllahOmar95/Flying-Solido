using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyText : MonoBehaviour
{

    public static EnemyText Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
