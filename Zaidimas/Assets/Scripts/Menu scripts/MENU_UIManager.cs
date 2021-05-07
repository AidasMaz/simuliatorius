using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MENU_UIManager : MonoBehaviour
{
    [Header("Main window objects")]
    public GameObject MainWindow;

    [Header("Level window objects")]
    public GameObject LevelWindow;
    [Space]
    public Button PlayGameButton;
    public Button DeleteProgressButton;

    [Header("Player choosing window objects")]
    public GameObject PlayerChoosingWindow;
    [Space]
    public Button ButtonAlex;
    public Button ButtonMolly;
    public Button ButtonRob;
    [Space]
    public Button PlayGame;

    [Header("Audio sources")]
    public AudioSource MenuMusic;
    [Space]
    public AudioSource ClickSound;

    [Header("Managers")]
    public SettingSaving SettingsManager;

    [Header("Variables and other stuff")]
    public Texture2D[] CursorTextures;

    private static string PlayerFileName = "player.v1";
    private static string LevelDataFileName = "dayData.v1";

    //+++++++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        SettingsManager.InitializeSettings();
        // set volume
        SetCursorSize();
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

        SettingsManager.SaveSettingsData();
    }

    private void Update()
    {

    }

    //--------------------------

    public void OpenLevelWindow()
    {
        //ClickSound.Play();
    }

    public void OpenPlayerChoosingWindow()
    {
        //ClickSound.Play();
    }

    //--------------------------

    public void GoToGame()
    {

    }

    public void DeleteProgress()
    {
        DeleteProgressButton.enabled = false;

        string playerPath = Application.persistentDataPath + "/" + PlayerFileName;
        if (File.Exists(playerPath))
            File.Delete(playerPath);

        string levelPath = Application.persistentDataPath + "/" + LevelDataFileName;
        if (File.Exists(levelPath))
            File.Delete(levelPath);

        Debug.Log("Progress deleted!(player: " + File.Exists(playerPath) + "; task: " + File.Exists(levelPath) + ")");
    }
}
