using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPTrigger : MonoBehaviour
{
    public ToiletPapperTaskManager TPManager;
    public MainUIManager MainUIMan;

    //-----------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TPManager = GameObject.Find("Toilet papper finding manager").GetComponent<ToiletPapperTaskManager>();
        MainUIMan = GameObject.Find("Main UI manager").GetComponent<MainUIManager>();

        if (collision.name == "PLAYER_ALEX(Clone)" || collision.name == "PLAYER_MOLLY(Clone)" || collision.name == "PLAYER_ROB(Clone)")
        {
            if (TPManager.CollecetedPaper)
            {
                TPManager.FinishTPFindingLevel();
                MainUIMan.ShowMiniGameEndWindow("TP", true);
            }
        }
    }
}
