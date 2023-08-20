using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// The different states and gamemodes to handle the game logic
/// </summary>
public enum GameState
{
    None,
    Shooter,
    Race,
    Win,
    Lose,
    Paused
}

/// <summary>
/// The GameManager handles the game states and the UI
/// Works together with the GameModeManager
/// </summary>

public class GameManager : MonoBehaviour
{
    //Game Manager Variables
    
    public static GameManager Instance { get; private set; }
    private GameState gameState;
    private GameState previousGameState;
    [SerializeField] private StartGen startGen;
    private bool newGame = false; // Needed to determine whether to load scene or continue ongoing game
    
    // Game Allow Variables
    private bool allowSecondPlayer; // Allow Multiplayer or not
    private bool allowFriendlyFire; // Allow Friendly Fire or not
   
    // Player Variables
    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;
    [SerializeField] private PlayerInputManager playerInputManager; 
    [Header("Scene Variables")] 
    public bool loadLevel;
    public int levelToLoad;

    [Header("UI Variables")]
    [SerializeField] private Canvas inGameUI;
    [SerializeField] private GameObject inGameUIPlayer2;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Canvas pauseCanvas;
    
    // Start Game and correct Events
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        newGame = true;

        // Event Subscriptions
        playerInputManager.onPlayerJoined += GetSecondPlayer;
        GameModeManager.Player1Win += SetWin;
        GameModeManager.Player2Win += SetWin;
        
        PauseGame(); // Pause Game at Start so Player is not available
    }

    // Load Level when called
    private void Update()
    {
        if (loadLevel)
        {
            loadLevel = false;
            StartCoroutine(LoadLevelAsync());
        }

    }
    
    // ------ Load Level ------
    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
       
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        Debug.Log("Level Loaded!");
       // if(inGameUI == null) inGameUI = GameObject.Find("InGameUI").GetComponent<Canvas>();
         }

    // ------ Game State Setters ------ necessary for Event Delegates and Buttons
    public void SetNone()
    {
        this.gameState = GameState.None;
        SwitchGameState();
    }
    public void SetShooter()
    {
        this.gameState = GameState.Shooter;
        ContinueGame();
        SwitchGameState();
    }
    
    public void SetRacer()
    {
        this.gameState = GameState.Race;
        ContinueGame();
        SwitchGameState();
    }
    
    public void SetPause()
    {
        pauseCanvas.gameObject.SetActive(true);
        previousGameState = gameState;
        this.gameState = GameState.Paused;
        SwitchGameState();
    }
    
    public void SetLose()
    {
        previousGameState = gameState;
        this.gameState = GameState.Lose;
        SwitchGameState();
    }
    
    public void SetWin()
    {
        previousGameState = gameState;
        this.gameState = GameState.Win;
        SwitchGameState();
    }
    
    // GameState Switch
  private void SwitchGameState()
  {
      switch (gameState)
      {
          case GameState.Shooter:
              if (newGame)
              {
                  levelToLoad = 1;
                  loadLevel = true;
                  startGen.raceMode = false;
                  ShowCanvas(inGameUI);
                  newGame = false;
              }
              else
              {
                  DisableCanvas(pauseCanvas);
                  ContinueGame();
              }
              break;
          case GameState.Race:
              if (newGame)
              {
                  levelToLoad = 2;
                  loadLevel = true;
                  startGen.raceMode = true;
                  ShowCanvas(inGameUI);
                  newGame = false;
              }
              else
              {
                  DisableCanvas(pauseCanvas);
                  ContinueGame();
              }
              break;
          case GameState.Win:
              newGame = true;
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(winCanvas);
              break;
          case GameState.Lose:
              newGame = true;
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(loseCanvas);
              break;
          case GameState.Paused:
              newGame = false;
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(pauseCanvas);
              break;
      }
  }
  
    // ------ GameState Methods ------
    
  private void PauseGame()
  {
      // Pause game
      Time.timeScale = 0;
      player1.GetComponent<ShipMovement>().enabled = false;
      player1.GetComponent<PlayerShoot>().enabled = false;
      player1.transform.GetChild(0).gameObject.SetActive(false);
      if (player2 != null)
      {
          player2.transform.GetChild(2).gameObject.SetActive(false);
          player2.GetComponent<PlayerInput>().StopAllCoroutines();
          player2.GetComponent<ShipMovement>().enabled = false;
          player2.GetComponent<PlayerShoot>().enabled = false;
          player2.transform.GetChild(0).gameObject.SetActive(false);
      }
  }

  public void ContinueGame()
  {
      Debug.Log("Continue Game");
      if (!newGame)
      {
          gameState = previousGameState;
          SwitchGameState();
      }
      pauseCanvas.gameObject.SetActive(false);
      Time.timeScale = 1;
      player1.GetComponent<ShipMovement>().enabled = true;
      player1.GetComponent<PlayerShoot>().enabled = true;
      player1.transform.GetChild(0).gameObject.SetActive(true);
      
      if (player2 != null)
      {
          player2.transform.GetChild(2).gameObject.SetActive(true);
          player2.GetComponent<ShipMovement>().enabled = true;
          player2.GetComponent<PlayerShoot>().enabled = true;
          player2.transform.GetChild(0).gameObject.SetActive(true);
      }
      
  }

  // ------ Multiplayer ------
  // Toggle Multiplayer
  public void AllowSecondPlayer()
  {
      //Toggle second player
      allowSecondPlayer = !allowSecondPlayer;

      if (!allowSecondPlayer)
      {
          PlayerInputManager.instance.DisableJoining();
      }
      else
      {
          PlayerInputManager.instance.EnableJoining();
      }
  }

  // Get Instantiated Second Player
  private void GetSecondPlayer(PlayerInput obj)
  {
      player2 = obj.gameObject;
      if(inGameUIPlayer2 == null) inGameUIPlayer2 = GameObject.Find("InGameUIP2").gameObject;
      inGameUIPlayer2.gameObject.SetActive(true);
  }

  // ------ UI ------
  // Canvas Handling Methods
  private void ShowCanvas(Canvas canvas)
  {
      canvas.gameObject.SetActive(true);
  }

  private void DisableCanvas(Canvas canvas)
  {
      canvas.gameObject.SetActive(false);
  }

  public void GoToMainMenu()
  {
      SceneManager.LoadScene(0);
  }

}
