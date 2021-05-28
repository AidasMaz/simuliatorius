using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gynimas3 : MonoBehaviour
{
    public Image[] Ingridientai;
    //public AudioManager Audio;

    public List<string> original;

    public GameObject langasShow;

    public Collider2D collition;

    private void Start()
    {
        //collition = GameObject.Find("showobjektas").GetComponent<Collider2D>();
        //Audio = GameObject.Find("AUDIO OBJECT").GetComponent<AudioManager>();

        //Audio.PlaySound("Phone error");
        ShuffleArray();
        int atrinkti = Random.Range(2, 4);
        for (int i = 0; i < 6; i++)
        {
            if (i < atrinkti)
            {
                original.Add(Ingridientai[i].name);
                Ingridientai[i].gameObject.SetActive(true);
            }
            else
            {
                Ingridientai[i].gameObject.SetActive(false);
            }
        }
    }

    public void Atidaryti()
    { 
        langasShow.SetActive(true);
    }

    private void ShuffleArray()
    {
        for (int i = 0; i < Ingridientai.Length; i++)
        {
            Image temp = Ingridientai[i];
            int rand = Random.Range(i, Ingridientai.Length);
            Ingridientai[i] = Ingridientai[rand];
            Ingridientai[rand] = temp;
        }
    }

    public void CloseShow()
    {
        langasShow.SetActive(false);
    }

    
}
