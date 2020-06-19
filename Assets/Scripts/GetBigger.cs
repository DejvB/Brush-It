using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetBigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, ISelectHandler, IDeselectHandler //WTF this does?
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    public void OnPointerDown(PointerEventData data) // doubleclick just for black color in the middle :)
    {
        if (this.transform.name == "color4")
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;

            if (clicked > 1 && Time.time - clicktime < clickdelay)
            {
                clicked = 0;
                clicktime = 0;

                //SRSLY?
                ColorBlock cb = this.GetComponent<Toggle>().colors;
                cb.normalColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                cb.highlightedColor = cb.normalColor;
                this.GetComponent<Toggle>().colors = cb;

            }
            else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(2f, 2f, 0);
        gameObject.transform.SetAsLastSibling();
        //gameObject.
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 0);

        //gameObject.layer = 5;
    }

    public void OnSelect(BaseEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(2f, 2f, 0);
        gameObject.transform.SetAsLastSibling();  // to be on top of other objects
        gameObject.transform.parent.transform.SetAsLastSibling();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 0);
    }


}
