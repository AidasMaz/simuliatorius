using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [Header("Level Up Objects")]
    public GameObject LevelUpWindow;
    public Image LevelUpWindowImage;
    public Sprite[] LevelUpSprites;

    [Header("Tutorial objects")]
    public GameObject TutorialWindow;

    [Header("Mini Game Windows and objs")]
    public GameObject MiniGameStartWindow;
    //public Sprite MiniGameStartWindowSprite;
    public Text MiniGameText;
    [Space]
    public GameObject MiniGameEndWindow;
    //public Sprite MiniGameEndGoodSprite;
    //public Sprite MiniGameEndBadSprite;
    public Text MiniGameEndText;

    [Header("Players side UI")]
    public Image PlayerInSideMenuImage;
    public Sprite[] PlayerSprites;
    [Space]
    public Button TaskStartButton;

    [Header("Sounds")]
    public AudioSource LevelUpSound;
    public AudioSource ClickSound;

    [Header("Managers and controllers")]
    public PlayerDataSaving PlayerInfo;
    public TaskGeneration DayTaskManager;
    // minigame manager

    [Header("Variables")]
    public ButtonVariants ButtonTask;
    public bool tutorialDone;
    public float waitTime = 1f;

    private List<uint> timerIDs = new List<uint>();

    public enum ButtonVariants
    {
        Sleep,
        Ducks,
        Trashes,
        Dishes,
        TP
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public void InitializeUI()
    {
        tutorialDone = PlayerInfo.PlayerDataObject.TutorialDone;

        if (!tutorialDone)
            ShowTutorial();

        Debug.Log(PlayerInfo.PlayerDataObject.Name);
        switch (PlayerInfo.PlayerDataObject.Name)
        {
            case "Alex":
                PlayerInSideMenuImage.sprite = PlayerSprites[0];
                break;
            case "Molly":
                PlayerInSideMenuImage.sprite = PlayerSprites[1];
                break;
            case "Rob":
                PlayerInSideMenuImage.sprite = PlayerSprites[2];
                break;
        }

        //timerIDs.Add(TimerManager.StartTimer(waitTime, false, delegate { }));
    }

    private void Update()
    {
        if (!tutorialDone)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
            {
                PlayerInfo.PlayerDataObject.TutorialDone = true;
                CloseTutorial();
            }
        }
    }

    public void ShowTutorial()
    {
        TutorialWindow.SetActive(true);
    }
    public void CloseTutorial()
    {
        TutorialWindow.SetActive(false);
    }

    //------------------------------------------------------------

    public void ShowLevelUpWindow()
    {
        int lvl = PlayerInfo.PlayerDataObject.Level;
        LevelUpWindowImage.sprite = LevelUpSprites[lvl - 1];
        LevelUpWindow.SetActive(true);
        //LevelUpSound.Play();
        timerIDs.Add(TimerManager.StartTimer(2f, false, delegate { LevelUpWindow.SetActive(false); }));
    }

    public void ShowMiniGameStartWindow(ButtonVariants game)
    {
        //MiniGameStartWindowSprite = Resources.Load<Sprite>("" + game);
        switch (game)
        {
            case ButtonVariants.Ducks:
                MiniGameText.text = "Find the duck!";
                break;
            case ButtonVariants.Trashes:
                MiniGameText.text = "Clean the trashes!";
                break;
            case ButtonVariants.Dishes:
                MiniGameText.text = "Clean the dishes!";
                break;
            case ButtonVariants.TP:
                MiniGameText.text = "Find the toilet papper!";
                break;
        }

        MiniGameStartWindow.SetActive(true);
    }

    public void StartMiniGame()
    {
        ShowMiniGameStartWindow(ButtonTask);
    }

    public void ShowMiniGameEndWindow(string game, bool goodEnd)
    {

        MiniGameEndWindow.SetActive(true);
        // parodyti gera arba bloga pabaiga
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
