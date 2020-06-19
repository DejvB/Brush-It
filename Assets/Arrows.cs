using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public float value;
    public TextMeshProUGUI text;
    GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("PlayerMenu");

    }

    // Update is called once per frame
    void Update()
    {
        if (value.ToString() != text.text)
        {
            Text();
        }
    }

    public void Increase()
    {
        if (menu.GetComponent<StatsHandler>().stats.points > 0)
        {
            menu.GetComponent<StatsHandler>().stats.points -= 1;
            value += 1;
            Text();
        }
    }

    public void Decrease()
    {
        if (value > 0)
        {
            menu.GetComponent<StatsHandler>().stats.points += 1;
            value -= 1;
            Text();
        }
    }

    public void Text()
    {
        text.text = value.ToString();
    }
}
