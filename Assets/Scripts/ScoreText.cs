using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public static ScoreText Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
