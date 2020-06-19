using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ButtonScript : MonoBehaviour
{
    public static AsyncOperation async;

    void Start()
    {
        //Time.timeScale = 1; // hidious solution
    }

    void Update()
    {  
    }

    public void LoadScene(int Scene)
    {
        AsyncOperation async =  SceneManager.LoadSceneAsync(Scene);
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
        Debug.Log(path);
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

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/" + gameObject.transform.name.ToString() + ".sex";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            float[] data = formatter.Deserialize(stream) as float[];
            stream.Close();
            ParseAndShow(data);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    public void ParseAndShow(float[] data)
    {
        Array.Sort(data);
        for (int i = 0; i < data.Length; i++)
        {
            WriteLine(data[data.Length - i - 1], i);
            Debug.Log(data[i].ToString());
        }
    }
    public void WriteLine(float data, int i)
    {
        
    }
}
