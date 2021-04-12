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
    public Image[] CalendarNumbersForMain;
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
    public Image[] CalendarNumbersForCalendar;
    public Image[] TaskDoneImages;
    public Image[] TaskFailedImages;
    public Image[] BigTaskImages;
    public Image[] HomeTaskLine1Images;
    public Image[] HomeTaskLine2Images;
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
        SettingsInfo.InitializeSettings();
        MusicSlider.value = SettingsInfo.SettingsObject.MusicVolume;
        SoundSlider.value = SettingsInfo.SettingsObject.SoundVolume;
        SetCursorSize(SettingsInfo.SettingsObject.CursorSize.ToString(), true);
        //Cursor.lockState = CursorLockMode.Confined;

        PlayerInfo.LoadPlayerData();
        UpdateCalendarNumbersForMain();
        UpdateCalendarNumbersForCalendar();
        SetPhoneCase();
        SetSaveWindowObjects();

        //paleisti tasku script
        SetTaskImages();
    }

    public void UpdateCalendarNumbersForMain()
    {
        for (int i = 0; i < 14; i++)
        {
            if (PlayerInfo.PlayerDataObject.CurrentDay == i + 1)
                CalendarNumbersForMain[i].enabled = true;
            else
                CalendarNumbersForMain[i].enabled = false;
        }
    }

    public void UpdateCalendarNumbersForCalendar()
    {
        for (int i = 0; i < 14; i++)
        {
            if (PlayerInfo.PlayerDataObject.CurrentDay == i + 1)
                CalendarNumbersForCalendar[i].enabled = true;
            else
                CalendarNumbersForCalendar[i].enabled = false;
        }
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
        switch (PlayerInfo.PlayerDataObject.Level)
        {
            case 1:
                Level1Image.enabled = true;
                Level2Image.enabled = false;
                Level3Image.enabled = false;
                break;
            case 2:
                Level1Image.enabled = false;
                Level2Image.enabled = true;
                Level3Image.enabled = false;
                break;
            case 3:
                Level1Image.enabled = false;
                Level2Image.enabled = false;
                Level3Image.enabled = true;
                break;
        }

        switch (PlayerInfo.PlayerDataObject.Name)
        {
            case "":
                Player1Image.enabled = true;
                Player2Image.enabled = false;
                Player3Image.enabled = false;
                break;
            case "":
                Player1Image.enabled = false;
                Player2Image.enabled = true;
                Player3Image.enabled = false;
                break;
            case "":
                Player1Image.enabled = false;
                Player2Image.enabled = false;
                Player3Image.enabled = true;
                break;
        }
    }

    public void SetTaskImages()
    {
        // cleaning
        for (int i = 0; i < 3; i++)
        {
            TaskDoneImages[i].enabled = false;
            TaskFailedImages[i].enabled = false;
        }
        for (int i = 0; i < 2; i++)
        {
            BigTaskImages[i].enabled = false;
        }
        for (int i = 0; i < 4; i++)
        {
            HomeTaskLine1Images[i].enabled = false;
            HomeTaskLine1Images[i].enabled = false;
        }

        //settings
        foreach (var day in GameDaysInfo.GameDataObject.DayList)
        {
            if (day.number == PlayerInfo.PlayerDataObject.CurrentDay)
            {
                // cia negerai -------------------------------------------------------------------------------------------
                // kam cia viskas cikle? gal uztenka paimti viena objekta ir su juo ziasti kad kvietinet nereiktu?
                // big task
                if (day.DaysBigTasks.First().Task == TaskGeneration.BigTasks.Shopping)
                    BigTaskImages[0].enabled = true;
                else
                    BigTaskImages[1].enabled = true;

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
                        HomeTaskLine1Images[0].enabled = true;
                        break;
                    case TaskGeneration.HomeTasks.Ducks:
                        HomeTaskLine1Images[1].enabled = true;
                        break;
                    case TaskGeneration.HomeTasks.Trashes:
                        HomeTaskLine1Images[2].enabled = true;
                        break;
                    case TaskGeneration.HomeTasks.ToiletPapper:
                        HomeTaskLine1Images[3].enabled = true;
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
                            HomeTaskLine2Images[0].enabled = true;
                            break;
                        case TaskGeneration.HomeTasks.Ducks:
                            HomeTaskLine2Images[1].enabled = true;
                            break;
                        case TaskGeneration.HomeTasks.Trashes:
                            HomeTaskLine2Images[2].enabled = true;
                            break;
                        case TaskGeneration.HomeTasks.ToiletPapper:
                            HomeTaskLine2Images[3].enabled = true;
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

        UpdateCalendarNumbersForMain();

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

        UpdateCalendarNumbersForCalendar();

        // uzkrauti uzduotis

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
        // Issaugoti progresa

        Application.Quit();
    }
}
