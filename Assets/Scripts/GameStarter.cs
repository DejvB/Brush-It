using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStarter : MonoBehaviour
{
    private float time = 4;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        time = 4;
        started = false;
        /*while (!ButtonScript.async.isDone) // Ups! Error at the beggining, but it works. :)
        {
            //yield return null;
        }*/
    }

    // Update is called once per frame
    private bool started = false;
    void Update()
    {
        time -= Time.unscaledDeltaTime;
        text.text = ((int)time).ToString();
        if (time < 1 && !started)
        {
            started = true;
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }
}
