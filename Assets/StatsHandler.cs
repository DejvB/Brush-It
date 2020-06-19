using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public GameObject duration;
    public GameObject speed;
    public GameObject size;
    public GameObject mass;
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI points;
    public Stats stats;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.points.ToString() != points.text)
        {
            points.text = stats.points.ToString();
        }
    }
    // C:\Users\david\AppData\LocalLow\Dejv\Brush_It
    public void LoadStats()
    {
        string path = Application.persistentDataPath + "/" + "PlayerData" + ".sex";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            stats = formatter.Deserialize(stream) as Stats;
            stream.Close();
            //print(stats);
        }
        else
        {
            stats = new Stats
            {
                speed = 00,// GameSetting.Speed,
                size = 00,// GameSetting.PenWidth,
                mass = 00,// 1,
                name = "Dejv",
                points = 5,
                duration = 0
            };
            //Debug.LogError("Save file not found in " + path);
        }
        Init();
    }

    public void Init()
    {
        duration.GetComponent<Arrows>().value = stats.duration;
        speed.GetComponent<Arrows>().value = stats.speed;
        size.GetComponent<Arrows>().value = stats.size;
        mass.GetComponent<Arrows>().value = stats.mass;
        nickName.text = stats.name;
    }

    public void SaveStats() //Why static?
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + "PlayerData" + ".sex";
        
        //print(stats);
        FileStream stream = new FileStream(path, FileMode.Create);
        stats = new Stats
        {
            speed = speed.GetComponent<Arrows>().value,
            size = size.GetComponent<Arrows>().value,
            mass = mass.GetComponent<Arrows>().value,
            name = nickName.text,
            points = stats.points,
            duration = duration.GetComponent<Arrows>().value
        };
        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public void ResetStats() //Why static?
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + "PlayerData" + ".sex";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        Stats stats = new Stats
        {
            speed = 0,// GameSetting.Speed,
            size = 0,// GameSetting.PenWidth,
            mass = 0,// 1,
            name = "init",
            points = 5,
            duration = 0
        };
        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public void print(Stats stats)
    {
        Debug.Log("Name is:  " + stats.name);
        Debug.Log("Duration is:  " + stats.duration);
        Debug.Log("Speed is:  " + stats.speed);
        Debug.Log("Size is:  " + stats.size);
        Debug.Log("Mass is:  " + stats.mass);
        Debug.Log("Points is:  " + stats.points);
    }
}
