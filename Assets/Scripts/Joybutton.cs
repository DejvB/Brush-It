using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joybutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    protected bool Pressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = false;
    }
}
