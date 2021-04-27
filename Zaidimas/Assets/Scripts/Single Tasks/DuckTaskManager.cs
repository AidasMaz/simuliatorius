using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckTaskManager : MonoBehaviour
{
    [Header("Duck objects")]
    public GameObject[] Ducks;
    [Space]
    public GameObject OriginalDuck;

    private int number;

    //++++++++++++++++++++++++++++++++++++++++++++++++

    public void SetDuckFindingLevel()
    {
        number = Random.Range(0, Ducks.Length);
        Ducks[number].SetActive(true);
        OriginalDuck.SetActive(false);
    }

    public void FinishDuckFindingLevel()
    {
        Ducks[number].SetActive(false);
        OriginalDuck.SetActive(true);
    }
}
