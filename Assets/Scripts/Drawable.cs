using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;
using UnityEditor;

using System.Collections.Generic;

using System.Threading;
using Unity.Jobs;
using Unity.Collections;
using System;

namespace FreeDraw
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
    //                                                                        //
    //  public static Object prefab = Resources.Load("Prefabs/YourPrefab");   //
    //                                                                        //
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//


    public class Drawable : MonoBehaviour
    {

        int threadsStarted, threadsCompleted;
        public int threadsToUse = 8;

        //public Rigidbody2D rb2d;
        // PEN color
        public static Color Pen_color = Color.clear;     // Change these to change the default drawing settings
        // PEN WIDTH (actually, it's a radius, in pixels)
        public static int Pen_Width;

        private float restSeconds = GameSetting.GameLength;
        public TextMeshProUGUI remainSeconds;
        public TextMeshProUGUI Percent;

        private int NumberOfPlayers = GameSetting.NumberOfPlayers;
        public delegate void Brush_Function(Vector2 world_position);

        public LayerMask Drawing_Layers;

        public bool Reset_Canvas_On_Play = true;
        public static Color Reset_color = new Color(0, 0, 0, 0);

        public static Drawable drawable;
        // MUST HAVE READ/WRITE enabled set in the file editor of Unity
        public static Sprite DrawableSprite;
        Texture2D drawable_texture;


        public GameObject Panel;

        public static int King = -1;

        Vector2 previous_drag_position;
        Color[] clean_colors_array;
        Color transparent;
        public static Color32[] cur_colors; 


        Stopwatch sw = new Stopwatch(); //sw.Start();  sw.Stop();  Debug.Log(sw.Elapsed.ToString());


        public int sum;  //static is bad in general; it can have only one instance, but I don't care
        private int[] percentage = new int[GameSetting.NumberOfPlayers];
        public static float[] percentageStats = new float[8];

        public static Color32[] colors = new Color32[8]; // See ColorPicker, line 16. This note is idiotic
        private PlayerController Controller;

        private GameObject[] Players;
        public static GameObject[] Bars; //static for resulttable
        private Bar[] BarsScript;
        private PlayerController[] Controllers;

        private float cumsum;
        private float coef;

        private int drop_rate = GameSetting.DropRate;

        float pixelWidth;
        float pixelHeight;
        float unitsToPixels;
        float centered_x;
        float centered_y;
        Vector3 local_pos;
        Vector2 pixel_pos;
        float bounds;
        float LS;
        int height;
        int width;

        private Vector2[] Pos = new Vector2[GameSetting.NumberOfPlayers];
        public void PenBrush() //Way too expensive :'(
        {
            /*
            for (int i = 1; i <= NumberOfPlayers; i++)  //It would be really nice to iterate over players and their stats, but is it possible?
            {
                var CurColors = new NativeArray<Color32>(cur_colors, Allocator.TempJob);
                var Percentage = new NativeArray<int>(percentage, Allocator.TempJob);
                var controllers = new NativeArray<int>(PC, Allocator.TempJob);
                var job = new SimpleJob
                {
                    Time = Time.timeSinceLevelLoad,
                    col = Controllers[i - 1].Color,
                    per = Percentage,
                    pen = Controllers[i - 1].PenWidth,
                    cur = CurColors,
                    pos = Players[i - 1].transform.position
                };
            }*/
            //*
            for (int i = 1; i <= NumberOfPlayers; i++)  //It would be really nice to iterate over players and their stats, but is it possible?
            {
                Vector2 pixel_pos = WorldToPixelCoordinates(Players[i - 1].transform.position);
                MarkPixelsTocolor(i - 1, pixel_pos, (int)Controllers[i - 1].PenWidth, Controllers[i - 1].Color);
            }//*/
            /*
            for (int i = 1; i <= NumberOfPlayers; i++)
            {
                Pos[i - 1] = Players[i - 1].transform.position;
                System.Threading.ParameterizedThreadStart pts = new System.Threading.ParameterizedThreadStart(ThreadedPenBrush);
                System.Threading.Thread workerForOneRow = new System.Threading.Thread(pts);
                workerForOneRow.Start(i);
            }//*/
            
            GetPercentage();
            ApplyMarkedPixelChanges();
        }
        /*
        private void ThreadedPenBrush(object iVariable)
        {
            int i = (int)iVariable;
            Vector2 pixel_pos = ThreadSafeWorldToPixelCoordinates(Pos[i - 1]);
            ThreadSafeMarkPixelsTocolor(i - 1, pixel_pos, Controllers[i - 1].PenWidth, Controllers[i - 1].Color);
        }//*/
        int var;

        public void GetPercentage()
        {
            //int sum = 0;  //This is really bad place for this
            sum = percentage.Sum();
            //foreach (int item in percentage)
            //{ sum += item; }
            if (sum == 0) { sum = 1; }
            cumsum = 0;
            for (int i = 1; i < NumberOfPlayers; i++)
            {
                if (percentage[BarsScript[i - 1].ind] < percentage[BarsScript[i].ind])
                {
                    var = BarsScript[i - 1].ind;
                    BarsScript[i - 1].ind = BarsScript[i].ind;
                    BarsScript[i].ind = var;
                }
            }
            for (int i = 1; i <= NumberOfPlayers; i++)
            {
                coef = (float)percentage[BarsScript[i - 1].ind] / sum;// (Screen.width - 50);  // 30
                //Bars[i - 1].GetComponentInChildren<TextMeshProUGUI>().text = percentage[Bars[i - 1].GetComponentInChildren<Bar>().ind].ToString();
                Bars[i - 1].GetComponentInChildren<TextMeshProUGUI>().text = (System.Math.Round(100.0 * percentage[BarsScript[i - 1].ind] / sum, 1)).ToString();
                Bars[i - 1].transform.position = new Vector3(width / 2 - 825 + (cumsum + coef) * 750, 950, 0);
                Bars[i - 1].transform.GetChild(0).transform.localScale = new Vector3(coef, 1, 1);
                Bars[i - 1].GetComponentInChildren<Image>().color = colors[BarsScript[i - 1].ind];
                cumsum += 2 * coef;
            }
            BestPlayerColor = Bars[0].GetComponentInChildren<Image>().color;
        }

        public static Color32 BestPlayerColor;

        public void SetPenBrush()
        {
            // PenBrush is the NAME of the method we want to set as our current brush
            //current_brush = PenBrush;
        }
        private Joystick joystick;
        private bool ended = false;

        void Update()
        {
            PenBrush();
            restSeconds -= Time.deltaTime;
            remainSeconds.text = ((int)restSeconds).ToString(); //also expensive, I guess
            if (restSeconds <= 0)
            {
                if (!ended)
                {
                    ended = true;
                    EndGame();
                }
            }
            else if (UnityEngine.Random.Range(0, 1000f) < drop_rate)
            {
                PowerUpGen.GeneratePowerUp();
            }
        }

        public int[] percheck;

        private void EndGame()
        {
            King = -1;
            int j = 0;
            percheck = new int[NumberOfPlayers];
            //foreach (int item in percentage) { max = max < item ? item : max;}
            for (int i = 0; i < percentage.Length; i++) { j = percentage[j] < percentage[i] ? i : j; } // the loop is redundant, leader is the first one
            for (int i = 0; i < percentage.Length; i++) { percentageStats[i] = (float)percentage[i] / sum; }
            Panel.gameObject.SetActive(true);
            colors[j].a = 255;
            try
            {
                GameObject.Find("Winner").GetComponent<Image>().color = colors[j];
                Percent.text = "With " + (System.Math.Round(100.0 * percentage[j] / sum, 2)).ToString() + " %";
            }
            catch (Exception e) { }
            Time.timeScale = 0.000000f;
            if (!GameSetting.AI[j])
            {
                ButtonScript.SaveData(NumberOfPlayers, (float)percentage[j] / sum);
            }
            joystick.gameObject.SetActive(false);
            sw.Start();
            //StatsCheck();
            sw.Stop();
            Debug.Log(sw.Elapsed.ToString());
        }

        private void StatsCheck()
        {
            for (int p = 0; p < cur_colors.Length; p++)
            {
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    if (MyEqual(GameSetting.PlayerColors[i], cur_colors[p]))
                    {
                        percheck[i] += 1;
                    }
                }
            }
            /*var jobHandles = new List<JobHandle>();
            for (int p = 0 ; p< cur_colors.Length; p++)
            {
                var CurColors = new NativeArray<Color32>(cur_colors, Allocator.TempJob);
                var c = new NativeArray<Color32>(colors, Allocator.TempJob);
                var PerCheck = new NativeArray<int>(NumberOfPlayers, Allocator.TempJob);
                var job = new SimpleJob
                {
                    Time = Time.timeSinceLevelLoad,
                    cur = CurColors,
                    nop = NumberOfPlayers,
                    per = PerCheck,
                    pcur = c
                };
                if (p == 0)
                { jobHandles.Add(job.Schedule(NumberOfPlayers, 100)); }
                else
                { jobHandles.Add(job.Schedule(NumberOfPlayers, 100, jobHandles[p - 1])); }
                jobHandles.Last().Complete();
                CurColors.Dispose();
                PerCheck.CopyTo(percheck);
                PerCheck.Dispose();
                c.Dispose();

            }//*/
            
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Debug.Log(i.ToString() + " " + percheck[i].ToString() + " " + percentage[i].ToString());
            }
        }

        int last = 0;
        int center_x;
        int center_y;
        int ind;


        
        //*
        public void MarkPixelsTocolor(int k, Vector2 center_pixel, int PenThickness, Color32 color_of_pen)
        {
            // Figure out how many pixels we need to color in each direction (x and y)
            center_x = (int)center_pixel.x;
            center_y = (int)center_pixel.y;
            ind = 0;
            if (King != -1) //&& PlayerPowerUpHandler[k]
            {
                k = King;
            }
            int rsquare = (int)System.Math.Pow(PenThickness, 2);

            for (int x = center_x - PenThickness; x <= center_x + PenThickness; x++)
            {
                // Check if the X wraps around the image, so we don't draw pixels on the other side of the image
                if (x >= width || x < 0)
                    continue;
                int xsquare = rsquare - (center_x - x) * (center_x - x);
                //int xsqrt = (int)System.Math.Sqrt(xsquare);
                //for (int y = center_y - xsqrt; y <= center_y + xsqrt; y++)
                for (int y = center_y - PenThickness; y <= center_y + PenThickness; y++)
                {
                    if (y >= height || y < 0 || (center_y - y) * (center_y - y) >= xsquare)
                        continue;
                    ind = y * width + x;
                    Color32 cur_color = cur_colors[ind];
                    if (MyEqual(color_of_pen, cur_color))
                    {
                    continue;
                    }
                    if (cur_color.a == 0)
                    {
                        percentage[k] += 1;
                        cur_colors[ind] = color_of_pen;
                    }
                    else if (MyEqual(cur_color, colors[last]))
                    {
                        if (Controllers[last].erasable)
                        {
                            if (color_of_pen.a != 0)
                            {
                                percentage[k] += 1;
                            }
                            percentage[last] -= 1;
                            cur_colors[ind] = color_of_pen;
                        }
                    }
                    else
                    {
                        for (int l = 0; l < NumberOfPlayers; l++)
                        {

                            if (MyEqual(cur_color, colors[l]))
                            {
                                if (Controllers[l].erasable)
                                {
                                    if (color_of_pen.a != 0)
                                    {
                                        percentage[k] += 1;
                                    }
                                    percentage[l] -= 1;
                                    cur_colors[ind] = color_of_pen;
                                }
                                last = l;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public bool MyEqual(Color32 a, Color32 b)
        {
            return (a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a);
        }

        //*/
        /*
        private System.Object threadLocker = new System.Object();
        public void ThreadSafeMarkPixelsTocolor(int k, Vector2 center_pixel, int PenThickness, Color32 color_of_pen)
        {
            // Figure out how many pixels we need to color in each direction (x and y)
            lock (threadLocker)
            {
                center_x = (int)center_pixel.x;
                center_y = (int)center_pixel.y;
            }
            ind = 0;
            if (King != -1)
            {
                k = King;
            }
            int rsquare = (int)Mathf.Pow(PenThickness, 2);

            for (int x = center_x - PenThickness; x <= center_x + PenThickness; x++)
            {
                // Check if the X wraps around the image, so we don't draw pixels on the other side of the image
                if (x >= (int)width || x < 0)
                    continue;
                int xsquare = rsquare - (center_x - x) * (center_x - x);
                for (int y = center_y - PenThickness; y <= center_y + PenThickness; y++)
                {
                    if (y >= (int)height || y < 0)
                        continue;
                    if ((center_y - y) * (center_y - y) < xsquare)
                    {
                        ind = y * (int)width + x;
                        Color32 cur_color = cur_colors[ind];
                        if (!color_of_pen.Equals(cur_color))
                        {
                            if (cur_color.a == 0)
                            {
                                percentage[k] += 1;
                                lock (threadLocker)
                                {
                                    cur_colors[ind] = color_of_pen;
                                }
                            }
                            else if (cur_color.Equals(colors[last]))
                            {
                                if (Controllers[last].erasable)
                                {
                                    percentage[k] += 1;
                                    percentage[last] -= 1;
                                    lock (threadLocker)
                                    {
                                        cur_colors[ind] = color_of_pen;
                                    }
                                }
                            }
                            else
                            {
                                for (int l = 0; l < NumberOfPlayers; l++)
                                {
                                    if (k != l && cur_color.Equals(colors[l]))
                                    {
                                        if (Controllers[l].erasable)
                                        {
                                            percentage[k] += 1;
                                            percentage[l] -= 1;
                                            lock (threadLocker)
                                            {
                                                cur_colors[ind] = color_of_pen;
                                            }
                                        }
                                        last = l;
                                        break;
                                    }
                                }
                            }
                            //cur_colors[ind] = color_of_pen; // Original place of this
                            //MarkPixelToChange(x, y, color_of_pen);
                        }
                    }
                }
            }
        }
        //*/
        public RenderTexture renderTexture;

        public void ApplyMarkedPixelChanges()
        {
            drawable_texture.SetPixels32(cur_colors);
            drawable_texture.Apply();
            //Graphics.Blit(drawable_texture, renderTexture);


        }



        public Vector2 ThreadSafeWorldToPixelCoordinates(Vector2 local_pos)
        {
            centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;
            pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));
            return pixel_pos;
        }

        public Vector2 WorldToPixelCoordinates(Vector2 local_pos)
        {
            // Change coordinates to local coordinates of this image
            //local_pos = transform.InverseTransformPoint(world_position);


            // Need to center our coordinates
            centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

            // Round current mouse position to nearest pixel
            pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));
            return pixel_pos;
        }

        /*public Vector2 WorldToPixelCoordinates(Vector2 world_position)
        {
            // Change coordinates to local coordinates of this image
            local_pos = transform.InverseTransformPoint(world_position);

            // Change these to coordinates of pixels
            pixelWidth = DrawableSprite.rect.width;
            pixelHeight = DrawableSprite.rect.height;
            unitsToPixels = pixelWidth / DrawableSprite.bounds.size.x * transform.localScale.x;
            

            // Need to center our coordinates
            centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

            // Round current mouse position to nearest pixel
            pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

            return pixel_pos;
        }*/


        // Changes every pixel to be the reset color
        public void ResetCanvas()
        {
            drawable_texture.SetPixels(clean_colors_array);
            drawable_texture.Apply();
        }

        private void Start()
        {
            joystick = GameObject.FindObjectOfType<Joystick>(); // Must be just one joystick on gamefield:)
            Players = Generation.Players;
            Controllers = Generation.Controllers;
            Bars = Generation.Bars;
            BarsScript = new Bar[Bars.Length];
            for (int i = 0; i < Bars.Length; i++)
            {
                BarsScript[i] = Bars[i].GetComponentInChildren<Bar>();
            }

        }

        public static Drawable D;

        void Awake()
        {
            drawable = this;
            DrawableSprite = this.GetComponent<SpriteRenderer>().sprite;

            
            pixelWidth = DrawableSprite.rect.width;
            pixelHeight = DrawableSprite.rect.height;
            LS = transform.localScale.x;
            bounds = DrawableSprite.bounds.size.x;
            width = (int)DrawableSprite.rect.width;
            height = (int)DrawableSprite.rect.height;
            unitsToPixels = pixelWidth / bounds * LS;

            drawable_texture = DrawableSprite.texture;
            colors = GameSetting.PlayerColors;

            clean_colors_array = new Color[(int)DrawableSprite.rect.width * (int)DrawableSprite.rect.height];
            for (int x = 0; x < clean_colors_array.Length; x++)
                clean_colors_array[x] = Reset_color;

            // Should I reset our canvas image when we hit play in the editor?
            if (Reset_Canvas_On_Play)
                ResetCanvas();
            cur_colors = drawable_texture.GetPixels32();
            D = this;
            //PenBrush();
        }
    }
}