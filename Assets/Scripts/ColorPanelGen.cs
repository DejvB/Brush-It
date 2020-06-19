using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPanelGen : MonoBehaviour
{
    public GameObject ColorPanel;
    private GameObject CP;
    // Start is called before the first frame update
    void Start()
    {
        int I = 4;
        int J = 1;
        TableCreation(I, J);

    }

    void TableCreation(int I, int J)
    {
        for (int i = 0; i < I; i++)
        {
            for (int j = J; j >= 0; j--)
            {
                CP = CreateColorPanel(i, j);
            }
        }
    }

    private GameObject CreateColorPanel(int i, int j)
    {
        Vector3 position = new Vector3(416 * i + 200 - 824, 316 * j + 135 - 316, 0);
        CP = Instantiate(ColorPanel, transform.position + position, Quaternion.identity, gameObject.transform);
        CP.GetComponent<ColorPicker>().Count = (1 - j) * 4 + i; //starts with 0
        CP.name = "CP" + ((1 - j) * 4 + i + 1).ToString();

        return CP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
