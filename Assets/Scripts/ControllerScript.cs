using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Count = GrandParent.GetComponent<ColorPicker>().Count;
        AI = GameSetting.AI[Count] ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool controller = false;
    int Count;

    int state = 1;
    public void SetController()
    {
        switch (state)
        {
            case 0:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "keyboard");
                GameSetting.keyboard[(int)transform.parent.transform.parent.name[2] - 48 - 1] = true;
                state++;
                break;
            case 1:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "phone");
                GameSetting.keyboard[(int)transform.parent.transform.parent.name[2] - 48 - 1] = false;
                state++;
                break;
            case 2:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "gamepad");
                GameSetting.keyboard[(int)transform.parent.transform.parent.name[2] - 48 - 1] = true;
                state = 0;
                break;
        }
    }
    int AI;
    public GameObject GrandParent;
    public void SetPlayer()
    {
        switch (AI)
        {
            case 1:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "person");
                GameSetting.AI[Count] = false;
                AI--;
                break;
            case 0:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "pc");
                GameSetting.AI[Count] = true;
                AI++;
                break;
            /*case 2:
                GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + "gamepad");
                GameSetting.keyboard[(int)transform.parent.transform.parent.name[2] - 48 - 1] = true;
                state = 0;
                break;*/
        }
    }
}
