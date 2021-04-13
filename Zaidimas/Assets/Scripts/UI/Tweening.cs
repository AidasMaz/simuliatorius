using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    [Serializable]
    public enum AnimationType
    {
        QuickMoveFromLeft,
        QuickMoveFromRight,
        QuickMoveToRight,
        QuickMoveFromTop,
        QuickMoveFromBottom,
        ZoomIn,
        ZoomOut,
        ExpandByX,
        ExpandByY
    }

    public AnimationType animationType;

    public Vector3 OriginalPos;

    //-------------------------------------------------------

    public void SetAnimationType(UIAnimationEnumType type)
    {
        this.animationType = type.animationType;
    }

    public void OnEnable()
    {
        Execute();
    }

    public void Execute()
    {
        switch (animationType)
        {
            case AnimationType.QuickMoveFromLeft:
                QuickMoveFromLeft();
                break;
            case AnimationType.QuickMoveFromRight:
                QuickMoveFromRight();
                break;
            case AnimationType.QuickMoveToRight:
                QuickMoveToRight();
                break;
            case AnimationType.QuickMoveFromTop:
                QuickMoveFromTop();
                break;
            case AnimationType.QuickMoveFromBottom:
                QuickMoveFromBottom();
                break;
            case AnimationType.ZoomIn:
                ZoomIn();
                break;
            case AnimationType.ZoomOut:
                ZoomOut();
                break;
            case AnimationType.ExpandByX:
                ExpandByX();
                break;
            case AnimationType.ExpandByY:
                ExpandByY();
                break;
            default:
                break;
        }
    }

    //-------------------------------------------

    private void QuickMoveFromLeft()
    {
        OriginalPos = this.gameObject.GetComponent<RectTransform>().localPosition;
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-500, 0, 0);
        LeanTween.moveLocal(this.gameObject, OriginalPos, 0.1f);
    }

    private void QuickMoveFromRight()
    {
        OriginalPos = this.gameObject.GetComponent<RectTransform>().localPosition;
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(1500, 0, 0);
        LeanTween.moveLocal(this.gameObject, new Vector3(250, 0, 0), 0.7f);
    }

    private void QuickMoveToRight()
    {
        OriginalPos = this.gameObject.GetComponent<RectTransform>().localPosition;
        LeanTween.moveLocal(this.gameObject, new Vector3(1500, 0, 0), 0.7f);
    }

    private void QuickMoveFromTop()
    {
        OriginalPos = this.gameObject.GetComponent<RectTransform>().localPosition;
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 500, 0);
        LeanTween.moveLocal(this.gameObject, OriginalPos, 0.1f);
    }

    private void QuickMoveFromBottom()
    {
        OriginalPos = this.gameObject.GetComponent<RectTransform>().localPosition;
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -500, 0);
        LeanTween.moveLocal(this.gameObject, OriginalPos, 0.1f);
    }

    private void ZoomIn()
    {
        Vector3 scale = this.gameObject.GetComponent<RectTransform>().localScale;
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.5f * scale.x, 0.5f * scale.y, 0.5f * scale.z);
        LeanTween.scale(this.gameObject, scale, 0.1f);
    }

    private void ZoomOut()
    {
        Vector3 scale = this.gameObject.GetComponent<RectTransform>().localScale;
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale.x * 2, scale.y * 2, scale.z * 2);
        LeanTween.scale(this.gameObject, scale, 0.1f);
    }

    private void ExpandByX()
    {
        Vector3 scale = this.gameObject.GetComponent<RectTransform>().localScale;
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, scale.y, scale.z);
        LeanTween.scale(this.gameObject, scale, 0.2f);
    }

    private void ExpandByY()
    {
        Vector3 scale = this.gameObject.GetComponent<RectTransform>().localScale;
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale.x, 0, scale.z);
        LeanTween.scale(this.gameObject, scale, 0.2f);
    }
}
