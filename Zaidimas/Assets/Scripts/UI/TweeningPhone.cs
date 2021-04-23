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

    //public void NotificationZoomIn(GameObject obj)
    //{
    //    Vector3 scale = obj.gameObject.GetComponent<RectTransform>().localScale;
    //    obj.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.5f * scale.x, 0.5f * scale.y, 0.5f * scale.z);
    //    LeanTween.scale(obj.gameObject, scale, 0.1f);
    //}

    //public void NotificationZoomOut(GameObject obj)
    //{
    //    Vector3 scale = obj.gameObject.GetComponent<RectTransform>().localScale;
    //    obj.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale.x * 2, scale.y * 2, scale.z * 2);
    //    LeanTween.scale(obj.gameObject, scale, 0.1f);
    //}
}
