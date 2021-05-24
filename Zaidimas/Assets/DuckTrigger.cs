using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckTrigger : MonoBehaviour
{
    public DuckTaskManager DuckManager;
    public MainUIManager MainUIMan;

    //-----------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DuckManager = GameObject.Find("Duck finding manager").GetComponent<DuckTaskManager>();
        MainUIMan = GameObject.Find("Main UI manager").GetComponent<MainUIManager>();

        if (collision.name == "PLAYER_ALEX(Clone)" || collision.name == "PLAYER_MOLLY(Clone)" || collision.name == "PLAYER_ROB(Clone)")
        {
            if (DuckManager.DuckPickedUp)
            {
                DuckManager.FinishDuckFindingLevel();
                MainUIMan.ShowMiniGameEndWindow("Duck", true);
            }
        }
    }
}
