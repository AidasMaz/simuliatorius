using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Image CalendarNumberForMain;
    [Space]
    public GameObject MapWindow;
    [Space]
    public GameObject SaveWindow;
    public Image LevelImage;
    public Image PlayerImage;
    [Space]
    public GameObject TasksWindow;
    public Image CalendarNumberForCalendar;
    public Image[] TaskDoneImages;
    public Image[] TaskFailedImages;
    public Image BigTaskImage;
    public Image HomeTaskLine1Image;
    public Image HomeTaskLine2Image;
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

    [Header("Managers and controllers")]
    public TaskGeneration GameDaysInfo;
    public SettingSaving SettingsInfo;
    public PlayerDataSaving PlayerInfo;
    public TweeningPhone TweeningPhoneController;
    public cameraFollowing CameraFollowingController;

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        //isimti kai bus menu scena
        PlayerInfo.CreatePlayerData("Molly");

        PlayerInfo.LoadPlayerData();
        GameDaysInfo.InitializeDayData();
        SettingsInfo.InitializeSettings();

        SetTaskImages();
        UpdateCalendarNumberForCalendar();

        MusicSlider.value = SettingsInfo.SettingsObject.MusicVolume;
        SoundSlider.value = SettingsInfo.SettingsObject.SoundVolume;
        SetCursorSize(SettingsInfo.SettingsObject.CursorSize.ToString(), true);
        //Cursor.lockState = CursorLockMode.Confined;

        UpdateCalendarNumberForMain();
        SetPhoneCase();
        SetSaveWindowObjects();
    }

    public void TakeOutPhone()
    {
        TweeningPhoneController.QuickMoveFromRight();
        CameraFollowingController.ofset = true;
        Phone.SetActive(true);
        //TurnOnSound.Play();
    }

    public void PutAwayPhone()
    {
        TweeningPhoneController.QuickMoveToRight();
        CameraFollowingController.ofset = false;
        //TurnOffSound.Play();
    }

    public void UpdateCalendarNumberForMain()
    {
        int num = 10;// PlayerInfo.PlayerDataObject.CurrentDay;
        if (num < 10)
            CalendarNumberForMain.sprite = Resources.LoadAll<Sprite>("UI/Multi-use objects/Numbers/numbersSingleDigit")[num];
        else
            CalendarNumberForMain.sprite = Resources.LoadAll<Sprite>("UI/Multi-use objects/Numbers/numbersTwoDigit")[num - 10];
        CalendarNumberForMain.preserveAspect = true;
    }

    public void UpdateCalendarNumberForCalendar()
    {
        int num = PlayerInfo.PlayerDataObject.CurrentDay;
        if (num < 10)
            CalendarNumberForCalendar.sprite = Resources.LoadAll<Sprite>("UI/Multi-use objects/Numbers/numbersSingleDigit")[num];
        else
            CalendarNumberForCalendar.sprite = Resources.LoadAll<Sprite>("UI/Multi-use objects/Numbers/numbersTwoDigit")[num - 10];
        CalendarNumberForCalendar.preserveAspect = true;
    }

    public void SetPhoneCase()
    {
        switch (PlayerInfo.PlayerDataObject.PhoneColor)
        {
            case "White":
                CaseWhite.SetActive(true);
                CaseGreen.SetActive(false);
                CasePurple.SetActive(false);
                break;
            case "Green":
                CaseWhite.SetActive(false);
                CaseGreen.SetActive(true);
                CasePurple.SetActive(false);
                break;
            case "Purple":
                CaseWhite.SetActive(false);
                CaseGreen.SetActive(false);
                CasePurple.SetActive(true);
                break;
        }
    }

    public void SetSaveWindowObjects()
    {
        int lvl = PlayerInfo.PlayerDataObject.Level;
        LevelImage.sprite = Resources.LoadAll<Sprite>("UI/Saving objects/levelIcons")[lvl - 1];
        LevelImage.preserveAspect = true;

        string name = PlayerInfo.PlayerDataObject.Name;
        switch (name)
        {
            case "Alex":
                PlayerImage.sprite = Resources.LoadAll<Sprite>("UI/Saving objects/characters")[2];
                break;
            case "Molly":
                PlayerImage.sprite = Resources.LoadAll<Sprite>("UI/Saving objects/characters")[1];
                break;
            case "Rob":
                PlayerImage.sprite = Resources.LoadAll<Sprite>("UI/Saving objects/characters")[0];
                break;
        }
        PlayerImage.preserveAspect = true;
    }

    public void SetTaskImages()
    {
        // cleaning
        for (int i = 0; i < 3; i++)
        {
            TaskDoneImages[i].enabled = false;
            TaskFailedImages[i].enabled = false;
        }

        // showing
        foreach (var day in GameDaysInfo.GameDataObject.DayList)
        {
            if (day.number == PlayerInfo.PlayerDataObject.CurrentDay)
            {
                // big task
                if (day.DaysBigTasks.First().Task == TaskGeneration.BigTasks.Shopping)
                    BigTaskImage.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[2];
                else if (day.DaysBigTasks.First().Task == TaskGeneration.BigTasks.Working)
                    BigTaskImage.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[5];

                switch (day.DaysBigTasks.First().Status)
                {
                    case TaskGeneration.TaskStatus.Done:
                        TaskDoneImages[0].enabled = true;
                        break;
                    case TaskGeneration.TaskStatus.Failed:
                        TaskFailedImages[0].enabled = true;
                        break;
                }

                //other tasks
                switch (day.DaysHomeTasks.First().Task)
                {
                    case TaskGeneration.HomeTasks.Dishes:
                        HomeTaskLine1Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[0];
                        break;
                    case TaskGeneration.HomeTasks.Ducks:
                        HomeTaskLine1Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[4];
                        break;
                    case TaskGeneration.HomeTasks.Trashes:
                        HomeTaskLine1Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[3];
                        break;
                    case TaskGeneration.HomeTasks.ToiletPapper:
                        HomeTaskLine1Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[1];
                        break;
                }

                switch (day.DaysHomeTasks.First().Status)
                {
                    case TaskGeneration.TaskStatus.Done:
                        TaskDoneImages[1].enabled = true;
                        break;
                    case TaskGeneration.TaskStatus.Failed:
                        TaskFailedImages[1].enabled = true;
                        break;
                }

                if (day.DaysHomeTasks.Count > 1)
                {
                    switch (day.DaysHomeTasks.ElementAt(1).Task)
                    {
                        case TaskGeneration.HomeTasks.Dishes:
                            HomeTaskLine2Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[0];
                            break;
                        case TaskGeneration.HomeTasks.Ducks:
                            HomeTaskLine2Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[4];
                            break;
                        case TaskGeneration.HomeTasks.Trashes:
                            HomeTaskLine2Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[3];
                            break;
                        case TaskGeneration.HomeTasks.ToiletPapper:
                            HomeTaskLine2Image.sprite = Resources.LoadAll<Sprite>("UI/Calendar objects/Tasks")[1];
                            break;
                    }

                    switch (day.DaysHomeTasks.First().Status)
                    {
                        case TaskGeneration.TaskStatus.Done:
                            TaskDoneImages[2].enabled = true;
                            break;
                        case TaskGeneration.TaskStatus.Failed:
                            TaskFailedImages[2].enabled = true;
                            break;
                    }
                }
            }
        }

    }

    //--------------------------------------------

    public void OpenMainWindow()
    {
        //ClickSound.Play();

        UpdateCalendarNumberForMain();

        MainWindow.SetActive(true);
        MapWindow.SetActive(false);
        SaveWindow.SetActive(false);
        SettingsWindow.SetActive(false);
        TasksWindow.SetActive(false);
    }

    public void OpenMapWindow()
    {
        //ClickSound.Play();
        MainWindow.SetActive(false);
        MapWindow.SetActive(true);
    }

    public void OpenSaveWindow()
    {
        //ClickSound.Play();
        MainWindow.SetActive(false);
        SaveWindow.SetActive(true);
    }

    public void OpenSettingsWindow()
    {
        //ClickSound.Play();
        MainWindow.SetActive(false);
        SettingsWindow.SetActive(true);
    }

    public void OpenTasksWindow()
    {
        //ClickSound.Play();

        UpdateCalendarNumberForCalendar();
        SetTaskImages();

        MainWindow.SetActive(false);
        TasksWindow.SetActive(true);
    }

    //--------------------------------------------

    public void SaveSliderVolume(string name)
    {
        if (name == "music")
            SettingsInfo.SettingsObject.MusicVolume = MusicSlider.value;
        else
            SettingsInfo.SettingsObject.SoundVolume = SoundSlider.value;

        SettingsInfo.SaveSettingsData();
    }

    public void SetCursor(string size)
    {
        SetCursorSize(size, false);
    }

    private void SetCursorSize(string size, bool initial)
    {
        switch (size)
        {
            case "Small" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Small || initial:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Small;
                Cursor.SetCursor(CursorTextures[0], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                Debug.Log(Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI").Length);
                CursorSizeSmallButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[0];
                CursorSizeMediumButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[4];
                CursorSizeBigButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[5];

                //Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                // Jei norim saugoti po kiekvieno paspaudimo
                SettingsInfo.SaveSettingsData();
                break;

            case "Medium" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Medium || initial:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Medium;
                Cursor.SetCursor(CursorTextures[1], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[3];
                CursorSizeMediumButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[1];
                CursorSizeBigButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[5];
                //Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                // Jei norim saugoti po kiekvieno paspaudimo
                SettingsInfo.SaveSettingsData();
                break;

            case "Big" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Big || initial:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Big;
                Cursor.SetCursor(CursorTextures[2], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[3];
                CursorSizeMediumButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[4];
                CursorSizeBigButton.image.sprite = Resources.LoadAll<Sprite>("UI/Option objects/cursorButtonUI")[2];
                //Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                // Jei norim saugoti po kiekvieno paspaudimo
                SettingsInfo.SaveSettingsData();
                break;
        }
    }

    //--------------------------------------------

    public void SaveAndQuit()
    {
        PlayerInfo.SavePlayerData();
        GameDaysInfo.SaveLevelData();

        Application.Quit();
    }
}
