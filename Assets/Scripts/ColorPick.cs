//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPick : MonoBehaviour
{
    public Image Handle;
    private int val;
    public Slider slider;
    public Toggle toggle;
    public static Color32[] PlayerColors = new Color32[8];
    public static bool[] AI = new bool[8];
    // Start is called before the first frame update
    void Start()
    {
        slider.value = Random.Range(1, 1536);
        //Handle.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

    }

    // Update is called once per frame
    void Update()
    {
        Handle.color = GetColor();
        PlayerColors[(int)gameObject.name[2] - 48 - 1] = Handle.color;  //This is probably not the best way.
        AI[(int)gameObject.name[2] - 48 - 1] = toggle.isOn;
    }

    private Color32 GetColor()
    {
        val = (int)slider.value;
        if (val <= 512) // red-ish
        {
            if (val <= 256)
                return new Color32(255, (byte)(256 - val), 0, 255);
            else
                return new Color32(255, 0, (byte)(val - 257), 255);
        }
        else if (val <= 1024) // blue-ish
        {
            if (val <= 768)
                return new Color32((byte)(768 - val), 0, 255, 255);
            else
                return new Color32(0, (byte)(val - 769), 255, 255);
        }
        else // (val <= 768) // green-ish
        {
            if (val <= 1280)
                return new Color32(0, 255, (byte)(1280 - val), 255);
            else
                return new Color32((byte)(val - 1281), 255, 0, 255);
        }

    }
}

