using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public static AsyncOperation async;

    void Start()
    {
        //Time.timeScale = 1; // hidious solution
    }
    private Event e;

    /*void OnGUI()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
            }
        }
    }*/
    void Update()
    {

    }

    public void LoadScene(int Scene)
    {
        /*AsyncOperation async =  */SceneManager.LoadSceneAsync(Scene);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Freeze()
    {
        Time.timeScale = 0.0000001f;
    }

    public void UnFreeze()
    {
        Time.timeScale = 1.0f;
    }

    // Two ways, save unsorted and sort after load, or save sorted...

    public static void SaveData(int NoP, float winper) //Why static?
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + NoP.ToString() + ".sex";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        if (stream.Length == 0)
        {
            float[] table = new float[1];
            table[0] = winper;
            formatter.Serialize(stream, table);
        }
        else
        {
            float[] table = formatter.Deserialize(stream) as float[];
            int len = table.Length;
            float[] temp = new float[len + 1];
            table.CopyTo(temp, 0);

            stream.Close();
            stream = new FileStream(path, FileMode.Open, FileAccess.Write);
            temp[len] = winper;
            formatter.Serialize(stream, temp);
        }
        
        stream.Close();
    }

    /*public void SaveStats() //Why static?
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + "PlayerData" + ".sex";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, stats);
        stream.Close();
    }*/

    public void LoadStats()
    {
        string path = Application.persistentDataPath + "/" + "PlayerData" + ".sex";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            Stats stats = formatter.Deserialize(stream) as Stats;
            stream.Close();
            Debug.Log(stats.size);
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
        }
    }

    public void ShowRightHoFPanel()
    {
        gameObject.transform.SetAsLastSibling();
        /*
        foreach(GameObject panel in HallOfFameScript.HoFPanels)
        {
            panel.SetActive(false);
        }
        HallOfFameScript.HoFPanels[(int)gameObject.name[0] - 48].SetActive(true);*/
    }

    public void Delete()
    {
        for (int i = 1 ; i <= 8; i++)
        {
            string path = Application.persistentDataPath + "/" + i.ToString() +".sex";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    public static GameSetting PreviousGame;

    public void SaveSetting()
    {
        //PreviousGame = new GameSetting(); SOme sort of error!
    }
}
