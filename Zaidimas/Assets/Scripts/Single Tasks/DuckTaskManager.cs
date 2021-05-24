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

    public bool DuckPickedUp;

    //++++++++++++++++++++++++++++++++++++++++++++++++

    public void SetDuckFindingLevel()
    {
        DuckPickedUp = false;
        number = Random.Range(0, Ducks.Length);
        Ducks[number].SetActive(true);
        Ducks[number].GetComponent<ItemColorPulsing>().SetUpItem();
        //OriginalDuck.SetActive(false);
        OriginalDuck.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void FinishDuckFindingLevel()
    {
        Ducks[number].SetActive(false);
        //OriginalDuck.SetActive(true);
        OriginalDuck.GetComponent<SpriteRenderer>().enabled = true;
    }
}
