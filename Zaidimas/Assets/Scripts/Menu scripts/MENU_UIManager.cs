using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MENU_UIManager : MonoBehaviour
{
    [Header("Main window objects")]
    public Button StartButton;
    public Button AboutButton;

    [Header("Crane objects")]
    public GameObject CraneWindow;
    [Space]
    public Vector3 CraneWindowHidenPos;
    public Vector3 CraneWindowShownPos;
    [Space]
    public GameObject BackgroundPanel;

    [Header("Level window objects")]
    public GameObject LevelStartWindow;
    [Space]
    public Image PlayerImage;

    [Header("Player choosing window objects")]
    public GameObject PlayerChoosingWindow;
    [Space]
    public Button ButtonAlex;
    public Button ButtonMolly;
    public Button ButtonRob;
    [Space]
    public Sprite[] RegularPlayerSprites;
    public Sprite[] SelectedPlayerSprites;
    [Space]
    public Button GoToLevelStartWindow;

    [Header("About window objects")]
    public GameObject AboutWindow;
    [Space]
    public Vector3 AboutWindowHidenPos;
    public Vector3 AboutWindowShownPos;

    [Header("Audio sources")]
    public AudioSource MenuMusic;
    [Space]
    public AudioSource ClickSound;

    [Header("Managers")]
    public SettingSaving SettingsManager;
    public PlayerDataSaving PlayerDataManager;

    [Header("Variables and other stuff")]
    public Texture2D[] CursorTextures;
    [Space]
    public string playerNameFromChoosingWindow;
    [Space]
    public bool aboutWindowOpened;
    public bool craneWindowOpened;
    public bool justCreatedPlayer;
    [Space]
    public Vector3 normal;

    private List<uint> timerIDs = new List<uint>();

    private static string PlayerFileName = "player.v1";
    private static string LevelDataFileName = "dayData.v1";

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowHidenPos, 0.0f);

        PlayerDataManager.LoadDataOnly();
        SettingsManager.InitializeSettings();
        // set volume
        SetCursorSize();

        string playerPath = Application.persistentDataPath + "/" + PlayerFileName;
        if (!File.Exists(playerPath))
        {
            playerNameFromChoosingWindow = "";
            SetPlayerName("Alex");
        }
        else
        {
            SetPlayerName(PlayerDataManager.PlayerDataObject.Name);
            switch (playerNameFromChoosingWindow)
            {
                case "Alex":
                    PlayerImage.sprite = SelectedPlayerSprites[0];
                    break;
                case "Molly":
                    PlayerImage.sprite = SelectedPlayerSprites[1];
                    break;
                case "Rob":
                    PlayerImage.sprite = SelectedPlayerSprites[2];
                    break;
            }
        }

        aboutWindowOpened = false;
        justCreatedPlayer = false;

        normal = BackgroundPanel.gameObject.GetComponent<Transform>().localScale;
    }

    private void SetCursorSize()
    {
        switch (SettingsManager.SettingsObject.CursorSize.ToString())
        {
            case "Small":
                Cursor.SetCursor(CursorTextures[0], Vector2.zero, CursorMode.Auto);
                break;
            case "Medium":
                Cursor.SetCursor(CursorTextures[1], Vector2.zero, CursorMode.Auto);
                break;
            case "Big":
                Cursor.SetCursor(CursorTextures[2], Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    public void PlayButtonPress()
    {
        string levelPath = Application.persistentDataPath + "/" + PlayerFileName;

        if (aboutWindowOpened)
        {
            aboutWindowOpened = false;
            CloseAboutWindow();
        }

        if (!File.Exists(levelPath))
        {
            OpenPlayerChoosingWindow();
            LevelStartWindow.SetActive(false);
        }
        else
        {
            OpenLevelStartWindow();
            PlayerChoosingWindow.SetActive(false);
        }

        OpenCraneWindow();
    }

    public void OpenCraneWindow()
    {
        if (!craneWindowOpened)
        {
            //ClickSound.Play();
            craneWindowOpened = true;
            AboutButton.interactable = false;
            StartButton.interactable = false;
            LeanTween.moveLocal(CraneWindow.gameObject, CraneWindowHidenPos, 0.0f);
            LeanTween.moveLocal(CraneWindow.gameObject, CraneWindowShownPos, 0.7f);
        }
    }
    public void CloseCraneWindow()
    {
        //ClickSound.Play();
        craneWindowOpened = false;
        AboutButton.interactable = true;
        StartButton.interactable = true;
        LeanTween.moveLocal(CraneWindow.gameObject, CraneWindowHidenPos, 0.7f);
        
    }

    public void SqeezePlaneObj()
    {
        Vector3 final = new Vector3(0, normal.y, normal.z);
        LeanTween.scale(BackgroundPanel.gameObject, final, 0.4f);
    }
    public void ExpandPlaneObj()
    {
        Vector3 final = new Vector3(normal.x, normal.y, normal.z);
        LeanTween.scale(BackgroundPanel.gameObject, final, 0.4f);
    }

    //-----------------------------------

    public void OpenPlayerChoosingWindow()
    {
        //ClickSound.Play();
        //SetPlayerData();
        
        justCreatedPlayer = true;
        PlayerChoosingWindow.SetActive(true);
    }
    public void ClosePlayerChoosingWindow()
    {
        //ClickSound.Play();
        PlayerChoosingWindow.SetActive(false);
    }

    public void SetPlayerData()
    {
        PlayerDataManager.InitializePlayerData(playerNameFromChoosingWindow);
        SqeezePlaneObj();
        timerIDs.Add(TimerManager.StartTimer(0.4f, false, delegate
        {
            ClosePlayerChoosingWindow();
            OpenLevelStartWindow();
            ExpandPlaneObj();
        }));
    }

    public void SetPlayerName(string name)
    {
        if (playerNameFromChoosingWindow != name)
        {
            //ClickSound.Play();
            switch (name)
            {
                case "Alex":
                    ButtonAlex.image.sprite = SelectedPlayerSprites[0];
                    ButtonMolly.image.sprite = RegularPlayerSprites[1];
                    ButtonRob.image.sprite = RegularPlayerSprites[2];
                    break;
                case "Molly":
                    ButtonAlex.image.sprite = RegularPlayerSprites[0];
                    ButtonMolly.image.sprite = SelectedPlayerSprites[1];
                    ButtonRob.image.sprite = RegularPlayerSprites[2];
                    break;
                case "Rob":
                    ButtonAlex.image.sprite = RegularPlayerSprites[0];
                    ButtonMolly.image.sprite = RegularPlayerSprites[1];
                    ButtonRob.image.sprite = SelectedPlayerSprites[2];
                    break;
            }
            playerNameFromChoosingWindow = name;
        }
    }

    //-----------------------------------

    public void OpenLevelStartWindow()
    {
        //ClickSound.Play();

        switch (playerNameFromChoosingWindow)
        {
            case "Alex":
                PlayerImage.sprite = SelectedPlayerSprites[0];
                break;
            case "Molly":
                PlayerImage.sprite = SelectedPlayerSprites[1];
                break;
            case "Rob":
                PlayerImage.sprite = SelectedPlayerSprites[2];
                break;
        }

        LevelStartWindow.SetActive(true);
    }
    public void CloseLevelStartWindow()
    {
        //ClickSound.Play();
        if (justCreatedPlayer)
        {
            SqeezePlaneObj();
            timerIDs.Add(TimerManager.StartTimer(0.4f, false, delegate
            {
                OpenPlayerChoosingWindow();
                LevelStartWindow.SetActive(false);
                ExpandPlaneObj();
            }));
        }
        else
        {
            CloseCraneWindow();
        }
    }

    public void LoadGameScene()
    {
        //ClickSound.Play();
        PlayerPrefs.SetString("CHARACTER_NAME", playerNameFromChoosingWindow);
        SceneManager.LoadScene(1);
    }

    public void DeleteProgress()
    {
        //ClickSound.Play();

        //DeleteProgressButton.enabled = false;

        string playerPath = Application.persistentDataPath + "/" + PlayerFileName;
        if (File.Exists(playerPath))
            File.Delete(playerPath);

        string levelPath = Application.persistentDataPath + "/" + LevelDataFileName;
        if (File.Exists(levelPath))
            File.Delete(levelPath);

        playerNameFromChoosingWindow = "Alex";
        ButtonAlex.image.sprite = SelectedPlayerSprites[0];
        ButtonMolly.image.sprite = RegularPlayerSprites[1];
        ButtonRob.image.sprite = RegularPlayerSprites[2];

        SqeezePlaneObj();
        timerIDs.Add(TimerManager.StartTimer(0.4f, false, delegate
        {
            LevelStartWindow.SetActive(false);
            OpenPlayerChoosingWindow();
            ExpandPlaneObj();
        }));

        Debug.Log("Progress deleted!(player file exists: " + File.Exists(playerPath) + "; task file exists: " + File.Exists(levelPath) + ")");
    }

    //----------------------------------

    public void OpenAboutWindow()
    {
        if (!aboutWindowOpened)
        {
            //ClickSound.Play();
            aboutWindowOpened = true;
            AboutButton.interactable = false;
            StartButton.interactable = false;
            LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowHidenPos, 0.0f);
            LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowShownPos, 0.6f);
        }
    }
    public void CloseAboutWindow()
    {
        //ClickSound.Play();
        aboutWindowOpened = false;
        AboutButton.interactable = true;
        StartButton.interactable = true;
        LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowHidenPos, 0.5f);
    }

    public void QuitGame()
    {
        //ClickSound.Play();
        Application.Quit();
    }

    private void OnDestroy()
    {
        foreach (uint id in timerIDs)
        {
            if (TimerManager.TimeRemaining(id) > 0)
            {
                TimerManager.CancelTimer(id);
            }
        }
    }
}
