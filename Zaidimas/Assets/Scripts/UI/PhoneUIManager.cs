using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUIManager : MonoBehaviour
{
    [Header("Phone object")]
    public GameObject Phone;

    [Header("Phone cases")]
    public GameObject CaseWhite;
    public GameObject CaseGreen;
    public GameObject CasePurple;

    [Header("Windows")]
    public GameObject MainWindow;
    [Space]
    public GameObject MapWindow;
    [Space]
    public GameObject SaveWindow;
    public Image Level1Image;
    public Image Level2Image;
    public Image Level3Image;
    public Image Player1Image;
    public Image Player2Image;
    public Image Player3Image;
    [Space]
    public GameObject TasksWindow;
    [Space]
    public GameObject SettingsWindow;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Button CursorSizeSmallButton;
    public Button CursorSizeSmal2Button;
    public Button CursorSizeSmal3Button;

    //[Header("-----  ------")]
    //public GameObject MainWindow;

    [Header("Sounds")]
    public AudioSource ClickSound;
    public AudioSource TurnOnSound;
    public AudioSource TurnOffSound;

    //---------------------------------

    private void Awake()
    {
        GetInfoForGameStart();
    }

    private void GetInfoForGameStart()
    {
        // telefono remas

        // zaidejo foto ir lygis saugojime

        // kursoriaus dydis ir garsai nustatymuose
    }

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

    public void SaveAndQuit()
    {
        // Issaugoti progresa

        Application.Quit();
    }
}
