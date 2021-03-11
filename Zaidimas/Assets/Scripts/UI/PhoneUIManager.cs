using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUIManager : MonoBehaviour
{
    [Header("Phone object")]
    public GameObject Phone;

    [Header("Windows")]
    public GameObject MainWindow;
    [Space]
    public GameObject MapWindow;
    public GameObject SaveWindow;
    public GameObject SettingsWindow;
    public GameObject TasksWindow;

    [Header("Sounds")]
    public AudioSource ClickSound;
    public AudioSource ConffirmSound;
    public AudioSource TurnOnSound;
    public AudioSource TurnOffSound;

    //---------------------------------

    public void OpenMainWindow()
    {
        //ClickSound.Play();
        //MainWindow.SetActive(true);
        //MapWindow.SetActive(false);
        //SaveWindow.SetActive(false);
        //SettingsWindow.SetActive(false);
        //TasksWindow.SetActive(false);
    }

    public void OpenMapWindow()
    {
        //ClickSound.Play();
        //MainWindow.SetActive(false);
        //MapWindow.SetActive(true);
    }

    public void OpenSaveWindow()
    {
        //ClickSound.Play();
        //MainWindow.SetActive(false);
        //SaveWindow.SetActive(true);
    }

    public void OpenSettingsWindow()
    {
        //ClickSound.Play();
        //MainWindow.SetActive(false);
        //SettingsWindow.SetActive(true);
    }

    public void OpenTasksWindow()
    {
        //ClickSound.Play();
        //MainWindow.SetActive(false);
        //TasksWindow.SetActive(true);
    }
}
