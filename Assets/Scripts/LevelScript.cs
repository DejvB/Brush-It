using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int completed_level;
    public Stats stats;
    GameSetting campaign;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("ha");
        LoadStats();
        ApplyStats();
        Debug.Log(GameSetting.Speed[0]);
        Debug.Log("ah");
        //campaign = new GameSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyStats()
    {
        GameSetting.PenWidth[0] = (1 + (float)stats.size / 20) * GameSetting.PenWidth[1];
        GameSetting.Speed[0] = (1 + (float)stats.speed / 20) * GameSetting.Speed[1];
    }


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
    }

}
