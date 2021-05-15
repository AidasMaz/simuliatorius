using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColorPulsing : MonoBehaviour
{
    [System.Serializable]
    public enum ColorType
    {
        TPs,
        Plates,
        Trashes,
        Ducks
    }

    public ColorType pieceType;

    [Header("Colors")]
    public Color32 PulsingColor;
    [Space]
    [Range(0f, 1f)]
    public float fadeTime = 0.25f;
    [Range(0f, 1f)]
    public float scaleDownModifier = 0.3f;
    [Space]
    public bool pulse;
    public bool taken;
    [Space]
    public AudioSource FadeSound;

    //+++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        switch (pieceType)
        {
            case ColorType.TPs:
                PulsingColor = new Color32(175, 255, 162, 222);
                break;
            case ColorType.Plates:
                PulsingColor = new Color32(255, 198, 198, 222);
                break;
            case ColorType.Trashes:
                PulsingColor = new Color32(140, 170, 225, 215);
                break;
            case ColorType.Ducks:
                PulsingColor = new Color32(255, 220, 150, 215);
                break;
        }
        //FadeSound = GameObject.Find("").GetComponent<AudioSource>();
        //SetUpItem();
    }

    public void SetUpItem()
    {
        this.GetComponent<CircleCollider2D>().enabled = true;
        pulse = true;
        taken = false;
    }

    public void FadeOut()
    {
        //FadeSound.Play();
        Vector3 scale = this.gameObject.GetComponent<Transform>().localScale * scaleDownModifier;
        LeanTween.scale(this.gameObject, scale, fadeTime);
        LeanTween.alpha(this.gameObject, 0.0f, fadeTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PLAYER_ALEX(Clone)" || collision.name == "PLAYER_MOLLY(Clone)" || collision.name == "PLAYER_ROB(Clone)")
        {
            pulse = false;
            taken = true;
            //Debug.Log(collision.name);
            this.GetComponent<CircleCollider2D>().enabled = false;
            FadeOut();
            //GameObject.Find("").GetComponent<>();
        }
    }

    private void FixedUpdate()
    {
        if (pulse)
        {
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, PulsingColor, Mathf.PingPong(Time.time, 1));
        }
    }
}
