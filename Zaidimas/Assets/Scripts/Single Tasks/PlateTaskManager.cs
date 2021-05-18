using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTaskManager : MonoBehaviour
{
    [Header("Plate objects")]
    public GameObject[] Plates;
    [Space]
    public int activatedPlateCount;
    [Space]
    public int platesLeftCount;

    //++++++++++++++++++++++++++++++++++++++++++++++++

    public void SetPlateCleaningLevel(int day)
    {
        ShuffleArray();

        if (day < 3)
            activatedPlateCount = 5;
        else if (day < 7)
            activatedPlateCount = 7;
        else if (day < 10)
            activatedPlateCount = 10;
        else
            activatedPlateCount = 12;

        platesLeftCount = activatedPlateCount;

        for (int i = 0; i < activatedPlateCount; i++)
        {
            Plates[i].GetComponent<ItemColorPulsing>().SetUpItem();
        }
    }

    private void ShuffleArray()
    {
        for (int i = 0; i < Plates.Length; i++)
        {
            GameObject temp = Plates[i];
            int rand = Random.Range(i, Plates.Length);
            Plates[i] = Plates[rand];
            Plates[rand] = temp;
        }
    }
}
