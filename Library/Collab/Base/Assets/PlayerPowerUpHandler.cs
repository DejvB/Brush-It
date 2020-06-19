using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpHandler : MonoBehaviour
{
    private bool[] activePowerUp; // default is false.
    private float[] effectDurations;

    public float multiplier = 1.5f;
    public static float scaler = 1f;
    public Rigidbody2D Player;
    public GameObject[] Players;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        activePowerUp = new bool[GameSetting.NumberOfActivePowerUps()];
        effectDurations = new float[GameSetting.NumberOfActivePowerUps()];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activePowerUp.Length; i++) //foreach maybe
        {
            if (activePowerUp[i])
            {
                effectDurations[i] -= Time.deltaTime;
            }
            if (effectDurations[i] < 0)
            {
                DeactivatePowerUp(i);
                StopEffect(i);
                effectDurations[i] = 0;
            }
        }

    }

    public void ActivatePowerUp(int i)
    {
        activePowerUp[i] = true;
    }

    public void DeactivatePowerUp(int i)
    {
        activePowerUp[i] = false;
    }

    public void AddEffectDuration(int i, float time)
    {
        effectDurations[i] = time * scaler;
    }
    public void StopEffect(int i)
    {
        DeactivatePowerUp(i);
        int type = GameSetting.ListOfActivePowerUps()[i];
        switch (type)
        {
            case 0: //speed
                GetComponent<PlayerController>().speed = Player.GetComponent<PlayerController>().speed;
                break;
            case 1: //size
                GetComponent<PlayerController>().PenWidth = Player.GetComponent<PlayerController>().PenWidth;
                transform.localScale = Player.transform.localScale;
                break;
            case 2: //rubber
                GetComponent<PlayerController>().Color.a = Player.GetComponent<PlayerController>().Color.a;
                break;
            case 3:
                GetComponent<PlayerController>().speed = Player.GetComponent<PlayerController>().speed;
                break;
            case 4:
                GetComponent<PlayerController>().erasable = true;
                break;
            case 5:
                GetComponent<PlayerController>().Color = GameSetting.PlayerColors[(int)name[1] - 48 - 1];
                FreeDraw.Drawable.King = -1;
                break;
            case 6:
                break;
        }
    }

    public void BeginEffect(int i)
    {
        int type = GameSetting.ListOfActivePowerUps()[i]; //Expensive, better generate before
        switch (type)
        {
            case 0: //flash
                ActivatePowerUp(i);
                GetComponent<PlayerController>().speed = Player.GetComponent<PlayerController>().speed * multiplier;
                AddEffectDuration(i, 10);
                break;
            case 1: //hulk
                ActivatePowerUp(i);
                GetComponent<PlayerController>().PenWidth = (int)(Player.GetComponent<PlayerController>().PenWidth * multiplier);
                Vector3 var = Player.transform.localScale;
                var.x *= multiplier;
                var.y *= multiplier;
                transform.localScale = var;
                AddEffectDuration(i, 10);
                break;
            case 2: //rubber
                foreach (GameObject player in Players)
                {
                    if (gameObject != player)
                    {
                        player.GetComponent<PlayerPowerUpHandler>().ActivatePowerUp(i);
                        player.GetComponent<PlayerController>().Color.a = 0;
                        player.GetComponent<PlayerPowerUpHandler>().AddEffectDuration(i, 3);
                    }
                }
                break;
            case 3: //freeze
                foreach (GameObject player in Players)
                {
                    if (gameObject != player)
                    {
                        player.GetComponent<PlayerPowerUpHandler>().ActivatePowerUp(i);
                        player.GetComponent<PlayerController>().speed = 0;
                        player.GetComponent<PlayerPowerUpHandler>().AddEffectDuration(i, 3);
                    }
                }
                break;
            case 4: //permanent marker
                ActivatePowerUp(i);
                GetComponent<PlayerController>().erasable = false;
                AddEffectDuration(i, 10);
                break;
            case 5: // I am the king here!
                foreach (GameObject player in Players)
                {
                    player.GetComponent<PlayerPowerUpHandler>().ActivatePowerUp(i);
                    player.GetComponent<PlayerController>().Color = GameSetting.PlayerColors[(int)name[1] - 48 - 1];
                    player.GetComponent<PlayerPowerUpHandler>().AddEffectDuration(i, 1.5f);
                }
                FreeDraw.Drawable.King = (int)name[1] - 48 - 1;
                break;
            case 6: // bomb
                Vector2 pixel_pos = FreeDraw.Drawable.D.WorldToPixelCoordinates(transform.position);
                FreeDraw.Drawable.D.MarkPixelsTocolor((int)name[1] - 48 - 1, pixel_pos, (int)(GetComponent<PlayerController>().PenWidth * 4), GetComponent<PlayerController>().Color);
                FreeDraw.Drawable.D.ApplyMarkedPixelChanges();
                break;
        }
    }


}
