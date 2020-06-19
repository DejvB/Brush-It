using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGen : MonoBehaviour
{
    public GameObject Pickup;
    public static int[] array;
    public static int NumberOfActivePowerUps = 0;
    private static GameObject pp;
    // Start is called before the first frame update
    
    void Start()
    {
        NumberOfActivePowerUps = 0;
        pp = Pickup;
        foreach (bool item in GameSetting.ActivePowerUp) { if (item) { NumberOfActivePowerUps++; } }
        if (NumberOfActivePowerUps == 0)
            GameSetting.DropRate = 0;
        array = new int[NumberOfActivePowerUps];
        int ind = 0;
        for (int i = 0; i < GameSetting.ActivePowerUp.Length; i++)
        {
            if (GameSetting.ActivePowerUp[i])
            {
                array[ind] = i;
                ind++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GeneratePowerUp()
    {
        Vector3 position = new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 10f), 0);
        Instantiate(pp, position, Quaternion.identity);
    }
}
