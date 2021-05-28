using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gynimas3Lenta : MonoBehaviour
{
    public Collider2D collition;

    public GameObject Lenta;

    public GameObject PabaigosLangas;
    public Text text;

    public Gynimas3 ingridientai;

    public List<string> pasirinkti = new List<string>();

    public GameObject Viskas;

    public void Start()
    {
        PabaigosLangas.SetActive(false);
        Lenta.SetActive(false);
        collition = this.GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Lenta.SetActive(true);
    }

    public void Press(string num)
    {
        pasirinkti.Add(num);
    }

    public void SkaiciuotiRez()
    {
        if (pasirinkti.Count != ingridientai.original.Count)
            Pabaiga(false);
        else
        {
            foreach (var item in ingridientai.original)
            {
                if (!pasirinkti.Contains(item))
                {
                    Pabaiga(false);
                    return;
                }
            }
            Pabaiga(true);
        }
    }

    public void Pabaiga(bool gerai)
    {
        if (gerai)
            text.text = "Teisingai!";
        else
            text.text = "Neteisingai";
        PabaigosLangas.SetActive(true);
    }

    public void Uzdaryti()
    {
        Viskas.SetActive(false);
    }
}
