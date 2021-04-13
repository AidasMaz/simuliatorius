using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningPhone : MonoBehaviour
{
    public GameObject Phone;

    private Vector3 OriginalPhonePos;

    //---------------------------------------------

    public void QuickMoveFromRight()
    {
        OriginalPhonePos = Phone.gameObject.GetComponent<RectTransform>().localPosition;
        Phone.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(520, 0, 0);
        LeanTween.moveLocal(Phone.gameObject, new Vector3(250, 0, 0), 0.3f);
    }

    public void QuickMoveToRight()
    {
        OriginalPhonePos = Phone.gameObject.GetComponent<RectTransform>().localPosition;
        LeanTween.moveLocal(Phone.gameObject, new Vector3(520, 0, 0), 0.4f);
    }
}
