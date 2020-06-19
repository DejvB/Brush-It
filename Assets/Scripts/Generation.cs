//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generation : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bar;
    //public Rigidbody2D previous_Bar;
    static public GameObject[] Players;
    static public GameObject[] Bars;
    static public PlayerController[] Controllers;
    private int NumberOfPlayers = GameSetting.NumberOfPlayers;
    private bool[] AI = GameSetting.AI;
    //private int WoS = Screen.width;
    // Start is called before the first frame update

    public Sprite DefaultPlayfield;
    private Sprite PlayField = GameSetting.PlayField;
    private List <Vector3> StartPositions = new List<Vector3>();
    void Start()
    {
        float w = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect.width;
        
        if (PlayField == null)
        {
            PlayField = DefaultPlayfield;
        }
        GenStartPos();

        gameObject.GetComponent<SpriteRenderer>().sprite = PlayField;
        Players = new GameObject[NumberOfPlayers];
        Bars = new GameObject[NumberOfPlayers];
        Controllers = new PlayerController[NumberOfPlayers];
        for (int i = 1; i <= NumberOfPlayers; i++)
        {
            GameObject P = CreatePlayer(i);
            Players[i - 1] = P;
            P.GetComponent<PlayerController>().PenWidth = GameSetting.PenWidth[i - 1];
            Controllers[i - 1] = P.GetComponent<PlayerController>();
            P.name = "P" + i.ToString();
            Controllers[i - 1].AI = AI[i - 1];
            //Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 200);
            //GameObject.Find("Winner").GetComponent<Image>().color = colors[j];
            Color32 color = GameSetting.PlayerColors[i - 1];
            color.a = 200;
            P.GetComponent<PlayerController>().Color = color;
            P.GetComponent<PlayerController>().speed = GameSetting.Speed[i - 1];
            P.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color; //for ufo
            P.GetComponent<SpriteRenderer>().color = color;
            FreeDraw.Drawable.colors[i - 1] = color;
            CreateBar(P, i);
        }
    }

    private void GenStartPos()
    {
        while (StartPositions.Count < NumberOfPlayers)
        {
            bool SoFarSoGood = true;
            if (StartPositions.Count == 0)
            {
                StartPositions.Add(new Vector3(Random.Range(-22f, 22f), Random.Range(-10f, 10f), 0));
            }
            else
            {
                Vector3 temp = new Vector3(Random.Range(-22f, 22f), Random.Range(-10f, 10f), 0);
                foreach (Vector3 item in StartPositions)
                {
                    if (Vector3.Distance(temp, item) < 2 * Player.GetComponent<CircleCollider2D>().radius)
                    {
                        SoFarSoGood = false;
                    }
                }
                if (SoFarSoGood)
                {
                    StartPositions.Add(temp);
                }
            }
        }
    }

    private GameObject CreatePlayer(int i)
    {
        Vector3 position = StartPositions[i - 1];
        GameObject P = Instantiate(Player, position, Quaternion.identity); //Instantiate(Pickup, position, Quaternion.identity);
        return P;
    }

    /*private void CreateBar(GameObject P, int i)
    {
        GameObject B = Instantiate(Bar, new Vector3(-15 + 15/ NumberOfPlayers * (2*i-1), 14, 0), Quaternion.identity);
        B.name = "Bar" + i.ToString();
        B.GetComponent<SpriteRenderer>().color = P.GetComponent<PlayerController>().Color;
        B.GetComponent<Bar>().ind = i - 1;
        Bars[i - 1] = B;

    }*/
    private void CreateBar(GameObject P, int i)
    {
        Vector3 barPosition = new Vector3(0, 0, 0);//new Vector3(-15 + 15 / NumberOfPlayers * (2 * i - 1), 1000, 0);

        GameObject B = Instantiate(Bar, barPosition, Quaternion.identity, GameObject.FindObjectOfType<Canvas>().transform);
        B.name = "Bar" + i.ToString();
        B.GetComponentInChildren<Image>().color = P.GetComponent<PlayerController>().Color;
        B.GetComponentInChildren<Bar>().ind = i - 1;
        Bars[i - 1] = B;

    }
    
    // Update is called once per frame
    void Update()
    {

    }


}