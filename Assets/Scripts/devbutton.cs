using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class devbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public TextMeshProUGUI input;
    public TextMeshProUGUI button;
    void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 2 button " + i))
            {
                button.text = "joystick 2 button " + i.ToString();
                //print("joystick 1 button " + i);
            }
        }
        bool any = false;
        for (int i = 2; i < 12; i++)
        {
            if (Input.GetAxis(i.ToString()) > 0.5)
            {
                any = true;
                input.text = i.ToString();
            }

        }
        if (!any)
        {
            input.text = "nothing";
        }
        /*if (Input.GetAxis(2.ToString()) > 0.5)
        {
            input.text = "nope";
        }*/
    }
}
