using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameObject PowerUp;
    private float lifetime;
    private Rigidbody2D player;
    private float effect_duration;
    public GameObject Player;
    public GameObject powerupholder;
    private int type;
    public float multiplier = 1.5f;
    public float scaler = 1f;
    GameObject[] Players;

    private string[] PUList = new string[] { "speed", "plus", "rubber_2", "snowflake", "marker", "crown", "bomb" };
    private string folder = "Sprites/PowerUps/";
    private int typeInd;
    void Start()
    {
        lifetime = 5f;
        typeInd = Random.Range(0, GameSetting.NumberOfActivePowerUps());
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(folder + PUList[GameSetting.ListOfActivePowerUps()[typeInd]]);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.attachedRigidbody;
        if (player.tag == "Player")
        {
            other.GetComponent<PlayerPowerUpHandler>().BeginEffect(typeInd); 
            Destroy(gameObject);

            /*
            switch (type)
            {
                case 0: //flash
                    triggered = true;
                    P.GetComponent<PlayerController>().speed = Player.GetComponent<PlayerController>().speed * multiplier;
                    effect_duration = 10f * scaler;
                    break;
                case 1: //hulk
                    P.GetComponent<PlayerController>().PenWidth = (int)(Player.GetComponent<PlayerController>().PenWidth * multiplier);
                    Vector3 var = Player.transform.localScale;
                    var.x *= multiplier;
                    var.y *= multiplier;
                    P.transform.localScale = var;
                    effect_duration = 10f * scaler;
                    break;
                case 2: //rubber
                    foreach (GameObject player in Players)
                    {
                        player.GetComponent<PlayerController>().Color.a = 0;
                    }
                    P.GetComponent<PlayerController>().Color.a = Player.GetComponent<PlayerController>().Color.a;
                    effect_duration = 3f * scaler;
                    break;
                case 3: //freeze
                    foreach (GameObject player in Players)
                    {
                        player.GetComponent<PlayerController>().speed = 0;
                    }
                    P.GetComponent<PlayerController>().speed = Player.GetComponent<PlayerController>().speed;
                    effect_duration = 3f * scaler;
                    break;
                case 4: //permanent marker
                    foreach (GameObject player in Players)
                    {
                        player.GetComponent<PlayerController>().erasable = true;
                    }
                    P.GetComponent<PlayerController>().erasable = false;
                    effect_duration = 10f * scaler;
                    break;
                case 5: // I am the king here!
                    foreach (GameObject player in Players)
                    {
                        player.GetComponent<PlayerController>().Color = P.GetComponent<PlayerController>().Color;
                        FreeDraw.Drawable.King = (int)P.name[1] - 48 - 1;
                        effect_duration = 1.5f * scaler;
                    }
                    break;
                case 6: // bomb
                    Vector2 pixel_pos = FreeDraw.Drawable.D.WorldToPixelCoordinates(transform.position);
                    FreeDraw.Drawable.D.MarkPixelsTocolor((int)P.name[1] - 48 - 1, pixel_pos, (int)(P.GetComponent<PlayerController>().PenWidth * 4), P.GetComponent<PlayerController>().Color);
                    FreeDraw.Drawable.D.ApplyMarkedPixelChanges();
                    effect_duration = 1.5f;
                    break;
            }

            GameObject newObject = Instantiate(powerupholder) as GameObject;
            newObject.GetComponent<PowerUpHolderScript>().type = type;
            newObject.GetComponent<PowerUpHolderScript>().effect_duration = effect_duration;
            newObject.GetComponent<PowerUpHolderScript>().P = P;
            newObject.GetComponent<PowerUpHolderScript>().Players = Players;
            Destroy(gameObject);*/
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
