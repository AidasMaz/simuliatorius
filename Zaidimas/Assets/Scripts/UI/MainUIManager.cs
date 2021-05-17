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
    public bool inTask;
    public float waitTime = 1f;

    private List<uint> timerIDs = new List<uint>();

    public enum ButtonVariants
    {
        Sleep,
        Ducks,
        Trashes,
        Dishes,
        TP,
        Working
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public void InitializeUI()
    {
        inTask = false;
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

        GetNextTask();
    }

    public void GetNextTask()
    {
        TaskGeneration.Day day = DayTaskManager.GameDataObject.DayList[PlayerInfo.PlayerDataObject.CurrentDay - 1];
        foreach (var mainTask in day.DaysBigTasks)
        {
            if (mainTask.Status == TaskGeneration.TaskStatus.New)
            {
                switch (mainTask.Task)
                {
                    case TaskGeneration.BigTasks.Working:
                        ButtonTask = ButtonVariants.Working;
                        return;
                }
            }
        }
        foreach (var smallTask in day.DaysHomeTasks)
        {
            if (smallTask.Status == TaskGeneration.TaskStatus.New)
            {
                switch (smallTask.Task)
                {
                    case TaskGeneration.HomeTasks.Dishes:
                        ButtonTask = ButtonVariants.Dishes;
                        return;
                    case TaskGeneration.HomeTasks.Ducks:
                        ButtonTask = ButtonVariants.Ducks;
                        return;
                    case TaskGeneration.HomeTasks.Trashes:
                        ButtonTask = ButtonVariants.Trashes;
                        return;
                    case TaskGeneration.HomeTasks.ToiletPapper:
                        ButtonTask = ButtonVariants.TP;
                        return;
                }
            }
        }
        ButtonTask = ButtonVariants.Sleep;
    }

    public void MarkOffCurrentTask(bool succeeded)
    {
        TaskGeneration.Day day = DayTaskManager.GameDataObject.DayList[PlayerInfo.PlayerDataObject.CurrentDay - 1];

        TaskGeneration.TaskStatus asignableStatus;
        if (succeeded)
            asignableStatus = TaskGeneration.TaskStatus.Done;
        else
            asignableStatus = TaskGeneration.TaskStatus.Failed;

        foreach (var mainTask in day.DaysBigTasks)
        {
            if (mainTask.Status == TaskGeneration.TaskStatus.New)
            {
                mainTask.Status = asignableStatus;
                return;
            }
        }
        foreach (var smallTask in day.DaysHomeTasks)
        {
            if (smallTask.Status == TaskGeneration.TaskStatus.New)
            {
                smallTask.Status = asignableStatus;
                return;
            }
        }
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
        inTask = true;
        ShowMiniGameStartWindow(ButtonTask);

        // pradeti skaiciuoti laika ir parodyti objektus
    }

    public void ShowMiniGameEndWindow(string game, bool goodEnd)
    {
        bool goodend = true;

        if (goodend)
        {
            MiniGameEndText.text = "Task done successesfuly!";
            MarkOffCurrentTask(true);
        }
        else
        {
            MiniGameEndText.text = "Task was failed...";
            MarkOffCurrentTask(false);
        }

        MiniGameEndWindow.SetActive(true);

        timerIDs.Add(TimerManager.StartTimer(waitTime, false, delegate
        {
            GetNextTask();
            MiniGameEndWindow.SetActive(false);
            inTask = false;
        }));
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
