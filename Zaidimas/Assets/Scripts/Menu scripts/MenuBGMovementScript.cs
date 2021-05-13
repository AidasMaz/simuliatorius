using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBGMovementScript : MonoBehaviour
{
    public Image Background;
    [Range(0.1f, 30f)]
    public float smoothSpeed = 4.5f;
    [Space]
    private Vector2 desiredPosition;
    [Space]
    public int ScreenWidth;
    public int ScreenCenter;
    [Space]
    [Range(0, 20)]
    public int PixelsToMoveOneSide;
    [Space]
    float posBG;

    //----------------------------------------------

    private void Awake()
    {
        ScreenCenter = Screen.width / 2;
        ScreenWidth = Screen.width;
        posBG = ScreenCenter - (PixelsToMoveOneSide / (float)ScreenCenter * Input.mousePosition.x);
        Background.transform.position = new Vector2(posBG, Background.transform.position.y);
    }

    private void Update()
    {
        if (ScreenWidth != Screen.width)
        {
            ScreenCenter = Screen.width / 2;
            ScreenWidth = Screen.width;
        }
    }

    private void FixedUpdate()
    {
        if (Input.mousePosition.x > 0 && Input.mousePosition.x < ScreenWidth)
        {
            //Debug.Log("Cursor is right");
            posBG = ScreenWidth * 0.01f + ScreenCenter - (PixelsToMoveOneSide /  (float)ScreenCenter * Input.mousePosition.x);
        }
        //Debug.Log(pos);
        //Background.transform.position = new Vector2(pos, Background.transform.position.y);
        desiredPosition = new Vector2(posBG, Background.transform.position.y);
        Vector2 smoothedPosition = Vector2.Lerp(new Vector2(Background.transform.position.x, Background.transform.position.y), desiredPosition, smoothSpeed * Time.deltaTime);
        Background.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, Background.transform.position.z);
    }
}
