using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultTableScript : MonoBehaviour
{
    public TextMeshProUGUI Line;
    private TextMeshProUGUI L;
    public TextMeshProUGUI RoundCounter;
    public static int[] TotalPoints = new int[8]; //GameSetting.NumberOfPlayers
    private static float[] TotalPercent = new float[8];
    // Start is called before the first frame update
    GameObject[] Bars;

    Vector3 pos;

    void Start()
    {
        Bars = FreeDraw.Drawable.Bars;
        GetTotalPoints();
        for (int i = 0; i < GameSetting.NumberOfPlayers; i++)
        {
            int j = Bars[i].GetComponentInChildren<Bar>().ind;
            if (GameSetting.NumberOfPlayers <= 4)
            {
                pos = new Vector3(960, 760 - 80 * i, 0);
            }
            else
            {
                if ((i % 2) == 0)
                {
                    pos = new Vector3(810, 760 - 40 * i, 0);
                }
                else
                {
                    pos = new Vector3(1110, 760 - 40 * i, 0);
                }
            }
            L = Instantiate(Line, pos, Quaternion.identity, transform);
            L.GetComponentInChildren<Image>().color = GameSetting.PlayerColors[j];// Bars[i].GetComponentInChildren<Image>().color;
            L.text = TotalPoints[j] + " pts" + "   " + System.Math.Round(100.0 * TotalPercent[j]) + "%";
        }
        if (GameSetting.ThisRound == GameSetting.NumberOfRounds)
        {

            RoundCounter.text = "End of game";
            GameObject.Find("PlayButton").SetActive(false); //Do not use names.
        }
        else
        {

            RoundCounter.text = "End of round " + GameSetting.ThisRound;
        }
        GameSetting.ThisRound++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetTotalPoints()
    {
        for (int i = 0; i < GameSetting.NumberOfPlayers; i++)
        {
            TotalPoints[Bars[i].GetComponentInChildren<Bar>().ind] += GameSetting.NumberOfPlayers - i;
            TotalPercent[i] += FreeDraw.Drawable.percentageStats[i];
            //Debug.Log(Bars[i].GetComponentInChildren<Bar>().ind);
        }
    }
    void GetOrder()
    {

    }
}
