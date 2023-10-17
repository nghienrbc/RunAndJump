using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
    /// <summary>
    /// Spawns the player, and 
    /// </summary>
    public class LevelManagerForJumpingGame : Singleton<LevelManagerForJumpingGame>
    {
        public enum Controls { SingleButton, LeftRight, Swipe }

        /// The current speed the level is traveling at
        public float Speed { get; protected set; }
        /// The distance traveled since the start of the level
        public float DistanceTraveled { get; protected set; }

        /// the prefab you want for your player
        [Header("Prefabs")]
        public GameObject StartingPosition;
        /// the list of playable characters - use this to tell what characters you want in your level, don't access that at runtime
        public List<PlayableCharacter> PlayableCharacters;
        /// the list of playable characters currently instantiated in the game - use this to know what characters ARE currently in your level at runtime
        public List<PlayableCharacter> CurrentPlayableCharacters { get; set; }
        /// the x distance between each character
        public float DistanceBetweenCharacters = 1f;
        /// the elapsed time since the start of the level
        public float RunningTime { get; protected set; }
        /// the amount of points a player gets per second
        public float PointsPerSecond = 20;
        /// the text that will be shown (if not empty) at the start of the level
        [Multiline]
        public String InstructionsText;


        [Space(10)]
        [Header("Level Bounds")]
        /// the line after which objects can be recycled
        public Bounds RecycleBounds;

        [Space(10)]
        /// the line after which playable characters will die - leave it to zero if you don't want to use it
        public Bounds DeathBounds;

        [Space(10)]
        [Header("Speed")]
        /// the initial speed of the level
        public float InitialSpeed = 10f;
        /// the maximum speed the level will run at
        public float MaximumSpeed = 50f;
        /// the acceleration (per second) at which the level will go from InitialSpeed to MaximumSpeed
        public float SpeedAcceleration = 1f;

        [Space(10)]
        [Header("Intro and Outro durations")]
        /// duration of the initial fade in
        public float IntroFadeDuration = 1f;
        /// duration of the fade to black at the end of the level
        public float OutroFadeDuration = 1f;


        [Space(10)]
        [Header("Start")]
        /// the duration (in seconds) of the initial countdown
        public int StartCountdown;
        /// the text displayed at the end of the countdown
        public string StartText;

        [Space(10)]
        [Header("Mobile Controls")]
        /// the mobile control scheme applied to this level
        public Controls ControlScheme;

        [Space(10)]
        [Header("Life Lost")]
        /// the effect we instantiate when a life is lost
        public GameObject LifeLostExplosion;

        // protected stuff
        protected DateTime _started;
        protected float _savedPoints;
        protected float _recycleX;
        protected Bounds _tmpRecycleBounds;

        protected bool _temporarySpeedFactorActive;
        protected float _temporarySpeedFactor;
        protected float _temporarySpeedFactorRemainingTime;
        protected float _temporarySavedSpeed;

        /// <summary>
        /// Initialization
        /// </summary>
        protected virtual void Start()
        {
            Speed = InitialSpeed;
            
            DistanceTraveled = 0;
              

            // storage
            _savedPoints = GameManager.Instance.Points;
            _started = DateTime.UtcNow;
            GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
            GameManager.Instance.SetPointsPerSecond(PointsPerSecond);

            if (GUIManager.Instance != null)
            {
                // set the level name in the GUI
                GUIManager.Instance.SetLevelName(SceneManager.GetActiveScene().name);
                // fade in
                GUIManager.Instance.FaderOn(false, IntroFadeDuration);
            }

            PrepareStart();
        }

        /// <summary>
        /// Handles everything before the actual start of the game.
        /// </summary>
        protected virtual void PrepareStart()
        {
            //if we're supposed to show a countdown we schedule it, otherwise we just start the level
            if (StartCountdown > 0)
            {
                GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
                StartCoroutine(PrepareStartCountdown());
            }
            else
            {
                LevelStart();
            }
        }

        /// <summary>
        /// Handles the initial start countdown display
        /// </summary>
        /// <returns>The start countdown.</returns>
        protected virtual IEnumerator PrepareStartCountdown()
        {
            int countdown = StartCountdown;
            GUIManager.Instance.SetCountdownActive(true);

            // while the countdown is active, we display the current value, and wait for a second and show the next
            while (countdown > 0)
            {
                if (GUIManager.Instance.CountdownText != null)
                {
                    GUIManager.Instance.SetCountdownText(countdown.ToString());
                }
                countdown--;
                yield return new WaitForSeconds(1f);
            }

            // when the countdown reaches 0, and if we have a start message, we display it
            if ((countdown == 0) && (StartText != ""))
            {
                GUIManager.Instance.SetCountdownText(StartText);
                yield return new WaitForSeconds(1f);
            }

            // we turn the countdown inactive, and start the level
            GUIManager.Instance.SetCountdownActive(false);
            LevelStart();
        }

        /// <summary>
        /// Handles the start of the level : starts the autoincrementation of the score, sets the proper status and triggers the corresponding event.
        /// </summary>
        public virtual void LevelStart()
        {
            GameManager.Instance.SetStatus(GameManager.GameStatus.GameInProgress);
            GameManager.Instance.AutoIncrementScore(true);
            MMEventManager.TriggerEvent(new MMGameEvent("GameStart"));
        }
         
        /// <summary>
        /// Resets the level : repops dead characters, sets everything up for a new game
        /// </summary>
        public virtual void ResetLevel()
        { 
            PrepareStart();
        }
         

        /// <summary>
        /// Every frame
        /// </summary>
        public virtual void Update()
        {
            _savedPoints = GameManager.Instance.Points;
            _started = DateTime.UtcNow;

            // we increment the total distance traveled so far
            DistanceTraveled = DistanceTraveled + Speed * Time.fixedDeltaTime;

            // if we can still accelerate, we apply the level's speed acceleration
            if (Speed < MaximumSpeed)
            {
                Speed += SpeedAcceleration * Time.deltaTime;
            }
             

            RunningTime += Time.deltaTime;
        } 


        /// <summary>
        /// Gets the player to the specified level
        /// </summary>
        /// <param name="levelName">Level name.</param>
        public virtual void GotoLevel(string levelName)
        {
            GUIManager.Instance.FaderOn(true, OutroFadeDuration);
            StartCoroutine(GotoLevelCo(levelName));
        }

        /// <summary>
        /// Waits for a short time and then loads the specified level
        /// </summary>
        /// <returns>The level co.</returns>
        /// <param name="levelName">Level name.</param>
        protected virtual IEnumerator GotoLevelCo(string levelName)
        {
            if (Time.timeScale > 0.0f)
            {
                yield return new WaitForSeconds(OutroFadeDuration);
            }
            GameManager.Instance.UnPause();

            if (string.IsNullOrEmpty(levelName))
            {
                LoadingSceneManager.LoadScene("StartScreen");
            }
            else
            {
                LoadingSceneManager.LoadScene(levelName);
            }

        }

        /// <summary>
        /// Triggered when all lives are lost and you press the main action button
        /// </summary>
        public virtual void GameOverAction()
        {
            GameManager.Instance.UnPause();
            GotoLevel(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Triggered when a life is lost and you press the main action button
        /// </summary>
        public virtual void LifeLostAction()
        {
            ResetLevel();
        }
         

        /// <summary>
        /// Override this if needed
        /// </summary>
        protected virtual void OnEnable()
        {

        }

        /// <summary>
        /// Override this if needed
        /// </summary>
        protected virtual void OnDisable()
        {

        }

        public void PlayerComplelteLevel()
        {
            String gameName = SceneManager.GetActiveScene().name;
            if ((_savedPoints < PlayerPrefs.GetFloat(gameName) && PlayerPrefs.GetFloat(gameName) > 0) || PlayerPrefs.GetFloat(gameName) == 0)
            {
                PlayerPrefs.SetFloat(gameName, _savedPoints);
                GUIManager.Instance.SetHigeScoreScreen(true);
            }
            else GUIManager.Instance.SetGameOverScreen(true);
        }
    } 
}
