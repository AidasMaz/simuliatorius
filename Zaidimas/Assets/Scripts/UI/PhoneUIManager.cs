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

    [Header("Managers")]
    public TaskGeneration GameDaysInfo;
    public SettingSaving SettingsInfo;
    public PlayerDataSaving PlayerInfo;

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void Start()
    {
        GameDaysInfo.InitializeDayData();
        SetTaskImages();
        UpdateCalendarNumberForCalendar();

        SettingsInfo.InitializeSettings();
        MusicSlider.value = SettingsInfo.SettingsObject.MusicVolume;
        SoundSlider.value = SettingsInfo.SettingsObject.SoundVolume;
        SetCursorSize(SettingsInfo.SettingsObject.CursorSize.ToString(), true);
        //Cursor.lockState = CursorLockMode.Confined;

        PlayerInfo.LoadPlayerData();
        UpdateCalendarNumberForMain();
        SetPhoneCase();
        SetSaveWindowObjects();
    }

    public void UpdateCalendarNumberForMain()
    {
        int num = PlayerInfo.PlayerDataObject.CurrentDay;
        CalendarNumberForMain.sprite = Resources.Load<Sprite>("Images/test");
    }

    public void UpdateCalendarNumberForCalendar()
    {
        int num = PlayerInfo.PlayerDataObject.CurrentDay;
        CalendarNumberForCalendar.sprite = Resources.Load<Sprite>("Images/test");
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
        LevelImage.sprite = Resources.Load<Sprite>("Images/test");

        string name = PlayerInfo.PlayerDataObject.Name;
        PlayerImage.sprite = Resources.Load<Sprite>("Images/test");
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
                    BigTaskImage.sprite = Resources.Load<Sprite>("Images/test");
                else
                    BigTaskImage.sprite = Resources.Load<Sprite>("Images/test");

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
                        HomeTaskLine1Image.sprite = Resources.Load<Sprite>("Images/test");
                        break;
                    case TaskGeneration.HomeTasks.Ducks:
                        HomeTaskLine1Image.sprite = Resources.Load<Sprite>("Images/test");
                        break;
                    case TaskGeneration.HomeTasks.Trashes:
                        HomeTaskLine1Image.sprite = Resources.Load<Sprite>("Images/test");
                        break;
                    case TaskGeneration.HomeTasks.ToiletPapper:
                        HomeTaskLine1Image.sprite = Resources.Load<Sprite>("Images/test");
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
                            HomeTaskLine2Image.sprite = Resources.Load<Sprite>("Images/test");
                            break;
                        case TaskGeneration.HomeTasks.Ducks:
                            HomeTaskLine2Image.sprite = Resources.Load<Sprite>("Images/test");
                            break;
                        case TaskGeneration.HomeTasks.Trashes:
                            HomeTaskLine2Image.sprite = Resources.Load<Sprite>("Images/test");
                            break;
                        case TaskGeneration.HomeTasks.ToiletPapper:
                            HomeTaskLine2Image.sprite = Resources.Load<Sprite>("Images/test");
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

        UpdateCalendarNumberForCalendar();
        SetTaskImages();

        //MainWindow.SetActive(false);
        //TasksWindow.SetActive(true);
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
                CursorSizeSmallButton.image = Resources.Load<Image>("Settings/CursorButtonSmallActivated");
                CursorSizeMediumButton.image = Resources.Load<Image>("Settings/CursorButtonMediumDeactivated");
                CursorSizeBigButton.image = Resources.Load<Image>("Settings/CursorButtonBigDeactivated");

                //Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                // Jei norim saugoti po kiekvieno paspaudimo
                SettingsInfo.SaveSettingsData();
                break;

            case "Medium" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Medium || initial:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Medium;
                Cursor.SetCursor(CursorTextures[1], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image = Resources.Load<Image>("Settings/CursorButtonSmallDeactivated");
                CursorSizeMediumButton.image = Resources.Load<Image>("Settings/CursorButtonMediumActivated");
                CursorSizeBigButton.image = Resources.Load<Image>("Settings/CursorButtonBigDeactivated");
                //Debug.Log("Cursor size set to: " + SettingsInfo.SettingsObject.CursorSize);
                // Jei norim saugoti po kiekvieno paspaudimo
                SettingsInfo.SaveSettingsData();
                break;

            case "Big" when SettingsInfo.SettingsObject.CursorSize != SettingSaving.CursorSize.Big || initial:
                SettingsInfo.SettingsObject.CursorSize = SettingSaving.CursorSize.Big;
                Cursor.SetCursor(CursorTextures[2], Vector2.zero, CursorMode.Auto);
                //ClickSound.Play();
                CursorSizeSmallButton.image = Resources.Load<Image>("Settings/CursorButtonSmallDeactivated");
                CursorSizeMediumButton.image = Resources.Load<Image>("Settings/CursorButtonMediumDeactivated");
                CursorSizeBigButton.image = Resources.Load<Image>("Settings/CursorButtonBigActivated");
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
