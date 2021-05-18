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
        number = Random.Range(0, TPs.Length);
        TPs[number].SetActive(true);
        OriginalTP.SetActive(false);
    }

    public void FinishTPFindingLevel()
    {
        TPs[number].SetActive(false);
        OriginalTP.SetActive(true);
    }
}
