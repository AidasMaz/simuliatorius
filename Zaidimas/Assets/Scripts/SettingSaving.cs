using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine;

public class SettingSaving : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
        public float MusicVolume;
        public float SoundVolume;
        public CursorSize CursorSize;

        public Settings()
        {
            MusicVolume = 0.5f;
            SoundVolume = 0.5f;
            CursorSize = CursorSize.Medium;
        }

        public Settings(float musicV, float soundV, CursorSize cursor)
        {
            MusicVolume = musicV;
            SoundVolume = soundV;
            CursorSize = cursor;
        }
    }

    public enum CursorSize
    {
        Small,
        Medium,
        Big
    }

    public Settings SettingsObject;

    public static string SettingsFileName = "settings.v1";

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    void Awake()
    {
        // namo daiktu ir uzduociu valdymo scriptas
        // telefono UI
        // zaidejo keliavimo scriptas
        // animuoti objektai name
        // zaidejo duomenu saugojimo scriptas
        // nustatymu saugojimo scriptas
        // padaryti colliderius static
        // patestuoti ir paoptimizuoti teksturas ir scriptus
    }

    public void InitializeSettings()
    {
        string path = Application.persistentDataPath + "/" + SettingsFileName;
        if (File.Exists(path))
        {
            //Debug.Log("Settings will be loaded");
            SettingsObject = LoadSettingsData();
            //Debug.Log("Music: " + SettingsObject.MusicVolume + " Sound: " + SettingsObject.SoundVolume + " Cursor: " + SettingsObject.CursorSize);
        }
        else
        {
            //Debug.Log("Settings will be created");
            SettingsObject = new Settings();
            SaveSettingsData();
            //Debug.Log("Music: " + SettingsObject.MusicVolume + " Sound: " + SettingsObject.SoundVolume + " Cursor: " + SettingsObject.CursorSize);
        }
    }

    public static Settings LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/" + SettingsFileName;
        if (File.Exists(path))
        {
            //Debug.Log("Settings save file exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Settings data = formatter.Deserialize(stream) as Settings;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogWarning("Settings save file was not found");
            return null;
        }
    }

    public void SaveSettingsData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + SettingsFileName;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, SettingsObject);
        stream.Close();
    }

    public static void DeleteSettingsData()
    {
        string levelPath = Application.persistentDataPath + "/" + SettingsFileName;

        if (File.Exists(levelPath))
            File.Delete(levelPath);
    }

}
