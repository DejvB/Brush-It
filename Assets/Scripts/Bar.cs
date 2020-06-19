using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    //private int number_of_players = SliderScript.number_of_players;
    public int sum;
    //private int[] percentage = new int[SliderScript.number_of_players];
    public int ind;
    //private GameObject g;
    //private FreeDraw.Drawable CanvasScript;
    //public Component ha;
    void Start()
    {
        /*ind = (int)this.name[this.name.Length - 1] - 48 - 1;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        gameObject.GetComponent<SpriteRenderer>().color = GameObject.Find("P" + this.name[this.name.Length - 1]).GetComponent<PlayerController>().Color;
        GameObject g = GameObject.Find("transparent_png");
        FreeDraw.Drawable CanvasScript = g.GetComponent<FreeDraw.Drawable>();*/
    }
    void Update()
    {
        /*GetComponent<SpriteRenderer>().transform.Translate(100*Time.deltaTime, 0, 0);
        int a = CanvasScript.percentage[ind];
        int b = CanvasScript.sum;
        GetComponent<SpriteRenderer>().transform.localScale = new Vector3((float)CanvasScript.percentage[ind] / CanvasScript.sum, 1, 1);
        GameObject.Find("Tile1").transform.localScale = new Vector3((float)percentage[0] / sum * 140, 200, 0);*/
    }
}
