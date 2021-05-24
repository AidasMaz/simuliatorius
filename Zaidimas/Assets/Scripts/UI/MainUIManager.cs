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
    [Space]
    public GameObject ThoughBubble;
    [Space]
    public Image SearchableItem;
    public Sprite[] Items;

    //[Header("Sounds")]
    //public AudioSource LevelUpSound;
    //public AudioSource ClickSound;

    public GameObject Black;

    [Header("Managers and controllers")]
    public PlayerDataSaving PlayerInfo;
    public TaskGeneration DayTaskManager;
    [Space]
    public TrashTaskManager TaskTrashManager;
    public ToiletPapperTaskManager TaskTPManager;
    public PlateTaskManager TaskPlateManager;
    public DuckTaskManager TaskDuckManager;

    public AudioManager AudioMan;

    [Header("Variables")]
    public ButtonVariants ButtonTask;
    public bool tutorialDone;
    public bool inTask;
    public float waitTime = 1f;
    [Space]
    public int ItemsLeft;

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
        AudioMan = GameObject.Find("AUDIO OBJECT").GetComponent<AudioManager>();
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

        timerIDs.Add(TimerManager.StartTimer(2f, false, delegate { TaskStartButton.gameObject.SetActive(true); }));
    }

    public void PlayClick()
    {
        AudioMan.PlaySound("Click");
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

    public void GetNextDay()
    {
        Black.SetActive(true);
        inTask = false;
        ThoughBubble.SetActive(false);
        PlayerInfo.PlayerDataObject.CurrentDay += 1;
        GetNextTask();
        DayTaskManager.GameDataObject.PrintOutDayList();
        timerIDs.Add(TimerManager.StartTimer(2.5f, false, delegate { Black.SetActive(false); }));
        timerIDs.Add(TimerManager.StartTimer(4f, false, delegate { TaskStartButton.gameObject.SetActive(true); }));
    }

    public void MarkOffCurrentTask(bool succeeded)
    {
        PlayerInfo.PlayerDataObject.Score += 100;
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
                Debug.Log("assigned main");
                return;
            }
        }
        foreach (var smallTask in day.DaysHomeTasks)
        {
            if (smallTask.Status == TaskGeneration.TaskStatus.New)
            {
                smallTask.Status = asignableStatus;
                Debug.Log("assigned small");
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Restoranas padarytas");
            ShowMiniGameEndWindow("Work", true);
        }

        if (Input.GetKeyDown(KeyCode.O) && PlayerInfo.PlayerDataObject.Level < 3)
        {
            Debug.Log("Level up!");
            PlayerInfo.PlayerDataObject.Level += 1;
            ShowLevelUpWindow();
        }
    }

    public void ShowTutorial()
    {
        //TutorialWindow.SetActive(true);
    }
    public void CloseTutorial()
    {
        //TutorialWindow.SetActive(false);
        
    }

    //------------------------------------------------------------

    public void ShowLevelUpWindow()
    {
        int lvl = PlayerInfo.PlayerDataObject.Level;
        LevelUpWindowImage.sprite = LevelUpSprites[lvl - 1];
        LevelUpWindow.SetActive(true);
        //LevelUpSound.Play();
        timerIDs.Add(TimerManager.StartTimer(2.5f, false, delegate { LevelUpWindow.SetActive(false); }));
    }

    public void ShowMiniGameStartWindow(ButtonVariants game)
    {
        Debug.Log("2. show mini window");
        //MiniGameStartWindowSprite = Resources.Load<Sprite>("" + game);
        Debug.Log("3. game: " + game.ToString());
        switch (game)
        {
            case ButtonVariants.Ducks:
                MiniGameText.text = "Find the duck!";
                TaskDuckManager.SetDuckFindingLevel();
                ItemsLeft = 1;
                break;
            case ButtonVariants.Trashes:
                MiniGameText.text = "Clean the trashes!";
                Debug.Log("4. switch");
                TaskTrashManager.SetTrashCleaningLevel(PlayerInfo.PlayerDataObject.CurrentDay);
                ItemsLeft = TaskTrashManager.trashLeftCount;
                break;
            case ButtonVariants.Dishes:
                MiniGameText.text = "Clean the dishes!";
                TaskPlateManager.SetPlateCleaningLevel(PlayerInfo.PlayerDataObject.CurrentDay);
                ItemsLeft = TaskPlateManager.platesLeftCount;
                break;
            case ButtonVariants.TP:
                MiniGameText.text = "Find the toilet papper!";
                TaskTPManager.SetTPFindingLevel();
                ItemsLeft = 1;
                break;
            case ButtonVariants.Working:
                MiniGameText.text = "Time to work!";
                //TaskTPManager.SetTPFindingLevel();
                //ItemsLeft = 1;
                break;
        }

        if (game != ButtonVariants.Sleep)
        {
            MiniGameStartWindow.SetActive(true);
        }

        timerIDs.Add(TimerManager.StartTimer(2.4f, false, delegate
        {
            MiniGameStartWindow.SetActive(false);
            switch (ButtonTask)
            {
                case ButtonVariants.Sleep:
                    SearchableItem.sprite = Items[0];
                    break;
                case ButtonVariants.Ducks:
                    SearchableItem.sprite = Items[1];
                    break;
                case ButtonVariants.Trashes:
                    SearchableItem.sprite = Items[2];
                    break;
                case ButtonVariants.Dishes:
                    SearchableItem.sprite = Items[3];
                    break;
                case ButtonVariants.TP:
                    SearchableItem.sprite = Items[4];
                    break;
                case ButtonVariants.Working:
                    SearchableItem.sprite = Items[5];
                    break;
            }
            ThoughBubble.SetActive(true);
        }));
    }

    public void StartMiniGame()
    {
        Debug.Log("1. mygtukas");
        inTask = true;
        TaskStartButton.gameObject.SetActive(false);
        ShowMiniGameStartWindow(ButtonTask);

        switch (ButtonTask)
        {
            case ButtonVariants.Sleep:
                break;
            case ButtonVariants.Ducks:
                break;
            case ButtonVariants.Trashes:
                break;
            case ButtonVariants.Dishes:
                break;
            case ButtonVariants.TP:
                break;
            case ButtonVariants.Working:
                break;
        }
            
        // skaiciuoti laika
    }

    public void ShowMiniGameEndWindow(string game, bool goodEnd)
    {
        //bool goodend = true;

        if (goodEnd)
        {
            MiniGameEndText.text = "Task done successesfuly!";
            MarkOffCurrentTask(true);
        }
        else
        {
            MiniGameEndText.text = "Task was failed...";
            MarkOffCurrentTask(false);
        }
        DayTaskManager.GameDataObject.PrintOutDayList();
        ThoughBubble.SetActive(false);
        MiniGameEndWindow.SetActive(true);

        timerIDs.Add(TimerManager.StartTimer(waitTime, false, delegate
        {
            GetNextTask();
            timerIDs.Add(TimerManager.StartTimer(2.5f, false, delegate { TaskStartButton.gameObject.SetActive(true); }));
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
