using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class  PlayerController : MonoBehaviour
{
    
    public float speed;
    public float rotational_speed;
    public int PenWidth = 42;
    public Color Color = Color.clear;
    public bool AI;
    public bool erasable = true;
    private Vector2 movement;
    private float time_to_move;
    //public float speed2;
    Vector2 refVelocity;

    protected Joystick joystick;

    private Rigidbody2D rb2d;
    

    void Awake()
    {
        //Random.InitState((int)gameObject.name[1]);
        rb2d = GetComponent<Rigidbody2D>(); //What is this? Apparently, it return rigidbody of this object
    }

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector2 movement1 = new Vector2(Random.Range(-0.1f, 100f), Random.Range(-0.1f, 100f));
        //Debug.Log("change");
    }

    void FixedUpdate()
    {
        time_to_move += Time.deltaTime;
        if (AI)
        {
            GameObject Target = null;
            GameObject[] PAs = GameObject.FindGameObjectsWithTag("PickUp"); //Any pickup, closest would be nice, though
            Vector2 direction = GetCloseColorPosition();
            float dist = 120f; // Infinity
            foreach (GameObject PA in PAs)
            {
                float temp = Vector3.Distance(PA.transform.position, gameObject.transform.position);
                if (temp < dist)
                {
                    dist = temp;
                    Target = PA;
                }
            }
            if (Target != null)
            {
                movement = new Vector2(Target.transform.position.x - rb2d.position.x, Target.transform.position.y - rb2d.position.y).normalized;
            }
            else if (!direction.Equals(Vector2.zero))
            {
                Vector2 pos = FreeDraw.Drawable.D.WorldToPixelCoordinates(rb2d.position);
                movement = new Vector2(direction.x - pos.x, direction.y - pos.y).normalized;
            }
            else if (time_to_move > 1)
            {
                time_to_move = 0;
                movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
        }
        else
        {
            if (GameSetting.keyboard[(int)rb2d.name[1] - 48 - 1])
            {
                string Horizontal = "H" + rb2d.name;
                string Vertical = "V" + rb2d.name;
                float moveHorizontal = Input.GetAxis(Horizontal);
                float moveVertical = Input.GetAxis(Vertical);
                movement = new Vector2(moveHorizontal, moveVertical);
            }
            else
            {
                movement = joystick.Direction;
            }
        }
        if (Vector2.zero != movement)
        {
            Vector2 current = rb2d.velocity;
            if (Vector2.zero != current)
            {
                //rb2d.velocity = Vector2.SmoothDamp(current, movement, ref refVelocity, rotational_speed).normalized * speed;
                rb2d.velocity = Vector2.Lerp(current, movement, 0.3f).normalized * speed;
            }
            else
            {
                rb2d.velocity = movement.normalized * speed;
            }
        }
        else
        {
            //rb2d.velocity = rb2d.velocity;
            //rb2d1.rotation = 0f;
            //rb2d.inertia = 0f;
        }
        
    }

    public Vector2 GetCloseColorPosition()
    {
        Vector2 pos = FreeDraw.Drawable.D.WorldToPixelCoordinates(rb2d.position);
        float x = pos.x;
        float y = pos.y;
        float width = (int)FreeDraw.Drawable.DrawableSprite.rect.width;
        float height = (int)FreeDraw.Drawable.DrawableSprite.rect.height;
        Color32 BestPlayerColor = FreeDraw.Drawable.BestPlayerColor;
        Color32[] Field = FreeDraw.Drawable.cur_colors;
        //Debug.Log(x);
        //Debug.Log(y);
        //Debug.Log(rb2d.name[1]);
        if (BestPlayerColor.Equals(GameSetting.PlayerColors[(int)rb2d.name[1] - 48 - 1]))
        {
            return new Vector2(0, 0);
        }
        for (int distance = 40; distance < 1000; distance++)
        {
            if ((int)((y + distance) * width + x + distance) >= Field.Length || ((y - distance) * width + x - distance) < 0)
            {
                return new Vector2(0, 0);
            }
            if (x < distance || y < distance || x >= width - distance || y > height - distance)
            {
                return new Vector2(0, 0);
            }
            if (Field[(int)((y + distance) * width + x)].Equals(BestPlayerColor))
            {
                return new Vector2(x, y + distance);
            }
            if (Field[(int)((y + distance) * width + x + distance)].Equals(BestPlayerColor))
            {
                return new Vector2(x + distance, y + distance);
            }
            if (Field[(int)(y * width + x + distance)].Equals(BestPlayerColor))
            {
                return new Vector2(x + distance, y);
            }
            if (Field[(int)((y - distance) * width + x + distance)].Equals(BestPlayerColor))
            {
                return new Vector2(x + distance, y - distance);
            }
            if (Field[(int)((y - distance) * width + x)].Equals(BestPlayerColor))
            {
                return new Vector2(x, y - distance);
            }
            if (Field[(int)((y - distance) * width + x - distance)].Equals(BestPlayerColor))
            {
                return new Vector2(x - distance, y - distance);
            }
            if (Field[(int)(y * width + x - distance)].Equals(BestPlayerColor))
            {
                return new Vector2(x, y);
            }
            if (Field[(int)((y + distance) * width + x - distance)].Equals(BestPlayerColor))
            {
                return new Vector2(x - distance, y + distance);
            }
        }
        return new Vector2(0, 0);
    }


}
