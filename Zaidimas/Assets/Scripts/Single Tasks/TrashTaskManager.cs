using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTaskManager : MonoBehaviour
{
    [Header("Trash objects")]
    public GameObject[] Trashes;
    [Space]
    public int activatedTrashCount;
    [Space]
    public int trashLeftCount;

    //++++++++++++++++++++++++++++++++++++++++++++++++

    public void SetTrashCleaningLevel(int day)
    {
        ShuffleArray();

        if (day < 3)
            activatedTrashCount = 3;
        else if (day < 7)
            activatedTrashCount = 5;
        else if (day < 10)
            activatedTrashCount = 7;
        else
            activatedTrashCount = 8;

        trashLeftCount = activatedTrashCount;

        for (int i = 0; i < activatedTrashCount; i++)
        {
            Trashes[i].SetActive(true);
        }
    }

    private void ShuffleArray()
    {
        for (int i = 0; i < Trashes.Length; i++)
        {
            GameObject temp = Trashes[i];
            int rand = Random.Range(i, Trashes.Length);
            Trashes[i] = Trashes[rand];
            Trashes[rand] = temp;
        }
    }
}
