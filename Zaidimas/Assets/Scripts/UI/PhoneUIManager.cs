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
    public Button CursorSizeMediumButton;
    public Button CursorSizeBigButton;

    [Header("Sounds")]
    public AudioSource ClickSound;
    public AudioSource TurnOnSound;
    public AudioSource TurnOffSound;

    //---------------------------------
    [Header("Variables and sprites")]
    public Texture2D[] CursorTextures;

    [Header("Managers")]
    public TaskGeneration GameDaysInfo;
    public SettingSaving SettingsInfo;


    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void Awake()
    {
        // music sliders
        // cursor size
        // player level
        // player picture
        // phone case
    }

    private void Start()
    {
        SettingsInfo.InitializeSettings();
        SetCursorSize(SettingsInfo.SettingsObject.CursorSize.ToString());
    }

    //--------------------------------------------

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

    //--------------------------------------------

    public void SetCursorSize(string size)
    {
        switch (size)
        {
            case "Small" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Small:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Small;
                Cursor.SetCursor(CursorTextures[0], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image = Resources.Load<Image>("Settings/CursorButtonSmallActivated");
                CursorSizeMediumButton.image = Resources.Load<Image>("Settings/CursorButtonMediumDeactivated");
                CursorSizeBigButton.image = Resources.Load<Image>("Settings/CursorButtonBigDeactivated");
                Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                break;

            case "Medium" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Medium:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Medium;
                Cursor.SetCursor(CursorTextures[1], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image = Resources.Load<Image>("Settings/CursorButtonSmallDeactivated");
                CursorSizeMediumButton.image = Resources.Load<Image>("Settings/CursorButtonMediumActivated");
                CursorSizeBigButton.image = Resources.Load<Image>("Settings/CursorButtonBigDeactivated");
                Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                break;

            case "Big" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Big:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Big;
                Cursor.SetCursor(CursorTextures[2], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image = Resources.Load<Image>("Settings/CursorButtonSmallDeactivated");
                CursorSizeMediumButton.image = Resources.Load<Image>("Settings/CursorButtonMediumDeactivated");
                CursorSizeBigButton.image = Resources.Load<Image>("Settings/CursorButtonBigActivated");
                Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                break;

        }

        SettingsInfo.SaveSettingsData();
    }

    //--------------------------------------------

    public void SaveAndQuit()
    {
        // Issaugoti progresa

        Application.Quit();
    }
}
