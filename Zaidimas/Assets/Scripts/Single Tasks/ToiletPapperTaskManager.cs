using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPapperTaskManager : MonoBehaviour
{
    [Header("TP objects")]
    public GameObject[] TPs;
    [Space]
    public GameObject OriginalTP;

    public bool CollecetedPaper;

    private int number;

    //++++++++++++++++++++++++++++++++++++++++++++++++

    public void SetTPFindingLevel()
    {
        CollecetedPaper = false;
        number = Random.Range(0, TPs.Length);
        TPs[number].SetActive(true);
        TPs[number].GetComponent<ItemColorPulsing>().SetUpItem();
        //OriginalTP.SetActive(false);
        OriginalTP.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void FinishTPFindingLevel()
    {
        TPs[number].SetActive(false);
        //OriginalTP.SetActive(true);
        OriginalTP.GetComponent<SpriteRenderer>().enabled = true;
    }
}
