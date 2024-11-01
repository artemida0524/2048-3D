using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    public static Coroutine Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


}
