using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private BootConfig bootConfig;


    private void Awake()
    {
        Initialization();

        SceneManager.LoadScene(1);
    }

    private void Initialization()
    {
        Application.targetFrameRate = bootConfig.FPS;
    }
}
