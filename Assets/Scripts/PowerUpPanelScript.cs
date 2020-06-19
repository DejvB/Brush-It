using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPanelScript : MonoBehaviour
{
    private string[] PUList = new string[] { "speed", "plus", "rubber_2", "snowflake", "marker", "crown", "bomb" };
    private string folder = "Sprites/PowerUps/";
    // Start is called before the first frame update
    public GameObject PowerUp;
    private GameObject PU;
    void Start()
    {
        for (int i = -3; i <= 3; i++)
        {
            Vector3 position = new Vector3(100 * i, - 75, 0);
            PU = Instantiate(PowerUp, transform.position + position, Quaternion.identity, gameObject.transform);
            PU.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(folder + PUList[i + 3]);
            PU.name = (i + 3).ToString();
            PU.GetComponent<Toggle>().isOn = GameSetting.ActivePowerUp[i + 3];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(GameObject PowerUp)
    {
        GameSetting.ActivePowerUp[PowerUp.name[0] - 48] = PowerUp.GetComponent<Toggle>().isOn;
    }
}
