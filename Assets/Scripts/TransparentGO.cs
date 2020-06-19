using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TransparentGO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Transform[] children;

    // Start is called before the first frame update
    void Start()
    {
        /*int i = 0;
        foreach (Transform child in transform)
        {
            children[i] = child;
            i += 1;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Color c = this.GetComponent<Image>().color;
        c.a = 255;
        this.GetComponent<Image>().color = c;
        /*foreach (Transform child in children)
        {
            child.gameObject.SetActive(true);
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color c = this.GetComponent<Image>().color;
        c.a = 0;
        this.GetComponent<Image>().color = c;
        /*foreach (Transform child in children)
        {
            child.gameObject.SetActive(false);
        }*/
    }
}
