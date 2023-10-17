using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

namespace MoreMountains.InfiniteRunnerEngine
{	
	/// <summary>
	/// Handles all GUI effects and changes
	/// </summary>
	public class SettingGUIManager : Singleton<SettingGUIManager>, MMEventListener<MMGameEvent>
	{  
	    /// the points counter
	    public Text PointsText;
        /// the level display
        public Text LevelText;
        /// the high score display
        public Text HighScoreText;
        /// the countdown at the start of a level
        public Text CountdownText;
		/// the screen used for all fades
		public Image Fader;

        public Button EasyBtn;
        public Button BasicBtn;
        public Button HardBtn;

        public Button MusicSwitchBG;
        public Button SoundSwitchBG;
        public Button MusicBtn;
        public Button SoundBtn;

        public Sprite selectSprite;
        public Sprite unSelectSprite;
        public Sprite soundOn;
        public Sprite soundOff;
        public Sprite musicOn;
        public Sprite musicOff;
        public Sprite bgsoundOn;
        public Sprite bgsoundOff;

        private int isMusicOn;
        private int isSoundOn;
        private string gameDifficutyString;

        public void Start()
        {
            Initialize();
        }
        /// <summary>
        /// Initialization
        /// </summary>
        public virtual void Initialize()
		{ 
			if (CountdownText != null && CountdownText.text == null)
	        {
				CountdownText.enabled=false;
			}

            // check all state off setting
            isMusicOn = PlayerPrefs.GetInt("Music");
            isSoundOn = PlayerPrefs.GetInt("Sound");
            gameDifficutyString = PlayerPrefs.GetString("GameDifficuty");

            MusicSwitchBG.image.sprite = isMusicOn == 0 ? bgsoundOn : bgsoundOff;
            MusicBtn.image.sprite = isMusicOn == 0 ? musicOn : musicOff;

            SoundSwitchBG.image.sprite = isSoundOn == 0 ? bgsoundOn : bgsoundOff;
            SoundBtn.image.sprite = isSoundOn == 0 ? soundOn : soundOff;

            EasyBtn.image.sprite = gameDifficutyString == "Easy" ? selectSprite : unSelectSprite;
            BasicBtn.image.sprite = gameDifficutyString == "Basic" ? selectSprite : unSelectSprite;
            HardBtn.image.sprite = gameDifficutyString == "Hard" ? selectSprite : unSelectSprite;

            if (string.IsNullOrEmpty(gameDifficutyString)) EasyBtn.image.sprite = selectSprite;


        }
         
	    /// <summary>
	    /// Override this to have code executed on the GameStart event
	    /// </summary>
	    public virtual void OnGameStart()
	    {
	    	
	    } 
		/// <summary>
		/// Sets the countdown active.
		/// </summary>
		/// <param name="state">If set to <c>true</c> state.</param>
		public virtual void SetCountdownActive(bool state)
		{
			if (CountdownText==null) { return; }
			CountdownText.enabled=state;
		}
		
		/// <summary>
		/// Sets the countdown text.
		/// </summary>
		/// <param name="value">the new countdown text.</param>
		public virtual void SetCountdownText(string newText)
		{
			if (CountdownText==null) { return; }
			CountdownText.text=newText;
		} 
		
		/// <summary>
		/// Sets the level name in the HUD
		/// </summary>
		public virtual void SetLevelName(string name)
		{
			if (LevelText==null)
				return;

			LevelText.text=name;
            HighScoreText.text = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name).ToString("000 000 000");

        }
		
		/// <summary>
		/// Fades the fader in or out depending on the state
		/// </summary>
		/// <param name="state">If set to <c>true</c> fades the fader in, otherwise out if <c>false</c>.</param>
		public virtual void FaderOn(bool state,float duration)
		{
			if (Fader==null)
			{
				return;
			}
			Fader.gameObject.SetActive(true);
			if (state)
				StartCoroutine(MMFade.FadeImage(Fader,duration, new Color(0,0,0,1f)));
			else
				StartCoroutine(MMFade.FadeImage(Fader,duration,new Color(0,0,0,0f)));
		}		

		/// <summary>
		/// Fades the fader to the alpha set as parameter
		/// </summary>
		/// <param name="newColor">The color to fade to.</param>
		/// <param name="duration">Duration.</param>
		public virtual void FaderTo(Color newColor,float duration)
		{
			if (Fader==null)
			{
				return;
			}
			Fader.gameObject.SetActive(true);
			StartCoroutine(MMFade.FadeImage(Fader,duration, newColor));
		}	

		public virtual void OnMMEvent(MMGameEvent gameEvent)
		{
			switch (gameEvent.EventName)
			{
				case "PauseOn": 
					break;
				case "PauseOff":  
					break;
				case "GameStart":
					OnGameStart();
					break;
			}
		}

		protected virtual void OnEnable()
		{
			this.MMEventStartListening<MMGameEvent>();
		}

		protected virtual void OnDisable()
		{
			this.MMEventStopListening<MMGameEvent>();
		}

        public void EasyBtnClick()
        {
            gameDifficutyString = "Easy";
            EasyBtn.gameObject.GetComponent<Image>().sprite = selectSprite;
            BasicBtn.gameObject.GetComponent<Image>().sprite = unSelectSprite;
            HardBtn.gameObject.GetComponent<Image>().sprite = unSelectSprite;
        }
        public void BasicBtnClick()
        {
            gameDifficutyString = "Basic";
            EasyBtn.gameObject.GetComponent<Image>().sprite = unSelectSprite;
            BasicBtn.gameObject.GetComponent<Image>().sprite = selectSprite;
            HardBtn.gameObject.GetComponent<Image>().sprite = unSelectSprite;
        }
        public void HardBtnClick()
        {
            gameDifficutyString = "Hard";
            EasyBtn.gameObject.GetComponent<Image>().sprite = unSelectSprite;
            BasicBtn.gameObject.GetComponent<Image>().sprite = unSelectSprite;
            HardBtn.gameObject.GetComponent<Image>().sprite = selectSprite;
        }

        public void MusicBtnClick()
        {
            isMusicOn = isMusicOn == 0 ? 1 : 0;
            MusicSwitchBG.image.sprite = isMusicOn == 0 ? bgsoundOn : bgsoundOff;
            MusicBtn.image.sprite = isMusicOn == 0 ? musicOn : musicOff;

        }
        public void SoundBtnClick()
        {
            isSoundOn = isSoundOn == 0 ? 1 : 0;
            SoundSwitchBG.image.sprite = isSoundOn == 0 ? bgsoundOn : bgsoundOff;
            SoundBtn.image.sprite = isSoundOn == 0 ? soundOn : soundOff;
        }

        public void CancelBtnClick()
        {
            SceneManager.LoadScene("StartScreen");
        }

        public void SaveBtnClick()
        {
            // save

            PlayerPrefs.SetString("GameDifficuty", gameDifficutyString);
            PlayerPrefs.SetInt("Music", isMusicOn);
            PlayerPrefs.SetInt("Sound", isSoundOn);

            SceneManager.LoadScene("StartScreen");
        }
    }
}