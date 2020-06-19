using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    
    public Slider slider;  // It should be possible without this.
    public TextMeshProUGUI NoPText;
    public TextMeshProUGUI GLText;
    public TextMeshProUGUI DRText;
    public TextMeshProUGUI NoRText;
    private GameObject[] GOs;
    void Start()
    {

        GOs = GameObject.FindGameObjectsWithTag("CP");
        //number_of_players = 0;
        switch (gameObject.name)
        {
            case "NoPSlider":
                slider.value = GameSetting.NumberOfPlayers;
                NoPText.text = slider.value.ToString();
                foreach (GameObject GO in GOs)
                    GO.SetActive((int)GO.name[2] - 48 <= GameSetting.NumberOfPlayers ? true : false);
                break;
            case "GLSlider":
                slider.value = GameSetting.GameLength / 10;
                GLText.text = GameSetting.GameLength.ToString();
                break;
            case "DRSlider":
                slider.value = GameSetting.DropRate / 5;
                DRText.text = GameSetting.DropRate.ToString() + "‰";
                break;
            case "RoundSlider":
                slider.value = (GameSetting.NumberOfRounds - 1) / 2 ;
                DRText.text = GameSetting.NumberOfRounds.ToString();
                break;
            default:
                Debug.Log("Something bad will happen");
                break;
        }
    }
    

    void Update()
    {
        switch (gameObject.name)
        {
            case "NoPSlider":
                if (GameSetting.NumberOfPlayers != (int)slider.value) //I should place this everywhere, I guess
                {
                    GameSetting.NumberOfPlayers = (int)slider.value;
                    NoPText.text = slider.value.ToString();
                    foreach (GameObject GO in GOs)
                        GO.SetActive((int)GO.name[2] - 48 <= GameSetting.NumberOfPlayers ? true : false);
                }
                break;
            case "GLSlider":
                GameSetting.GameLength = 10 * (int)slider.value;
                GLText.text = GameSetting.GameLength.ToString();
                break;
            case "DRSlider":
                GameSetting.DropRate = (int)slider.value * 5;
                DRText.text = GameSetting.DropRate.ToString() + "‰";
                break;
            case "RoundSlider":
                GameSetting.NumberOfRounds = (int)slider.value * 2 + 1;
                DRText.text = GameSetting.NumberOfRounds.ToString();
                break;
            default:
                Debug.Log("Something bad will happen");
                break;
        }


    }
}
