using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayfieldScript : MonoBehaviour
{
    //public static Sprite PlayField; // = GameObject.Find("Toggle").GetComponentInChildren<Image>().sprite;
    // Start is called before the first frame update
    void Start()
    {
        //GameSetting.PlayField;
    }

    public void ColorClicked(GameObject ThisButton)
    {
        GameSetting.PlayField = ThisButton.GetComponent<Image>().sprite;  //This is probably not the best way.
        //AI[(int)ColorButton.transform.parent.name[2] - 48 - 1] = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
