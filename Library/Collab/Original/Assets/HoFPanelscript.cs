using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using TMPro;

public class HoFPanelscript : MonoBehaviour
{
    void Start()
    {
        LoadData(gameObject.name[1].ToString(), gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void LoadData(string name, GameObject parent)
    {
        string path = Application.persistentDataPath + "/" + name + ".sex";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            float[] data = formatter.Deserialize(stream) as float[];
            stream.Close();
            ParseAndShow(data, parent);
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
        }
    }

    public void ParseAndShow(float[] data, GameObject parent)
    {
        Array.Sort(data);
        for (int i = 0; i < Mathf.Min(data.Length, 8); i++)
        {
            WriteLine(data[data.Length - i - 1], i, parent);
        }
    }

    public TextMeshProUGUI LinePrefab;

    public void WriteLine(float data, int i, GameObject parent)
    {
        Vector3 position = new Vector3(960, 200 - 80 * i + 590, 0);
        TextMeshProUGUI line = Instantiate(LinePrefab, position, Quaternion.identity, parent.transform);
        line.text = (i + 1).ToString() + ".          " + (System.Math.Round(100.0 * data, 2)).ToString() + "%";
    }
}
