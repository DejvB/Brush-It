using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HallOfFameScript : MonoBehaviour
{
    public GameObject HoFNoPButton;
    public GameObject HoFPanel;
    public static GameObject[] HoFPanels = new GameObject[9];
    private TextMeshProUGUI HoFNoPButtonText;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 2; i <= 8; i++)
        {
            Vector3 position = new Vector3(-750 - 500 + 250 * i, 375, 0);
            GameObject HoFB = Instantiate(HoFNoPButton, transform.position + position, Quaternion.identity, gameObject.transform);
            HoFB.transform.name = i.ToString();
            HoFB.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString() + " players";

            position = new Vector3(0, 100, 0);
            HoFPanel = Instantiate(HoFPanel, transform.position - position, Quaternion.identity, HoFB.transform);
            HoFPanel.transform.name = "P" + i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
