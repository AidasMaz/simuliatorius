using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedColision : MonoBehaviour
{
    public BoxCollider2D col;

    public MainUIManager MainUI;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touch");
        if (MainUI.ButtonTask == MainUIManager.ButtonVariants.Sleep)
        {
            Debug.Log("Kita diena");
            MainUI.GetNextDay();
        }
    }
}
