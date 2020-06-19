using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HallOfFameScript : MonoBehaviour
{
    public GameObject HoFNoPButton;
    public TextMeshProUGUI HoFNoPButtonText;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 2; i <= 8; i++)
        {
            Vector3 position = new Vector3(-750 - 500 + 250 * i, 375, 0);
            GameObject HoFB = Instantiate(HoFNoPButton, transform.position + position, Quaternion.identity, gameObject.transform);
            HoFB.transform.name = i.ToString();
            HoFNoPButtonText.text = i.ToString() + " players";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
