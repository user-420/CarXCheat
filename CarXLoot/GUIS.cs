using System;
using System.IO;
using BepInEx;
using UnityEngine;

namespace usrMods
{
    public class GUIS
    {
        public static Texture2D LoadTexture(string name)
        {
            Texture2D texture2D = new Texture2D(4, 4);
            FileStream fileStream = new FileStream(Paths.PluginPath + "/OzarkCarX/assets" + name, FileMode.Open, FileAccess.Read);
            byte[] array = new byte[fileStream.Length];
            fileStream.Read(array, 0, (int)fileStream.Length);
            ImageConversion.LoadImage(texture2D, array);
            return texture2D;
        }

        public GUIS()
        {
            this.consolas = Font.CreateDynamicFontFromOSFont("Consolas", 12);
			this.TextSkin = new GUISkin();
			this.WindowSkin = new GUISkin();
			this.CheckBoxCheckSkin = new GUISkin();
			this.CheckBoxUncheckSkin = new GUISkin();
			this.CarOnSkin = new GUISkin();
			this.CarOffSkin = new GUISkin();
			this.SettingsOnSkin = new GUISkin();
			this.SettingsOffSkin = new GUISkin();
			this.GameOnSkin = new GUISkin();
			this.GameOffSkin = new GUISkin();
			this.ButtonSkin = new GUISkin();
			this.TextSkin.label.font = this.consolas;
			this.TextSkin.label.fontSize = 13;
			this.TextBottomSkin.label.font = this.consolas;
			this.TextBottomSkin.label.fontSize = 10;
			this.WindowSkin.box.normal.background = this.MENU_BG;
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
			this.GameOnSkin.button.normal.background = this.SETTINGS_BUTTON_ON;
			this.GameOffSkin.button.normal.background = this.SETTINGS_BUTTON_OFF;
			this.ButtonSkin.button.normal.background = this.BUTTON;
			this.ButtonSkin.button.active.background = this.BUTTON_PRESSED;
			this.ButtonSkin.button.font = this.consolas;
			this.ButtonSkin.button.normal.textColor = Color.white;
			this.SliderSkin.horizontalSlider.normal.background = this.SLIDER;
			this.SliderSkin.horizontalSlider.stretchHeight = false;
			this.SliderSkin.horizontalSlider.stretchWidth = false;
			this.SliderSkin.horizontalSlider.fixedHeight = 20f;
			this.SliderSkin.horizontalSliderThumb.normal.background = this.CHECK;
			this.SliderSkin.horizontalSliderThumb.stretchHeight = false;
			this.SliderSkin.horizontalSliderThumb.stretchWidth = false;
			this.SliderSkin.horizontalSliderThumb.fixedHeight = 20f;
			this.SliderSkin.horizontalSliderThumb.fixedWidth = 6f;
			// this.ButtonSkin.button.alignment = 4;
		}
		
		private Font consolas;

		public GUISkin TextBottomSkin;

		public GUISkin TextSkin;

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

		private Texture2D MENU_BG = GUIS.LoadTexture("menubg.png");

		private Texture2D CHECK = GUIS.LoadTexture("checkedbox.png");

		private Texture2D UNCHECK = GUIS.LoadTexture("button.png");

		private Texture2D CAR_BUTTON_ON = GUIS.LoadTexture("carbuttonon.png");

		private Texture2D CAR_BUTTON_OFF = GUIS.LoadTexture("carbuttonoff.png");

		private Texture2D SETTINGS_BUTTON_ON = GUIS.LoadTexture("settingsbuttonon.png");

		private Texture2D SETTINGS_BUTTON_OFF = GUIS.LoadTexture("settingsbuttonoff.png");

		private Texture2D BUTTON = GUIS.LoadTexture("button.png");

		private Texture2D BUTTON_PRESSED = GUIS.LoadTexture("buttonclicked.png");
		
		private Texture2D SLIDER = GUIS.LoadTexture("slider.png");

		private Texture2D SLIDER_THUMB = GUIS.LoadTexture("sliderthing.png");

	}
}
