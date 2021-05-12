using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MENU_UIManager : MonoBehaviour
{
    //[Header("Main window objects")]
    //public GameObject MainWindow;

    [Header("Level window objects")]
    public GameObject LevelStartWindow;
    [Space]
    public Button LoadLevelButton;
    public Button DeleteProgressButton;

    [Header("Player choosing window objects")]
    public GameObject PlayerChoosingWindow;
    [Space]
    public Button ButtonAlex;
    public Button ButtonMolly;
    public Button ButtonRob;
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

    private static string PlayerFileName = "player.v1";
    private static string LevelDataFileName = "dayData.v1";

    //+++++++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowHidenPos, 0.0f);

        SettingsManager.InitializeSettings();
        // set volume
        SetCursorSize();

        playerNameFromChoosingWindow = "Alex";
        // set button for alex

        aboutWindowOpened = false;
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
            CloseAboutWindow();

        if (!File.Exists(levelPath))
        {
            OpenPlayerChoosingWindow();
        }
        else
        {
            OpenLevelStartWindow();
        }
    }

    //-----------------------------------

    public void OpenPlayerChoosingWindow()
    {
        //ClickSound.Play();
        PlayerChoosingWindow.SetActive(true);
    }
    public void ClosePlayerChoosingWindow()
    {
        //ClickSound.Play();
        PlayerChoosingWindow.SetActive(false);
    }

    public void SetPlayerData()
    {
        PlayerDataManager.InitializePlayerData(name);
        ClosePlayerChoosingWindow();
        OpenLevelStartWindow();
    }

    public void SetPlayerName(string name)
    {
        if (playerNameFromChoosingWindow != name)
        {
            //ClickSound.Play();
            switch (name)
            {
                case "Alex":
                    // pakeiciam su mygtuku kazka
                    break;
                case "Molly":
                    // pakeiciam su mygtuku kazka
                    break;
                case "Rob":
                    // pakeiciam su mygtuku kazka
                    break;
            }
            playerNameFromChoosingWindow = name;
        }
    }

    //-----------------------------------

    public void OpenLevelStartWindow()
    {
        //ClickSound.Play();
        LevelStartWindow.SetActive(true);
    }
    public void CloseLevelStartWindow()
    {
        //ClickSound.Play();
        LevelStartWindow.SetActive(false);
    }

    public void LoadGameScene()
    {

    }

    //----------------------------------

    public void OpenAboutWindow()
    {
        if (!aboutWindowOpened)
        {
            //ClickSound.Play();
            aboutWindowOpened = true;
            LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowHidenPos, 0.0f);
            LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowShownPos, 0.4f);
        }
    }
    public void CloseAboutWindow()
    {
        //ClickSound.Play();
        aboutWindowOpened = false;
        LeanTween.moveLocal(AboutWindow.gameObject, AboutWindowHidenPos, 0.4f);
    }

    public void DeleteProgress()
    {
        //ClickSound.Play();

        DeleteProgressButton.enabled = false;

        string playerPath = Application.persistentDataPath + "/" + PlayerFileName;
        if (File.Exists(playerPath))
            File.Delete(playerPath);

        string levelPath = Application.persistentDataPath + "/" + LevelDataFileName;
        if (File.Exists(levelPath))
            File.Delete(levelPath);

        Debug.Log("Progress deleted!(player file exists: " + File.Exists(playerPath) + "; task file exists: " + File.Exists(levelPath) + ")");
    }

    public void QuitGame()
    {
        //ClickSound.Play();
        Application.Quit();
    }
}
