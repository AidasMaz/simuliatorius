using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColorPulsing : MonoBehaviour
{
    [Header("Colors")]
    public Color PulsingColor;
    [Space]
    public bool pulse;

    //+++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        pulse = false;
    }

    private void FixedUpdate()
    {
        if (pulse)
        {
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, PulsingColor, Mathf.PingPong(Time.time, 1));
        }
    }
}
