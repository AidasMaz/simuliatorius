using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine;

public class PlayerDataSaving : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {
        public int CurrentDay;
        public string Name;
        public int Level;
        public string PhoneColor;
        public string Place;
        public bool TutorialDone;

        public Player(string name)
        {
            CurrentDay = 1;
            Level = 1;
            Name = name;
            Place = "Home";
            TutorialDone = false;

            switch (name)
            {
                case "Alex":
                    PhoneColor = "White";
                    break;
                case "Rob":
                    PhoneColor = "Green";
                    break;
                case "Molly":
                    PhoneColor = "Purple";
                    break;
            }
        }
    }

    public Player PlayerDataObject;

    public GameObject AlexPrefab;
    public GameObject MollyPrefab;
    public GameObject RobPrefab;

    public Vector3 HomePosition;
    public Vector3 WorkPosition;

    public static string PlayerFileName = "player.v1";

    //-------------------------------

    public cameraFollowing CameraFollowing;

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public void CreatePlayerData(string playerName)
    {
        InitializePlayerData(playerName);

        Vector3 SpawningPoint = new Vector3();
        if (PlayerDataObject.Place == "Home")
            SpawningPoint = HomePosition;
        else if (PlayerDataObject.Place == "Work")
            SpawningPoint = WorkPosition;

        switch (playerName)
        {
            case "Alex":
                Instantiate(AlexPrefab, SpawningPoint, Quaternion.identity);
                break;
            case "Molly":
                Instantiate(MollyPrefab, SpawningPoint, Quaternion.identity);
                break;
            case "Rob":
                Instantiate(RobPrefab, SpawningPoint, Quaternion.identity);
                break;
        }

        CameraFollowing.SetTarget(playerName);
    }

    public void InitializePlayerData(string playerName)
    {
        string path = Application.persistentDataPath + "/" + PlayerFileName;
        if (File.Exists(path))
        {
            Debug.Log("Payer data already exists");
            LoadPlayerData();
            Debug.Log("Player: " + PlayerDataObject.Name + " Phone: " + PlayerDataObject.PhoneColor + " Day: " + PlayerDataObject.CurrentDay + " Level: " + PlayerDataObject.Level);
        }
        else
        {
            //Debug.Log("Payer data will be created");
            PlayerDataObject = new Player(playerName);
            SavePlayerData();
            //Debug.Log("Player: " + PlayerDataObject.Name + " Phone: " + PlayerDataObject.PhoneColor + " Day: " + PlayerDataObject.CurrentDay);
        }
    }

    public bool PlayerDataExists()
    {
        string path = Application.persistentDataPath + "/" + PlayerFileName;
        return File.Exists(path);
    }

    public void LoadDataOnly()
    {
        string path = Application.persistentDataPath + "/" + PlayerFileName;
        if (File.Exists(path))
        {
            Debug.Log("Player data save file exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerDataObject = formatter.Deserialize(stream) as Player;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("Player data save file was not found");
        }
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/" + PlayerFileName;
        if (File.Exists(path))
        {
            Debug.Log("Player data save file exists");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerDataObject = formatter.Deserialize(stream) as Player;
            stream.Close();

            Vector3 SpawningPoint = new Vector3();
            if (PlayerDataObject.Place == "Home")
                SpawningPoint = HomePosition;
            else if (PlayerDataObject.Place == "Work")
                SpawningPoint = WorkPosition;

            switch (PlayerDataObject.Name)
            {
                case "Alex":
                    Instantiate(AlexPrefab, SpawningPoint, Quaternion.identity);
                    break;
                case "Molly":
                    Instantiate(MollyPrefab, SpawningPoint, Quaternion.identity);
                    break;
                case "Rob":
                    Instantiate(RobPrefab, SpawningPoint, Quaternion.identity);
                    break;
            }

            CameraFollowing.SetTarget(PlayerDataObject.Name);
        }
        else
        {
            Debug.LogWarning("Player data save file was not found");
        }
    }

    public void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + PlayerFileName;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, PlayerDataObject);
        stream.Close();
    }

    public void DeletePlayerData()
    {
        string levelPath = Application.persistentDataPath + "/" + PlayerFileName;

        if (File.Exists(levelPath))
            File.Delete(levelPath);
    }
}
