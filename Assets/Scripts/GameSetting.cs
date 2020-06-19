using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static int NumberOfPlayers = 2;
    public static Sprite PlayField;
    public static int GameLength = 30;
    public static bool[] AI = new bool[8] { false, true, true, true, true, true, true, true};
    public static Color32[] PlayerColors = new Color32[32];
    public static int[] ColorPos = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 }; // just init value for players - why???
    public static int[] adam = new int[8] {0, 1, 2, 5, 8, 7, 6, 3};
    public static int GameType = 1;
    public static int NumberOfRounds = 5;
    public static int ThisRound = 1;
    public static int DropRate = 5;
    public static float[] PenWidth = new float[8] { 55, 55, 55, 55, 55, 55, 55, 55 };
    public static float[] Speed = new float[8] { 7, 7, 7, 7, 7, 7, 7, 7 };
    public static bool[] ActivePowerUp = new bool[10] { true, true, true, true, true, true, true, false, false, false };
    public static bool[] keyboard = new bool[8] {true, true, true, true, true, true, true, true};

    // Start is called before the first frame update
    void Start()
    {
        PlayField = Resources.Load<Sprite>("Sprites/canvas_crop");
        /*for (int i = 0; i < PlayerColors.Length; i++)
        {
            PlayerColors[i] = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int NumberOfActivePowerUps()
    {
        int number = 0;
        foreach (bool item in ActivePowerUp)
        {
            if (item)
            {
                number++;
            }
        }
        return number;
    }

    public static List<int> ListOfActivePowerUps()
    {
        int ind = 0;
        List<int> array = new List<int>();
        foreach (bool powerup in ActivePowerUp)
        {
            if (powerup)
            {
                array.Add(ind);
            }
            ind++;
        }
        return array;
    }
}
