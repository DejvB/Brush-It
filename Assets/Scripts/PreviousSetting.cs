using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousSetting : MonoBehaviour
{
    public static int NumberOfPlayers = 2;
    public static Sprite PlayField;
    public static int GameLength = 30;
    public static bool[] AI = new bool[8] { false, true, true, true, true, true, true, true };
    public static Color32[] PlayerColors = new Color32[8];
    public static int GameType;
    public static int DropRate;
    public static bool[] ActivePowerUp = new bool[10] { true, true, true, true, true, true, true, false, false, false };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
