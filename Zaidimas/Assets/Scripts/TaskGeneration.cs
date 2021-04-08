using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskGeneration : MonoBehaviour
{
    public class GameData
    {
        public class Day
        {
            public int number;
            public List<HomeTasks> DaysHomeTasks = new List<HomeTasks>(0);
            public List<BigTasks> DaysBigTasks = new List<BigTasks>(0);

            public Day() { }

            public Day(int num, List<HomeTasks> tasks)
            {
                FormDay(num);

                DaysHomeTasks.AddRange(tasks);
            }

            public Day(int num, HomeTasks task)
            {
                FormDay(num);

                DaysHomeTasks.Add(task);
            }

            public void FormDay(int num)
            {
                number = num;

                if (number % 3 == 0 || number % 2 == 0)
                {
                    // Days: 2, 3, 4, 6, 8, 9, 10, 12, 14
                    DaysBigTasks.Add(BigTasks.Working);
                }
                else
                {
                    // Days: 1, 5, 7, 11, 13
                    DaysBigTasks.Add(BigTasks.Shopping);
                }
            }
        }

        List<Day> DayList = new List<Day>(0);

        public GameData()
        {
            List<HomeTasks> HomeTaskList = new List<HomeTasks>(0);
            HomeTasks lastUsedTask = HomeTasks.ToiletPapper;
            var random = new System.Random();
            int index = 0;
            Day d = new Day();
            List<HomeTasks> l = new List<HomeTasks>(0);

            for (int i = 0; i < 14; i++)
            {
                //Debug.Log(i);
                if (HomeTaskList.Count == 0)
                {
                    HomeTaskList = FormHomeTaskList();
                    Debug.LogWarning("refiled");
                }

                if (i < 6)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        index = random.Next(HomeTaskList.Count);
                        if (lastUsedTask != HomeTaskList[index])
                            break;
                    }
                    lastUsedTask = HomeTaskList[index];
                    d = new Day(i + 1, HomeTaskList[index]);
                    HomeTaskList.RemoveAt(index);
                }
                else
                {
                    for (int j = 0; j < 10; j++)
                    {
                        index = random.Next(HomeTaskList.Count);
                        if (lastUsedTask != HomeTaskList[index])
                            break;
                    }
                    Debug.Log("index " + index + " " + HomeTaskList[index]);
                    l.Add(HomeTaskList[index]);
                    lastUsedTask = HomeTaskList[index];
                    HomeTaskList.RemoveAt(index);
                    for (int k = 0; k < 10; k++)
                    {
                        index = random.Next(HomeTaskList.Count);
                        if (lastUsedTask != HomeTaskList[index])
                            break;
                    }
                    Debug.Log("index " + index + " " + HomeTaskList[index]);
                    l.Add(HomeTaskList[index]);
                    lastUsedTask = HomeTaskList[index];
                    HomeTaskList.RemoveAt(index);

                    d = new Day(i + 1, l);
                    l.Clear();
                }
                
                DayList.Add(d);

                Debug.Log(i + "|" + d.number + "-" + d.DaysBigTasks.First().ToString());
                Debug.Log("Other tasks " + d.DaysHomeTasks.First().ToString());
                if (d.number > 6)
                    Debug.Log("        " + d.DaysHomeTasks.ElementAt(1).ToString());
            }
        }

        public List<HomeTasks> FormHomeTaskList()
        {
            List<HomeTasks> RandomisedHomeTaskList = new List<HomeTasks>();
            RandomisedHomeTaskList.Add(HomeTasks.Dishes);
            RandomisedHomeTaskList.Add(HomeTasks.Ducks);
            RandomisedHomeTaskList.Add(HomeTasks.ToiletPapper);
            RandomisedHomeTaskList.Add(HomeTasks.Trashes);
            return RandomisedHomeTaskList;
        }
    }

    public enum HomeTasks
    {
        Dishes,
        Ducks,
        Trashes,
        ToiletPapper
    }

    public enum BigTasks
    {
        Working,
        Shopping
    }

    public GameData GameDataObject;

    public void SetMainData()
    {
        GameDataObject = new GameData();
    }

    void Start()
    {
        // patikrinam ar reik kurti ar atkurti
        SetMainData();
    }


}
