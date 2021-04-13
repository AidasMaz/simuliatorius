using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowing : MonoBehaviour
{
    [Header("Follow targer (Player)")]
    public Transform target;
    [Range(0.01f, 30f)]
    public float smoothSpeed = 5f;

    //-------------------------------

    public void SetTarget(string name)
    {
        //switch (name)
        //{
        //    case "Alex":
        //        target = GameObject.Find("PLAYER(Clone)").transform;
        //        break;
            //case "Molly":
            //    target = GameObject.Find("PLAYER MOLLY(Clone)").transform;
            //    break;
            //case "Rob":
            //    target = GameObject.Find("PLAYER ROB(Clone)").transform;
            //    break;
        //} 
    }

    void FixedUpdate()
    {
        Vector2 desiredPosition = new Vector2(target.position.x, target.position.y);
        Vector2 smoothedPosition = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y ), desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
