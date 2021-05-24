using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSceene : MonoBehaviour
{
    public static CheckSceene instance;

    public int NUMBER = 0;

    //-------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
