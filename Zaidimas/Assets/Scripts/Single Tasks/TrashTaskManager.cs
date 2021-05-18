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
        Debug.Log("5. trash task manager");

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
        Debug.Log("6. cikliukas");
        for (int i = 0; i < activatedTrashCount; i++)
        {
            Debug.Log("7. viduj ciklo");
            //Trashes[i].SetActive(true);
            Trashes[i].GetComponent<ItemColorPulsing>().SetUpItem();
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
