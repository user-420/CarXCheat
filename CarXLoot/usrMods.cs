using System;
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
        public static bool lightRainbow = false;
        public static bool lightGrad = false;
        public static bool lerpColorWindow1 = false;
        public static bool lerpColorWindow2 = false;
        public static bool lightColorWindow1 = false;
        public static bool lightColorWindow2 = false;
        public static bool carClicked = true;
        public static bool gameClicked = false;
        public static bool settingsClicked = false;
        public static float rbSpeed = 1.0F;
        public static float lerpSpeed = 1.0F;
        public static float lightSpeed = 0.5F;
        public static float lightSpeedG = 0.5F;
        public static float xOffset = 0F;
        public static float yOffset = 0F;
        public static float lRed1 = 1;
        public static float lGreen1 = 1;
        public static float lBlue1 = 1;
        public static float lRed2 = 0;
        public static float lGreen2 = 0;
        public static float lBlue2 = 0;
        public static float lLRed1 = 1;
        public static float lLGreen1 = 0;
        public static float lLBlue1 = 0;
        public static float lLRed2 = 0;
        public static float lLGreen2 = 0;
        public static float lLBlue2 = 1;
        public static string rbStr;
        public static Color smokeColor;
        public static Color smokeLerp1 = Color.white;
        public static Color smokeLerp2 = Color.black;
        public static Color lightLerp1 = Color.red;
        public static Color lightLerp2 = Color.blue;
        public static GUIStyle lerpButt1 = new GUIStyle();
        public static GUIStyle lerpButt2 = new GUIStyle();
        public static GUIStyle lightButt1 = new GUIStyle();
        public static GUIStyle lightButt2 = new GUIStyle();
        private Font consolas;
        public GUISkin TextBottomSkin;
        public GUISkin TextSkin;
        public GUISkin TextCenterSkin;
        public GUISkin WindowSkin;
        public GUISkin CheckBoxCheckSkin;
        public GUISkin CheckBoxUncheckSkin;
        public GUISkin CarOnSkin;
        public GUISkin CarOffSkin;
        public GUISkin SettingsOnSkin;
        public GUISkin SettingsOffSkin;
        public GUISkin GameOnSkin;
        public GUISkin GameOffSkin;
        public GUISkin ButtonSkin;
        public GUISkin SliderSkin;
        public GUISkin SliderThumbSkin;
        private Texture2D MENU_BG = LoadTexture("menubg.png");
        private Texture2D CHECK = LoadTexture("checkedbox.png");
        private Texture2D UNCHECK = LoadTexture("button.png");
        private Texture2D CAR_BUTTON_ON = LoadTexture("carbuttonon.png");
        private Texture2D CAR_BUTTON_OFF = LoadTexture("carbuttonoff.png");
        private Texture2D SETTINGS_BUTTON_ON = LoadTexture("settingsbuttonon.png");
        private Texture2D SETTINGS_BUTTON_OFF = LoadTexture("settingsbuttonoff.png");
        private Texture2D GAME_BUTTON_ON = LoadTexture("gamebuttonon.png");
        private Texture2D GAME_BUTTON_OFF = LoadTexture("gamebuttonoff.png");
        private Texture2D BUTTON = LoadTexture("button.png");
        private Texture2D BUTTON_PRESSED = LoadTexture("buttonclicked.png");
        private Texture2D SLIDER = LoadTexture("slider.png");
        private Texture2D SLIDER_THUMB = LoadTexture("sliderthing.png");
        public Rect menu_area = new Rect(50, 20, 756, 420);
        private const int ELEMENT_SIZE = 40;
        private const int SPACING = 15;
        private const int GAP = 10;
        private int element_y;
        public RaceCar[] cars;
        public RaceCar playersCar;

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

        public static Texture2D LoadTexture(string name)
        {
            Texture2D texture2D = new Texture2D(4, 4);
            FileStream fileStream = new FileStream(Paths.PluginPath + "/OzarkCarX/assets/" + name, FileMode.Open, FileAccess.Read);
            byte[] array = new byte[fileStream.Length];
            fileStream.Read(array, 0, (int)fileStream.Length);
            ImageConversion.LoadImage(texture2D, array);
            return texture2D;
        }

        public bool AddButton()
        {
            Rect r = new Rect(menu_area.xMin + GAP, element_y, menu_area.xMax - GAP * 2, ELEMENT_SIZE);
            element_y = (int)r.yMax + SPACING;
            return GUI.Button(r, "");
        }
        private void SearchForPlayersCar()
        {
            this.cars = UnityEngine.Object.FindObjectsOfType<RaceCar>();
            for (int i = 0; i < this.cars.Length; i++)
            {
                bool flag = !this.cars[i].isNetworkCar;
                if (flag)
                {
                    this.playersCar = this.cars[i];
                    break;
                }
            }
        }


        public void Start()
        {
            bool pcIsNull = this.playersCar == null;
            if (pcIsNull)
            {
                this.SearchForPlayersCar();
            }
            this.consolas = Font.CreateDynamicFontFromOSFont("Consolas", 18);
            TextBottomSkin = ScriptableObject.CreateInstance<GUISkin>();
            TextSkin = ScriptableObject.CreateInstance<GUISkin>();
            TextCenterSkin = ScriptableObject.CreateInstance<GUISkin>();
            WindowSkin = ScriptableObject.CreateInstance<GUISkin>();
            CheckBoxCheckSkin = ScriptableObject.CreateInstance<GUISkin>();
            CheckBoxUncheckSkin = ScriptableObject.CreateInstance<GUISkin>();
            CarOnSkin = ScriptableObject.CreateInstance<GUISkin>();
            CarOffSkin = ScriptableObject.CreateInstance<GUISkin>();
            SettingsOnSkin = ScriptableObject.CreateInstance<GUISkin>();
            SettingsOffSkin = ScriptableObject.CreateInstance<GUISkin>();
            GameOnSkin = ScriptableObject.CreateInstance<GUISkin>();
            GameOffSkin = ScriptableObject.CreateInstance<GUISkin>();
            ButtonSkin = ScriptableObject.CreateInstance<GUISkin>();
            SliderSkin = ScriptableObject.CreateInstance<GUISkin>();
            SliderThumbSkin = ScriptableObject.CreateInstance<GUISkin>();
            this.TextSkin.label.font = this.consolas;
            this.TextSkin.label.fontSize = 13;
            this.TextSkin.label.normal.textColor = Color.white;
            this.TextCenterSkin.label.font = this.consolas;
            this.TextCenterSkin.label.fontSize = 13;
            this.TextCenterSkin.label.normal.textColor = Color.white;
            this.TextCenterSkin.label.alignment = TextAnchor.MiddleCenter;
            this.TextBottomSkin.label.font = this.consolas;
            this.TextBottomSkin.label.fontSize = 10;
            this.WindowSkin.box.normal.background = this.MENU_BG;
            this.WindowSkin.box.font = this.consolas;
            this.WindowSkin.box.normal.textColor = Color.white;
            this.WindowSkin.box.fontSize = 15;
            this.WindowSkin.box.alignment = TextAnchor.UpperCenter;
            this.CheckBoxCheckSkin.button.normal.background = this.CHECK;
            this.CheckBoxCheckSkin.button.stretchHeight = false;
            this.CheckBoxCheckSkin.button.stretchWidth = false;
            this.CheckBoxCheckSkin.button.fixedHeight = 20f;
            this.CheckBoxCheckSkin.button.fixedWidth = 20f;
            this.CheckBoxUncheckSkin.button.normal.background = this.UNCHECK;
            this.CheckBoxUncheckSkin.button.stretchHeight = false;
            this.CheckBoxUncheckSkin.button.stretchWidth = false;
            this.CheckBoxUncheckSkin.button.fixedHeight = 20f;
            this.CheckBoxUncheckSkin.button.fixedWidth = 20f;
            this.CarOnSkin.button.normal.background = this.CAR_BUTTON_ON;
            this.CarOffSkin.button.normal.background = this.CAR_BUTTON_OFF;
            this.SettingsOnSkin.button.normal.background = this.SETTINGS_BUTTON_ON;
            this.SettingsOffSkin.button.normal.background = this.SETTINGS_BUTTON_OFF;
            this.GameOnSkin.button.normal.background = this.GAME_BUTTON_ON;
            this.GameOffSkin.button.normal.background = this.GAME_BUTTON_OFF;
            this.ButtonSkin.button.normal.background = this.BUTTON;
            this.ButtonSkin.button.active.background = this.BUTTON_PRESSED;
            this.ButtonSkin.button.font = this.consolas;
            this.ButtonSkin.button.normal.textColor = Color.white;
            this.ButtonSkin.button.alignment = TextAnchor.MiddleCenter;
            this.SliderSkin.horizontalSlider.normal.background = this.SLIDER;
            this.SliderSkin.horizontalSlider.stretchHeight = false;
            this.SliderSkin.horizontalSlider.stretchWidth = false;
            this.SliderSkin.horizontalSlider.fixedHeight = 20f;
            this.SliderSkin.horizontalSliderThumb.normal.background = this.SLIDER_THUMB;
            this.SliderSkin.horizontalSliderThumb.stretchHeight = false;
            this.SliderSkin.horizontalSliderThumb.stretchWidth = false;
            this.SliderSkin.horizontalSliderThumb.fixedHeight = 20f;
            this.SliderSkin.horizontalSliderThumb.fixedWidth = 6f;
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
            if (colorTrans)
            {
                smokeColor = Color.Lerp(smokeLerp1, smokeLerp2, Mathf.PingPong(Time.time, lerpSpeed));
                NetworkController.InstanceGame.LocalPlayer.userCar.SetSmokeColor(smokeColor, null);
            }
            bool pcIsNull = this.playersCar == null;
            if (pcIsNull)
            {
                this.SearchForPlayersCar();
                GameConsole.Print("PlayerCar Was Null");
            }
            if (lightRainbow)
            {
                GameObject pc = playersCar.gameObject;
                Light[] lights = pc.GetComponentsInChildren<UnityEngine.Light>();
                for (int i = 0; i < lights.Length; i++)
                {
                    string gameObj = lights[i].gameObject.ToString();
                    if (gameObj == "HeadLight(Clone) (UnityEngine.GameObject)")
                    {
                        lights[i].color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time * lightSpeed, 1f), 1f, 1f));
                    }
                }
            }
            if (lightGrad)
            {
                GameObject pc = playersCar.gameObject;
                Light[] lights = pc.GetComponentsInChildren<UnityEngine.Light>();
                for (int i = 0; i < lights.Length; i++)
                {
                    string gameObj = lights[i].gameObject.ToString();
                    if (gameObj == "HeadLight(Clone) (UnityEngine.GameObject)")
                    {
                        lights[i].color = Color.Lerp(lightLerp1, lightLerp2, Mathf.PingPong(Time.time, lerpSpeed));
                    }
                }
            }
        }

        public void OnGUI()
        {
            if (uiToggle.showUi)
            {
                element_y = (int)menu_area.yMin + GAP;
                GameInput.InputManager.LockInput(this);
                GUI.skin = WindowSkin;
                GUI.Box(menu_area, "");

                if (gameClicked == false && settingsClicked == false)
                {
                    carClicked = true;
                }

                // ---CAR TAB---
                GUI.skin = carClicked ? CarOnSkin : CarOffSkin;
                if (GUI.Button(new Rect(56, 25, 247, 41), ""))
                {
                    carClicked = !carClicked;
                    gameClicked = false;
                    settingsClicked = false;
                }

                // ---GAME TAB---
                GUI.skin = gameClicked ? GameOnSkin : GameOffSkin;
                if (GUI.Button(new Rect(305, 25, 247, 41), ""))
                {
                    gameClicked = !gameClicked;
                    carClicked = false;
                    settingsClicked = false;
                }

                // ---SETTINGS TAB---
                GUI.skin = settingsClicked ? SettingsOnSkin : SettingsOffSkin;
                if(GUI.Button(new Rect(554, 25, 248, 41), ""))
                {
                    settingsClicked = !settingsClicked;
                    carClicked = false;
                    gameClicked = false;
                }

                lerpButt1.normal.background = MakeTex(30, 30, smokeLerp1);
                lerpButt2.normal.background = MakeTex(30, 30, smokeLerp2);
                lightButt1.normal.background = MakeTex(30, 30, lightLerp1);
                lightButt2.normal.background = MakeTex(30, 30, lightLerp2);

                if (carClicked)
                {
                    //RAINBOW
                    GUIStyle localStyle = new GUIStyle(GUI.skin.label);
                    localStyle.normal.textColor = Color.white;
                    localStyle.fontSize = 12;
                    localStyle.font = consolas;
                    GUI.skin = rbSmokeBool ? CheckBoxCheckSkin : CheckBoxUncheckSkin;
                    if(GUI.Button(new Rect(76, 85, 20, 20), ""))
                        {
                            rbSmokeBool = !rbSmokeBool;
                            colorTrans = false;
                        }
                    GUI.skin = SliderSkin;
                    GUI.Label(new Rect(101, 88, 20, 100), "Rainbow Tire Smoke", localStyle);
                    rbSpeed = GUI.HorizontalSlider(new Rect(76, 110, 150, 15), rbSpeed, 0.5f, 10f);
                    GUI.Label(new Rect(230, 113, 20, 100), "Smoke Rainbow Cycle Speed + [" + rbSpeed.ToString("0.0") + "]", localStyle);
                    //LERP
                    GUI.skin = colorTrans ? CheckBoxCheckSkin : CheckBoxUncheckSkin;
                    if (GUI.Button(new Rect(76, 140, 20, 20), ""))
                    {
                        colorTrans = !colorTrans;
                        rbSmokeBool = false;
                    }
                    GUI.skin = SliderSkin;
                    GUI.Label(new Rect(101, 143, 20, 100), "Gradient Smoke", localStyle);
                    lerpSpeed = GUI.HorizontalSlider(new Rect(76, 165, 150, 15), lerpSpeed, 0.5f, 10f);
                    GUI.Label(new Rect(230, 168, 20, 100), "Smoke Gradient Speed [" + lerpSpeed.ToString("0.0") + "]", localStyle);
                    if(GUI.Button(new Rect(226, 140, 20, 20), "", lerpButt1))
                    {
                        lerpColorWindow1 = !lerpColorWindow1;
                        lerpColorWindow2 = false;
                    }
                    if(lerpColorWindow1)
                    {
                        GUI.skin = WindowSkin;
                        Rect lerpWindow1 = new Rect(274, 140, 250, 250);
                        GUI.Box(lerpWindow1, "Gradient Color 1");
                        GUI.skin = SliderSkin;
                        lRed1 = GUI.HorizontalSlider(new Rect(334, 190, 120, 20), lRed1, 0f, 1f);
                        lGreen1 = GUI.HorizontalSlider(new Rect(334, 256, 120, 20), lGreen1, 0f, 1f);
                        lBlue1 = GUI.HorizontalSlider(new Rect(334, 322, 120, 20), lBlue1, 0f, 1f);
                        GUI.skin = TextCenterSkin;
                        GUI.Label(new Rect(355, 214, 80, 12), "Red");
                        GUI.Label(new Rect(355, 280, 80, 12), "Green");
                        GUI.Label(new Rect(355, 346, 80, 12), "Blue");
                        smokeLerp1 = new Color(lRed1, lGreen1, lBlue1);
                        GUI.skin = ButtonSkin;
                        if (GUI.Button(new Rect(504, 140, 20, 20), "x"))
                            lerpColorWindow1 = false;
                    }
                    if(GUI.Button(new Rect(249, 140, 20, 20), "", lerpButt2))
                    {
                        lerpColorWindow2 = !lerpColorWindow2;
                        lerpColorWindow1 = false;
                    }
                    if (lerpColorWindow2)
                    {
                        GUI.skin = WindowSkin;
                        Rect lerpWindow2 = new Rect(274, 140, 250, 250);
                        GUI.Box(lerpWindow2, "Gradient Color 2");
                        GUI.skin = SliderSkin;
                        lRed2 = GUI.HorizontalSlider(new Rect(334, 190, 120, 20), lRed2, 0f, 1f);
                        lGreen2 = GUI.HorizontalSlider(new Rect(334, 256, 120, 20), lGreen2, 0f, 1f);
                        lBlue2 = GUI.HorizontalSlider(new Rect(334, 322, 120, 20), lBlue2, 0f, 1f);
                        GUI.skin = TextCenterSkin;
                        GUI.Label(new Rect(355, 214, 80, 12), "Red");
                        GUI.Label(new Rect(355, 280, 80, 12), "Green");
                        GUI.Label(new Rect(355, 346, 80, 12), "Blue");
                        smokeLerp2 = new Color(lRed2, lGreen2, lBlue2);
                        GUI.skin = ButtonSkin;
                        if (GUI.Button(new Rect(504, 140, 20, 20), "x"))
                            lerpColorWindow2 = false;
                    }
                    GUI.skin = lightRainbow ? CheckBoxCheckSkin : CheckBoxUncheckSkin;
                    if (GUI.Button(new Rect(76, 196, 20, 20), ""))
                    {
                        lightRainbow = !lightRainbow;
                        lightGrad = false;
                    }
                    GUI.Label(new Rect(101, 199, 20, 100), "Rainbow Lights", localStyle);
                    GUI.skin = SliderSkin;
                    lightSpeed = GUI.HorizontalSlider(new Rect(76, 221, 150, 15), lightSpeed, 0.1f, 1f);
                    GUI.Label(new Rect(230, 224, 20, 100), "Light Rainbow Cycle Speed + [" + lightSpeed.ToString("0.0") + "]", localStyle);
                    GUI.skin = lightGrad ? CheckBoxCheckSkin : CheckBoxUncheckSkin;
                    if (GUI.Button(new Rect(76, 252, 20, 20), ""))
                    {
                        lightGrad = !lightGrad;
                        lightRainbow = false;
                    }
                    GUI.Label(new Rect(101, 255, 20, 100), "Gradient Lights", localStyle);
                    GUI.skin = SliderSkin;
                    lightSpeedG = GUI.HorizontalSlider(new Rect(76, 277, 150, 15), lightSpeedG, 0.1f, 1f);
                    GUI.Label(new Rect(230, 280, 20, 100), "Smoke Rainbow Cycle Speed + [" + lightSpeedG.ToString("0.0") + "]", localStyle);
                    if (GUI.Button(new Rect(226, 255, 20, 20), "", lightButt1))
                    {
                        lightColorWindow1 = !lightColorWindow1;
                        lightColorWindow2 = false;
                    }
                    if (lightColorWindow1)
                    {
                        GUI.skin = WindowSkin;
                        Rect lightWindow1 = new Rect(274, 255, 250, 250);
                        GUI.Box(lightWindow1, "Gradient Color 1");
                        GUI.skin = SliderSkin;
                        lLRed1 = GUI.HorizontalSlider(new Rect(334, 305, 120, 20), lLRed1, 0f, 1f);
                        lLGreen1 = GUI.HorizontalSlider(new Rect(334, 371, 120, 20), lLGreen1, 0f, 1f);
                        lLBlue1 = GUI.HorizontalSlider(new Rect(334, 437, 120, 20), lLBlue1, 0f, 1f);
                        GUI.skin = TextCenterSkin;
                        GUI.Label(new Rect(355, 329, 80, 12), "Red");
                        GUI.Label(new Rect(355, 395, 80, 12), "Green");
                        GUI.Label(new Rect(355, 461, 80, 12), "Blue");
                        lightLerp1 = new Color(lLRed1, lLGreen1, lLBlue1);
                        GUI.skin = ButtonSkin;
                        if (GUI.Button(new Rect(504, 255, 20, 20), "x"))
                            lightColorWindow1 = false;
                    }
                    if (GUI.Button(new Rect(249, 255, 20, 20), "", lightButt2))
                    {
                        lightColorWindow2 = !lightColorWindow2;
                        lightColorWindow1 = false;
                    }
                    if (lightColorWindow2)
                    {
                        GUI.skin = WindowSkin;
                        Rect lightWindow2 = new Rect(274, 255, 250, 250);
                        GUI.Box(lightWindow2, "Gradient Color 2");
                        GUI.skin = SliderSkin;
                        lLRed2 = GUI.HorizontalSlider(new Rect(334, 305, 120, 20), lLRed2, 0f, 1f);
                        lLGreen2 = GUI.HorizontalSlider(new Rect(334, 371, 120, 20), lLGreen2, 0f, 1f);
                        lLBlue2 = GUI.HorizontalSlider(new Rect(334, 437, 120, 20), lLBlue2, 0f, 1f);
                        GUI.skin = TextCenterSkin;
                        GUI.Label(new Rect(355, 329, 80, 12), "Red");
                        GUI.Label(new Rect(355, 395, 80, 12), "Green");
                        GUI.Label(new Rect(355, 461, 80, 12), "Blue");
                        lightLerp2 = new Color(lLRed2, lLGreen2, lLBlue2);
                        GUI.skin = ButtonSkin;
                        if (GUI.Button(new Rect(504, 255, 20, 20), "x"))
                            lightColorWindow2 = false;
                    }
                }

                //UIHelper.Label("--------------");

                //UIHelper.Label("Money/Level/Unlocks");
                //if (UIHelper.Button("Set All (XP,Money & Unlocks)"))
                //    GameConsole.GiveAll();
                //if (UIHelper.Button("Set Money to 99999999"))
                //    GameConsole.GiveMoney();
                //if (UIHelper.Button("Add Level"))
                //    GameConsole.GiveLevel();
            }
            else {GameInput.InputManager.ResetLockInput(this);}
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
