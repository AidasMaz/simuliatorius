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
        Ducks,
        Sleep
    }
    public ColorType pieceType;
    [Space]
    public ToiletPapperTaskManager TPManager;
    public PlateTaskManager PlateManager;
    public TrashTaskManager TrashManager;
    public DuckTaskManager DuckManager;
    [Space]
    public MainUIManager UIManager;
    public AudioManager AudioManager;

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
    //[Space]
    //public AudioSource FadeSound;

    //+++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        //Debug.Log("start funkcija");
        //this.GetComponent<CircleCollider2D>().enabled = false;
        //gameObject.SetActive(false);
        
        //FadeSound = GameObject.Find("").GetComponent<AudioSource>();
        //SetUpItem();
    }

    public void SetUpItem()
    {
        UIManager = GameObject.Find("Main UI manager").GetComponent<MainUIManager>();
        AudioManager =  GameObject.Find("AUDIO OBJECT").GetComponent<AudioManager>();
        Debug.Log("set up item");
        switch (pieceType)
        {
            case ColorType.TPs:
                PulsingColor = new Color32(175, 255, 162, 222);
                TPManager = GameObject.Find("Toilet papper finding manager").GetComponent<ToiletPapperTaskManager>();
                break;
            case ColorType.Plates:
                PulsingColor = new Color32(255, 198, 198, 222);
                PlateManager = GameObject.Find("Plate collecting manager").GetComponent<PlateTaskManager>();
                break;
            case ColorType.Trashes:
                PulsingColor = new Color32(140, 170, 225, 215);
                TrashManager = GameObject.Find("Trash collecting manager").GetComponent<TrashTaskManager>();
                break;
            case ColorType.Ducks:
                PulsingColor = new Color32(255, 220, 150, 215);
                DuckManager = GameObject.Find("Duck finding manager").GetComponent<DuckTaskManager>();
                break;
        }
        gameObject.SetActive(true);
        pulse = true;
        taken = false;
    }

    public void FadeOut()
    {
        switch (pieceType)
        {
            case ColorType.TPs:
                AudioManager.PlaySound("TP");
                break;
            case ColorType.Plates:
                AudioManager.PlaySound("Plate");
                break;
            case ColorType.Trashes:
                AudioManager.PlaySound("Trash");
                break;
            case ColorType.Ducks:
                AudioManager.PlaySound("Duck");
                break;
        }
        Vector3 scale = this.gameObject.GetComponent<Transform>().localScale * scaleDownModifier;
        LeanTween.scale(this.gameObject, scale, fadeTime);
        LeanTween.alpha(this.gameObject, 0.0f, fadeTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yra");
        if (collision.name == "PLAYER_ALEX(Clone)" || collision.name == "PLAYER_MOLLY(Clone)" || collision.name == "PLAYER_ROB(Clone)")
        {
            Debug.Log("Yra2");
            pulse = false;
            taken = true;
            //Debug.Log(collision.name);
            if (pieceType != ColorType.Sleep)
            {
                this.GetComponent<CircleCollider2D>().enabled = false;
                FadeOut();
            }
            switch (pieceType)
            {
                case ColorType.TPs:
                    TPManager.CollecetedPaper = true;
                    break;
                case ColorType.Plates:
                    if (PlateManager.platesLeftCount > 1)
                        PlateManager.platesLeftCount = PlateManager.platesLeftCount - 1;
                    else
                        UIManager.ShowMiniGameEndWindow("Plate", true);
                    break;
                case ColorType.Trashes:
                    if (TrashManager.trashLeftCount > 1)
                        TrashManager.trashLeftCount = TrashManager.trashLeftCount - 1;
                    else
                        UIManager.ShowMiniGameEndWindow("Trash", true);
                    break;
                case ColorType.Ducks:
                    DuckManager.DuckPickedUp = true;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (pulse && pieceType != ColorType.Sleep)
        {
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, PulsingColor, Mathf.PingPong(Time.time, 1));
        }
    }
}
