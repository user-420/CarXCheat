﻿using System;
using System.IO;
using BepInEx;
using UnityEngine;
using SyncMultiplayer;

namespace usrMods
{
    [BepInPlugin("carx.usrmods", "CarX", "0.0.2")]
    public class usrMods : BaseUnityPlugin
    {  
        // loader for ui
        // private static UnityEngine.GameObject usrObject;

        // vars
        public static bool rbSmokeBool = false;
        public static bool colorTrans = false;
        public static bool lerpColorWindow1 = false;
        public static bool lerpColorWindow2 = false;
        public static bool carClicked = false;
        public static bool gameClicked = false;
        public static bool settingsClicked = false;
        public static float rbSpeed = 1.0F;
        public static float lerpSpeed = 1.0F;
        public static float xOffset = 0F;
        public static float yOffset = 0F;
        public static float lRed1 = 1;
        public static float lGreen1 = 1;
        public static float lBlue1 = 1;
        public static float lRed2 = 0;
        public static float lGreen2 = 0;
        public static float lBlue2 = 0;
        public static string rbStr;
        public static Color smokeColor;
        public static Color smokeLerp1 = Color.white;
        public static Color smokeLerp2 = Color.black;
        public static GUIStyle lerpButt1 = new GUIStyle();
        public static GUIStyle lerpButt2 = new GUIStyle();
        public GUIS skin = new GUIS();

        //funcs
        public static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        public void Start()
        {
            //usrObject = new UnityEngine.GameObject();
            //usrObject.AddComponent<usrStuff>();
            //UnityEngine.Object.DontDestroyOnLoad(usrObject);
        }
        public void Update()
        {
            bool keyDown2 = Input.GetKeyDown(KeyCode.Delete);
            if (keyDown2)
                uiToggle.showUi = !uiToggle.showUi;
            if (rbSmokeBool)
            {
                smokeColor = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * rbSpeed, 1f), 1f, 1f));
                NetworkController.InstanceGame.LocalPlayer.userCar.SetSmokeColor(smokeColor, null);
            }
            if(colorTrans)
            {
                smokeColor = Color.Lerp(smokeLerp1, smokeLerp2, Mathf.PingPong(Time.time, lerpSpeed));
                NetworkController.InstanceGame.LocalPlayer.userCar.SetSmokeColor(smokeColor, null);
            }
        }

            public void OnGUI()
            {
                if (uiToggle.showUi)
                {
                GUI.skin = skin.WindowSkin; 
                GUI.Box(new Rect(50, 20, 756, 420), ""); // rect is x, y, w, h

                if (!carClicked)
                {
                    GUI.skin = skin.CarOffSkin;
                    bool cbclicked = GUI.Button(new Rect(66, 25, 247, 41), "");
                    if (cbclicked)
                    {
                        carClicked = !carClicked;
                    }
                }

                //GUI.skin = this.skin.CarOnSkin;
                //GUI.Button(new Rect(66, 25, 247, 41), "");

                //GUI.skin = this.skin.GameOffSkin;
                //GUI.Button(new Rect(315, 25, 247, 41), "");
                //GUI.skin = this.skin.GameOnSkin;
                //GUI.Button(new Rect(315, 25, 247, 41), "");

                //GUI.skin = this.skin.SettingsOffSkin;
                //GUI.Button(new Rect(564, 25, 247, 41), "");
                //GUI.skin = this.skin.SettingsOnSkin;
                //GUI.Button(new Rect(564, 25, 247, 41), "");

                //lerpButt1.normal.background = MakeTex(30, 30, smokeLerp1);
                //lerpButt2.normal.background = MakeTex(30, 30, smokeLerp2);
                //UIHelper.Begin("/usr/'s CarX Stuff", 5 + xOffset, 5 + yOffset, 300, 600, 15, 20, 2/*, GUIStyle.none*/);
                //UIHelper.Label("");

                //if (UIHelper.Button("RB Smoke ", rbSmokeBool/*, GUIStyle.none*/))
                //{
                //    if (!colorTrans)
                //    {
                //        rbSmokeBool = !rbSmokeBool;
                //    }

                //}
                //UIHelper.Label("RB Speed: " + rbSpeed.ToString("0.0"));
                //rbSpeed = UIHelper.Slider(rbSpeed, 0.5f, 5);

                //UIHelper.Label("Smoke Color Transistion");
                //if (UIHelper.Button("Smoke LERP ", colorTrans/*, GUIStyle.none*/))
                //{
                //    if (!rbSmokeBool)
                //    {
                //        colorTrans = !colorTrans;
                //    }
                //}
                //if (GUI.Button(new Rect(246 + xOffset, 153 + yOffset, 20, 20), "", lerpButt1))
                //{
                //    if (lerpColorWindow2)
                //    {
                //        lerpColorWindow2 = !lerpColorWindow2;
                //    }
                //    lerpColorWindow1 = !lerpColorWindow1;
                //}
                //if (GUI.Button(new Rect(271 + xOffset, 153 + yOffset, 20, 20), "", lerpButt2))
                //{
                //    if (lerpColorWindow1)
                //    {
                //        lerpColorWindow1 = !lerpColorWindow1;
                //    }
                //    lerpColorWindow2 = !lerpColorWindow2;
                //}
                //if (lerpColorWindow1)
                //{
                //    UIHelper.Begin2("Lerp Color 1", 308 + xOffset, 5 + yOffset, 250, 250, 15, 20, 2);
                //    UIHelper.Label2("");
                //    UIHelper.Label2("Red");
                //    lRed1 = UIHelper.Slider2(lRed1, 0, 1);
                //    UIHelper.Label2("Green");
                //    lGreen1 = UIHelper.Slider2(lGreen1, 0, 1);
                //    UIHelper.Label2("Blue");
                //    lBlue1 = UIHelper.Slider2(lBlue1, 0, 1);
                //    smokeLerp1 = new Color(lRed1, lGreen1, lBlue1);
                //    if (UIHelper.Button2("Close"))
                //    {
                //        lerpColorWindow1 = !lerpColorWindow1;
                //    }
                //}

                //if (lerpColorWindow2)
                //{
                //    UIHelper.Begin2("Lerp Color 2", 308 + xOffset, 5 + yOffset, 250, 250, 15, 20, 2);
                //    UIHelper.Label2("");
                //    UIHelper.Label2("Red");
                //    lRed2 = UIHelper.Slider2(lRed2, 0, 1);
                //    UIHelper.Label2("Green");
                //    lGreen2 = UIHelper.Slider2(lGreen2, 0, 1);
                //    UIHelper.Label2("Blue");
                //    lBlue2 = UIHelper.Slider2(lBlue2, 0, 1);
                //    smokeLerp2 = new Color(lRed2, lGreen2, lBlue2);
                //    if (UIHelper.Button2("Close"))
                //    {
                //        lerpColorWindow2 = !lerpColorWindow2;
                //    }
                //}

                //UIHelper.Label("Lerp Speed: " + lerpSpeed.ToString("0.0"));
                //lerpSpeed = UIHelper.Slider(lerpSpeed, 0.5f, 5);

                //UIHelper.Label("--------------");

                //UIHelper.Label("Money/Level/Unlocks");
                //if (UIHelper.Button("Set All (XP,Money & Unlocks)"))
                //    GameConsole.GiveAll();
                //if (UIHelper.Button("Set Money to 99999999"))
                //    GameConsole.GiveMoney();
                //if (UIHelper.Button("Add Level"))
                //    GameConsole.GiveLevel();

                //UIHelper.Label("--------------");

                //UIHelper.Label("Menu Location");
                //UIHelper.Label("X Offset");
                //xOffset = UIHelper.Slider(xOffset, 0, 1615);
                //UIHelper.Label("Y Offset");
                //yOffset = UIHelper.Slider(yOffset, 0, 475);
            }

        }
        public class uiToggle
        {
            public static bool showUi = false;
        }
    }

    public static class UIHelper
    {
        private static float
            x, y,
            width, height,
            margin,
            controlHeight,
            controlDist,
            nextControlY,
            x2, y2,
            width2, height2,
            margin2,
            controlHeight2,
            controlDist2,
            nextControlY2;

        public static void Begin(string text, float _x, float _y, float _width, float _height, float _margin, float _controlHeight, float _controlDist/*, GUIStyle style*/)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            margin = _margin;
            controlHeight = _controlHeight;
            controlDist = _controlDist;
            nextControlY = 20f + usrMods.yOffset;
            GUI.Box(new Rect(x, y, width, height), text/*, style*/);
        }

        private static Rect NextControlRect()
        {
            Rect r = new Rect(x + margin, nextControlY, width - margin * 2, controlHeight);
            nextControlY += controlHeight + controlDist;
            return r;
        }

        public static string MakeEnable(string text, bool state)
        {
            return string.Format("{0}{1}", text, state ? "[on]" : "[off]");
        }

        public static bool Button(string text, bool state/*, GUIStyle style*/)
        {
            return Button(MakeEnable(text, state)/*, style*/);
        }

        public static bool Button(string text/*, GUIStyle style*/)
        {
            return GUI.Button(NextControlRect(), text/*,  style*/ );
        }

        public static void Label(string text, float value, int decimals = 2)
        {
            Label(string.Format("{0}{1}", text, Math.Round(value, 2).ToString()));
        }

        public static void Label(string text)
        {
            GUI.Label(NextControlRect(), text);
        }

        public static float Slider(float val, float min, float max)
        {
            return GUI.HorizontalSlider(NextControlRect(), val, min, max);
        }
        // --- 2
        public static void Begin2(string text, float _x, float _y, float _width, float _height, float _margin, float _controlHeight, float _controlDist)
        {
            x2 = _x;
            y2 = _y;
            width2 = _width;
            height2 = _height;
            margin2 = _margin;
            controlHeight2 = _controlHeight;
            controlDist2 = _controlDist;
            nextControlY2 = 20f + usrMods.yOffset;
            GUI.Box(new Rect(x2, y2, width2, height2), text);
        }
        private static Rect NextControlRect2()
        {
            Rect r = new Rect(x2 + margin2, nextControlY2, width2 - margin2 * 2, controlHeight2);
            nextControlY2 += controlHeight2 + controlDist2;
            return r;
        }
        public static float Slider2(float val, float min, float max)
        {
            return GUI.HorizontalSlider(NextControlRect2(), val, min, max);
        }
        public static string MakeEnable2(string text, bool state)
        {
            return string.Format("{0}{1}", text, state ? "[on]" : "[off]");
        }
        public static bool Button2(string text)
        {
            return GUI.Button(NextControlRect2(), text);
        }
        public static void Label2(string text)
        {
            GUI.Label(NextControlRect2(), text);
        }
    }
}