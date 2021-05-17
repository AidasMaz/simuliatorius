using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowing : MonoBehaviour
{
    [Header("Follow targer (Player)")]
    public Transform target;
    [Range(0.1f, 30f)]
    public float smoothSpeed = 4.5f;
    [Range(0.1f, 5f)]
    public float ofsetForPhone;
    public bool ofset = false;
    private Vector2 desiredPosition;

    //------------------------------------------

    public void SetTarget(string name)
    {
        switch (name)
        {
            case "Alex":
                target = GameObject.Find("PLAYER_ALEX(Clone)").transform;
                break;
            case "Molly":
                target = GameObject.Find("PLAYER_MOLLY(Clone)").transform;
                break;
            case "Rob":
                target = GameObject.Find("PLAYER_ROB(Clone)").transform;
                break;
        }

        InstantCameraMove();
    }

    public void InstantCameraMove()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (ofset)
        {
            //Debug.Log("Ofset X: " + (target.position.x + ofsetForPhone));
            desiredPosition = new Vector2(target.position.x + ofsetForPhone, target.position.y);
        }
        else
        {
            //Debug.Log("Normal X: " + target.position.x);
            desiredPosition = new Vector2(target.position.x, target.position.y);
        }

        Vector2 smoothedPosition = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
