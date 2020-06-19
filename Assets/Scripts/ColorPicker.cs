using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, IPointerEnterHandler
{
    public GameObject ColorButton;
    private GameObject CB;
    public int Count; // unique id of this object
    //private Color32[] BasicColors= new Color32[12] {new Color32(230,25,75, 255), new Color32(11, 102, 35, 255), new Color32(16, 52, 166, 255), new Color32(245, 130, 48, 255), new Color32(255,242,00, 255), new Color32(10,105,4, 255), new Color32(240,50,230, 255), new Color32(0,0,0, 255), new Color32(120,0,0, 255), new Color32(64,224,208, 255), new Color32(43,0,0, 255), new Color32(255,0,0, 255)};
    //private Color32[] ColorBlindColors = new Color32[12] { new Color32(230, 25, 75, 255), new Color32(60, 180, 75, 255), new Color32(255, 225, 25, 255), new Color32(0, 130, 200, 255), new Color32(245, 130, 48, 255), new Color32(70, 240, 240, 255), new Color32(240, 50, 230, 255), new Color32(250, 190, 190, 255), new Color32(0, 128, 128, 255), new Color32(128, 0, 0, 255), new Color32(170, 110, 40, 255), new Color32(255, 250, 200, 255)};
    //private Color32[] BasicColors = new Color32[12] { new Color32(230, 25, 75, 255), new Color32(60, 180, 75, 255), new Color32(255, 225, 25, 255), new Color32(0, 130, 200, 255), new Color32(245, 130, 48, 255), new Color32(145, 30, 180, 255), new Color32(70, 240, 240, 255), new Color32(240, 50, 230, 255), new Color32(210, 245, 60, 255), new Color32(250, 190, 190, 255), new Color32(0, 128, 128, 255), new Color32(230, 190, 255, 255)};
    //private Color32[] BlueColors = new Color32[12] { new Color32(0, 0, 255, 255), new Color32(10, 10, 255, 255), new Color32(20, 20, 255, 255), new Color32(30, 30, 255, 255), new Color32(40, 40, 255, 255), new Color32(50, 50, 255, 255), new Color32(60, 60, 255, 255), new Color32(70, 70, 255, 255), new Color32(80, 80, 255, 255), new Color32(90, 90, 255, 255), new Color32(100, 100, 255, 255), new Color32(110, 110, 255, 255) };
    private Color32[] MaterialColors = new Color32[] {/*Red*/ new Color32(239, 83, 80, 255),
                                                      /*Purple*/ new Color32(171, 71, 188, 255),
                                                      /*Blue*/ new Color32(41, 98, 255, 255),
                                                      /*Orange*/ new Color32(255, 167, 38, 255),
                                                      /*Black*/ new Color32(0, 0, 0, 255),
                                                      /*Light Blue*/ new Color32(66, 165, 245, 255),
                                                      /*Yellow*/ new Color32(250, 250, 10, 255),
                                                      /*Green*/ new Color32(90, 200, 90, 255),
                                                      /*Teal*/ new Color32(38, 166, 154, 255) };
    

    void Start()
    {
        int I = 1;
        int J = 1;
        TableCreation(I, J);

    }

    void TableCreation(int I, int J)
    {
        for (int j = J; j >= -J; j--)
            {
            for (int i = -I; i <= I; i++)
                {
                    CB = CreateColorButton(i, j);
                }
            }
    }

    //private int[] adam = new int[8] { 0, 1, 2, 5, 8, 7, 6, 3 };

    int count = 0;
    private GameObject CreateColorButton(int i, int j)
    {
        Vector3 position = new Vector3(100 * i + 50, 100 * j, 0);
        CB = Instantiate(ColorButton, transform.position + position, Quaternion.identity, gameObject.transform);
        //SRSLY?
        ColorBlock cb = CB.GetComponent<Toggle>().colors;
        
        cb.normalColor = MaterialColors[count];
        cb.highlightedColor = cb.normalColor;
        cb.pressedColor = cb.normalColor;
        //cb.selectedColor = cb.normalColor;

        CB.GetComponent<Toggle>().colors = cb;
        CB.GetComponent<Toggle>().group = gameObject.GetComponent<ToggleGroup>();
        CB.transform.name = "color" + count.ToString();
        if (!GameSetting.AI[Count])//(!GameSetting.AI[(int)CB.transform.parent.name[2] - 48 - 1])
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Toggle>().isOn = true; // Do I really want to rely on the order of children?
        } // I do! I do! I do!
        else
        {
            gameObject.transform.GetChild(0).GetChild(1).GetComponent<Toggle>().isOn = true;
        }
        if (count == GameSetting.adam[Count])
        {
            GameSetting.PlayerColors[Count] = CB.GetComponent<Toggle>().colors.normalColor;
            CB.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            CB.GetComponent<Toggle>().isOn = false;
        }

        count += 1;
        return CB;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ColorClicked(Toggle ColorButton) // RGBA(168, 27, 23, 255)
    {
        int Count = ColorButton.transform.parent.GetComponent<ColorPicker>().Count;
        GameSetting.PlayerColors[Count] = ColorButton.GetComponent<Toggle>().colors.normalColor;  //This is probably not the best way.
    

        //GameSetting.PlayerColors[(int)ColorButton.transform.parent.name[2] - 48 - 1] = ColorButton.GetComponent<Toggle>().colors.normalColor;  //This is probably not the best way.
        GameSetting.adam[Count] = (int)ColorButton.name[5] - 48;


        //GameSetting.adam[Count] = (int)ColorButton.name[5] - 48;
        //AI[(int)ColorButton.transform.parent.name[2] - 48 - 1] = false;
    }

    /*public void PersonCLicked()
    {
        Debug.Log("pclicked " + Count);
        GameSetting.AI[Count] = false;
    }

    public void PCCLicked()
    {
        Debug.Log("pccclicked " + Count);
        GameSetting.AI[Count] = true;
    }*/


    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.SetAsLastSibling();
    }

}
